using System;
using System.Collections.Generic;
using System.Linq;

namespace ChallengeCalculator.Core
{
    public class Calculator : ICalculator
    {
        public int Add(IEnumerable<int> numbers)
        {
            return numbers?.Sum() ?? 0;
        }

        public int Subtract(int first, int second)
        {
            return first - second;
        }

        public int Multiply(IEnumerable<int> numbers)
        {
            if (numbers == null || !numbers.Any())
                return 0;

            return numbers.Aggregate(1, (current, next) => current * next);
        }

        public double Divide(int dividend, int divisor)
        {
            if (divisor == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero");
            }

            return (double)dividend / divisor;
        }
    }
} 