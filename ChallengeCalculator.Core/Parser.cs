using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ChallengeCalculator.Core
{
    public class Parser
    {
        private const int MaxValidNumber = 1000;

        public int[] Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Array.Empty<int>();

            string delimiter = ",|\\\\n"; // Default delimiters
            string numbersInput = input;

            // Check for custom delimiter format
            if (input.StartsWith("//"))
            {
                if (input.StartsWith("//[") && input.Contains("]\\n"))
                {
                    // Handle multiple or single delimiters in bracket format
                    var endOfDelimiters = input.IndexOf("\\n");
                    if (endOfDelimiters > 0)
                    {
                        var delimitersSection = input.Substring(2, endOfDelimiters - 2);
                        numbersInput = input.Substring(endOfDelimiters + 2);

                        if (delimitersSection.Count(c => c == '[') > 1)
                        {
                            // Multiple delimiters
                            var delimiters = new List<string>();
                            var currentDelimiter = "";
                            var insideDelimiter = false;

                            foreach (var c in delimitersSection)
                            {
                                if (c == '[')
                                {
                                    insideDelimiter = true;
                                    currentDelimiter = "";
                                }
                                else if (c == ']')
                                {
                                    insideDelimiter = false;
                                    if (!string.IsNullOrEmpty(currentDelimiter))
                                    {
                                        delimiters.Add(Regex.Escape(currentDelimiter));
                                    }
                                }
                                else if (insideDelimiter)
                                {
                                    currentDelimiter += c;
                                }
                            }

                            delimiter = string.Join("|", delimiters) + "|\\\\n"; // Add newline as a delimiter
                        }
                        else
                        {
                            // Single delimiter in brackets
                            var match = Regex.Match(delimitersSection, @"\[(.*?)\]");
                            if (match.Success)
                            {
                                delimiter = Regex.Escape(match.Groups[1].Value) + "|\\\\n"; // Add newline as a delimiter
                            }
                        }
                    }
                }
                else
                {
                    // Original single-character format: //delimiter\n
                    var parts = input.Split(new[] { "\\n" }, 2, StringSplitOptions.None);
                    if (parts.Length == 2)
                    {
                        delimiter = Regex.Escape(parts[0].Substring(2)) + "|\\\\n"; // Add newline as a delimiter
                        numbersInput = parts[1];
                    }
                }
            }

            // Split by delimiter(s)
            var numbers = Regex.Split(numbersInput, delimiter)
                             .Where(s => !string.IsNullOrWhiteSpace(s))
                             .Select(part =>
                             {
                                 if (int.TryParse(part.Trim(), out int number))
                                 {
                                     return number > MaxValidNumber ? 0 : number;
                                 }
                                 return 0; // Invalid numbers are converted to 0
                             })
                             .ToArray();

            var negativeNumbers = numbers.Where(n => n < 0).ToList();
            if (negativeNumbers.Any())
            {
                throw new ArgumentException($"Input includes negative numbers: {string.Join(", ", negativeNumbers)}");
            }

            return numbers;
        }
    }
} 