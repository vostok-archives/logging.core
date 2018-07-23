using System.Collections.Generic;
using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Patterns
{
    public class PropertiesPattern : IConversionPatternFragment
    {
        private readonly string suffix;

        public PropertiesPattern(string suffix = null)
        {
            this.suffix = suffix ?? string.Empty;
        }

        public void Render(LogEvent @event, TextWriter writer)
        {
            if (TryWriteProperties(@event.Properties, writer))
                writer.Write(suffix);
        }

        public override string ToString() => "%p" + suffix;

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