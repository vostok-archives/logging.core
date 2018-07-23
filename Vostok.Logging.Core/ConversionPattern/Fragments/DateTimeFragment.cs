using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Fragments
{
    internal class DateTimeFragment : IConversionPatternFragment
    {
        private const string DateTimeFormatString = "HH:mm:ss zzz";

        public DateTimeFragment(string suffix = null, string format = null)
        {
            Suffix = suffix ?? string.Empty;
            Property = format;
        }

        public string Property { get; }
        public string Suffix { get; }

        public void Render(LogEvent @event, TextWriter writer) =>
            writer.Write(
                @event.Timestamp.ToString(
                    string.IsNullOrEmpty(Property)
                        ? DateTimeFormatString
                        : Property) + Suffix);
    }
}