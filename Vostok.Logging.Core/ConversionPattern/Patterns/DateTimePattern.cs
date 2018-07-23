using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Patterns
{
    internal class DateTimePattern : IConversionPatternFragment
    {
        private const string DateTimeFormatString = "HH:mm:ss zzz";

        public DateTimePattern(string suffix = null, string format = null)
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