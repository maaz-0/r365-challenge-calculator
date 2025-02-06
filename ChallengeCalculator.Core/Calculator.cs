using System;
using System.Collections.Generic;
using System.Linq;

namespace ChallengeCalculator.Core
{
    public class Calculator
    {
        public int Add(IEnumerable<int> numbers)
        {
            return numbers?.Sum() ?? 0;
        }
    }
} 