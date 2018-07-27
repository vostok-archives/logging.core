using System;
using System.Globalization;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Core.Fragments;

namespace Vostok.Logging.Core.Tests.Fragments
{
    [TestFixture]
    public class PropertiesFragment_Tests
    {
        [SetUp]
        public void SetUp()
        {
            @event = new LogEvent(LogLevel.Info, DateTimeOffset.UtcNow, "All is good.")
                .WithProperty("prop_int", 123)
                .WithProperty("prop_dbl", 1.23)
                .WithProperty("prop_str", "string");
        }

        [Test]
        public void TryParse_should_parse_fragment()
        {
            var offset = 1;
            PropertiesFragment.TryParse(" %p", ref offset).Should().Be(new PropertiesFragment());
        }

        [Test]
        public void TryParse_should_move_offset_accordingly()
        {
            var offset = 1;
            PropertiesFragment.TryParse(" %p", ref offset);

            offset.Should().Be(3);
        }

        [TestCase(" %")]
        [TestCase(" %d")]
        [TestCase(" p")]
        public void TryParse_should_return_null_on_invalid_input(string input)
        {
            var offset = 1;
            PropertiesFragment.TryParse(input, ref offset).Should().BeNull();
        }

        [Test]
        public void TryParse_should_not_move_offset_on_invalid_input()
        {
            var offset = 1;
            PropertiesFragment.TryParse(" %d", ref offset);

            offset.Should().Be(1);
        }

        [Test]
        public void TryParse_should_be_case_insensitive()
        {
            var offset = 1;
            PropertiesFragment.TryParse(" %P", ref offset).Should().NotBeNull();
        }

        [Test]
        public void HasValue_should_return_true()
        {
            new PropertiesFragment().HasValue(@event).Should().BeTrue();
        }

        [Test]
        public void Render_should_render_Exception()
        {
            var writer = new StringWriter();
            new PropertiesFragment().Render(@event, writer);

            writer.ToString().Should().Be(
                "[properties: " +
                string.Join(", ", 
                    @event.Properties.Select(p => $"{p.Key} = {(p.Value is double dbl ? dbl.ToString(CultureInfo.InvariantCulture) : p.Value.ToString())}")) +
                "]");
        }

        [Test]
        public void Equals_should_consider_fragments_equal()
        {
            new PropertiesFragment().Equals(new PropertiesFragment()).Should().BeTrue();
        }

        [Test]
        public void GetHashCode_should_be_same_for_equal_instances()
        {
            new PropertiesFragment().GetHashCode().Should().Be(new PropertiesFragment().GetHashCode());
        }

        private LogEvent @event;
    }
}