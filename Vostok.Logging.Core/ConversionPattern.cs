using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core
{
    public class ConversionPattern
    {
        private const string DateTimeFormatString = "HH:mm:ss zzz";
        private const string PrefixPropertyName = "prefix";

        private readonly List<IConversionPatternFragment> fragments;

        public ConversionPattern()
        {
            fragments = new List<IConversionPatternFragment>();
        }

        public ConversionPattern AddFragment(PatternPartType type, string suffix = null)
        {
            fragments.Add(
                new Fragment
                {
                    Type = type,
                    Suffix = suffix,
                });
            return this;
        }

        public ConversionPattern AddFragment(PatternPartType type, string property, string suffix)
        {
            fragments.Add(
                new Fragment
                {
                    Type = type,
                    Property = property,
                    Suffix = suffix,
                });
            return this;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            fragments.ForEach(
                fr =>
                {
                    var f = (Fragment)fr;
                    switch (f.Type)
                    {
                        case PatternPartType.StringStart:
                            break;
                        case PatternPartType.DateTime:
                            sb.Append("%d");
                            break;
                        case PatternPartType.Level:
                            sb.Append("%l");
                            break;
                        case PatternPartType.Prefix:
                            sb.Append("%x");
                            break;
                        case PatternPartType.Message:
                            sb.Append("%m");
                            break;
                        case PatternPartType.Exception:
                            sb.Append("%e");
                            break;
                        case PatternPartType.Properties:
                            sb.Append("%p");
                            break;
                        case PatternPartType.Property:
                            sb.Append("%p");
                            break;
                        case PatternPartType.NewLine:
                            sb.Append("%n");
                            break;
                    }

                    if (!string.IsNullOrEmpty(f.Property))
                        sb.Append($"({f.Property})");
                    sb.Append(f.Suffix);
                });
            return sb.ToString();
        }

        internal void Render(LogEvent @event, TextWriter writer) =>
            fragments.ForEach(f => f.Render(@event, writer));

        private class Fragment : IConversionPatternFragment
        {
            public PatternPartType Type { get; set; }
            public string Property { get; set; }
            public string Suffix { get; set; }

            public void Render(LogEvent @event, TextWriter writer)
            {
                switch (Type)
                {
                    case PatternPartType.StringStart:
                        writer.Write(Suffix);
                        break;
                    case PatternPartType.DateTime:
                        writer.Write(
                            @event.Timestamp.ToString(
                                string.IsNullOrEmpty(Property)
                                    ? DateTimeFormatString
                                    : Property) + Suffix);
                        break;
                    case PatternPartType.Level:
                        writer.Write(@event.Level + Suffix);
                        break;
                    case PatternPartType.Prefix:
                        var prefixProperty = GetPropertyOrNull(@event, PrefixPropertyName);
                        if (prefixProperty is IReadOnlyList<string> prefixes)
                            TryWritePrefixes(prefixes, writer);
                        writer.Write(Suffix);
                        break;
                    case PatternPartType.Message:
                        writer.Write(@event.MessageTemplate + Suffix);
                        break;
                    case PatternPartType.Exception:
                        writer.Write(@event.Exception + Suffix);
                        break;
                    case PatternPartType.Properties:
                        TryWriteProperties(@event.Properties, writer);
                        writer.Write(Suffix);
                        break;
                    case PatternPartType.Property:
                        var property = GetPropertyOrNull(@event, Property);
                        TryWriteProperty(property, writer);
                        writer.Write(Suffix);
                        break;
                    case PatternPartType.NewLine:
                        writer.Write(Environment.NewLine + Suffix);
                        break;
                }
            }

            private void TryWriteProperties(IReadOnlyDictionary<string, object> properties, TextWriter writer)
            {
                if (properties == null)
                    return;
                writer.Write("[properties: ");
                var i = 0;
                var len = properties.Count;
                foreach (var pair in properties)
                {
                    writer.Write(pair.Key + " = ");
                    TryWriteProperty(pair.Value, writer);
                    if (i++ < len - 1)
                        writer.Write(", ");
                }

                writer.Write("]");
            }

            private void TryWritePrefixes(IReadOnlyList<string> prefixes, TextWriter writer)
            {
                for (var i = 0; i < prefixes.Count; i++)
                {
                    writer.Write($"[{prefixes[i]}]");
                    if (i != prefixes.Count - 1)
                        writer.Write(" ");
                }
            }

            private static object GetPropertyOrNull(LogEvent @event, string propertyName) =>
                @event.Properties == null
                    ? null
                    : (@event.Properties.TryGetValue(propertyName, out var property)
                        ? property
                        : null);

            private static void TryWriteProperty(object property, TextWriter writer)
            {
                if (property == null)
                    return;
                writer.Write((property as IFormattable)?.ToString(null, CultureInfo.InvariantCulture) ?? property.ToString());
            }
        }
    }
}