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

            // Split by comma or \n
            var numbers = Regex.Split(input, @",|\\n")
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