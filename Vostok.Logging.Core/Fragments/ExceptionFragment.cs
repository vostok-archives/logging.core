using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.Fragments
{
    internal class ExceptionFragment : CompareByType<ExceptionFragment>, IConversionPatternFragment
    {
        protected const string Text = "%e";

        public static ExceptionFragment TryParse(string value, ref int offset) =>
            FragmentHelpers.TryParse<ExceptionFragment>(Text, value, ref offset);

        public void Render(LogEvent @event, TextWriter writer) =>
            writer.Write(@event.Exception);

        public override string ToString() => Text;
    }
}