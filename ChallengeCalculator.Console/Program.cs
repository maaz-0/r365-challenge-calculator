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
                System.Console.WriteLine("\nEnter numbers separated by comma, '\\n', or use a custom delimiter:");
                System.Console.WriteLine("Examples: 1,2\\n3,4");
                System.Console.WriteLine("         //#\\n2#5 (using # as delimiter)");
                System.Console.WriteLine("(numbers > 1000 will be ignored, type 'exit' to quit)");
                var input = System.Console.ReadLine();

                if (input?.ToLower() == "exit")
                    break;

                try
                {
                    var numbers = parser.Parse(input ?? string.Empty);
                    var result = calculator.Add(numbers);
                    var currentColor = System.Console.ForegroundColor;
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine($"Result: {result}");
                    System.Console.ForegroundColor = currentColor;
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
