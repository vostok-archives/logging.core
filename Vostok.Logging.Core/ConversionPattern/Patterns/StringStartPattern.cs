using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Patterns
{
    public class StringStartPattern : IConversionPatternFragment
    {
        private readonly string @string;

        public StringStartPattern(string @string = null)
        {
            this.@string = @string ?? string.Empty;
        }

        public void Render(LogEvent @event, TextWriter writer) =>
            writer.Write(@string);

        public override string ToString() => @string;
    }
}