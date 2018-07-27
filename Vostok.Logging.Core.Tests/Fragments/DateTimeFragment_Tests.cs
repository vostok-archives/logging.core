using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Core.Fragments;

namespace Vostok.Logging.Core.Tests.Fragments
{
    [TestFixture]
    public class DateTimeFragment_Tests
    {
        [SetUp]
        public void SetUp()
        {
            @event = new LogEvent(LogLevel.Info, DateTimeOffset.UtcNow, "All is good.");
        }

        [Test]
        public void TryParse_should_parse_fragment_without_format()
        {
            var offset = 1;
            DateTimeFragment.TryParse(" %d", ref offset).Should().Be(new DateTimeFragment(null));
        }

        [Test]
        public void TryParse_should_parse_fragment_with_format()
        {
            var offset = 1;
            DateTimeFragment.TryParse(" %d(yyyy)", ref offset).Should().Be(new DateTimeFragment("yyyy"));
        }

        [Test]
        public void TryParse_should_move_offset_accordingly()
        {
            var offset = 1;
            DateTimeFragment.TryParse(" %d(yyyy)", ref offset);

            offset.Should().Be(9);
        }

        [TestCase(" %")]
        [TestCase(" %l")]
        [TestCase(" d")]
        public void TryParse_should_return_null_on_invalid_input(string input)
        {
            var offset = 1;
            DateTimeFragment.TryParse(input, ref offset).Should().BeNull();
        }

        [Test]
        public void TryParse_should_not_move_offset_on_invalid_input()
        {
            var offset = 1;
            DateTimeFragment.TryParse(" %l", ref offset);

            offset.Should().Be(1);
        }

        [TestCase(" %D(yyyy)")]
        [TestCase(" %D")]
        public void TryParse_should_be_case_insensitive(string input)
        {
            var offset = 1;
            DateTimeFragment.TryParse(input, ref offset).Should().NotBeNull();
        }

        [Test]
        public void HasValue_should_return_true()
        {
            new DateTimeFragment(null).HasValue(@event).Should().BeTrue();
        }

        [Test]
        public void Render_should_render_Timestamp_with_default_format_if_format_is_null()
        {
            var writer = new StringWriter();
            new DateTimeFragment(null).Render(@event, writer);

            writer.ToString().Should().Be(@event.Timestamp.ToString("yyyy-MM-dd HH:mm:ss,fff"));
        }

        [Test]
        public void Render_should_render_Timestamp_with_custom_format_if_it_is_set()
        {
            var writer = new StringWriter();
            new DateTimeFragment("yyyy").Render(@event, writer);

            writer.ToString().Should().Be(@event.Timestamp.ToString("yyyy"));
        }

        [Test]
        public void Equals_should_respect_format()
        {
            new DateTimeFragment("yyyy").Equals(new DateTimeFragment("hh")).Should().BeFalse();
            new DateTimeFragment("yyyy").Equals(new DateTimeFragment("yyyy")).Should().BeTrue();
        }

        [Test]
        public void Equals_should_consider_fragments_without_format_equal()
        {
            new DateTimeFragment(null).Equals(new DateTimeFragment(null)).Should().BeTrue();
        }

        [Test]
        public void GetHashCode_should_depend_on_format()
        {
            var h1 = new DateTimeFragment(null).GetHashCode();
            var h2 = new DateTimeFragment("yyyy").GetHashCode();

            h1.Should().NotBe(h2);
        }

        [Test]
        public void GetHashCode_should_be_same_for_equal_instances()
        {
            new DateTimeFragment("yyyy").GetHashCode().Should().Be(new DateTimeFragment("yyyy").GetHashCode());
            new DateTimeFragment(null).GetHashCode().Should().Be(new DateTimeFragment(null).GetHashCode());
        }

        private LogEvent @event;
    }
}