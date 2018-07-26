using System.Collections.Generic;
using System.IO;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Core.Helpers;

namespace Vostok.Logging.Core.Fragments
{
    internal class PropertiesFragment : CompareByType<PropertiesFragment>, IConversionPatternFragment
    {
        private const string Text = "%p";

        public static PropertiesFragment TryParse(string value, ref int offset) =>
            FragmentHelpers.TryParse<PropertiesFragment>(Text, value, ref offset);

        public void Render(LogEvent @event, TextWriter writer) =>
            WriteProperties(@event.Properties, writer);

        public bool HasValue(LogEvent @event) => @event.Properties != null && @event.Properties.Count > 0;

        public override string ToString() => Text;

        private static void WriteProperties(IReadOnlyDictionary<string, object> properties, TextWriter writer)
        {
            if (properties == null || properties.Count == 0)
                return;

            writer.Write("[properties: ");

            var i = 0;
            foreach (var pair in properties)
            {
                writer.Write(pair.Key);
                writer.Write(" = ");
                FragmentHelpers.TryWriteProperty(pair.Value, null, writer);
                if (++i < properties.Count)
                    writer.Write(", ");
            }

            writer.Write("]");
        }
    }
}