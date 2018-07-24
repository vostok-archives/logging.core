using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Fragments
{
    internal interface IConversionPatternFragment
    {
        string Property { get; }
        string Suffix { get; }

        void Render(LogEvent @event, TextWriter writer);
    }
}   