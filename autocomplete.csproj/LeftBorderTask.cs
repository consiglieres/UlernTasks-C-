using System;
using System.Collections.Generic;

namespace Autocomplete
{    
    public class LeftBorderTask
    {        
        public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            var divider = 2;
            var constanta = 1;
            
            if (left == right - constanta) 
                return left;
            
            var mid = left + (right - left) / divider;
 
            if (string.Compare(prefix, phrases[mid], StringComparison.OrdinalIgnoreCase) < 0
                || phrases[mid].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return GetLeftBorderIndex(phrases, prefix, left, mid);            
            
            return GetLeftBorderIndex(phrases, prefix, mid, right);
        }
    }
}

