using ChallengeCalculator.Core;
using System;

namespace ChallengeCalculator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new Calculator();

            while (true)
            {
                System.Console.WriteLine("\nEnter numbers separated by comma (or 'exit' to quit):");
                var input = System.Console.ReadLine();

                if (input?.ToLower() == "exit")
                    break;

                try
                {
                    var result = calculator.Add(input);
                    System.Console.WriteLine($"Result: {result}");
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
        }
    }
}
