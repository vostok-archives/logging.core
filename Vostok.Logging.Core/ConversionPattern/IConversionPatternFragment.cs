using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern
{
    public interface IConversionPatternFragment
    {
        void Render(LogEvent @event, TextWriter writer);
    }
}