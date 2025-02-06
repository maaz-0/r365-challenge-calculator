﻿using ChallengeCalculator.Core;
using System;
using System.Linq;

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
                System.Console.WriteLine("\nEnter numbers separated by comma, '\\n', or use custom delimiter(s):");
                System.Console.WriteLine("Examples: 1,2\\n3,4");
                System.Console.WriteLine("         //#\\n2#5 (using # as delimiter)");
                System.Console.WriteLine("         //[***]\\n11***22***33 (using *** as delimiter)");
                System.Console.WriteLine("         //[*][!!][r9r]\\n11r9r22*33!!44 (using multiple delimiters)");
                System.Console.WriteLine("(numbers > 1000 will be ignored, press Ctrl+C to quit)");
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
