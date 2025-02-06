using NUnit.Framework;
using ChallengeCalculator.Core;
using ChallengeCalculator.Console;
using System;

namespace ChallengeCalculator.Tests
{
    public class Requirement1_BasicCalculatorTests
    {
        private Calculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [Test]
        public void Add_EmptyString_Returns0()
        {
            var numbers = Program.ParseInput("");
            Assert.That(_calculator.Add(numbers), Is.EqualTo(0));
        }

        [Test]
        public void Add_SingleNumber_ReturnsThatNumber()
        {
            var numbers = Program.ParseInput("20");
            Assert.That(_calculator.Add(numbers), Is.EqualTo(20));
        }

        [Test]
        public void Add_TwoNumbers_ReturnsSum()
        {
            var numbers = Program.ParseInput("1,5000");
            Assert.That(_calculator.Add(numbers), Is.EqualTo(5001));
        }

        [Test]
        public void Add_NegativeNumbers_ThrowsException()
        {
            var input = "4,-3";
            var ex = Assert.Throws<ArgumentException>(() => Program.ParseInput(input));
            Assert.That(ex.Message, Is.EqualTo("Input includes negative numbers: -3"));
        }

        [Test]
        public void Add_InvalidNumber_TreatsAsZero()
        {
            var numbers = Program.ParseInput("5,tytyt");
            Assert.That(_calculator.Add(numbers), Is.EqualTo(5));
        }

        [Test]
        public void Add_SpacesAroundNumbers_HandlesCorrectly()
        {
            var numbers = Program.ParseInput(" 1 , 2 ");
            Assert.That(_calculator.Add(numbers), Is.EqualTo(3));
        }
    }
} 