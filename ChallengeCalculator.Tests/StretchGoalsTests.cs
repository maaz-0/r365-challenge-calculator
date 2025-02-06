using NUnit.Framework;
using ChallengeCalculator.Core;
using System;

namespace ChallengeCalculator.Tests
{
    public class StretchGoalsTests
    {
        private Calculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [Test]
        public void Add_AlternateDelimiterPipe_ReturnsSum()
        {
            var parser = new Parser("|");
            var input = "1|2,3|4";
            var numbers = parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(10)); // 1 + 2 + 3 + 4
        }

        [Test]
        public void Add_AlternateDelimiterSemicolon_WithInvalidNumbers()
        {
            var parser = new Parser(";");
            var input = "1;abc;2,3;xyz;4";
            var numbers = parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(10)); // 1 + 0 + 2 + 3 + 0 + 4
        }

        [Test]
        public void Add_AlternateDelimiterHash_WithCustomDelimiter()
        {
            var parser = new Parser("#");
            var input = "//@\\n1@2#3#4";
            var numbers = parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(10)); // 1 + 2 + 3 + 4
        }

        [Test]
        public void Add_AlternateDelimiterDollar_WithLargeNumbers()
        {
            var parser = new Parser("$");
            var input = "2$1001$6,1500$4";
            var numbers = parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(12)); // 2 + 0 + 6 + 0 + 4
        }

        [Test]
        public void Add_AlternateDelimiterAsterisk_WithMultipleDelimiters()
        {
            var parser = new Parser("*");
            var input = "//[##][%%]\\n1##2*3%%4*5";
            var numbers = parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(15)); // 1 + 2 + 3 + 4 + 5
        }

        [Test]
        public void Add_AlternateDelimiterCaret_WithNegativeNumbers()
        {
            var parser = new Parser("^");
            var input = "1^-2^3";
            var ex = Assert.Throws<ArgumentException>(() => parser.Parse(input));
            Assert.That(ex.Message, Is.EqualTo("Input includes negative numbers: -2"));
        }

        [Test]
        public void Add_AlternateDelimiterTilde_WithEmptyValues()
        {
            var parser = new Parser("~");
            var input = "1~2~~3,4~~5";
            var numbers = parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(15)); // 1 + 2 + 0 + 3 + 4 + 0 + 5
        }

        [Test]
        public void Add_DefaultDelimiter_StillWorks()
        {
            var parser = new Parser();
            var input = "1\\n2,3\\n4";
            var numbers = parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(10)); // 1 + 2 + 3 + 4
        }

        [Test]
        public void Add_AlternateDelimiterWithCustomDelimiterFormat_WorksTogether()
        {
            var parser = new Parser("$");
            var input = "//[***]\\n1***2$3***4$5";
            var numbers = parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(15)); // 1 + 2 + 3 + 4 + 5
        }

        [Test]
        public void Add_AllowNegativeNumbers_ReturnsSum()
        {
            var parser = new Parser("\\n", true); // Allow negative numbers
            var input = "1,-2,3,-4,5";
            var numbers = parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(3)); // 1 + (-2) + 3 + (-4) + 5
        }

        [Test]
        public void Add_AllowNegativeNumbers_WithCustomDelimiter()
        {
            var parser = new Parser("\\n", true);
            var input = "//[***]\\n1***-2***3***-4***5";
            var numbers = parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(3)); // 1 + (-2) + 3 + (-4) + 5
        }

        [Test]
        public void Add_AllowNegativeNumbers_WithAlternateDelimiter()
        {
            var parser = new Parser("|", true);
            var input = "1|-2|3|-4|5";
            var numbers = parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(3)); // 1 + (-2) + 3 + (-4) + 5
        }

        [Test]
        public void Add_AllowNegativeNumbers_WithMixedDelimiters()
        {
            var parser = new Parser("$", true);
            var input = "//[***]\\n1***-2$3***-4$5";
            var numbers = parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(3)); // 1 + (-2) + 3 + (-4) + 5
        }

        [Test]
        public void Add_DenyNegativeNumbers_DefaultBehavior()
        {
            var parser = new Parser(); // Default behavior: deny negative numbers
            var input = "1,-2,3";
            var ex = Assert.Throws<ArgumentException>(() => parser.Parse(input));
            Assert.That(ex.Message, Is.EqualTo("Input includes negative numbers: -2"));
        }
    }
} 