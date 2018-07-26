using System.IO;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Core.Helpers;

namespace Vostok.Logging.Core.Fragments
{
    internal class NewLineFragment : CompareByType<NewLineFragment>, IConversionPatternFragment
    {
        private const string Text = "%n";

        public static NewLineFragment TryParse(string value, ref int offset) =>
            FragmentHelpers.TryParse<NewLineFragment>(Text, value, ref offset);

        public void Render(LogEvent @event, TextWriter writer) =>
            writer.WriteLine();

        public bool HasValue(LogEvent @event) => true;

        public override string ToString() => Text;
    }
}