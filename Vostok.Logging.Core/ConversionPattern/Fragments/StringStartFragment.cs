using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Fragments
{
    internal class StringStartFragment : IConversionPatternFragment
    {
        public StringStartFragment(string @string = null)
        {
            Suffix = @string ?? string.Empty;
        }

        string IConversionPatternFragment.Property => null;
        public string Suffix { get; }

        public void Render(LogEvent @event, TextWriter writer) =>
            writer.Write(Suffix);
    }
}