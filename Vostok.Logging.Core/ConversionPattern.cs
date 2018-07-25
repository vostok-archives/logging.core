using System.Collections.Generic;
using System.IO;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Core.Fragments;

namespace Vostok.Logging.Core
{
    // TODO(krait): xml doc
    public class ConversionPattern
    {
        // TODO(krait): CreateDefault
        public static ConversionPatternBuilder Create() => new ConversionPatternBuilder();

        public static ConversionPattern Parse(string value) =>
            ConversionPatternParser.Parse(value);

        private readonly IList<IConversionPatternFragment> fragments;

        internal ConversionPattern(IList<IConversionPatternFragment> fragments) =>
            this.fragments = fragments;

        public void Render(LogEvent @event, TextWriter writer)
        {
            foreach (var f in fragments)
                f.Render(@event, writer);
        }

        /// <inheritdoc />
        public override string ToString() => string.Concat(fragments);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is ConversionPattern other)
                return Equals(other);

            return false;
        }

        public override int GetHashCode() => fragments.ElementwiseHash();

        protected bool Equals(ConversionPattern other) => other != null && fragments.ElementwiseEquals(other.fragments);
    }
}