using System;
using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Fragments
{
    internal class PropertyFragment : IConversionPatternFragment
    {
        public PropertyFragment(string propertyName, string suffix = null)
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
            var prop = FragmentsHelper.GetPropertyOrNull(@event, Property);
            if (prop != null)
            {
                FragmentsHelper.TryWriteProperty(prop, writer);
                writer.Write(Suffix);
            }
        }
    }
}