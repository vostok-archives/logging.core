using System.IO;
using Vostok.Logging.Abstractions;

namespace Vostok.Logging.Core.Fragments
{
    internal class TextFragment : IConversionPatternFragment
    {
        private readonly string text;

        public TextFragment(string text) => this.text = text;

        public void Render(LogEvent @event, TextWriter writer) => writer.Write(text);

        public override string ToString() => text;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is TextFragment other)
                return Equals(other);

            return false;
        }

        public override int GetHashCode() => text.GetHashCode();

        protected bool Equals(TextFragment other) => other != null && text == other.text;
    }
}