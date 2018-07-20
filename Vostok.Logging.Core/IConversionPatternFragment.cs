using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core
{
    internal interface IConversionPatternFragment
    {
        void Render(LogEvent @event, TextWriter writer);
    }
}