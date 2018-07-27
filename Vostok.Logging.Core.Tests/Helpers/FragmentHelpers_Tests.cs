using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Core.Fragments;
using Vostok.Logging.Core.Helpers;
using Vostok.Logging.Core.Tests.Fragments;

namespace Vostok.Logging.Core.Tests.Helpers
{
    [TestFixture]
    public class FragmentHelpers_Tests
    {
        private static LogEvent LogEvent() => new LogEvent(LogLevel.Info, DateTimeOffset.Now, "msg");

        [Test]
        public void GetPropertyOrNull_shoult_return_null_if_event_has_no_properties()
        {
            var le = LogEvent();
            FragmentHelpers.GetPropertyOrNull(le, "name").Should().BeNull();
        }

        [Test]
        public void GetPropertyOrNull_shoult_return_null_if_given_wrong_property_name()
        {
            var le = LogEvent().WithProperty("name", 123);
            FragmentHelpers.GetPropertyOrNull(le, "wrong name").Should().BeNull();
        }

        [Test]
        public void GetPropertyOrNull_shoult_return_value()
        {
            var le = LogEvent().WithProperty("name", 123);
            FragmentHelpers.GetPropertyOrNull(le, "name").Should().Be(123);
        }

        [Test]
        public void TryWriteProperty_should_not_write_if_event_has_no_properties()
        {
            var writer = new StringWriter();
            FragmentHelpers.TryWriteProperty(null, null, writer);
            writer.ToString().Should().BeEmpty();
        }

        [Test]
        public void TryWriteProperty_should_write_property_without_format()
        {
            const string propName = "name";
            const int value = 123;

            var writer = new StringWriter();
            var le = LogEvent().WithProperty(propName, value);
            var prop = FragmentHelpers.GetPropertyOrNull(le, propName);

            FragmentHelpers.TryWriteProperty(prop, null, writer);
            writer.ToString().Should().Be(value.ToString());
        }

        [Test]
        public void TryWriteProperty_should_write_property_with_format()
        {
            const string propName = "name";
            const string format = "yyyy-MM-dd";
            var value = DateTime.Now;

            var writer = new StringWriter();
            var le = LogEvent().WithProperty(propName, value);
            var prop = FragmentHelpers.GetPropertyOrNull(le, propName);

            FragmentHelpers.TryWriteProperty(prop, format, writer);
            writer.ToString().Should().Be(value.ToString(format));
        }

        [Test]
        public void TryParse_should_parse_and_move_offset()
        {
            var offset = 3;
            FragmentHelpers.TryParse<LevelFragment>("%l", "%d %l", ref offset).Should().NotBeNull();
            offset.Should().Be(5);
        }

        [Test]
        public void TryParse_should_parse_and_move_offset_ignoring_case()
        {
            var offset = 3;
            FragmentHelpers.TryParse<LevelFragment>("%l", "%d %L", ref offset).Should().NotBeNull();
            offset.Should().Be(5);
        }

        [Test]
        public void TryParse_should_not_parse_if_wrong_fragment()
        {
            var offset = 3;
            FragmentHelpers.TryParse<LevelFragment>("%?", "%d %l", ref offset).Should().BeNull();
            offset.Should().Be(3);
        }

        [Test]
        public void TryParse_should_throw_exception_if_fragment_is_null()
        {
            var offset = 3;
            new Action(() => FragmentHelpers.TryParse<LevelFragment>(null, "%d %l", ref offset)).Should().Throw<Exception>();
            offset.Should().Be(3);
        }

        [Test]
        public void TryParse_should_not_parse_if_format_has_no_fragmet()
        {
            var offset = 3;
            FragmentHelpers.TryParse<LevelFragment>("%l", "%d %p", ref offset).Should().BeNull();
            offset.Should().Be(3);
        }

        [Test]
        public void TryParse_should_not_parse_if_format_is_null()
        {
            var offset = 3;
            FragmentHelpers.TryParse<LevelFragment>("%l", null, ref offset).Should().BeNull();
            offset.Should().Be(3);
        }

        [Test]
        public void TryParse_should_not_parse_if_wrong_offset_is_given()
        {
            var offset = 4;
            FragmentHelpers.TryParse<LevelFragment>("%l", "%d %l", ref offset).Should().BeNull();
            offset.Should().Be(4);
        }
    }
}