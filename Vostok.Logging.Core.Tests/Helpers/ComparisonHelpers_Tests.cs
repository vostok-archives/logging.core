using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Logging.Core.Helpers;

namespace Vostok.Logging.Core.Tests.Helpers
{
    [TestFixture]
    public class ComparisonHelpers_Tests
    {
        [Test]
        public void Should_return_0_if_collection_is_null()
        {
            List<int> list = null;
            list.ElementwiseHash().Should().Be(0);
        }

        [Test]
        public void Should_return_0_if_collection_is_empty()
        {
            var list = new List<int>();
            list.ElementwiseHash().Should().Be(0);
        }

        [Test]
        public void Should_return_hash_of_all_elements()
        {
            var list = new List<int> {1, 2, 3};
            list.ElementwiseHash().Should().NotBe(0).And.NotBe(1.GetHashCode() + 2.GetHashCode() + 3.GetHashCode());
        }

        [Test]
        public void Should_not_be_equal_if_one_of_collections_is_null()
        {
            var list1 = new List<int>();
            List<int> list2 = null;
            list1.ElementwiseEquals(list2).Should().BeFalse();
            list2.ElementwiseEquals(list1).Should().BeFalse();
        }

        [Test]
        public void Should_not_be_equal_if_has_different_sizes()
        {
            var list1 = new List<int> {1};
            var list2 = new List<int> {1, 2};

            list1.ElementwiseEquals(list2).Should().BeFalse();
            list2.ElementwiseEquals(list1).Should().BeFalse();
        }

        [Test]
        public void Should_be_equal_by_the_same_reference()
        {
            var list1 = new List<int>();
            var list2 = list1;

            list1.ElementwiseEquals(list2).Should().BeTrue();
            list2.ElementwiseEquals(list1).Should().BeTrue();
        }

        [Test]
        public void Should_be_equal_with_same_elements()
        {
            var list1 = new List<int>{1, 2, 3};
            var list2 = new List<int>{1, 2, 3};

            list1.ElementwiseEquals(list2).Should().BeTrue();
            list2.ElementwiseEquals(list1).Should().BeTrue();
        }

        [Test]
        public void Should_not_be_equal_with_same_elements_but_different_order()
        {
            var list1 = new List<int>{1, 2, 3};
            var list2 = new List<int>{3, 2, 1};

            list1.ElementwiseEquals(list2).Should().BeFalse();
            list2.ElementwiseEquals(list1).Should().BeFalse();
        }
    }
}