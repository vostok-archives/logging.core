using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.Fragments
{
    internal class LevelFragment : CompareByType<LevelFragment>, IConversionPatternFragment
    {
        private const string Text = "%l";

        public static LevelFragment TryParse(string value, ref int offset) =>
            FragmentHelpers.TryParse<LevelFragment>(Text, value, ref offset);

        public void Render(LogEvent @event, TextWriter writer) =>
            writer.Write(@event.Level);

        public override string ToString() => Text;
    }
}