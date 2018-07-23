using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Patterns
{
    public class MessagePattern : IConversionPatternFragment
    {
        private readonly string suffix;

        public MessagePattern(string suffix = null)
        {
            this.suffix = suffix ?? string.Empty;
        }

        public void Render(LogEvent @event, TextWriter writer)
        {
            if (@event.MessageTemplate != null)
                writer.Write(@event.MessageTemplate + suffix);
        }

        public override string ToString() => "%m" + suffix;
    }
}