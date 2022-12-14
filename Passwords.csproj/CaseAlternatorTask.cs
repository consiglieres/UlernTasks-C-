using System;
using System.Collections.Generic;
using System.Linq;

namespace Passwords
{
    public class CaseAlternatorTask
    {
        public static List<string> AlternateCharCases(string lowercaseWord)
        {
            var result = new List<string>();
            AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
            return result;
        }

        private static void AlternateCharCases(char[] word, int startIndex, List<string> result)
        {
            if (startIndex == word.Length)
            {
                result.Add(new string(word));
                return;
            }

            char below, top;
            if (char.IsLetter(word[startIndex]))
            {
                below = char.ToLower(word[startIndex]);
                top = char.ToUpper(word[startIndex]);
            }
            else
                below = top = word[startIndex];

            word[startIndex] = below;

            AlternateCharCases(word, startIndex + 1, result);

            if (below == top) return;
            word[startIndex] = top;
            AlternateCharCases(word, startIndex + 1, result);
        }
    }
}