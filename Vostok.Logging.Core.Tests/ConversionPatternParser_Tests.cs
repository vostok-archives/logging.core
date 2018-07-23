﻿using FluentAssertions;
using NUnit.Framework;
using Vostok.Logging.Core.ConversionPattern;

namespace Vostok.Logging.Core.Tests
{
    [TestFixture]
    public class ConversionPatternParser_Tests
    {
        [Test]
        public void Should_parse_date()
        {
            const string format = "%d";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be(format);

            new ConversionPatternBuilder()
                .AddDateTime()
                .ToPattern().ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_date_uppercase_with_property()
        {
            const string dateFormat = "yyyy-MM-dd";
            var format = $"%D({dateFormat})";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be($"%d({dateFormat})");

            new ConversionPatternBuilder()
                .AddDateTime(null, dateFormat)
                .ToPattern().ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_level()
        {
            var format = "%l";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be(format);

            new ConversionPatternBuilder()
                .AddLevel()
                .ToPattern().ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_message()
        {
            const string format = "%m";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be(format);

            new ConversionPatternBuilder()
                .AddMessage()
                .ToPattern().ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_exception()
        {
            const string format = "%e";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be(format);

            new ConversionPatternBuilder()
                .AddException()
                .ToPattern().ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_new_line()
        {
            const string format = "%n";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be(format);

            new ConversionPatternBuilder()
                .AddNewLine()
                .ToPattern().ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_prefix()
        {
            const string format = "%x";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be(format);

            new ConversionPatternBuilder()
                .AddPrefix()
                .ToPattern().ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_properties()
        {
            const string format = "%p";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be(format);

            new ConversionPatternBuilder()
                .AddProperties()
                .ToPattern().ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_property()
        {
            const string prop = "prop";
            var format = $"%p({prop})";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be(format);

            new ConversionPatternBuilder()
                .AddProperty(prop)
                .ToPattern().ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_string_start()
        {
            const string format = "start";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be(format);

            new ConversionPatternBuilder()
                .AddStringStart(format)
                .ToPattern().ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_multiple_values_pattern()
        {
            const string format = "start %D(yyyy-MM-dd) %l %x %p(prop) message: %M%N";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be("start %d(yyyy-MM-dd) %l %x %p(prop) message: %m%n");

            new ConversionPatternBuilder()
                .AddStringStart("start ")
                .AddDateTime(" ", "yyyy-MM-dd")
                .AddLevel(" ")
                .AddPrefix(" ")
                .AddProperty("prop", " message: ")
                .AddMessage()
                .AddNewLine()
                .ToPattern().ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_parse_all_values_with_suffixes_except_NewLine()
        {
            const string format = "X%dX%D(yyyy-MM-dd)X%lX%xX%mX%eX%pX%p(prop)X%NX";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be("X%dX%d(yyyy-MM-dd)X%lX%xX%mX%eX%pX%p(prop)X%n");

            new ConversionPatternBuilder()
                .AddStringStart("X")
                .AddDateTime("X")
                .AddDateTime("X", "yyyy-MM-dd")
                .AddLevel("X")
                .AddPrefix("X")
                .AddMessage("X")
                .AddException("X")
                .AddProperties("X")
                .AddProperty("prop", "X")
                .AddNewLine()
                .ToPattern().ToString().Should().Be(pattern.ToString());
        }

        [Test]
        public void Should_render_only_template_string_if_it_not_contains_any_keys()
        {
            const string format = "abc";
            var pattern = ConversionPatternParser.Parse(format);
            pattern.ToString().Should().Be("abc");
        }
    }
}