using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern
{
    // CR(krait): Is there a reason to have multiple renderers? Couldn't ConversionPatternRenderer just be static?
    internal interface IConversionPatternRenderer
    {
        void Render(ConversionPattern pattern, LogEvent @event, TextWriter writer);
    }
}