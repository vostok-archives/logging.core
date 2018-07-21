using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern
{
    internal interface IConversionPatternFragment
    {
        void Render(LogEvent @event, TextWriter writer);
    }
}