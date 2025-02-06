using NUnit.Framework;
using ChallengeCalculator.Core;
using System;

namespace ChallengeCalculator.Tests
{
    public class CalculatorFacadeTests
    {
        private ICalculatorFacade _facade;

        [SetUp]
        public void Setup()
        {
            _facade = new CalculatorFacade(new Calculator());
        }

        [Test]
        public void Calculate_Addition_ReturnsCorrectResult()
        {
            var result = _facade.Calculate("1,2,3", MathOperation.Add);
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Calculate_Subtraction_ReturnsCorrectResult()
        {
            var result = _facade.Calculate("10,3", MathOperation.Subtract);
            Assert.That(result, Is.EqualTo(7));
        }

        [Test]
        public void Calculate_Multiplication_ReturnsCorrectResult()
        {
            var result = _facade.Calculate("2,3,4", MathOperation.Multiply);
            Assert.That(result, Is.EqualTo(24));
        }

        [Test]
        public void Calculate_Division_ReturnsCorrectResult()
        {
            var result = _facade.Calculate("10,2", MathOperation.Divide);
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void Calculate_WithCustomDelimiter_ReturnsCorrectResult()
        {
            _facade.Configure(alternateDelimiter: "#");
            var result = _facade.Calculate("//#\\n1#2#3", MathOperation.Add);
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Calculate_WithNegativeNumbersAllowed_ReturnsCorrectResult()
        {
            _facade.Configure(allowNegativeNumbers: true);
            var result = _facade.Calculate("1,-2,3,-4", MathOperation.Add);
            Assert.That(result, Is.EqualTo(-2));
        }

        [Test]
        public void Calculate_WithCustomUpperBound_ReturnsCorrectResult()
        {
            _facade.Configure(maxValidNumber: 500);
            var result = _facade.Calculate("1,501,2,499", MathOperation.Add);
            Assert.That(result, Is.EqualTo(502)); // 1 + 0 + 2 + 499
        }

        [Test]
        public void Calculate_WithInvalidInput_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _facade.Calculate("1,2,3", MathOperation.Subtract));
        }

        [Test]
        public void Calculate_WithNullCalculator_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CalculatorFacade(null));
        }

        [Test]
        public void Calculate_WithComplexDelimiters_ReturnsCorrectResult()
        {
            var result = _facade.Calculate("//[***][!!]\\n1***2!!3", MathOperation.Add);
            Assert.That(result, Is.EqualTo(6));
        }
    }
} 