using System;

namespace ChallengeCalculator.Core
{
    public interface IParser
    {
        int[] Parse(string input);
    }
} 