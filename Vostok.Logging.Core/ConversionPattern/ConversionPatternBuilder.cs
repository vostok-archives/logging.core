using Vostok.Logging.Core.ConversionPattern.Fragments;

namespace Vostok.Logging.Core.ConversionPattern
{
    // TODO(krait): xml doc
    public class ConversionPatternBuilder
    {
        private readonly ConversionPattern conversionPattern;

        public ConversionPatternBuilder()
        {
            conversionPattern = new ConversionPattern();
        }

        public ConversionPatternBuilder AddStringStart(string @string = null)
        {
            conversionPattern.Fragments.Add(new StringStartFragment(@string));
            return this;
        }

        public ConversionPatternBuilder AddDateTime(string format = null, string suffix = null)
        {
            conversionPattern.Fragments.Add(new DateTimeFragment(suffix, format));
            return this;
        }

        public ConversionPatternBuilder AddLevel(string suffix = null)
        {
            conversionPattern.Fragments.Add(new LevelFragment(suffix));
            return this;
        }

        public ConversionPatternBuilder AddMessage(string suffix = null)
        {
            conversionPattern.Fragments.Add(new MessageFragment(suffix));
            return this;
        }

        public ConversionPatternBuilder AddException(string suffix = null)
        {
            conversionPattern.Fragments.Add(new ExceptionFragment(suffix));
            return this;
        }

        public ConversionPatternBuilder AddProperties(string suffix = null)
        {
            conversionPattern.Fragments.Add(new PropertiesFragment(suffix));
            return this;
        }

        public ConversionPatternBuilder AddProperty(string propertyName, string suffix = null)
        {
            conversionPattern.Fragments.Add(new PropertyFragment(propertyName, suffix));
            return this;
        }

        public ConversionPatternBuilder AddNewLine()
        {
            conversionPattern.Fragments.Add(new NewLineFragment());
            return this;
        }

        public ConversionPattern ToPattern() => conversionPattern;
    }
}