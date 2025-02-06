using ChallengeCalculator.Core;
using System;
using System.Linq;

namespace ChallengeCalculator.Console
{
    public class Program
    {
        private static string _alternateDelimiter = "\\n";
        private static bool _allowNegativeNumbers = false;
        private static int _maxValidNumber = 1000;
        private static readonly ICalculatorFacade _calculatorFacade;

        static Program()
        {
            _calculatorFacade = new CalculatorFacade(new Calculator());
        }

        static void Main(string[] args)
        {
            try
            {
                ProcessCommandLineArguments(args);
                RunCalculator();
            }
            catch (Exception ex)
            {
                WriteError($"Error processing command line arguments: {ex.Message}");
                ShowHelp();
            }
        }

        private static void WriteError(string message)
        {
            var currentColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(message);
            System.Console.ForegroundColor = currentColor;
        }

        private static void ProcessCommandLineArguments(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                switch (arg)
                {
                    case "--alt-delimiter" when i + 1 < args.Length:
                        _alternateDelimiter = args[++i].Replace("\"", "").Replace("'", "");
                        System.Console.WriteLine($"Using alternate delimiter: '{_alternateDelimiter}'");
                        break;
                    case "--allow-negative":
                        _allowNegativeNumbers = true;
                        System.Console.WriteLine("Negative numbers are allowed");
                        break;
                    case "--upper-bound" when i + 1 < args.Length:
                        if (int.TryParse(args[++i], out int bound))
                        {
                            _maxValidNumber = bound;
                            System.Console.WriteLine($"Numbers greater than {_maxValidNumber} will be ignored");
                        }
                        break;
                    case "--help":
                    case "-h":
                        ShowHelp();
                        Environment.Exit(0);
                        break;
                }
            }

            _calculatorFacade.Configure(_alternateDelimiter, _allowNegativeNumbers, _maxValidNumber);
        }

        private static void RunCalculator()
        {
            while (true)
            {
                var operation = GetOperationFromUser();
                DisplayInputInstructions(operation);
                var input = System.Console.ReadLine();

                try
                {
                    ProcessCalculation(input, operation);
                }
                catch (ArgumentException ex)
                {
                    WriteError($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    WriteError($"An unexpected error occurred: {ex.Message}");
                }
            }
        }

        private static MathOperation GetOperationFromUser()
        {
            System.Console.WriteLine("\nSelect operation:");
            System.Console.WriteLine("1. Add (+)");
            System.Console.WriteLine("2. Subtract (-)");
            System.Console.WriteLine("3. Multiply (*)");
            System.Console.WriteLine("4. Divide (/)");
            System.Console.Write("Enter operation number (1-4): ");

            var operationInput = System.Console.ReadLine()?.Trim();
            return operationInput switch
            {
                "2" => MathOperation.Subtract,
                "3" => MathOperation.Multiply,
                "4" => MathOperation.Divide,
                _ => MathOperation.Add
            };
        }

        private static void DisplayInputInstructions(MathOperation operation)
        {
            System.Console.WriteLine($"\nEnter numbers separated by comma, '{_alternateDelimiter}', or use custom delimiter(s):");
            System.Console.WriteLine($"Examples: 1,2{_alternateDelimiter}3,4");
            System.Console.WriteLine($"         //#\\n2#5 (using # as delimiter)");
            System.Console.WriteLine($"         //[***]\\n11***22***33 (using *** as delimiter)");
            System.Console.WriteLine($"         //[*][!!][r9r]\\n11r9r22*33!!44 (using multiple delimiters)");
            
            if (operation == MathOperation.Subtract || operation == MathOperation.Divide)
            {
                System.Console.WriteLine($"Note: {operation} operation requires exactly two numbers");
            }
            
            System.Console.WriteLine($"(numbers > {_maxValidNumber} will be ignored, " +
                                   $"{(_allowNegativeNumbers ? "negative numbers allowed" : "negative numbers not allowed")}, " +
                                   "press Ctrl+C to quit)");
        }

        private static void ProcessCalculation(string input, MathOperation operation)
        {
            var result = _calculatorFacade.Calculate(input ?? string.Empty, operation);
            DisplayResult(input, result, operation);
        }

        private static void DisplayResult(string input, double result, MathOperation operation)
        {
            string operationSymbol = operation switch
            {
                MathOperation.Subtract => "-",
                MathOperation.Multiply => "*",
                MathOperation.Divide => "/",
                _ => "+"
            };

            var currentColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine($"{input} = {result}");
            System.Console.ForegroundColor = currentColor;
        }

        private static void ShowHelp()
        {
            System.Console.WriteLine("\nUsage: dotnet run [options]");
            System.Console.WriteLine("\nOptions:");
            System.Console.WriteLine("  --alt-delimiter <char>   Specify alternate delimiter (default: \\n)");
            System.Console.WriteLine("  --allow-negative         Allow negative numbers");
            System.Console.WriteLine("  --upper-bound <number>   Set maximum valid number (default: 1000)");
            System.Console.WriteLine("  --help, -h              Show this help message");
            System.Console.WriteLine("\nExamples:");
            System.Console.WriteLine("  dotnet run --alt-delimiter \"|\"     # Use pipe as delimiter");
            System.Console.WriteLine("  dotnet run --alt-delimiter \"$\"     # Use dollar sign as delimiter");
            System.Console.WriteLine("  dotnet run --alt-delimiter \";\"     # Use semicolon as delimiter");
            System.Console.WriteLine("  dotnet run --alt-delimiter \"#\" --allow-negative");
            System.Console.WriteLine("  dotnet run --upper-bound 500");
            System.Console.WriteLine("\nNote: When using special characters as delimiters (|, &, *, etc.),");
            System.Console.WriteLine("      wrap them in quotes to prevent shell interpretation.");
        }
    }
}
