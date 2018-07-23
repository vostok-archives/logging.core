using System;
using System.Globalization;
using System.IO;
using System.Text;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Patterns
{
    internal static class PatternsHelper
    {
        public static object GetPropertyOrNull(LogEvent @event, string propertyName) =>
            @event.Properties == null
                ? null
                : (@event.Properties.TryGetValue(propertyName, out var property)
                    ? property
                    : null);

        public static void TryWriteProperty(object property, TextWriter writer)
        {
            if (property == null)
                return;
            writer.Write((property as IFormattable)?.ToString(null, CultureInfo.InvariantCulture) ?? property.ToString());
        }

        public static string ToString(string symbol, string property, string suffix)
        {
            var sb = new StringBuilder(symbol);
            if (!string.IsNullOrEmpty(property))
                sb.Append($"({property})");
            sb.Append(suffix);
            return sb.ToString();
        }
    }
}