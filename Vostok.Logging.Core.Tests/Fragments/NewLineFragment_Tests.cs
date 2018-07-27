using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Core.Fragments;

namespace Vostok.Logging.Core.Tests.Fragments
{
    [TestFixture]
    public class NewLineFragment_Tests
    {
        [SetUp]
        public void SetUp()
        {
            @event = new LogEvent(LogLevel.Info, DateTimeOffset.UtcNow, "All is good.");
        }

        [Test]
        public void TryParse_should_parse_fragment()
        {
            var offset = 1;
            NewLineFragment.TryParse(" %n", ref offset).Should().Be(new NewLineFragment());
        }

        [Test]
        public void TryParse_should_move_offset_accordingly()
        {
            var offset = 1;
            NewLineFragment.TryParse(" %n", ref offset);

            offset.Should().Be(3);
        }

        [TestCase(" %")]
        [TestCase(" %d")]
        [TestCase(" n")]
        public void TryParse_should_return_null_on_invalid_input(string input)
        {
            var offset = 1;
            NewLineFragment.TryParse(input, ref offset).Should().BeNull();
        }

        [Test]
        public void TryParse_should_not_move_offset_on_invalid_input()
        {
            var offset = 1;
            NewLineFragment.TryParse(" %d", ref offset);

            offset.Should().Be(1);
        }

        [Test]
        public void TryParse_should_be_case_insensitive()
        {
            var offset = 1;
            NewLineFragment.TryParse(" %N", ref offset).Should().NotBeNull();
        }

        [Test]
        public void HasValue_should_return_true()
        {
            new NewLineFragment().HasValue(@event).Should().BeTrue();
        }

        [Test]
        public void Render_should_render_Exception()
        {
            var writer = new StringWriter();
            new NewLineFragment().Render(@event, writer);

            writer.ToString().Should().Be(Environment.NewLine);
        }

        [Test]
        public void Equals_should_consider_fragments_equal()
        {
            new NewLineFragment().Equals(new NewLineFragment()).Should().BeTrue();
        }

        [Test]
        public void GetHashCode_should_be_same_for_equal_instances()
        {
            new NewLineFragment().GetHashCode().Should().Be(new NewLineFragment().GetHashCode());
        }

        private LogEvent @event;
    }
}