using NUnit.Framework;
using ChallengeCalculator.Core;
using System;

namespace ChallengeCalculator.Tests
{
    public class Requirement1Tests
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
            Assert.That(_calculator.Add(""), Is.EqualTo(0));
        }

        [Test]
        public void Add_SingleNumber_ReturnsThatNumber()
        {
            Assert.That(_calculator.Add("20"), Is.EqualTo(20));
        }

        [Test]
        public void Add_TwoNumbers_ReturnsSum()
        {
            Assert.That(_calculator.Add("1,5000"), Is.EqualTo(5001));
        }

        [Test]
        public void Add_NegativeNumbers_ReturnsSum()
        {
            Assert.That(_calculator.Add("4,-3"), Is.EqualTo(1));
        }

        [Test]
        public void Add_InvalidNumber_TreatsAsZero()
        {
            Assert.That(_calculator.Add("5,tytyt"), Is.EqualTo(5));
        }

        [Test]
        public void Add_MoreThanTwoNumbers_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => _calculator.Add("1,2,3"));
        }

        [Test]
        public void Add_SpacesAroundNumbers_HandlesCorrectly()
        {
            Assert.That(_calculator.Add(" 1 , 2 "), Is.EqualTo(3));
        }
    }
} 