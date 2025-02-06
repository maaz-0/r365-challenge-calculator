using System.Collections.Generic;

namespace ChallengeCalculator.Core
{
    public interface ICalculator
    {
        int Add(IEnumerable<int> numbers);
        int Subtract(int first, int second);
        int Multiply(IEnumerable<int> numbers);
        double Divide(int dividend, int divisor);
    }
} 