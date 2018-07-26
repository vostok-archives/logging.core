using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Core.Fragments;

namespace Vostok.Logging.Core
{
    /// <summary>
    /// A builder for manual creation of a <see cref="ConversionPattern"/>.
    /// </summary>
    [PublicAPI]
    public sealed class ConversionPatternBuilder
    {
        private readonly List<IConversionPatternFragment> fragments = new List<IConversionPatternFragment>();

        /// <summary>
        /// Adds a <see cref="LogEvent.Timestamp"/> fragment to the pattern.
        /// </summary>
        /// <param name="format">An optional format string. If nothing is specified, the default format of <c>yyyy-MM-dd HH:mm:ss,fff</c> is used.</param>
        public ConversionPatternBuilder AddDateTime(string format = null)
        {
            fragments.Add(new DateTimeFragment(format));
            return this;
        }

        /// <summary>
        /// Adds a <see cref="LogEvent.Level"/> fragment to the pattern.
        /// </summary>
        public ConversionPatternBuilder AddLevel()
        {
            fragments.Add(new LevelFragment());
            return this;
        }

        /// <summary>
        /// <para>Adds a Message fragment to the pattern.</para>
        /// <para>Message here is the rendered log message. See <see cref="LogEvent.MessageTemplate"/> and <see cref="LogMessageFormatter"/> for details.</para>
        /// </summary>
        public ConversionPatternBuilder AddMessage()
        {
            fragments.Add(new MessageFragment());
            return this;
        }

        /// <summary>
        /// Adds an <see cref="LogEvent.Exception"/> fragment to the pattern.
        /// </summary>
        public ConversionPatternBuilder AddException()
        {
            fragments.Add(new ExceptionFragment());
            return this;
        }

        /// <summary>
        /// <para>Adds a <see cref="LogEvent.Properties"/> fragment to the pattern.</para>
        /// <para>This fragment will be substituted with a concatenation of all properties and their values.</para>
        /// </summary>
        public ConversionPatternBuilder AddProperties()
        {
            fragments.Add(new PropertiesFragment());
            return this;
        }

        /// <summary>
        /// <para>Adds a fragment referencing a single property from <see cref="LogEvent.Properties"/> to the pattern.</para>
        /// </summary>
        /// <param name="propertyName">The name of the property to reference.</param>
        /// <param name="format">An optional format string to be used when rendering the property value.</param>
        public ConversionPatternBuilder AddProperty(string propertyName, string format = null)
        {
            fragments.Add(new PropertyFragment(propertyName, format));
            return this;
        }

        /// <summary>
        /// Adds a line break fragment to the pattern.
        /// </summary>
        public ConversionPatternBuilder AddNewLine()
        {
            fragments.Add(new NewLineFragment());
            return this;
        }

        /// <summary>
        /// <para>Adds a plain text fragment to the pattern.</para>
        /// <para>The given text is transferred into the log output as-is.</para>
        /// </summary>
        public ConversionPatternBuilder AddText(string text)
        {
            fragments.Add(new TextFragment(text));
            return this;
        }

        /// <summary>
        /// Returns a <see cref="ConversionPattern"/> consisting of the specified fragments.
        /// </summary>
        public ConversionPattern Build() => new ConversionPattern(fragments);
    }
}