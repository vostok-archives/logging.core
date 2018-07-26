using System;
using System.Collections.Generic;
using System.Linq;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.Helpers
{
    internal static class LogLevelFormatter
    {
        private static readonly Dictionary<LogLevel, string> RenderedLevels = RenderLogLevels();

        private static Dictionary<LogLevel, string> RenderLogLevels()
        {
            var maxNameLength = Enum.GetNames(typeof (LogLevel)).Max(n => n.Length);

            return Enum.GetValues(typeof (LogLevel))
                .Cast<LogLevel>()
                .Select(l => (key: l, value: l.ToString().ToUpperInvariant().PadRight(maxNameLength)))
                .ToDictionary(pair => pair.key, pair => pair.value);
        }

        public static string Format(LogLevel level) =>
            RenderedLevels.TryGetValue(level, out var value) ? value : level.ToString();
    }
}