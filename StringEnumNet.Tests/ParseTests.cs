using System;
using FluentAssertions;
using NUnit.Framework;

namespace StringEnumNet.Tests
{
    [TestFixture]
    public class ParseTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void Parse_When_invalidStringValue_Should_throwException(string stringValue)
        {
            Action act = () => Wine.Parse(stringValue);

            act.Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        [TestCase("  White  ")]
        [TestCase("white")]
        [TestCase("Rose")]
        public void Parse_When_undefinedStringValue_Should_throwException(string stringValue)
        {
            Action act = () => Wine.Parse(stringValue);

            act.Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Parse_When_definedStringValue_Should_returnExpectedStringEnum()
        {
            var stringEnum = Wine.Parse("Red");

            stringEnum.Should().Be(Wine.Red);
        }
        
        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void TryParse_When_invalidStringValue_Should_throwException(string stringValue)
        {
            Action act = () => Wine.TryParse(stringValue, out _);

            act.Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        [TestCase("  White  ")]
        [TestCase("white")]
        [TestCase("Rose")]
        public void TryParse_When_undefinedStringValue_Should_returnFalse(string stringValue)
        {
            var isParsed = Wine.TryParse(stringValue, out var stringEnum);

            isParsed.Should().BeFalse();
            stringEnum.Should().BeNull();
        }
        
        [Test]
        public void TryParse_When_definedStringValue_Should_returnFalse()
        {
            var isParsed = Wine.TryParse("Sparkling", out var stringEnum);

            isParsed.Should().BeTrue();
            stringEnum.Should().Be(Wine.Sparkling);
        }
        
        public class Wine : StringEnum<Wine>
        {
            public static readonly Wine White = Define("White");
            public static readonly Wine Red = Define("Red");
            public static readonly Wine Sparkling = Define("Sparkling");
            public static readonly Wine Dessert = Define("Dessert");
        }
    }
}