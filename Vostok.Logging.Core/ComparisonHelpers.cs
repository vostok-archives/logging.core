using System.Collections.Generic;
using System.Linq;

namespace Vostok.Logging.Core
{
    internal static class ComparisonHelpers
    {
        public static int ElementwiseHash<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                return 0;

            return enumerable.Aggregate(0, (current, element) => unchecked ((current * 397) ^ element.GetHashCode()));
        }

        public static bool ElementwiseEquals<T>(this ICollection<T> collection, ICollection<T> other)
        {
            if (ReferenceEquals(collection, other))
                return true;

            if (collection == null || other == null || collection.Count != other.Count)
                return false;

            return collection.Zip(other, (item1, item2) => Equals(item1, item2)).All(e => e);
        }
    }
}