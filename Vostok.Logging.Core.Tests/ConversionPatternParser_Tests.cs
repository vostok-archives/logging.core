using FluentAssertions;
using NUnit.Framework;

namespace Vostok.Logging.Core.Tests
{
    [TestFixture]
    public class ConversionPatternParser_Tests
    {
        [Test]
        public void Should_parse_date()
        {
            const string format = "%d";
            var pattern = ConversionPatternParser.Parse(format);
            
            pattern.Should().Be(ConversionPattern.Create().AddDateTime().Build());
        }

        [Test]
        public void Should_parse_date_uppercase_with_property()
        {
            const string dateFormat = "yyyy-MM-dd";
            var format = $"%D({dateFormat})";
            var pattern = ConversionPatternParser.Parse(format);

            pattern.Should().Be(ConversionPattern.Create().AddDateTime(dateFormat).Build());
        }

        [Test]
        public void Should_parse_level()
        {
            const string format = "%l";
            var pattern = ConversionPatternParser.Parse(format);

            pattern.Should().Be(ConversionPattern.Create().AddLevel().Build());
        }

        [Test]
        public void Should_parse_message()
        {
            const string format = "%m";
            var pattern = ConversionPatternParser.Parse(format);

            pattern.Should().Be(ConversionPattern.Create().AddMessage().Build());
        }

        [Test]
        public void Should_parse_exception()
        {
            const string format = "%e";
            var pattern = ConversionPatternParser.Parse(format);

            pattern.Should().Be(ConversionPattern.Create().AddException().Build());
        }

        [Test]
        public void Should_parse_new_line()
        {
            const string format = "%n";
            var pattern = ConversionPatternParser.Parse(format);

            pattern.Should().Be(ConversionPattern.Create().AddNewLine().Build());
        }

        [Test]
        public void Should_parse_properties()
        {
            const string format = "%p";
            var pattern = ConversionPatternParser.Parse(format);

            pattern.Should().Be(ConversionPattern.Create().AddProperties().Build());
        }

        [Test]
        public void Should_parse_property()
        {
            const string prop = "prop";
            var format = $"%p({prop})";
            var pattern = ConversionPatternParser.Parse(format);

            pattern.Should().Be(ConversionPattern.Create().AddProperty(prop).Build()); // TODO(krait): with format
        }

        [Test]
        public void Should_parse_text()
        {
            const string format = "text";
            var pattern = ConversionPatternParser.Parse(format);

            pattern.Should().Be(ConversionPattern.Create().AddText(format).Build());
        }

        // TODO(krait): test ToString() separately
        [Test]
        public void Should_parse_multiple_values_pattern()
        {
            const string format = "start %D(yyyy-MM-dd) %l %p(prop) message: %M%N";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be("start %d(yyyy-MM-dd) %l %p(prop) message: %m%n");

            pattern.Should()
                .Be(
                    ConversionPattern.Create()
                        .AddText("start ")
                        .AddDateTime("yyyy-MM-dd")
                        .AddText(" ")
                        .AddLevel()
                        .AddText(" ")
                        .AddProperty("prop")
                        .AddText(" message: ")
                        .AddMessage()
                        .AddNewLine()
                        .Build());
        }

        [Test]
        public void Should_render_only_template_string_if_it_not_contains_any_keys()
        {
            const string format = "abc";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be("abc");
        }
    }
}