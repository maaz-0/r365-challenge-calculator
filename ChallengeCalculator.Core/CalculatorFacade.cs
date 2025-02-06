using System;

namespace ChallengeCalculator.Core
{
    public class CalculatorFacade : ICalculatorFacade
    {
        private readonly ICalculator _calculator;
        private string _alternateDelimiter = "\\n";
        private bool _allowNegativeNumbers = false;
        private int _maxValidNumber = 1000;

        public CalculatorFacade(ICalculator calculator)
        {
            _calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
        }

        public void Configure(string alternateDelimiter = "\\n", bool allowNegativeNumbers = false, int maxValidNumber = 1000)
        {
            _alternateDelimiter = alternateDelimiter;
            _allowNegativeNumbers = allowNegativeNumbers;
            _maxValidNumber = maxValidNumber;
        }

        public double Calculate(string input, MathOperation operation)
        {
            var parser = new Parser(_alternateDelimiter, _allowNegativeNumbers, _maxValidNumber, operation);
            var numbers = parser.Parse(input ?? string.Empty);

            return operation switch
            {
                MathOperation.Subtract => _calculator.Subtract(numbers[0], numbers[1]),
                MathOperation.Multiply => _calculator.Multiply(numbers),
                MathOperation.Divide => _calculator.Divide(numbers[0], numbers[1]),
                _ => _calculator.Add(numbers)
            };
        }
    }
} 