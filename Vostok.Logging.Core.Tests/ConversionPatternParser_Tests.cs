using FluentAssertions;
using NUnit.Framework;
using Vostok.Logging.Core.ConversionPattern;

namespace Vostok.Logging.Core.Tests.New
{
    [TestFixture]
    public class ConversionPatternParser_Tests
    {
        [Test]
        public void Should_parse_date()
        {
            const string format = "%d";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be(format);

            new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.DateTime)
                .ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_date_uppercase_with_property()
        {
            const string dateFormat = "yyyy-MM-dd";
            var format = $"%D({dateFormat})";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be($"%d({dateFormat})");

            new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.DateTime, dateFormat, null)
                .ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_level_with_suffix()
        {
            const string suffix = "suffix";
            var format = $"%l{suffix}";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be(format);

            new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.Level, suffix)
                .ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_message()
        {
            const string format = "%m";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be(format);

            new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.Message)
                .ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_exception()
        {
            const string format = "%e";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be(format);

            new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.Exception)
                .ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_new_line()
        {
            const string format = "%n";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be(format);

            new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.NewLine)
                .ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_prefix()
        {
            const string format = "%x";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be(format);

            new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.Prefix)
                .ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_properties()
        {
            const string format = "%p";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be(format);

            new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.Properties)
                .ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_property()
        {
            const string prop = "prop";
            var format = $"%p({prop})";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be(format);

            new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.Property, prop, null)
                .ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_property_with_suffix()
        {
            const string prop = "prop";
            const string suffix = "SUFFIX";
            var format = $"%p({prop}){suffix}";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be(format);

            new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.Property, prop, suffix)
                .ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_string_start()
        {
            const string format = "start";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be(format);

            new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.StringStart, format)
                .ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_multiple_values_pattern()
        {
            const string format = "start %D(yyyy-MM-dd) %l %p(prop) message: %M%N";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be("start %d(yyyy-MM-dd) %l %p(prop) message: %m%n");

            new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.StringStart, "start ")
                .AddFragment(PatternPartType.DateTime, "yyyy-MM-dd", " ")
                .AddFragment(PatternPartType.Level, " ")
                .AddFragment(PatternPartType.Property, "prop", " message: ")
                .AddFragment(PatternPartType.Message)
                .AddFragment(PatternPartType.NewLine)
                .ToString().Should().Be(pattern.ToString());
        }
    }
}