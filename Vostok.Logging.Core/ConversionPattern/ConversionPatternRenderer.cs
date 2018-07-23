using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern
{
    public static class ConversionPatternRenderer
    {
        public static void Render(ConversionPattern pattern, LogEvent @event, TextWriter writer)
        {
            foreach (var f in pattern.Fragments)
                f.Render(@event, writer);
        }
    }
}