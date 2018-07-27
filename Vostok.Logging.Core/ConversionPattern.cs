using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Core.Fragments;
using Vostok.Logging.Core.Helpers;

namespace Vostok.Logging.Core
{
    /// <summary>
    /// A pattern of one log line.
    /// </summary>
    [PublicAPI]
    public sealed class ConversionPattern
    {
        /// <summary>
        /// <para>The default pattern: <c>%d %l %p(logging.prefix)%m%n%e%n</c></para>
        /// <para>See <see cref="Parse"/> for the meaning of these symbols.</para>
        /// </summary>
        public static readonly ConversionPattern Default = Parse("%d %l %p(logging.prefix) %m%n%e%n");

        /// <summary>
        /// Create a new <see cref="ConversionPattern"/> via a <see cref="ConversionPatternBuilder"/>.
        /// </summary>
        public static ConversionPatternBuilder Create() => new ConversionPatternBuilder();

        // TODO(krait): Add <description> tags inside <item>s everywhere in logging.core, logging.abstractions, configuration.abstractions. VisualStudio intellisense can't render items without this tag.
        /// <summary>
        /// <para>Create a new <see cref="ConversionPattern"/> from a string.</para>
        /// <list type="bullet">
        ///     <listheader>Allowed substitutions:</listheader>
        ///     <item><description>%d — insert <see cref="LogEvent.Timestamp"/> with the default format (see <see cref="ConversionPatternBuilder.AddDateTime"/>).</description></item>
        ///     <item>%d(<c>format</c>) — insert <see cref="LogEvent.Timestamp"/> with the format specified in braces).</item>
        ///     <item>%l — insert <see cref="LogEvent.Level"/>.</item>
        ///     <item>%m — insert rendered log message (see <see cref="ConversionPatternBuilder.AddMessage"/>).</item>
        ///     <item>%n — insert a line break.</item>
        ///     <item>%e — insert <see cref="LogEvent.Exception"/>.</item>
        ///     <item>%p — insert all <see cref="LogEvent.Properties"/>.</item>
        ///     <item>%p(<c>prop</c>) — insert the value of property <c>prop</c> from <see cref="LogEvent.Properties"/>.</item>
        ///     <item>%p(<c>prop</c>:<c>format</c>) — insert the value of property <c>prop</c> from <see cref="LogEvent.Properties"/> formatted with <c>format</c>.</item>
        /// </list>
        /// <para>All text between these substitutions will be printed as-is.</para>
        /// </summary>
        public static ConversionPattern Parse(string value) =>
            ConversionPatternParser.Parse(value);

        private readonly IList<IConversionPatternFragment> fragments;

        internal ConversionPattern(IList<IConversionPatternFragment> fragments) =>
            this.fragments = fragments;

        /// <summary>
        /// Render the given <paramref name="event"/> to the given <paramref name="writer"/> using this <see cref="ConversionPattern"/>.
        /// </summary>
        public void Render(LogEvent @event, TextWriter writer) =>
            ConversionPatternRenderer.Render(@event, writer, fragments);

        /// <inheritdoc />
        public override string ToString() => string.Concat(fragments);

        #region Equality

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is ConversionPattern other)
                return Equals(other);

            return false;
        }

        /// <inheritdoc />
        public override int GetHashCode() => fragments.ElementwiseHash();

        private bool Equals(ConversionPattern other) => other != null && fragments.ElementwiseEquals(other.fragments);

        #endregion
    }
}