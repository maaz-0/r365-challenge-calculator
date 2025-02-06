using System;

namespace ChallengeCalculator.Core
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            if (string.IsNullOrWhiteSpace(numbers))
                return 0;

            var parts = numbers.Split(',');
            var sum = 0;
            foreach (var part in parts)
            {
                if (int.TryParse(part.Trim(), out int number))
                {
                    sum += number;
                }
                // Invalid numbers are converted to 0, so we just continue
            }

            return sum;
        }
    }
} 