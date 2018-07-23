using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Patterns
{
    internal class StringStartPattern : IConversionPatternFragment
    {
        public StringStartPattern(string @string = null)
        {
            Suffix = @string ?? string.Empty;
        }

        string IConversionPatternFragment.Property => null;
        public string Suffix { get; }

        public void Render(LogEvent @event, TextWriter writer) =>
            writer.Write(Suffix);
    }
}