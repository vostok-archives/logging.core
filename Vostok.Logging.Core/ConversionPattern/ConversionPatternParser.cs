using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Vostok.Logging.Core.ConversionPattern
{
    internal class ConversionPatternParser : IConversionPatternParser
    {
        private static readonly Dictionary<PatternPartType, (string pattern, string startsFrom)> PatternKeys;
        private static readonly string RegexPattern;

        static ConversionPatternParser()
        {
            PatternKeys = new Dictionary<PatternPartType, (string, string)>
            {
                {PatternPartType.DateTime, (@"d(?:\(([^)]*)\))?", "d")},
                {PatternPartType.Level, ("l", "l")},
                {PatternPartType.Prefix, ("x", "x")},
                {PatternPartType.Message, ("m", "m")},
                {PatternPartType.Exception, ("e", "e")},
                {PatternPartType.Property, (@"p\((\w*)\)", "p(")},
                {PatternPartType.Properties, ("p", "p")},
                {PatternPartType.NewLine, ("n", "n")},
            };

            var anyKeyRegex = string.Join("|", PatternKeys.Values.Select(v => v.pattern));
            RegexPattern = $"(?:%(?:{anyKeyRegex})|^)(?<suffix>[^%]*)";
        }

        public ConversionPattern Parse(string pattern)
        {
            var result = new ConversionPattern();
            if (string.IsNullOrEmpty(pattern))
                return result;

            var matches = Regex.Matches(pattern, RegexPattern, RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                var (type, prop, suffix) = ParseMatch(match, match.Index);
                result.AddFragment(type, prop, suffix);
            }

            return result;
        }

        private static (PatternPartType type, string property, string suffix) ParseMatch(Match match, int matchOffset)
        {
            var type = PatternPartType.StringStart;
            var value = match.Value;
            foreach (var pair in PatternKeys)
            {
                var start = "%" + pair.Value.startsFrom;
                if (value.StartsWith(start, StringComparison.InvariantCultureIgnoreCase))
                {
                    type = pair.Key;
                    break;
                }
            }

            var suffix = match.Groups["suffix"].Value;
            var suffixIndex = match.Groups["suffix"].Index - matchOffset;

            var i = 0;
            var property = string.Empty;
            if (suffixIndex > 0)
                foreach (Group gr in match.Groups)
                    if (!string.IsNullOrEmpty(gr.Value) && i++ > 0 && i != suffixIndex)
                    {
                        property = gr.Value;
                        break;
                    }
            
            return (type, property, suffix);
        }
    }
}