using System.Collections.Generic;
using System.IO;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Core.Fragments;

namespace Vostok.Logging.Core
{
    internal static class ConversionPatternRenderer
    {
        public static void Render(LogEvent @event, TextWriter writer, IList<IConversionPatternFragment> fragments)
        {
            var skipNextText = false;

            foreach (var f in fragments)
            {
                if (skipNextText && (f is TextFragment || f is NewLineFragment))
                {
                    skipNextText = false;
                    continue;
                }

                if (f.HasValue(@event))
                    f.Render(@event, writer);
                else
                    skipNextText = true;
            }
        }
    }
}