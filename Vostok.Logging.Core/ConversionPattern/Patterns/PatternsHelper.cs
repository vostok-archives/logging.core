using System;
using System.Globalization;
using System.IO;
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
    }
}