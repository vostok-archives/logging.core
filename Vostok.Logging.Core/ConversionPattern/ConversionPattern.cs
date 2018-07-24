using System;
using System.Collections.Generic;
using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern
{
    public class ConversionPattern
    {
        internal readonly List<IConversionPatternFragment> Fragments;

        public ConversionPattern() => Fragments = new List<IConversionPatternFragment>();

        public override string ToString() => ConversionPatternParser.ToString(this);
    }

    // CR(iloktionov): Something like this (and the builder) should become the only public API related to ConversionPattern.
    public class ConversionPattern2
    {
        public void Render(LogEvent @event, TextWriter writer)
        {
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public static ConversionPatternBuilder Builder()
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(string value, out ConversionPattern pattern)
        {
            throw new NotImplementedException();
        }
    }
}