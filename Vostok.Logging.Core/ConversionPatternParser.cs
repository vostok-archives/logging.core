using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vostok.Logging.Core.Fragments;

namespace Vostok.Logging.Core
{
    // TODO(krait): Tests.
    internal static class ConversionPatternParser
    {
        private delegate IConversionPatternFragment FragmentParser(string value, ref int offset);

        private static readonly FragmentParser[] Parsers = {
            ExceptionFragment.TryParse,
            LevelFragment.TryParse,
            MessageFragment.TryParse,
            NewLineFragment.TryParse,
            DateTimeFragment.TryParse,
            PropertyFragment.TryParse,
            PropertiesFragment.TryParse
        };

        public static ConversionPattern Parse(string pattern)
        {
            if (pattern == null)
                return null;

            var fragments = new List<IConversionPatternFragment>();
            var plainText = new StringBuilder();

            void FlushPlainText()
            {
                if (plainText.Length > 0)
                {
                    fragments.Add(new TextFragment(plainText.ToString()));
                    plainText.Clear();
                }
            }

            var offset = 0;
            while (offset < pattern.Length)
            {
                if (pattern[offset] == '%')
                {
                    var fragment = Parsers.Select(p => p(pattern, ref offset)).FirstOrDefault(f => f != null);
                    if (fragment != null)
                    {
                        FlushPlainText();
                        fragments.Add(fragment);
                        continue;
                    }
                }

                plainText.Append(pattern[offset++]);
            }
            FlushPlainText();

            return new ConversionPattern(fragments);
        }
    }
}