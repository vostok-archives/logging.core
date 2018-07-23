using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Patterns
{
    public class LevelPattern : IConversionPatternFragment
    {
        private readonly string suffix;

        public LevelPattern(string suffix = null)
        {
            this.suffix = suffix ?? string.Empty;
        }

        public void Render(LogEvent @event, TextWriter writer) =>
            writer.Write(@event.Level + suffix);

        public override string ToString() => "%l" + suffix;
    }
}