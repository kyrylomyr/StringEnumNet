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

        [Test]
        public void GetHashCode_When_differentStringEnumMembers_Should_haveDifferentHashCode()
        {
            var redWineHash = Wine.Red.GetHashCode();
            var sparklingWineHash = Wine.Sparkling.GetHashCode();

            redWineHash.Should().NotBe(sparklingWineHash);
        }

        [Test]
        public void GetHashCode_When_sameStringEnumMembers_Should_haveSameHashCode()
        {
            var hash1 = Wine.Red.GetHashCode();
            var hash2 = Wine.Red.GetHashCode();

            hash1.Should().Be(hash2);
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