using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ChallengeCalculator.Core
{
    public class Parser
    {
        private const int MaxValidNumber = 1000;
        private readonly string _alternateDelimiter;
        private readonly bool _allowNegativeNumbers;

        public Parser(string alternateDelimiter = "\\n", bool allowNegativeNumbers = false)
        {
            _alternateDelimiter = alternateDelimiter;
            _allowNegativeNumbers = allowNegativeNumbers;
        }

        public int[] Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Array.Empty<int>();

            var processedInput = ProcessAlternateDelimiter(input);
            var (delimiter, numbersInput) = ExtractDelimiterAndNumbers(processedInput);
            var numbers = ParseNumbers(numbersInput, delimiter);
            
            if (!_allowNegativeNumbers)
            {
                ValidateNumbers(numbers);
            }

            return numbers;
        }

        private string ProcessAlternateDelimiter(string input)
        {
            if (_alternateDelimiter != "\\n")
            {
                return input?.Replace(_alternateDelimiter, "\\n");
            }
            return input;
        }

        private (string delimiter, string numbersInput) ExtractDelimiterAndNumbers(string input)
        {
            string delimiter = ",|\\\\n";
            string numbersInput = input;

            if (!input.StartsWith("//"))
                return (delimiter, numbersInput);

            if (input.StartsWith("//[") && input.Contains("]\\n"))
            {
                return ExtractBracketDelimiters(input);
            }
            else
            {
                return ExtractSingleCharacterDelimiter(input);
            }
        }

        private (string delimiter, string numbersInput) ExtractBracketDelimiters(string input)
        {
            var endOfDelimiters = input.IndexOf("\\n");
            if (endOfDelimiters <= 0)
                return (",|\\\\n", input);

            var delimitersSection = input.Substring(2, endOfDelimiters - 2);
            var numbersInput = input.Substring(endOfDelimiters + 2);

            if (delimitersSection.Count(c => c == '[') > 1)
            {
                return (ExtractMultipleDelimiters(delimitersSection), numbersInput);
            }
            else
            {
                return (ExtractSingleBracketDelimiter(delimitersSection), numbersInput);
            }
        }

        private string ExtractMultipleDelimiters(string delimitersSection)
        {
            var delimiters = new List<string>();
            var currentDelimiter = "";
            var insideDelimiter = false;

            foreach (var c in delimitersSection)
            {
                if (c == '[')
                {
                    insideDelimiter = true;
                    currentDelimiter = "";
                }
                else if (c == ']')
                {
                    insideDelimiter = false;
                    if (!string.IsNullOrEmpty(currentDelimiter))
                    {
                        delimiters.Add(Regex.Escape(currentDelimiter));
                    }
                }
                else if (insideDelimiter)
                {
                    currentDelimiter += c;
                }
            }

            return string.Join("|", delimiters) + "|\\\\n";
        }

        private string ExtractSingleBracketDelimiter(string delimitersSection)
        {
            var match = Regex.Match(delimitersSection, @"\[(.*?)\]");
            return match.Success ? Regex.Escape(match.Groups[1].Value) + "|\\\\n" : ",|\\\\n";
        }

        private (string delimiter, string numbersInput) ExtractSingleCharacterDelimiter(string input)
        {
            var parts = input.Split(new[] { "\\n" }, 2, StringSplitOptions.None);
            if (parts.Length == 2)
            {
                return (Regex.Escape(parts[0].Substring(2)) + "|\\\\n", parts[1]);
            }
            return (",|\\\\n", input);
        }

        private int[] ParseNumbers(string numbersInput, string delimiter)
        {
            return Regex.Split(numbersInput, delimiter)
                       .Where(s => !string.IsNullOrWhiteSpace(s))
                       .Select(ParseNumber)
                       .ToArray();
        }

        private int ParseNumber(string part)
        {
            if (int.TryParse(part.Trim(), out int number))
            {
                return number > MaxValidNumber ? 0 : number;
            }
            return 0;
        }

        private void ValidateNumbers(int[] numbers)
        {
            var negativeNumbers = numbers.Where(n => n < 0).ToList();
            if (negativeNumbers.Any())
            {
                throw new ArgumentException($"Input includes negative numbers: {string.Join(", ", negativeNumbers)}");
            }
        }
    }
} 