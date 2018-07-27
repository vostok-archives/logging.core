using FluentAssertions;
using NUnit.Framework;

namespace Vostok.Logging.Core.Tests
{
    [TestFixture]
    public class ConversionPatternParser_Tests
    {
        [Test]
        public void Shuld_return_null_if_pattern_is_null()
        {
            ConversionPatternParser.Parse(null).Should().BeNull();
        }

        [Test]
        public void Shuld_parse_empty_pattern_to_empty_result()
        {
            ConversionPatternParser.Parse(string.Empty).Should()
                .BeEquivalentTo(ConversionPattern.Create().Build());
        }

        [Test]
        public void Should_parse_multiple_values_pattern()
        {
            const string pattern = "start %d %D(yyyy-MM-dd) %l %P %p(prop)%p(prop2:format) message: %M%N %e%n end";
            var conversionPattern = ConversionPatternParser.Parse(pattern);
            conversionPattern.ToString().Should().Be("start %d %d(yyyy-MM-dd) %l %p %p(prop)%p(prop2:format) message: %m%n %e%n end");

            conversionPattern.Should()
                .BeEquivalentTo(
                    ConversionPattern.Create()
                        .AddText("start ")
                        .AddDateTime()
                        .AddText(" ")
                        .AddDateTime("yyyy-MM-dd")
                        .AddText(" ")
                        .AddLevel()
                        .AddText(" ")
                        .AddProperties()
                        .AddText(" ")
                        .AddProperty("prop")
                        .AddProperty("prop2", "format")
                        .AddText(" message: ")
                        .AddMessage()
                        .AddNewLine()
                        .AddText(" ")
                        .AddException()
                        .AddNewLine()
                        .AddText(" end")
                        .Build());
        }
    }
}