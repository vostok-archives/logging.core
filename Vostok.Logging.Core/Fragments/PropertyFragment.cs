using System.IO;
using System.Text.RegularExpressions;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.Fragments
{
    internal class PropertyFragment : IConversionPatternFragment
    {
        private static readonly Regex Regex = new Regex(@"^%p\((?<property>.+?)(?::(?<format>.+?))?\)", 
            RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static PropertyFragment TryParse(string value, ref int offset)
        {
            var match = Regex.Match(value, offset, value.Length - offset);
            if (!match.Success)
                return null;

            offset += match.Length;
            return new PropertyFragment(match.Groups["property"].GetValueOrNull(), match.Groups["format"].GetValueOrNull());
        }

        private readonly string property;
        private readonly string format;

        public PropertyFragment(string property, string format)
        {
            this.property = property;
            this.format = format;
        }

        public void Render(LogEvent @event, TextWriter writer) =>
            FragmentHelpers.TryWriteProperty(FragmentHelpers.GetPropertyOrNull(@event, property), format, writer);

        public override string ToString() =>
            format == null ? $"%p({property})" : $"%p({property}:{format})";

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is PropertyFragment other)
                return Equals(other);

            return false;
        }

        public override int GetHashCode() => (format?.GetHashCode() ?? 0) * 397 ^ property.GetHashCode();

        protected bool Equals(PropertyFragment other) => other != null && format == other.format && property == other.property;
    }
}