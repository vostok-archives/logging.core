using System.Collections.Generic;
using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Fragments
{
    internal class PrefixFragment : IConversionPatternFragment
    {
        private const string PrefixPropertyName = "prefix";

        public PrefixFragment(string suffix = null)
        {
            Suffix = suffix ?? string.Empty;
        }

        public string Suffix { get; }

        string IConversionPatternFragment.Property => null;

        public void Render(LogEvent @event, TextWriter writer)
        {
            var prefixProperty = FragmentsHelper.GetPropertyOrNull(@event, PrefixPropertyName);
            if (prefixProperty is IReadOnlyList<string> prefixes)
            {
                TryWritePrefixes(prefixes, writer);
                writer.Write(Suffix);
            }
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
    }
}