using NUnit.Framework;
using ChallengeCalculator.Core;
using System;

namespace ChallengeCalculator.Tests
{
    public class Requirement7_CustomDelimiterAnyLengthTests
    {
        private Calculator _calculator;
        private Parser _parser;

        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator();
            _parser = new Parser();
        }

        [Test]
        public void Add_CustomDelimiterThreeStars_ReturnsSum()
        {
            var input = "//[***]\\n11***22***33";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(66));
        }

        [Test]
        public void Add_CustomDelimiterLongString_ReturnsSum()
        {
            var input = "//[delimiter]\\n1delimiter2delimiter3";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Add_CustomDelimiterSpecialChars_ReturnsSum()
        {
            var input = "//[***###@@@]\\n1***###@@@2***###@@@3";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Add_CustomDelimiterWithInvalidNumbers_ReturnsSum()
        {
            var input = "//[sep]\\n1sepabcsep2sepxyzsep3";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Add_CustomDelimiterWithLargeNumbers_ReturnsSum()
        {
            var input = "//[big]\\n2big1001big6";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(8)); // 2 + 0 + 6
        }

        [Test]
        public void Add_CustomDelimiterWithNegativeNumbers_ThrowsException()
        {
            var input = "//[neg]\\n1neg-2neg3";
            var ex = Assert.Throws<ArgumentException>(() => _parser.Parse(input));
            Assert.That(ex.Message, Is.EqualTo("Input includes negative numbers: -2"));
        }

        [Test]
        public void Add_PreviousFormatsStillWork()
        {
            // Original format
            Assert.That(_calculator.Add(_parser.Parse("1,2\\n3")), Is.EqualTo(6));
            
            // Single character custom delimiter
            Assert.That(_calculator.Add(_parser.Parse("//#\\n1#2#3")), Is.EqualTo(6));
        }

        [Test]
        public void Add_EmptyCustomDelimiter_TreatsAsNormalInput()
        {
            var input = "//[]\\n1,2,3";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Add_SingleCharacterInBrackets_ReturnsSum()
        {
            var input = "//[*]\\n1*2*3";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(6));
        }
    }
} 