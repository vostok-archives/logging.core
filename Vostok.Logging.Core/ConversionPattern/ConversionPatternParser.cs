using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Vostok.Logging.Core.ConversionPattern.Fragments;

namespace Vostok.Logging.Core.ConversionPattern
{
    // TODO(krait): xml doc
    public static class ConversionPatternParser
    {
        private static readonly Dictionary<Type, (Action<ConversionPatternBuilder, string, string> ctor, string pattern, string startsFrom)> PatternKeys;
        private static readonly Regex Regex;

        static ConversionPatternParser()
        {
            PatternKeys = new Dictionary<Type, (Action<ConversionPatternBuilder, string, string>, string, string)>
            {
                {typeof (DateTimeFragment), ((builder, property, suffix) => builder.AddDateTime(suffix, property), @"d(?:\(([^)]*)\))?", "d")},
                {typeof (LevelFragment), ((builder, _, suffix) => builder.AddLevel(suffix), "l", "l")},
                {typeof (MessageFragment), ((builder, _, suffix) => builder.AddMessage(suffix), "m", "m")},
                {typeof (ExceptionFragment), ((builder, _, suffix) => builder.AddException(suffix), "e", "e")},
                {typeof (PropertyFragment), ((builder, property, suffix) => builder.AddProperty(property, suffix), @"p\((\w*)\)", "p(")},
                {typeof (PropertiesFragment), ((builder, _, suffix) => builder.AddProperties(suffix), "p", "p")},
                {typeof (NewLineFragment), ((builder, _, __) => builder.AddNewLine(), "n", "n")},
            };

            var anyKeyRegex = string.Join("|", PatternKeys.Values.Select(v => v.pattern));
            var regexPattern = $"(?:%(?:{anyKeyRegex})|^)(?<suffix>[^%]*)";
            Regex = new Regex(regexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        public static ConversionPattern Parse(string pattern)
        {
            var result = new ConversionPatternBuilder();
            if (string.IsNullOrEmpty(pattern))
                return result.ToPattern();

            var matches = Regex.Matches(pattern);
            foreach (Match match in matches)
            {
                var (type, prop, suffix) = ParseMatch(match, match.Index);
                if (type == null)
                    result.AddStringStart(suffix);
                else
                    PatternKeys[type].ctor(result, prop, suffix);
            }

            return result.ToPattern();
        }

        public static string ToString(ConversionPattern pattern)
        {
            var sb = new StringBuilder();
            foreach (var fragment in pattern.Fragments)
            {
                if (PatternKeys.TryGetValue(fragment.GetType(), out var value))
                    sb.Append("%" + value.startsFrom);
                if (value.startsFrom != null && value.startsFrom.EndsWith("("))
                    sb.Remove(sb.Length - 1, 1);
                if (!string.IsNullOrEmpty(fragment.Property))
                    sb.Append($"({fragment.Property})");
                sb.Append(fragment.Suffix);
            }

            return sb.ToString();
        }

        private static (Type type, string property, string suffix) ParseMatch(Match match, int matchOffset)
        {
            Type type = null;
            var value = match.Value;
            foreach (var pair in PatternKeys)
            {
                var start = "%" + pair.Value.startsFrom;
                if (value.StartsWith(start, StringComparison.InvariantCultureIgnoreCase))
                {
                    type = pair.Key;
                    break;
                }
            }

            var suffix = match.Groups["suffix"].Value;
            var suffixIndex = match.Groups["suffix"].Index - matchOffset;

            var i = 0;
            var property = string.Empty;
            if (suffixIndex > 0)
                foreach (Group gr in match.Groups)
                    if (!string.IsNullOrEmpty(gr.Value) && i++ > 0 && i != suffixIndex)
                    {
                        property = gr.Value;
                        break;
                    }

            return (type, property, suffix);
        }
    }
}