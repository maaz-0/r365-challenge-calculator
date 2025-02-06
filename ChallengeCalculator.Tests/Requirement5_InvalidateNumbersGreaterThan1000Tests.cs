using NUnit.Framework;
using ChallengeCalculator.Core;
using System;

namespace ChallengeCalculator.Tests
{
    public class Requirement5_InvalidateNumbersGreaterThan1000Tests
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
        public void Add_NumberGreaterThan1000_TreatsAsZero()
        {
            var input = "2,1001,6";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(8)); // 2 + 0 + 6
        }

        [Test]
        public void Add_MultipleNumbersGreaterThan1000_TreatsAllAsZero()
        {
            var input = "2,1001,1002,6,2000,3";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(11)); // 2 + 0 + 0 + 6 + 0 + 3
        }

        [Test]
        public void Add_ExactlyOneThousand_IsValid()
        {
            var input = "2,1000,6";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(1008)); // 2 + 1000 + 6
        }

        [Test]
        public void Add_NumbersGreaterThan1000WithNewlines_TreatsAsZero()
        {
            var input = "2\\n1001\\n6,1500\\n4";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(12)); // 2 + 0 + 6 + 0 + 4
        }

        [Test]
        public void Add_MixOfValidInvalidAndLargeNumbers_HandlesCorrectly()
        {
            var input = "2,abc,1001,6,xyz,2000,3,1000";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(1011)); // 2 + 0 + 0 + 6 + 0 + 0 + 3 + 1000
        }

        [Test]
        public void Add_AllNumbersGreaterThan1000_ReturnsZero()
        {
            var input = "1001,2000,1500,5000";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(0)); // All numbers are treated as 0
        }
    }
} 