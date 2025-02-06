using NUnit.Framework;
using ChallengeCalculator.Core;
using System;

namespace ChallengeCalculator.Tests
{
    public class Requirement1_BasicCalculatorTests
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
        public void Add_EmptyString_Returns0()
        {
            var numbers = _parser.Parse("");
            Assert.That(_calculator.Add(numbers), Is.EqualTo(0));
        }

        [Test]
        public void Add_SingleNumber_ReturnsThatNumber()
        {
            var numbers = _parser.Parse("20");
            Assert.That(_calculator.Add(numbers), Is.EqualTo(20));
        }

        [Test]
        public void Add_TwoNumbers_ReturnsSum()
        {
            var numbers = _parser.Parse("1,5000");
            Assert.That(_calculator.Add(numbers), Is.EqualTo(1)); // 1 + 0 (5000 > 1000 so treated as 0)
        }

        [Test]
        public void Add_NegativeNumbers_ThrowsException()
        {
            var input = "4,-3";
            var ex = Assert.Throws<ArgumentException>(() => _parser.Parse(input));
            Assert.That(ex.Message, Is.EqualTo("Input includes negative numbers: -3"));
        }

        [Test]
        public void Add_InvalidNumber_TreatsAsZero()
        {
            var numbers = _parser.Parse("5,tytyt");
            Assert.That(_calculator.Add(numbers), Is.EqualTo(5));
        }

        [Test]
        public void Add_SpacesAroundNumbers_HandlesCorrectly()
        {
            var numbers = _parser.Parse(" 1 , 2 ");
            Assert.That(_calculator.Add(numbers), Is.EqualTo(3));
        }
    }
} 