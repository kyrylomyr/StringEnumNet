using FluentAssertions;
using NUnit.Framework;

namespace StringEnumNet.Tests
{
    [TestFixture]
    public class HashCodeTests
    {
        [Test]
        public void GetHashCode_When_differentStringEnumsHaveTheEqualStringValue_Should_haveDifferentHashCode()
        {
            var wineHash = Wine.Red.GetHashCode();
            var colorHash = Color.Red.GetHashCode();

            wineHash.Should().NotBe(colorHash);
        }
        
        public class Wine : StringEnum<Wine>
        {
            public static readonly Wine White = Define("White");
            public static readonly Wine Red = Define("Red");
            public static readonly Wine Sparkling = Define("Sparkling");
            public static readonly Wine Dessert = Define("Dessert");
        }
        
        public class Color : StringEnum<Color>
        {
            public static readonly Color White = Define("White");
            public static readonly Color Red = Define("Red");
            public static readonly Color Black = Define("Black");
            public static readonly Color Blue = Define("Blue");
        }
    }
}