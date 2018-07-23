using System;
using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Patterns
{
    internal class PropertyPattern : IConversionPatternFragment
    {
        public PropertyPattern(string propertyName, string suffix = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException($"{nameof(propertyName)} must not be null or empty!");
            Property = propertyName;
            Suffix = suffix ?? string.Empty;
        }

        public string Property { get; }
        public string Suffix { get; }

        public void Render(LogEvent @event, TextWriter writer)
        {
            var prop = PatternsHelper.GetPropertyOrNull(@event, Property);
            if (prop != null)
            {
                PatternsHelper.TryWriteProperty(prop, writer);
                writer.Write(Suffix);
            }
        }
    }
}