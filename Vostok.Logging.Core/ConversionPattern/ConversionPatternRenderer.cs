using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern
{
    public static class ConversionPatternRenderer
    {
        public static void Render(ConversionPattern pattern, LogEvent @event, TextWriter writer) =>
            pattern.Fragments.ForEach(f => f.Render(@event, writer));
    }
}