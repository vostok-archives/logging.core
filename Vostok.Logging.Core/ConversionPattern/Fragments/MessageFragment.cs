using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Fragments
{
    internal class MessageFragment : IConversionPatternFragment
    {
        public MessageFragment(string suffix = null)
        {
            Suffix = suffix ?? string.Empty;
        }

        public string Suffix { get; }

        string IConversionPatternFragment.Property => null;

        public void Render(LogEvent @event, TextWriter writer)
        {
            if (@event.MessageTemplate != null)
                writer.Write(@event.MessageTemplate + Suffix);
        }
    }
}