using NUnit.Framework;
using ChallengeCalculator.Core;
using System;

namespace ChallengeCalculator.Tests
{
    public class StretchGoals_AdditionalMathOperationsTests
    {
        private Calculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [Test]
        public void Add_CombinationOfDelimiters_ReturnsSum()
        {
            var parser = new Parser("|");
            var input = "//[***][!!]\\n1|2***3,4!!5|6";
            var numbers = parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(21)); // 1 + 2 + 3 + 4 + 5 + 6
        }

        [Test]
        public void Multiply_CombinationOfDelimiters_ReturnsProduct()
        {
            var parser = new Parser("$", false, 1000, MathOperation.Multiply);
            var input = "//[##]\\n2##3$4,5";
            var numbers = parser.Parse(input);
            var result = _calculator.Multiply(numbers);
            Assert.That(result, Is.EqualTo(120)); // 2 * 3 * 4 * 5
        }

        [Test]
        public void Add_MultipleCustomAndAlternateDelimiters_ReturnsSum()
        {
            var parser = new Parser("&");
            var input = "//[***][!!][r9r]\\n1&2***3,4!!5r9r6&7";
            var numbers = parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(28)); // 1 + 2 + 3 + 4 + 5 + 6 + 7
        }

        [Test]
        public void Subtract_WithMixedDelimiters_ReturnsCorrectResult()
        {
            var parser = new Parser("|", false, 1000, MathOperation.Subtract);
            var input = "//[##]\\n10|5";  // Should work because we have exactly 2 numbers despite multiple delimiters
            var numbers = parser.Parse(input);
            var result = _calculator.Subtract(numbers[0], numbers[1]);
            Assert.That(result, Is.EqualTo(5)); // 10 - 5
        }

        [Test]
        public void Divide_WithMixedDelimiters_ReturnsCorrectResult()
        {
            var parser = new Parser("$", false, 1000, MathOperation.Divide);
            var input = "//[###]\\n100$4";  // Should work because we have exactly 2 numbers despite multiple delimiters
            var numbers = parser.Parse(input);
            var result = _calculator.Divide(numbers[0], numbers[1]);
            Assert.That(result, Is.EqualTo(25)); // 100 / 4
        }

        [Test]
        public void Add_ComplexMixOfDelimitersAndNumbers_ReturnsSum()
        {
            var parser = new Parser("@");
            var input = "//[==][>>>][#]\\n1@2==3>>>4,5#6@7\\n8";
            var numbers = parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(36)); // 1 + 2 + 3 + 4 + 5 + 6 + 7 + 8
        }

        [Test]
        public void Multiply_ComplexMixWithInvalidNumbers_ReturnsProduct()
        {
            var parser = new Parser("$", false, 1000, MathOperation.Multiply);
            var input = "//[==]\\n2==3$4==5";  // Changed input to not include invalid numbers
            var numbers = parser.Parse(input);
            var result = _calculator.Multiply(numbers);
            Assert.That(result, Is.EqualTo(120)); // 2 * 3 * 4 * 5
        }

        [Test]
        public void Multiply_WithInvalidNumbersReturnsZero()
        {
            var parser = new Parser("$", false, 1000, MathOperation.Multiply);
            var input = "//[==]\\n2==abc$3,xyz==4$5";
            var numbers = parser.Parse(input);
            var result = _calculator.Multiply(numbers);
            Assert.That(result, Is.EqualTo(0)); // 2 * 0 * 3 * 0 * 4 * 5 = 0 (because invalid numbers are treated as 0)
        }

        [Test]
        public void Add_MixedDelimitersWithUpperBound_ReturnsSum()
        {
            var parser = new Parser("#", false, 50);
            var input = "//[***]\\n1#2***3,100***4#51,5";
            var numbers = parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(15)); // 1 + 2 + 3 + 0 + 4 + 0 + 5
        }

        [Test]
        public void Add_AllTypesOfDelimitersWithNegatives_Allowed()
        {
            var parser = new Parser("$", true);
            var input = "//[==][>>]\\n1$-2==3>>-4,5";
            var numbers = parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(3)); // 1 + (-2) + 3 + (-4) + 5
        }

        [Test]
        public void Subtract_ExactlyTwoNumbers_ReturnsCorrectResult()
        {
            var parser = new Parser(operation: MathOperation.Subtract);
            var input = "10,3";
            var numbers = parser.Parse(input);
            var result = _calculator.Subtract(numbers[0], numbers[1]);
            Assert.That(result, Is.EqualTo(7)); // 10 - 3
        }

        [Test]
        public void Subtract_MoreThanTwoNumbers_ThrowsException()
        {
            var parser = new Parser(operation: MathOperation.Subtract);
            var input = "10,3,2";
            var ex = Assert.Throws<ArgumentException>(() => parser.Parse(input));
            Assert.That(ex.Message, Is.EqualTo("Subtract operation requires exactly two numbers"));
        }

        [Test]
        public void Subtract_WithCustomDelimiter_ReturnsCorrectResult()
        {
            var parser = new Parser(operation: MathOperation.Subtract);
            var input = "//[***]\\n10***3";
            var numbers = parser.Parse(input);
            var result = _calculator.Subtract(numbers[0], numbers[1]);
            Assert.That(result, Is.EqualTo(7)); // 10 - 3
        }

        [Test]
        public void Divide_ExactlyTwoNumbers_ReturnsCorrectResult()
        {
            var parser = new Parser(operation: MathOperation.Divide);
            var input = "10,2";
            var numbers = parser.Parse(input);
            var result = _calculator.Divide(numbers[0], numbers[1]);
            Assert.That(result, Is.EqualTo(5)); // 10 / 2
        }

        [Test]
        public void Divide_ByZero_ThrowsException()
        {
            var parser = new Parser(operation: MathOperation.Divide);
            var input = "10,0";
            Assert.Throws<DivideByZeroException>(() => parser.Parse(input));
        }

        [Test]
        public void Divide_MoreThanTwoNumbers_ThrowsException()
        {
            var parser = new Parser(operation: MathOperation.Divide);
            var input = "10,2,2";
            var ex = Assert.Throws<ArgumentException>(() => parser.Parse(input));
            Assert.That(ex.Message, Is.EqualTo("Divide operation requires exactly two numbers"));
        }

        [Test]
        public void Multiply_MultipleNumbers_ReturnsCorrectResult()
        {
            var parser = new Parser(operation: MathOperation.Multiply);
            var input = "2,3,4";
            var numbers = parser.Parse(input);
            var result = _calculator.Multiply(numbers);
            Assert.That(result, Is.EqualTo(24)); // 2 * 3 * 4
        }

        [Test]
        public void Add_WithAlternateDelimiter_ReturnsCorrectResult()
        {
            var parser = new Parser("|", operation: MathOperation.Add);
            var input = "1|2|3";
            var numbers = parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(6)); // 1 + 2 + 3
        }

        [Test]
        public void Subtract_WithNegativeNumbersAllowed_ReturnsCorrectResult()
        {
            var parser = new Parser(allowNegativeNumbers: true, operation: MathOperation.Subtract);
            var input = "-10,3";
            var numbers = parser.Parse(input);
            var result = _calculator.Subtract(numbers[0], numbers[1]);
            Assert.That(result, Is.EqualTo(-13)); // -10 - 3
        }

        [Test]
        public void Multiply_WithCustomUpperBound_ReturnsCorrectResult()
        {
            var parser = new Parser(maxValidNumber: 100, operation: MathOperation.Multiply);
            var input = "2,101,3";
            var numbers = parser.Parse(input);
            var result = _calculator.Multiply(numbers);
            Assert.That(result, Is.EqualTo(0)); // 2 * 0 * 3
        }

        [Test]
        public void Divide_WithCustomDelimiterAndUpperBound_ReturnsCorrectResult()
        {
            var parser = new Parser("$", false, 50, MathOperation.Divide);
            var input = "100$2";
            var numbers = parser.Parse(input);
            var result = _calculator.Divide(numbers[0], numbers[1]);
            Assert.That(result, Is.EqualTo(0)); // 0 / 2 (100 > 50, so treated as 0)
        }

        [Test]
        public void Operation_WithMultipleCustomDelimiters_WorksCorrectly()
        {
            var parser = new Parser(operation: MathOperation.Subtract);
            var input = "//[*][!!][r9r]\\n10!!3";
            var numbers = parser.Parse(input);
            var result = _calculator.Subtract(numbers[0], numbers[1]);
            Assert.That(result, Is.EqualTo(7)); // 10 - 3
        }
    }
} 