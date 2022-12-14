using System;
using System.Collections.Generic;

namespace Autocomplete
{
    public class RightBorderTask
    {        
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            var constanta = 1;
            
            if (phrases.Count == 0) 
                return right;
            
            left++;
            right--;
            
            while (left < right)
            {
                var divider = 2;
                var mid = left + (right - left) / divider;

                if (string.Compare(prefix, phrases[mid], StringComparison.OrdinalIgnoreCase) >= 0
                    || phrases[mid].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    left = mid + constanta;
                else 
                    right = mid - constanta;              
            }
            
            if (string.Compare(prefix, phrases[right], StringComparison.OrdinalIgnoreCase) >= 0
                || phrases[left].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return right + constanta;
            else 
                return right;
        }
    }
}