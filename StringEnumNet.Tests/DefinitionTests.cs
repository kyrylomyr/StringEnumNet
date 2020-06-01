using System;
using FluentAssertions;
using NUnit.Framework;

namespace StringEnumNet.Tests
{
    [TestFixture]
    public class DefinitionTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void Define_When_valueIsNullOrEmpty_Should_throwException(string value)
        {
            Action act = () => Orientation.DefineInternal(value);

            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Define_When_newStringValue_Should_defineAndReturnWithoutException()
        {
            const string stringValue = "Down Orientation";

            Orientation stringEnum = null;
            Func<Orientation> func = () => stringEnum = Orientation.DefineInternal(stringValue);

            func.Should().NotThrow();
            stringEnum.ToString().Should().Be(stringValue);
        }

        [Test]
        public void Define_When_alreadyDefinedStringValue_Should_throwException()
        {
            Action act = () => Orientation.DefineInternal("South");

            act.Should().NotThrow();
            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void IsDefine_When_invalidStringValue_Should_throwException(string stringValue)
        {
            Action act = () => Orientation.IsDefined(stringValue);

            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        [TestCase("North orientation", true)]
        [TestCase("  North orientation  ", false)]
        [TestCase("north orientation", false)]
        [TestCase("NORTH ORIENTATION", false)]
        [TestCase("West orientation", false)]
        public void IsDefine_When_validStringValue_Should_returnExpectedResult(string stringValue, bool expectedResult)
        {
            var isDefined = Orientation.IsDefined(stringValue);

            isDefined.Should().Be(expectedResult);
        }

        public class Orientation : StringEnum<Orientation>
        {
            public static readonly Orientation North = Define("North orientation");
        }
    }
}