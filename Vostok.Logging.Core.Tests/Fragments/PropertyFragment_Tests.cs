using System;
using System.Globalization;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Core.Fragments;

namespace Vostok.Logging.Core.Tests.Fragments
{
    [TestFixture]
    public class PropertyFragment_Tests
    {
        [SetUp]
        public void SetUp()
        {
            @event = new LogEvent(LogLevel.Info, DateTimeOffset.UtcNow, "All is good.")
                .WithProperty(PropNameInt, 123)
                .WithProperty(PropNameDbl, 1.2345)
                .WithProperty(PropNameStr, "string");
        }

        [Test]
        public void TryParse_should_parse_fragment_without_format()
        {
            var offset = 1;
            PropertyFragment.TryParse($" %p({PropNameInt})", ref offset).Should().Be(new PropertyFragment(PropNameInt, null));
        }

        [Test]
        public void TryParse_should_parse_fragment_with_format()
        {
            var offset = 1;
            PropertyFragment.TryParse($" %p({PropNameDbl}:0.00)", ref offset).Should().Be(new PropertyFragment(PropNameDbl, "0.00"));
        }

        [Test]
        public void TryParse_should_move_offset_accordingly()
        {
            var offset = 1;
            PropertyFragment.TryParse($" %p({PropNameInt})", ref offset);

            offset.Should().Be(5 + PropNameInt.Length);
        }

        [TestCase(" %")]
        [TestCase(" %l")]
        [TestCase(" p")]
        public void TryParse_should_return_null_on_invalid_input(string input)
        {
            var offset = 1;
            PropertyFragment.TryParse(input, ref offset).Should().BeNull();
        }

        [Test]
        public void TryParse_should_not_move_offset_on_invalid_input()
        {
            var offset = 1;
            PropertyFragment.TryParse(" %l", ref offset);

            offset.Should().Be(1);
        }

        [TestCase(" %P(prop_int)")]
        [TestCase(" %P(prop_dbl:f2)")]
        public void TryParse_should_be_case_insensitive(string input)
        {
            var offset = 1;
            PropertyFragment.TryParse(input, ref offset).Should().NotBeNull();
        }

        [Test]
        public void HasValue_should_return_true()
        {
            new PropertyFragment(PropNameInt, null).HasValue(@event).Should().BeTrue();
            new PropertyFragment(PropNameDbl, "f2").HasValue(@event).Should().BeTrue();
            new PropertyFragment(PropNameStr, null).HasValue(@event).Should().BeTrue();
        }

        [Test]
        public void Render_should_render_Timestamp_with_default_format_if_format_is_null()
        {
            var writer = new StringWriter();
            new PropertyFragment(PropNameInt, null).Render(@event, writer);
            new PropertyFragment(PropNameStr, null).Render(@event, writer);

            writer.ToString().Should().Be(@event.Properties[PropNameInt].ToString()+@event.Properties[PropNameStr]);
        }

        [Test]
        public void Render_should_render_Timestamp_with_custom_format_if_it_is_set()
        {
            var writer = new StringWriter();
            new PropertyFragment(PropNameDbl, "0.00").Render(@event, writer);

            writer.ToString().Should().Be(((double)@event.Properties[PropNameDbl]).ToString("0.00", CultureInfo.InvariantCulture));
        }

        [Test]
        public void Equals_should_respect_format()
        {
            new PropertyFragment(PropNameDbl, "f2").Equals(new PropertyFragment(PropNameDbl, "0.000")).Should().BeFalse();
            new PropertyFragment(PropNameDbl, "f2").Equals(new PropertyFragment(PropNameDbl, "f2")).Should().BeTrue();
        }

        [Test]
        public void Equals_should_consider_fragments_without_format_equal()
        {
            new PropertyFragment(PropNameInt, null).Equals(new PropertyFragment(PropNameStr, null)).Should().BeFalse();
            new PropertyFragment(PropNameInt, null).Equals(new PropertyFragment(PropNameInt, null)).Should().BeTrue();
        }

        [Test]
        public void GetHashCode_should_depend_on_format()
        {
            var h1 = new PropertyFragment(PropNameDbl, null).GetHashCode();
            var h2 = new PropertyFragment(PropNameDbl, "f2").GetHashCode();

            h1.Should().NotBe(h2);
        }

        [Test]
        public void GetHashCode_should_be_same_for_equal_instances()
        {
            new PropertyFragment(PropNameDbl, "f2").GetHashCode().Should().Be(new PropertyFragment(PropNameDbl, "f2").GetHashCode());
            new PropertyFragment(PropNameDbl, null).GetHashCode().Should().Be(new PropertyFragment(PropNameDbl, null).GetHashCode());
        }

        private LogEvent @event;
        private const string PropNameInt = "prop_int";
        private const string PropNameDbl = "prop_dbl";
        private const string PropNameStr = "prop_str";
    }
}