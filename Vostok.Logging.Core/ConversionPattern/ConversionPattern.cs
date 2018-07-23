using System.Collections.Generic;
using System.Text;

namespace Vostok.Logging.Core.ConversionPattern
{
    public class ConversionPattern
    {
        internal readonly List<IConversionPatternFragment> Fragments;

        public ConversionPattern()
        {
            Fragments = new List<IConversionPatternFragment>();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            Fragments.ForEach(f => sb.Append(f.ToString()));
            return sb.ToString();
        }
    }
}