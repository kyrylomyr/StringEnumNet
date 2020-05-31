using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace StringEnumNet.Tests
{
    [TestFixture]
    public class UsageTests
    {
        public static IEnumerable<TestCaseData> EqualityOperatorBothStringEnumsTestCases
        {
            get
            {
                yield return new TestCaseData(null, null, true);
                yield return new TestCaseData(Orientation.North, null, false);
                yield return new TestCaseData(null, Orientation.South, false);
                yield return new TestCaseData(Orientation.East, Orientation.West, false);
                yield return new TestCaseData(Orientation.North, Orientation.North, true);
            }
        }

        [Test]
        [TestCaseSource(nameof(EqualityOperatorBothStringEnumsTestCases))]
        public void EqualityOperator_When_bothArgumentsAreStringEnums_Should_returnExpectedValue(
            Orientation orientation1, Orientation orientation2, bool expectedEquality)
        {
            var areEqual = orientation1 == orientation2;

            areEqual.Should().Be(expectedEquality);
        }
        
        public static IEnumerable<TestCaseData> EqualityOperatorStringEnumAndStringTestCases
        {
            get
            {
                yield return new TestCaseData(null, null, true);
                yield return new TestCaseData(Orientation.North, null, false);
                yield return new TestCaseData(null, "South orientation", false);
                yield return new TestCaseData(Orientation.East, "West orientation", false);
                yield return new TestCaseData(Orientation.North, "North orientation", true);
            }
        }

        [Test]
        [TestCaseSource(nameof(EqualityOperatorStringEnumAndStringTestCases))]
        public void EqualityOperator_When_stringEnumAndStringArguments_Should_returnExpectedValue(
            Orientation orientation, string stringValue, bool expectedEquality)
        {
            var areEqual = orientation == stringValue;

            areEqual.Should().Be(expectedEquality);
        }
        
        public static IEnumerable<TestCaseData> EqualityOperatorStringAndStringEnumTestCases
        {
            get
            {
                yield return new TestCaseData(null, null, true);
                yield return new TestCaseData("North orientation", null, false);
                yield return new TestCaseData(null, Orientation.South, false);
                yield return new TestCaseData("West orientation", Orientation.East, false);
                yield return new TestCaseData("North orientation", Orientation.North, true);
            }
        }

        [Test]
        [TestCaseSource(nameof(EqualityOperatorStringAndStringEnumTestCases))]
        public void EqualityOperator_When_stringEnumAndStringArguments_Should_returnExpectedValue(
            string stringValue, Orientation orientation, bool expectedEquality)
        {
            var areEqual = orientation == stringValue;

            areEqual.Should().Be(expectedEquality);
        }

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