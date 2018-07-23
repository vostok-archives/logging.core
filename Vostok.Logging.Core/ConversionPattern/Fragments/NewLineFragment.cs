using System;
using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Fragments
{
    internal class NewLineFragment : IConversionPatternFragment
    {
        string IConversionPatternFragment.Property => null;
        string IConversionPatternFragment.Suffix => null;

        public void Render(LogEvent @event, TextWriter writer) =>
            writer.Write(Environment.NewLine);
    }
}