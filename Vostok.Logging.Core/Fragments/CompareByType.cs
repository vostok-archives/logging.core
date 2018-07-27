namespace Vostok.Logging.Core.Fragments
{
    internal abstract class CompareByType<T>
    {
        public override bool Equals(object obj) => obj is T;

        public override int GetHashCode() => typeof(T).GetHashCode();
    }
}