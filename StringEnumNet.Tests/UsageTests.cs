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
                _ => throw new ArgumentOutOfRangeException(nameof(orientation), "Unknown orientation")
            };

            direction.Should().Be("Down");
        }

        public class Orientation : StringEnum<Orientation>
        {
            public static readonly Orientation North = Define("North orientation");
            public static readonly Orientation South = Define("South orientation");
            public static readonly Orientation East  = Define("East orientation");
            public static readonly Orientation West  = Define("West orientation");
        }
    }
}