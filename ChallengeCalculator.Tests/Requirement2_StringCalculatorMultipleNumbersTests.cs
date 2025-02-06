using NUnit.Framework;
using ChallengeCalculator.Core;
using System;

namespace ChallengeCalculator.Tests
{
    public class Requirement2_StringCalculatorMultipleNumbersTests
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
        public void Add_MultipleNumbers_ReturnsSum()
        {
            var input = "1,2,3,4,5,6,7,8,9,10,11,12";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(78));
        }

        [Test]
        public void Add_MixOfValidAndInvalidNumbers_ReturnsSum()
        {
            var input = "1,abc,2,def,3";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(6));
        }
    }
} 