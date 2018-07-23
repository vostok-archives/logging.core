using System;
using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Patterns
{
    public class PropertyPattern : IConversionPatternFragment
    {
        private readonly string propertyName;
        private readonly string suffix;

        public PropertyPattern(string propertyName, string suffix = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException($"{nameof(propertyName)} must not be null or empty!");
            this.propertyName = propertyName;
            this.suffix = suffix ?? string.Empty;
        }

        public void Render(LogEvent @event, TextWriter writer)
        {
            var prop = PatternsHelper.GetPropertyOrNull(@event, propertyName);
            if (prop != null)
            {
                PatternsHelper.TryWriteProperty(prop, writer);
                writer.Write(suffix);
            }
        }

        public override string ToString() =>
            PatternsHelper.ToString("%p", propertyName, suffix);
    }
}