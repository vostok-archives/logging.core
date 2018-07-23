using System.Collections.Generic;
using Vostok.Logging.Core.ConversionPattern.Fragments;

namespace Vostok.Logging.Core.ConversionPattern
{
    // TODO(krait): xml doc
    public class ConversionPattern
    {
        internal readonly List<IConversionPatternFragment> Fragments;

        public ConversionPattern() => Fragments = new List<IConversionPatternFragment>();

        public override string ToString() => ConversionPatternParser.ToString(this);
    }
}