using NUnit.Framework;
using ChallengeCalculator.Core;
using System;

namespace ChallengeCalculator.Tests
{
    public class Requirement3_NewLineDelimiterTests
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
        public void Add_NewlineDelimiter_ReturnsCorrectSum()
        {
            var input = "1\\n2,3";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Add_MixedDelimiters_ReturnsCorrectSum()
        {
            var input = "1\\n2\\n3,4,5\\n6";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(21));
        }

        [Test]
        public void Add_ConsecutiveDelimiters_TreatsEmptyAsZero()
        {
            var input = "1,\\n2";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Add_OnlyNewlines_ReturnsCorrectSum()
        {
            var input = "1\\n2\\n3";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Add_NewlineWithInvalidNumbers_HandlesCorrectly()
        {
            var input = "1\\nabc\\n2";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Add_MultipleNumbers_ReturnsCorrectSum()
        {
            var input = "1\\n2\\n3\\n4\\n5\\n6\\n7\\n8\\n9\\n10\\n11\\n12";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(78));
        }

        [Test]
        public void Add_MixOfCommasAndNewlines_ReturnsCorrectSum()
        {
            var input = "1,2,3\\n4,5,6\\n7,8,9\\n10,11,12";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(78));
        }
    }
} 