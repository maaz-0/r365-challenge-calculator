using NUnit.Framework;
using ChallengeCalculator.Core;
using System;

namespace ChallengeCalculator.Tests
{
    public class Requirement4_DenyNegativeNumbersTests
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
        public void Add_SingleNegativeNumber_ThrowsException()
        {
            var input = "1,2,-3,4";
            var ex = Assert.Throws<ArgumentException>(() => _parser.Parse(input));
            Assert.That(ex.Message, Is.EqualTo("Input includes negative numbers: -3"));
        }

        [Test]
        public void Add_MultipleNegativeNumbers_ThrowsException()
        {
            var input = "1,-2,-3,4,-5";
            var ex = Assert.Throws<ArgumentException>(() => _parser.Parse(input));
            Assert.That(ex.Message, Is.EqualTo("Input includes negative numbers: -2, -3, -5"));
        }

        [Test]
        public void Add_NegativeNumbersWithNewlines_ThrowsException()
        {
            var input = "1\\n-2\\n3\\n-4";
            var ex = Assert.Throws<ArgumentException>(() => _parser.Parse(input));
            Assert.That(ex.Message, Is.EqualTo("Input includes negative numbers: -2, -4"));
        }

        [Test]
        public void Add_NegativeNumbersWithInvalidInput_ThrowsException()
        {
            var input = "-1,abc,-2\\nxyz,-3";
            var ex = Assert.Throws<ArgumentException>(() => _parser.Parse(input));
            Assert.That(ex.Message, Is.EqualTo("Input includes negative numbers: -1, -2, -3"));
        }

        [Test]
        public void Add_NoNegativeNumbers_Succeeds()
        {
            var input = "1,2,3\\n4,5";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(15));
        }

        [Test]
        public void Add_EmptyInput_Succeeds()
        {
            var input = "";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(0));
        }
    }
} 