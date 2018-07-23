﻿using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.ConversionPattern.Patterns
{
    internal class ExceptionPattern : IConversionPatternFragment
    {
        public ExceptionPattern(string suffix = null)
        {
            Suffix = suffix ?? string.Empty;
        }

        public string Suffix { get; }

        string IConversionPatternFragment.Property => null;

        public void Render(LogEvent @event, TextWriter writer)
        {
            if (@event.Exception != null)
                writer.Write(@event.Exception + Suffix);
        }
    }
}