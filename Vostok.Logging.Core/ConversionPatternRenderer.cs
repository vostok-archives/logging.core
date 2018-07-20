using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core
{
    public class ConversionPatternRenderer : IConversionPatternRenderer
    {
        public void Render(ConversionPattern pattern, LogEvent @event, TextWriter writer) =>
            pattern.Render(@event, writer);
    }
}