using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ChallengeCalculator.Core
{
    public class Parser
    {
        private const int MaxValidNumber = 1000;

        public int[] Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Array.Empty<int>();

            string delimiter = ",|\\\\n"; // Default delimiters
            string numbersInput = input;

            // Check for custom delimiter format
            if (input.StartsWith("//"))
            {
                var parts = input.Split(new[] { "\\n" }, 2, StringSplitOptions.None);
                if (parts.Length == 2)
                {
                    delimiter = Regex.Escape(parts[0].Substring(2));
                    numbersInput = parts[1];
                }
            }

            // Split by delimiter(s)
            var numbers = Regex.Split(numbersInput, delimiter)
                             .Where(s => !string.IsNullOrWhiteSpace(s))
                             .Select(part =>
                             {
                                 if (int.TryParse(part.Trim(), out int number))
                                 {
                                     return number > MaxValidNumber ? 0 : number;
                                 }
                                 return 0; // Invalid numbers are converted to 0
                             })
                             .ToArray();

            var negativeNumbers = numbers.Where(n => n < 0).ToList();
            if (negativeNumbers.Any())
            {
                throw new ArgumentException($"Input includes negative numbers: {string.Join(", ", negativeNumbers)}");
            }

            return numbers;
        }
    }
} 