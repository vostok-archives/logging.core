using System;
using Vostok.Logging.Core.ConversionPattern.Patterns;

namespace Vostok.Logging.Core.ConversionPattern
{
    public class ConversionPatternBuilder
    {
        private readonly ConversionPattern conversionPattern;

        public ConversionPatternBuilder()
        {
            conversionPattern = new ConversionPattern();
        }

        public ConversionPatternBuilder AddStringStart(string @string = null)
        {
            conversionPattern.Fragments.Add(new StringStartPattern(@string));
            return this;
        }

        public ConversionPatternBuilder AddDateTime(string suffix = null, string format = null)
        {
            conversionPattern.Fragments.Add(new DateTimePattern(suffix, format));
            return this;
        }

        public ConversionPatternBuilder AddLevel(string suffix = null)
        {
            conversionPattern.Fragments.Add(new LevelPattern(suffix));
            return this;
        }

        public ConversionPatternBuilder AddPrefix(string suffix = null)
        {
            conversionPattern.Fragments.Add(new PrefixPattern(suffix));
            return this;
        }

        public ConversionPatternBuilder AddMessage(string suffix = null)
        {
            conversionPattern.Fragments.Add(new MessagePattern(suffix));
            return this;
        }

        public ConversionPatternBuilder AddException(string suffix = null)
        {
            conversionPattern.Fragments.Add(new ExceptionPattern(suffix));
            return this;
        }

        public ConversionPatternBuilder AddProperties(string suffix = null)
        {
            conversionPattern.Fragments.Add(new PropertiesPattern(suffix));
            return this;
        }

        public ConversionPatternBuilder AddProperty(string propertyName, string suffix = null)
        {
            conversionPattern.Fragments.Add(new PropertyPattern(propertyName, suffix));
            return this;
        }

        public ConversionPatternBuilder AddNewLine()
        {
            conversionPattern.Fragments.Add(new NewLinePattern());
            return this;
        }

        public ConversionPattern ToPattern() => conversionPattern;
    }
}