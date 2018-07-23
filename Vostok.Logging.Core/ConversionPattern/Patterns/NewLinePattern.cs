using System;
using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Patterns
{
    public class NewLinePattern : IConversionPatternFragment
    {
        public void Render(LogEvent @event, TextWriter writer) =>
            writer.Write(Environment.NewLine);

        public override string ToString() => "%n";
    }
}