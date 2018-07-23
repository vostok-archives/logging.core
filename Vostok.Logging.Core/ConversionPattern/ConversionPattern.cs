using System.Collections.Generic;

namespace Vostok.Logging.Core.ConversionPattern
{
    public class ConversionPattern
    {
        internal readonly List<IConversionPatternFragment> Fragments;

        public ConversionPattern() => Fragments = new List<IConversionPatternFragment>();

        public override string ToString() => ConversionPatternParser.ToString(this);
    }
}