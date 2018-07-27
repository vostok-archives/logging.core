using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Logging.Core.Fragments;

namespace Vostok.Logging.Core.Tests
{
    [TestFixture]
    public class ConversionPatternBuilder_Tests
    {
        [Test]
        public void AddDateTime_should_add_DateTimeFragment()
        {
            ConversionPattern.Create().AddDateTime().Build()
                .Should().Be(ConversionPattern.Parse("%d"));
        }

        [Test]
        public void AddDateTime_should_add_DateTimeFragment_with_format()
        {
            ConversionPattern.Create().AddDateTime("yyyy").Build()
                .Should().Be(ConversionPattern.Parse("%d(yyyy)"));
        }

        [Test]
        public void AddException_should_add_ExceptionFragment()
        {
            ConversionPattern.Create().AddException().Build()
                .Should().Be(ConversionPattern.Parse("%e"));
        }

        [Test]
        public void AddLevel_should_add_LevelFragment()
        {
            ConversionPattern.Create().AddLevel().Build()
                .Should().Be(ConversionPattern.Parse("%l"));
        }

        [Test]
        public void AddMessage_should_add_MessageFragment()
        {
            ConversionPattern.Create().AddMessage().Build()
                .Should().Be(ConversionPattern.Parse("%m"));
        }

        [Test]
        public void AddNewLine_should_add_NewLineFragment()
        {
            ConversionPattern.Create().AddNewLine().Build()
                .Should().Be(ConversionPattern.Parse("%n"));
        }

        [Test]
        public void AddProperties_should_add_PropertiesFragment()
        {
            ConversionPattern.Create().AddProperties().Build()
                .Should().Be(ConversionPattern.Parse("%p"));
        }

        [Test]
        public void AddProperty_should_add_PropertyFragment()
        {
            ConversionPattern.Create().AddProperty("name").Build()
                .Should().Be(ConversionPattern.Parse("%p(name)"));
        }

        [Test]
        public void AddProperty_should_add_PropertyFragment_with_format()
        {
            ConversionPattern.Create().AddProperty("name", "format").Build()
                .Should().Be(ConversionPattern.Parse("%p(name:format)"));
        }

        [Test]
        public void AddText_should_add_TextFragment()
        {
            ConversionPattern.Create().AddText("text").Build()
                .Should().Be(ConversionPattern.Parse("text"));
        }

        [Test]
        public void All_fragments_should_be_tested()
        {
            var allFragments = typeof(IConversionPatternFragment)
                .Assembly.DefinedTypes.ThatImplement<IConversionPatternFragment>()
                .ToList();

            var testMethods = GetType().GetMethods().Select(m => m.Name);

            var testedFragments = allFragments.Where(f => testMethods.Any(m => m.Contains(f.Name)));

            testedFragments.Should().Equal(allFragments);
        }
    }
}