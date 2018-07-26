using System.IO;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Core.Helpers;

namespace Vostok.Logging.Core.Fragments
{
    internal class MessageFragment : CompareByType<MessageFragment>, IConversionPatternFragment
    {
        private const string Text = "%m";

        public static MessageFragment TryParse(string value, ref int offset) =>
            FragmentHelpers.TryParse<MessageFragment>(Text, value, ref offset);

        public void Render(LogEvent @event, TextWriter writer) =>
            writer.Write(LogMessageFormatter.FormatMessage(@event.MessageTemplate, @event.Properties));

        public bool HasValue(LogEvent @event) => !string.IsNullOrEmpty(@event.MessageTemplate);

        public override string ToString() => Text;
    }
}