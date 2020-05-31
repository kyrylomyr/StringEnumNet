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

        public class Orientation : StringEnum<Orientation>
        {
        }
    }
}