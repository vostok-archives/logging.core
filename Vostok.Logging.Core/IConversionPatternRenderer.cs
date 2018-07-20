using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core
{
    internal interface IConversionPatternRenderer
    {
        void Render(ConversionPattern pattern, LogEvent @event, TextWriter writer);
    }
}