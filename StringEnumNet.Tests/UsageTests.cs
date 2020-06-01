using System;
using FluentAssertions;
using NUnit.Framework;

namespace StringEnumNet.Tests
{
    [TestFixture]
    public class UsageTests
    {
        [Test]
        public void SwitchExpression()
        {
            var orientation = Orientation.South;

            var direction = orientation switch
            {
                _ when orientation == Orientation.North => "Up",
                _ when orientation == Orientation.South => "Down",
                _ when orientation == Orientation.East  => "Left",
                _ when orientation == Orientation.West  => "Right",
                _ => throw new ArgumentOutOfRangeException(nameof(orientation), "Unknown")
            };

            direction.Should().Be("Down");
        }

        [Test]
        public void Equals_When_sameEnumMemberAsObjects_Should_beEqual()
        {
            object stringEnum1 = Orientation.North;
            object stringEnum2 = Orientation.North;

            var areEqual = stringEnum1.Equals(stringEnum2);

            areEqual.Should().BeTrue();
        }
        
        [Test]
        public void Equals_When_differentEnumMembersAsObjects_Should_beNotEqual()
        {
            object stringEnum1 = Orientation.North;
            object stringEnum2 = Orientation.West;

            var areEqual = stringEnum1.Equals(stringEnum2);

            areEqual.Should().BeFalse();
        }
        
        [Test]
        public void Equals_When_enumMemberAsObjectAndNull_Should_beNotEqual()
        {
            object stringEnum1 = Orientation.North;

            var areEqual = stringEnum1.Equals(null);

            areEqual.Should().BeFalse();
        }
        
        [Test]
        public void Equals_When_membersOfDifferentEnumsAsObjects_Should_beNotEqual()
        {
            object stringEnum1 = Orientation.North;
            object stringEnum2 = Pole.South;

            var areEqual = stringEnum1.Equals(stringEnum2);

            areEqual.Should().BeFalse();
        }
        
        [Test]
        public void Equals_When_membersOfDifferentEnumsWithSameStringValuesAsObjects_Should_beNotEqual()
        {
            object stringEnum1 = Orientation.North;
            object stringEnum2 = Pole.North;

            var areEqual = stringEnum1.Equals(stringEnum2);

            areEqual.Should().BeFalse();
        }
        
        [Test]
        public void Equals_When_sameEnumMember_Should_beEqual()
        {
            Orientation stringEnum1 = Orientation.North;
            Orientation stringEnum2 = Orientation.North;

            var areEqual = stringEnum1.Equals(stringEnum2);

            areEqual.Should().BeTrue();
        }
        
        [Test]
        public void Equals_When_differentMembersOfSameEnum_Should_beNotEqual()
        {
            Orientation stringEnum1 = Orientation.North;
            Orientation stringEnum2 = Orientation.West;

            var areEqual = stringEnum1.Equals(stringEnum2);

            areEqual.Should().BeFalse();
        }
        
        [Test]
        public void Equals_When_differentMembersOfDifferentEnumsWithSameStringValue_Should_beNotEqual()
        {
            Orientation stringEnum1 = Orientation.North;
            Pole stringEnum2 = Pole.North;

            var areEqual = stringEnum1.Equals(stringEnum2);

            areEqual.Should().BeFalse();
        }

        public class Orientation : StringEnum<Orientation>
        {
            public static readonly Orientation North = Define("North");
            public static readonly Orientation South = Define("South");
            public static readonly Orientation East  = Define("East");
            public static readonly Orientation West  = Define("West");
        }
        
        public class Pole : StringEnum<Pole>
        {
            public static readonly Pole North = Define("North");
            public static readonly Pole South = Define("South");
        }
    }
}