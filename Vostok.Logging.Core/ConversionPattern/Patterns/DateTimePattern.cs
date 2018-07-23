using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Patterns
{
    public class DateTimePattern : IConversionPatternFragment
    {
        private const string DateTimeFormatString = "HH:mm:ss zzz";
        private readonly string format;
        private readonly string suffix;

        public DateTimePattern(string suffix = null, string format = null)
        {
            this.suffix = suffix ?? string.Empty;
            this.format = format;
        }

        public void Render(LogEvent @event, TextWriter writer) =>
            writer.Write(
                @event.Timestamp.ToString(
                    string.IsNullOrEmpty(format)
                        ? DateTimeFormatString
                        : format) + suffix);

        public override string ToString() =>
            PatternsHelper.ToString("%d", format, suffix);
    }
}