using System.Collections.Generic;
using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Patterns
{
    public class PrefixPattern : IConversionPatternFragment
    {
        private const string PrefixPropertyName = "prefix";
        private readonly string suffix;

        public PrefixPattern(string suffix = null)
        {
            this.suffix = suffix ?? string.Empty;
        }

        public void Render(LogEvent @event, TextWriter writer)
        {
            var prefixProperty = PatternsHelper.GetPropertyOrNull(@event, PrefixPropertyName);
            if (prefixProperty is IReadOnlyList<string> prefixes)
            {
                TryWritePrefixes(prefixes, writer);
               writer.Write(suffix);
            }
        }

        public override string ToString() => "%x" + suffix;

        private void TryWritePrefixes(IReadOnlyList<string> prefixes, TextWriter writer)
        {
            for (var i = 0; i < prefixes.Count; i++)
            {
                writer.Write($"[{prefixes[i]}]");
                if (i != prefixes.Count - 1)
                    writer.Write(" ");
            }
        }
    }
}