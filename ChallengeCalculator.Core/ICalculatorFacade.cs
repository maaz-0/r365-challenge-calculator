using System;

namespace ChallengeCalculator.Core
{
    public interface ICalculatorFacade
    {
        double Calculate(string input, MathOperation operation);
        void Configure(string alternateDelimiter = "\\n", bool allowNegativeNumbers = false, int maxValidNumber = 1000);
    }
} 