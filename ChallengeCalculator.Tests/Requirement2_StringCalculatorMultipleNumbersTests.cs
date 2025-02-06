using NUnit.Framework;
using ChallengeCalculator.Core;
using ChallengeCalculator.Console;
using System;

namespace ChallengeCalculator.Tests
{
    public class Requirement2_StringCalculatorMultipleNumbersTests
    {
        private Calculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [Test]
        public void Add_MultipleNumbers_ReturnsSum()
        {
            var numbers = Program.ParseInput("1,2,3,4,5,6,7,8,9,10,11,12");
            Assert.That(_calculator.Add(numbers), Is.EqualTo(78));
        }

        [Test]
        public void Add_MixOfValidAndInvalidNumbers_ReturnsSum()
        {
            var numbers = Program.ParseInput("1,abc,2,def,3");
            Assert.That(_calculator.Add(numbers), Is.EqualTo(6));
        }
    }
} 