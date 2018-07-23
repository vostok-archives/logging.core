using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Patterns
{
    public class ExceptionPattern : IConversionPatternFragment
    {
        private readonly string suffix;

        public ExceptionPattern(string suffix = null)
        {
            this.suffix = suffix ?? string.Empty;
        }

        public void Render(LogEvent @event, TextWriter writer)
        {
            if (@event.Exception != null)
                writer.Write(@event.Exception + suffix);
        }

        public override string ToString() => "%e" + suffix;
    }
}