using System.Collections.Generic;
using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Patterns
{
    internal class PropertiesPattern : IConversionPatternFragment
    {
        public PropertiesPattern(string suffix = null)
        {
            Suffix = suffix ?? string.Empty;
        }

        public string Suffix { get; }

        string IConversionPatternFragment.Property => null;

        public void Render(LogEvent @event, TextWriter writer)
        {
            if (TryWriteProperties(@event.Properties, writer))
                writer.Write(Suffix);
        }

        private static bool TryWriteProperties(IReadOnlyDictionary<string, object> properties, TextWriter writer)
        {
            if (properties == null)
                return false;
            var i = 0;
            var len = properties.Count;
            if (len <= 0)
                return false;

            writer.Write("[properties: ");
            foreach (var pair in properties)
            {
                writer.Write(pair.Key + " = ");
                PatternsHelper.TryWriteProperty(pair.Value, writer);
                if (i++ < len - 1)
                    writer.Write(", ");
            }

            writer.Write("]");
            return true;
        }
    }
}