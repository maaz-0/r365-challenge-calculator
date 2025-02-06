using ChallengeCalculator.Core;
using System;
using System.Linq;

namespace ChallengeCalculator.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Process command line arguments
            var alternateDelimiter = "\\n";
            var allowNegativeNumbers = false;
            var maxValidNumber = 1000;

            foreach (var arg in args)
            {
                if (arg.StartsWith("--alt-delimiter="))
                {
                    alternateDelimiter = arg.Substring("--alt-delimiter=".Length);
                    System.Console.WriteLine($"Using alternate delimiter: '{alternateDelimiter}'");
                }
                else if (arg == "--allow-negative")
                {
                    allowNegativeNumbers = true;
                    System.Console.WriteLine("Negative numbers are allowed");
                }
                else if (arg.StartsWith("--upper-bound="))
                {
                    if (int.TryParse(arg.Substring("--upper-bound=".Length), out int bound))
                    {
                        maxValidNumber = bound;
                        System.Console.WriteLine($"Numbers greater than {maxValidNumber} will be ignored");
                    }
                }
            }

            var calculator = new Calculator();
            var parser = new Parser(alternateDelimiter, allowNegativeNumbers, maxValidNumber);

            while (true)
            {
                System.Console.WriteLine($"\nEnter numbers separated by comma, '{alternateDelimiter}', or use custom delimiter(s):");
                System.Console.WriteLine($"Examples: 1,2{alternateDelimiter}3,4");
                System.Console.WriteLine($"         //#\\n2#5 (using # as delimiter)");
                System.Console.WriteLine($"         //[***]\\n11***22***33 (using *** as delimiter)");
                System.Console.WriteLine($"         //[*][!!][r9r]\\n11r9r22*33!!44 (using multiple delimiters)");
                System.Console.WriteLine($"(numbers > {maxValidNumber} will be ignored, {(allowNegativeNumbers ? "negative numbers allowed" : "negative numbers not allowed")}, press Ctrl+C to quit)");
                var input = System.Console.ReadLine();

                try
                {
                    var numbers = parser.Parse(input ?? string.Empty);
                    var result = calculator.Add(numbers);
                    
                    // Create formula string
                    var formula = string.Join(" + ", numbers);
                    
                    var currentColor = System.Console.ForegroundColor;
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine($"{formula} = {result}");
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
