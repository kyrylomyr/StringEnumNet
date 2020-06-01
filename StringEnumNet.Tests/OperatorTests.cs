using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace StringEnumNet.Tests
{
    [TestFixture]
    public class OperatorTests
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
        public void EqualityOperator_When_bothArgumentsAreStringEnums_Should_beExpectedEquality(
            Orientation stringEnum1, Orientation stringEnum2, bool expectedEquality)
        {
            var areEqual = stringEnum1 == stringEnum2;

            areEqual.Should().Be(expectedEquality);
        }
        
        public static IEnumerable<TestCaseData> NotEqualityOperatorBothStringEnumsTestCases
        {
            get
            {
                yield return new TestCaseData(null, null, false);
                yield return new TestCaseData(Orientation.North, null, true);
                yield return new TestCaseData(null, Orientation.South, true);
                yield return new TestCaseData(Orientation.East, Orientation.West, true);
                yield return new TestCaseData(Orientation.North, Orientation.North, false);
            }
        }
        
        [Test]
        [TestCaseSource(nameof(NotEqualityOperatorBothStringEnumsTestCases))]
        public void NotEqualityOperator_When_bothArgumentsAreStringEnums_Should_beExpectedEquality(
            Orientation stringEnum1, Orientation stringEnum2, bool expectedEquality)
        {
            var areEqual = stringEnum1 != stringEnum2;

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
        public void EqualityOperator_When_stringEnumAndStringArguments_Should_beExpectedEquality(
            Orientation stringEnum, string stringValue, bool expectedEquality)
        {
            var areEqual = stringEnum == stringValue;

            areEqual.Should().Be(expectedEquality);
        }
        
        public static IEnumerable<TestCaseData> NotEqualityOperatorStringEnumAndStringTestCases
        {
            get
            {
                yield return new TestCaseData(null, null, false);
                yield return new TestCaseData(Orientation.North, null, true);
                yield return new TestCaseData(null, "South orientation", true);
                yield return new TestCaseData(Orientation.East, "West orientation", true);
                yield return new TestCaseData(Orientation.North, "North orientation", false);
            }
        }

        [Test]
        [TestCaseSource(nameof(NotEqualityOperatorStringEnumAndStringTestCases))]
        public void NotEqualityOperator_When_stringEnumAndStringArguments_Should_beExpectedEquality(
            Orientation stringEnum, string stringValue, bool expectedEquality)
        {
            var areEqual = stringEnum != stringValue;

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
        public void EqualityOperator_When_stringAndStringEnumArguments_Should_beExpectedEquality(
            string stringValue, Orientation stringEnum, bool expectedEquality)
        {
            var areEqual = stringValue == stringEnum;

            areEqual.Should().Be(expectedEquality);
        }
        
        public static IEnumerable<TestCaseData> NotEqualityOperatorStringAndStringEnumTestCases
        {
            get
            {
                yield return new TestCaseData(null, null, false);
                yield return new TestCaseData("North orientation", null, true);
                yield return new TestCaseData(null, Orientation.South, true);
                yield return new TestCaseData("West orientation", Orientation.East, true);
                yield return new TestCaseData("North orientation", Orientation.North, false);
            }
        }

        [Test]
        [TestCaseSource(nameof(NotEqualityOperatorStringAndStringEnumTestCases))]
        public void NotEqualityOperator_When_stringAndStringEnumArguments_Should_beExpectedEquality(
            string stringValue, Orientation stringEnum, bool expectedEquality)
        {
            var areEqual = stringValue != stringEnum;

            areEqual.Should().Be(expectedEquality);
        }

        public static IEnumerable<TestCaseData> ImplicitConversionTestCases
        {
            get
            {
                yield return new TestCaseData(Orientation.West, "West orientation");
                yield return new TestCaseData(null, null);
            }
        }
        
        [Test]
        [TestCaseSource(nameof(ImplicitConversionTestCases))]
        public void ImplicitConversion(Orientation stringEnum, string expectedStringValue)
        {
            string stringValue = stringEnum;

            stringValue.Should().Be(expectedStringValue);
        }

        public static IEnumerable<TestCaseData> ExplicitConversionValidStringValueTestCases
        {
            get
            {
                yield return new TestCaseData(null, null);
                yield return new TestCaseData(string.Empty, null);
                yield return new TestCaseData("West orientation", Orientation.West);
            }
        }
        
        [Test]
        [TestCaseSource(nameof(ExplicitConversionValidStringValueTestCases))]
        public void ExplicitConversion_When_validStringValue_Should_beExpectedEqualityStringEnum(
            string stringValue, Orientation expectedStringEnum)
        {
            Orientation stringEnum = (Orientation)stringValue;
            
            stringEnum.Should().Be(expectedStringEnum);
        }
        
        [Test]
        public void ExplicitConversion_When_invalidStringValue_Should_throwException()
        {
            Func<Orientation> func = () => (Orientation)"Invalid value";
            
            func.Should().Throw<InvalidCastException>();
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