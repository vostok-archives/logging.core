using FluentAssertions;
using NUnit.Framework;
using Vostok.Logging.Core.Fragments;

namespace Vostok.Logging.Core.Tests
{
    [TestFixture]
    internal class CompareByType_Tests
    {
        [Test]
        public void Equals_should_return_false_for_null_other()
        {
            var c1 = new TestClass();
            var c2 = null as TestClass;

            c1.Equals(c2).Should().BeFalse();
        }

        [Test]
        public void Equals_should_return_true_for_objects_of_same_type()
        {
            var c1 = new TestClass();
            var c2 = new TestClass();

            c1.Equals(c2).Should().BeTrue();
        }

        [Test]
        public void Equals_should_return_false_for_objects_of_different_types()
        {
            var c1 = new TestClass();
            var c2 = new object();

            c1.Equals(c2).Should().BeFalse();
            c2.Equals(c1).Should().BeFalse();
        }

        [Test]
        public void GetHashCode_should_return_zero()
        {
            var c1 = new TestClass();

            c1.GetHashCode().Should().Be(new TestClass().GetHashCode());
        }

        private class TestClass : CompareByType<TestClass>
        {
        }
    }
}