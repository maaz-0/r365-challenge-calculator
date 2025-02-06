using NUnit.Framework;
using ChallengeCalculator.Core;
using System;

namespace ChallengeCalculator.Tests
{
    public class Requirement6_CustomDelimiterTests
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
        public void Add_CustomDelimiterHash_ReturnsSum()
        {
            var input = "//#\\n2#5";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(7));
        }

        [Test]
        public void Add_CustomDelimiterComma_ReturnsSum()
        {
            var input = "//,\\n2,ff,100";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(102));
        }

        [Test]
        public void Add_CustomDelimiterSemicolon_WithInvalidNumbers()
        {
            var input = "//;\\n1;invalid;2;3";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Add_CustomDelimiterDot_WithLargeNumbers()
        {
            var input = "//.\\n2.1001.6";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(8)); // 2 + 0 + 6
        }

        [Test]
        public void Add_CustomDelimiterAt_WithNegativeNumber_ThrowsException()
        {
            var input = "//@\\n1@-2@3";
            var ex = Assert.Throws<ArgumentException>(() => _parser.Parse(input));
            Assert.That(ex.Message, Is.EqualTo("Input includes negative numbers: -2"));
        }

        [Test]
        public void Add_CustomDelimiterPipe_WithMultipleNumbers()
        {
            var input = "//|\\n1|2|3|4|5";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(15));
        }

        [Test]
        public void Add_OriginalDelimitersStillWork()
        {
            var input = "1,2\\n3,4";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(10));
        }

        [Test]
        public void Add_EmptyCustomDelimiter_TreatsAsNormalInput()
        {
            var input = "//\\n1,2,3";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(6));
        }
    }
} 