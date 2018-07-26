using System.IO;
using System.Text.RegularExpressions;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.Fragments
{
    internal class DateTimeFragment : IConversionPatternFragment
    {
        private const string DefaultFormatString = "yyyy-MM-dd HH:mm:ss,fff";

        private static readonly Regex Regex = new Regex(@"^%d(?:\((?<format>.+?)\))?", 
            RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static DateTimeFragment TryParse(string value, ref int offset)
        {
            var match = Regex.Match(value, offset, value.Length - offset);
            if (!match.Success)
                return null;

            offset += match.Length;
            return new DateTimeFragment(match.Groups["format"].GetValueOrNull());
        }

        private readonly string format;

        public DateTimeFragment(string format) => this.format = format;

        public void Render(LogEvent @event, TextWriter writer) =>
            writer.Write(@event.Timestamp.ToString(format ?? DefaultFormatString));

        public bool HasValue(LogEvent @event) => true;

        public override string ToString() =>
            format == null ? "%d" : $"%d({format})";

        #region Equality

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is DateTimeFragment other)
                return Equals(other);

            return false;
        }

        public override int GetHashCode() => format?.GetHashCode() ?? 0;

        protected bool Equals(DateTimeFragment other) => other != null && format == other.format;

        #endregion
    }
}