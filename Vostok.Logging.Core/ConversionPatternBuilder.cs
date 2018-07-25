using System.Collections.Generic;
using Vostok.Logging.Core.Fragments;

namespace Vostok.Logging.Core
{
    // TODO(krait): xml doc
    // TODO(krait): test that it has all the required methods
    public class ConversionPatternBuilder
    {
        private readonly List<IConversionPatternFragment> fragments = new List<IConversionPatternFragment>();
        
        public ConversionPatternBuilder AddDateTime(string format = null)
        {
            fragments.Add(new DateTimeFragment(format));
            return this;
        }

        public ConversionPatternBuilder AddLevel()
        {
            fragments.Add(new LevelFragment());
            return this;
        }

        public ConversionPatternBuilder AddMessage()
        {
            fragments.Add(new MessageFragment());
            return this;
        }

        public ConversionPatternBuilder AddException()
        {
            fragments.Add(new ExceptionFragment());
            return this;
        }

        public ConversionPatternBuilder AddProperties()
        {
            fragments.Add(new PropertiesFragment());
            return this;
        }

        public ConversionPatternBuilder AddProperty(string propertyName, string format = null)
        {
            fragments.Add(new PropertyFragment(propertyName, format));
            return this;
        }

        public ConversionPatternBuilder AddNewLine()
        {
            fragments.Add(new NewLineFragment());
            return this;
        }

        public ConversionPatternBuilder AddText(string text)
        {
            fragments.Add(new TextFragment(text));
            return this;
        }

        public ConversionPattern Build() => new ConversionPattern(fragments);
    }
}