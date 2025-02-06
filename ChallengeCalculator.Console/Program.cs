using ChallengeCalculator.Core;
using System;

namespace ChallengeCalculator.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var calculator = new Calculator();
            var parser = new Parser();

            while (true)
            {
                System.Console.WriteLine("\nEnter numbers separated by comma or '\\n'.");
                System.Console.WriteLine("Example: 1,2\\n3,4 (numbers > 1000 will be ignored)");
                var input = System.Console.ReadLine();

                if (input?.ToLower() == "exit")
                    break;

                try
                {
                    var numbers = parser.Parse(input ?? string.Empty);
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
    }
}
