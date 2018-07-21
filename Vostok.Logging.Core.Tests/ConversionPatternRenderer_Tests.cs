using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Core.ConversionPattern;

namespace Vostok.Logging.Core.Tests.New
{
    [TestFixture]
    public class ConversionPatternRenderer_Tests
    {
        private static LogEvent DefaultEvent() => new LogEvent(LogLevel.Info, DateTime.Now, "some message");

        [Test]
        public void Should_render_date()
        {
            var writer = new StringWriter();

            var dt = DateTime.Now;
            var logEvent = new LogEvent(LogLevel.Info, dt, "some message");
            var pattern = new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.DateTime);
            pattern.Render(logEvent, writer);
            writer.Flush();

            writer.ToString().Should().Be(dt.ToString("HH:mm:ss zzz"));
        }

        [Test]
        public void Should_render_date_with_format()
        {
            var writer = new StringWriter();

            var dt = DateTime.Now;
            var logEvent = new LogEvent(LogLevel.Info, dt, "some message");
            var pattern = new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.DateTime, "yyyy-MM-dd", null);
            pattern.Render(logEvent, writer);
            writer.Flush();

            writer.ToString().Should().Be(dt.ToString("yyyy-MM-dd"));
        }

        [Test]
        public void Should_render_level_with_suffix()
        {
            var writer = new StringWriter();

            const string suffix = "suffix";
            var logEvent = DefaultEvent();
            var pattern = new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.Level, suffix);
            pattern.Render(logEvent, writer);
            writer.Flush();

            writer.ToString().Should().Be(LogLevel.Info+suffix);
        }

        [Test]
        public void Should_render_message()
        {
            var writer = new StringWriter();

            const string message = "some message";
            var logEvent = new LogEvent(LogLevel.Info, DateTime.Now, message);
            var pattern = new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.Message);
            pattern.Render(logEvent, writer);
            writer.Flush();

            writer.ToString().Should().Be(message);
        }

        [Test]
        public void Should_render_exception()
        {
            var writer = new StringWriter();

            var exception = new Exception("test");
            var logEvent = new LogEvent(LogLevel.Info, DateTime.Now, "some message", exception);
            var pattern = new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.Exception);
            pattern.Render(logEvent, writer);
            writer.Flush();

            writer.ToString().Should().Be(exception.ToString());
        }

        [Test]
        public void Should_render_new_line()
        {
            var writer = new StringWriter();

            var logEvent = DefaultEvent();
            var pattern = new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.NewLine);
            pattern.Render(logEvent, writer);
            writer.Flush();

            writer.ToString().Should().Be(Environment.NewLine);
        }

        [Test]
        public void Should_render_prefix()
        {
            var writer = new StringWriter();

            const string first = "first";
            const string second = "second";
            var logEvent = DefaultEvent()
                .WithProperty("prefix", new[]{first, second});
            var pattern = new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.Prefix);
            pattern.Render(logEvent, writer);
            writer.Flush();

            writer.ToString().Should().Be($"[{first}] [{second}]");
        }

        [Test]
        public void Should_render_properties()
        {
            var writer = new StringWriter();

            const string first = "first";
            const string second = "second";
            var logEvent = DefaultEvent()
                .WithProperty(first, "value 1")
                .WithProperty(second, "value 2");
            var pattern = new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.Properties);
            pattern.Render(logEvent, writer);
            writer.Flush();

            writer.ToString().Should().Be($"[properties: {first} = value 1, {second} = value 2]");
        }

        [Test]
        public void Should_render_property()
        {
            var writer = new StringWriter();

            const string prop = "prop";
            const string value = "value";
            var logEvent = DefaultEvent()
                .WithProperty(prop, value);
            var pattern = new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.Property, prop, null);
            pattern.Render(logEvent, writer);
            writer.Flush();

            writer.ToString().Should().Be($"{value}");
        }

        [Test]
        public void Should_render_property_with_suffix()
        {
            var writer = new StringWriter();

            const string prop = "prop";
            const string value = "value";
            const string suffix = "SUFFIX";
            var logEvent = DefaultEvent()
                .WithProperty(prop, value);
            var pattern = new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.Property, prop, suffix);
            pattern.Render(logEvent, writer);
            writer.Flush();

            writer.ToString().Should().Be($"{value}{suffix}");
        }

        [Test]
        public void Should_render_string_start()
        {
            var writer = new StringWriter();

            const string suffix = "string start";
            var logEvent = DefaultEvent();
            var pattern = new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.StringStart, suffix);
            pattern.Render(logEvent, writer);
            writer.Flush();

            writer.ToString().Should().Be(suffix);
        }

        [Test]
        public void Should_render_multiple_values()
        {
            var writer = new StringWriter();

            var dt = DateTime.Now;
            const string message = "some message";
            const string start = "start ";
            const string dtFormat = "yyyy-MM-dd";
            const string prop = "prop";
            const string propValue = "value";
            var logEvent = new LogEvent(LogLevel.Info, dt, message)
                .WithProperty(prop, propValue);
            var pattern = new ConversionPattern.ConversionPattern()
                .AddFragment(PatternPartType.StringStart, start)
                .AddFragment(PatternPartType.DateTime, dtFormat, " ")
                .AddFragment(PatternPartType.Level, " ")
                .AddFragment(PatternPartType.Property, prop, " message: ")
                .AddFragment(PatternPartType.Message)
                .AddFragment(PatternPartType.NewLine);
            pattern.Render(logEvent, writer);
            writer.Flush();

            writer.ToString().Should().Be($"{start}{dt.ToString(dtFormat)} {LogLevel.Info} {propValue} message: {message}{Environment.NewLine}");
        }
    }
}