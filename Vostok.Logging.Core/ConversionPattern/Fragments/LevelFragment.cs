using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Fragments
{
    internal class LevelFragment : IConversionPatternFragment
    {
        public LevelFragment(string suffix = null)
        {
            Suffix = suffix ?? string.Empty;
        }

        public string Suffix { get; }

        string IConversionPatternFragment.Property => null;

        public void Render(LogEvent @event, TextWriter writer) =>
            writer.Write(@event.Level + Suffix);
    }
}