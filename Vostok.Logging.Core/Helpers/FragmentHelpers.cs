using System;
using System.Globalization;
using System.IO;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Core.Fragments;

namespace Vostok.Logging.Core.Helpers
{
    internal static class FragmentHelpers
    {
        public static object GetPropertyOrNull(LogEvent @event, string propertyName) =>
            @event.Properties == null
                ? null
                : (@event.Properties.TryGetValue(propertyName, out var property)
                    ? property
                    : null);

        public static void TryWriteProperty(object property, string format, TextWriter writer)
        {
            if (property == null)
                return;

            writer.Write((property as IFormattable)?.ToString(format, CultureInfo.InvariantCulture) ?? property.ToString());
        }

        public static T TryParse<T>(string fragmentText, string input, ref int offset)
            where T : IConversionPatternFragment, new()
        {
            if (string.Compare(input, offset, fragmentText, 0, fragmentText.Length, StringComparison.InvariantCultureIgnoreCase) != 0)
                return default;

            offset += fragmentText.Length;
            return new T();
        }
    }
}