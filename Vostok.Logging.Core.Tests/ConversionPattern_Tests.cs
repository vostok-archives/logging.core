using FluentAssertions;
using NUnit.Framework;

namespace Vostok.Logging.Core.Tests
{
    [TestFixture]
    public class ConversionPattern_Tests
    {
        [Test]
        public void ToString_should_return_right_string()
        {
            ConversionPattern.Create().AddDateTime().Build().ToString().Should().Be("%d");

            ConversionPattern.Create()
                .AddText("test: ")
                .AddDateTime()
                .AddText(" ")
                .AddLevel()
                .AddText(" ")
                .AddProperty("logging.prefix")
                .AddText(" ")
                .AddMessage()
                .AddNewLine()
                .AddException()
                .AddNewLine()
                .Build().ToString().Should().Be("test: %d %l %p(logging.prefix) %m%n%e%n");
        }

        [Test]
        public void Should_be_equal_by_references()
        {
            var pattern1 = ConversionPattern.Default;
            var pattern2 = pattern1;

            pattern1.Equals(pattern2).Should().BeTrue();
            pattern2.Equals(pattern1).Should().BeTrue();
        }

        [Test]
        public void Should_be_equal_by_values()
        {
            var pattern1 = ConversionPattern.Parse("%l %m");
            var pattern2 = ConversionPattern.Create().AddLevel().AddText(" ").AddMessage().Build();

            pattern1.Equals(pattern2).Should().BeTrue();
            pattern2.Equals(pattern1).Should().BeTrue();
        }

        [Test]
        public void Should_not_be_equal_by_values()
        {
            var pattern1 = ConversionPattern.Parse("%l %m");
            var pattern2 = ConversionPattern.Parse("%l %e");

            pattern1.Equals(pattern2).Should().BeFalse();
            pattern2.Equals(pattern1).Should().BeFalse();
        }

        [Test]
        public void Hashes_should_be_equal()
        {
            var hash1 = ConversionPattern.Parse("%l %m").GetHashCode();
            var hash2 = ConversionPattern.Create().AddLevel().AddText(" ").AddMessage().Build().GetHashCode();

            hash1.Should().Be(hash2);
        }

        [Test]
        public void Hashes_should_not_be_equal()
        {
            var hash1 = ConversionPattern.Parse("%l %m").GetHashCode();
            var hash2 = ConversionPattern.Parse("%l %e").GetHashCode();

            hash1.Should().NotBe(hash2);
        }

        [Test]
        public void Hashes_should_not_be_equal_by_formats()
        {
            var hash1 = ConversionPattern.Parse("%l %p(prop:f2)").GetHashCode();
            var hash2 = ConversionPattern.Parse("%l %p(prop:f3)").GetHashCode();

            hash1.Should().NotBe(hash2);
        }
    }
}