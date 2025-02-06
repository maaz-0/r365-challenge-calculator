using ChallengeCalculator.Core;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ChallengeCalculator.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var calculator = new Calculator();

            while (true)
            {
                System.Console.WriteLine("\nEnter numbers separated by comma or '\\n'.");
                System.Console.WriteLine("Example: 1,2\\n3,4 (or type 'exit' to quit)");
                var input = System.Console.ReadLine();

                if (input?.ToLower() == "exit")
                    break;

                try
                {
                    var numbers = ParseInput(input ?? string.Empty);
                    var result = calculator.Add(numbers);
                    System.Console.WriteLine($"Result: {result}");
                }
                catch (ArgumentException ex)
                {
                    System.Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
        }

        public static int[] ParseInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Array.Empty<int>();

            // Split by comma or \n
            var numbers = Regex.Split(input, @",|\\n")
                             .Where(s => !string.IsNullOrWhiteSpace(s))
                             .Select(part =>
                             {
                                 int.TryParse(part.Trim(), out int number);
                                 return number; // Returns 0 if parsing fails
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
