using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Core.Fragments;

namespace Vostok.Logging.Core.Tests
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
            
            var fragments = new List<IConversionPatternFragment>
            {
                new DateTimeFragment(null),
            };
            ConversionPatternRenderer.Render(logEvent, writer, fragments);
            writer.Flush();

            writer.ToString().Should().Be(dt.ToString("yyyy-MM-dd HH:mm:ss,fff"));
        }

        [Test]
        public void Should_render_date_with_format()
        {
            var writer = new StringWriter();

            var dt = DateTime.Now;
            var logEvent = new LogEvent(LogLevel.Info, dt, "some message");
            var fragments = new List<IConversionPatternFragment>
            {
                new DateTimeFragment("yyyy-MM-dd"),
            };
            ConversionPatternRenderer.Render(logEvent, writer, fragments);
            writer.Flush();

            writer.ToString().Should().Be(dt.ToString("yyyy-MM-dd"));
        }

        [Test]
        public void Should_render_level_with_suffix()
        {
            var writer = new StringWriter();

            const string suffix = "suffix";
            var logEvent = DefaultEvent();
            var fragments = new List<IConversionPatternFragment>
            {
                new LevelFragment(),
                new TextFragment(suffix),
            };
            ConversionPatternRenderer.Render(logEvent, writer, fragments);
            writer.Flush();

            writer.ToString().Should().Be(LogLevel.Info.ToString().ToUpper()+" "+suffix);
        }

        [Test]
        public void Should_render_message()
        {
            var writer = new StringWriter();

            const string message = "some message";
            var logEvent = new LogEvent(LogLevel.Info, DateTime.Now, message);
            var fragments = new List<IConversionPatternFragment>
            {
                new MessageFragment(),
            };
            ConversionPatternRenderer.Render(logEvent, writer, fragments);
            writer.Flush();

            writer.ToString().Should().Be(message);
        }

        [Test]
        public void Should_render_exception()
        {
            var writer = new StringWriter();

            var exception = new Exception("test");
            var logEvent = new LogEvent(LogLevel.Info, DateTime.Now, "some message", exception);
            var fragments = new List<IConversionPatternFragment>
            {
                new  ExceptionFragment(),
            };
            ConversionPatternRenderer.Render(logEvent, writer, fragments);
            writer.Flush();

            writer.ToString().Should().Be(exception.ToString());
        }

        [Test]
        public void Should_render_new_line()
        {
            var writer = new StringWriter();

            var logEvent = DefaultEvent();
            var fragments = new List<IConversionPatternFragment>
            {
                new NewLineFragment(),
            };
            ConversionPatternRenderer.Render(logEvent, writer, fragments);
            writer.Flush();

            writer.ToString().Should().Be(Environment.NewLine);
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
            var fragments = new List<IConversionPatternFragment>
            {
                new PropertiesFragment(),
            };
            ConversionPatternRenderer.Render(logEvent, writer, fragments);
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
            var fragments = new List<IConversionPatternFragment>
            {
                new PropertyFragment(prop, null),
            };
            ConversionPatternRenderer.Render(logEvent, writer, fragments);
            writer.Flush();

            writer.ToString().Should().Be($"{value}");
        }

        [Test]
        public void Should_render_property_with_suffix()
        {
            var writer = new StringWriter();

            const string prop = "prop";
            const double value = 1.23456;
            const string format = "0.00";
            var logEvent = DefaultEvent()
                .WithProperty(prop, value);
            var fragments = new List<IConversionPatternFragment>
            {
                new PropertyFragment(prop, format),
            };
            ConversionPatternRenderer.Render(logEvent, writer, fragments);
            writer.Flush();

            writer.ToString().Should().Be($"{value.ToString(format, CultureInfo.InvariantCulture)}");
        }

        [Test]
        public void Should_render_string_start()
        {
            var writer = new StringWriter();

            const string text = "string start";
            var logEvent = DefaultEvent();
            var fragments = new List<IConversionPatternFragment>
            {
                new TextFragment(text),
            };
            ConversionPatternRenderer.Render(logEvent, writer, fragments);
            writer.Flush();

            writer.ToString().Should().Be(text);
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
            var fragments = new List<IConversionPatternFragment>
            {
                new TextFragment(start),
                new DateTimeFragment(dtFormat),
                new TextFragment(" "),
                new LevelFragment(),
                new PropertyFragment(prop, null),
                new TextFragment(" message: "),
                new MessageFragment(),
                new NewLineFragment(),
            };
            ConversionPatternRenderer.Render(logEvent, writer, fragments);
            writer.Flush();

            writer.ToString().Should().Be($"{start}{dt.ToString(dtFormat)} {LogLevel.Info.ToString().ToUpper()} {propValue} message: {message}{Environment.NewLine}");
        }

        [Test]
        public void Should_not_render_substrings_after_keys_if_event_has_not_such_properties()
        {
            var writer = new StringWriter();

            var dt = DateTime.Now;
            const LogLevel level = LogLevel.Info;
            var logEvent = new LogEvent(LogLevel.Info, dt, null);

            var fragments = new List<IConversionPatternFragment>
            {
                new TextFragment("a"),
                new DateTimeFragment(null),
                new TextFragment("a"),
                new LevelFragment(),
                new TextFragment("a"),
                new TextFragment("a"),
                new MessageFragment(),
                new TextFragment("a1"),
                new ExceptionFragment(),
                new TextFragment("a2"),
                new PropertiesFragment(),
                new TextFragment("a3"),
                new PropertyFragment("prop", null),
                new TextFragment("a4"),
                new NewLineFragment(),
            };
            var template = string.Format("a{0:yyyy-MM-dd HH:mm:ss,fff}a{1} aa\r\n", dt, level.ToString().ToUpper());
            ConversionPatternRenderer.Render(logEvent, writer, fragments);
            writer.Flush();

            writer.ToString().Should().Be(template);
        }
    }
}