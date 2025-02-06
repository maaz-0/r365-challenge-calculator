using NUnit.Framework;
using ChallengeCalculator.Core;
using System;

namespace ChallengeCalculator.Tests
{
    public class Requirement8_MultipleDelimitersTests
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
        public void Add_MultipleDelimiters_ReturnsSum()
        {
            var input = "//[*][!!][r9r]\\n11r9r22*hh*33!!44";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(110)); // 11 + 22 + 0 + 33 + 44
        }

        [Test]
        public void Add_MultipleDelimitersWithDifferentLengths_ReturnsSum()
        {
            var input = "//[*][%%%%%][#]\\n1*2%%%%%3#4";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(10));
        }

        [Test]
        public void Add_MultipleDelimitersWithSpecialChars_ReturnsSum()
        {
            var input = "//[$$][##][@@]\\n1$$2##3@@4";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(10));
        }

        [Test]
        public void Add_MultipleDelimitersWithInvalidNumbers_ReturnsSum()
        {
            var input = "//[|][&]\\n1|abc&2|xyz&3";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Add_MultipleDelimitersWithLargeNumbers_ReturnsSum()
        {
            var input = "//[##][%%]\\n2##1001%%6";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(8)); // 2 + 0 + 6
        }

        [Test]
        public void Add_MultipleDelimitersWithNegativeNumbers_ThrowsException()
        {
            var input = "//[+][=]\\n1+-2=3";
            var ex = Assert.Throws<ArgumentException>(() => _parser.Parse(input));
            Assert.That(ex.Message, Is.EqualTo("Input includes negative numbers: -2"));
        }

        [Test]
        public void Add_AllPreviousFormatsStillWork()
        {
            // Original format
            Assert.That(_calculator.Add(_parser.Parse("1,2\\n3")), Is.EqualTo(6));
            
            // Single character custom delimiter
            Assert.That(_calculator.Add(_parser.Parse("//#\\n1#2#3")), Is.EqualTo(6));
            
            // Single delimiter in brackets
            Assert.That(_calculator.Add(_parser.Parse("//[***]\\n1***2***3")), Is.EqualTo(6));
        }

        [Test]
        public void Add_MultipleDelimitersWithRepeatedDelimiters_ReturnsSum()
        {
            var input = "//[+][+][++]\\n1+2++3+4";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(10));
        }

        [Test]
        public void Add_MultipleDelimitersWithEmptyDelimiter_TreatsAsNormalInput()
        {
            var input = "//[][#][]\\n1#2#3";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Add_MultipleDelimitersWithNewlines_ReturnsSum()
        {
            var input = "//[*][#]\\n1*2\\n3#4\\n5";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(15)); // 1 + 2 + 3 + 4 + 5
        }

        [Test]
        public void Add_MultipleDelimitersWithConsecutiveNewlines_ReturnsSum()
        {
            var input = "//[**][##]\\n1**2\\n\\n3##4";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(10)); // 1 + 2 + 0 + 3 + 4
        }

        [Test]
        public void Add_MultipleDelimitersWithNewlinesAndInvalidNumbers_ReturnsSum()
        {
            var input = "//[@@][&&]\\n1@@abc\\n2&&xyz\\n3";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(6)); // 1 + 0 + 2 + 0 + 3
        }

        [Test]
        public void Add_ComplexMixOfDelimitersAndNewlines_ReturnsSum()
        {
            var input = "//[***][#@#]\\n1***2\\n3#@#4\\n5***6\\n7#@#8";
            var numbers = _parser.Parse(input);
            var result = _calculator.Add(numbers);
            Assert.That(result, Is.EqualTo(36)); // 1 + 2 + 3 + 4 + 5 + 6 + 7 + 8
        }
    }
} 