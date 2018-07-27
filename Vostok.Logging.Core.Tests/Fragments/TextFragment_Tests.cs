using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Core.Fragments;

namespace Vostok.Logging.Core.Tests.Fragments
{
    [TestFixture]
    public class TextFragment_Tests
    {
        [SetUp]
        public void SetUp()
        {
            @event = new LogEvent(LogLevel.Info, DateTimeOffset.UtcNow, "All is good.");
        }

        [TestCase(null, false)]
        [TestCase("text", true)]
        public void HasValue_should_return_true(string value, bool result)
        {
            new TextFragment(value).HasValue(@event).Should().Be(result);
        }

        [Test]
        public void Render_should_render_Exception()
        {
            var writer = new StringWriter();
            new TextFragment("text").Render(@event, writer);

            writer.ToString().Should().Be("text");
        }

        [Test]
        public void Equals_should_consider_fragments_equal()
        {
            new TextFragment(null).Equals(new TextFragment(null)).Should().BeTrue();
            new TextFragment("text").Equals(new TextFragment("text")).Should().BeTrue();
        }

        [Test]
        public void GetHashCode_should_be_same_for_equal_instances()
        {
            new TextFragment(null).GetHashCode().Should().Be(new TextFragment(null).GetHashCode());
            new TextFragment("text").GetHashCode().Should().Be(new TextFragment("text").GetHashCode());
        }

        [Test]
        public void GetHashCode_should_depend_on_value()
        {
            var h1 = new TextFragment(null).GetHashCode();
            var h2 = new TextFragment("text").GetHashCode();

            h1.Should().NotBe(h2);
        }

        private LogEvent @event;
    }
}