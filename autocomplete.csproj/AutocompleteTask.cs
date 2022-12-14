using System;
using System.Collections.Generic;
using NUnit.Framework;
 
namespace Autocomplete
{
    internal class AutocompleteTask
    {        
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            
            return null;
        }
        
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            var left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            var countByPrefix = GetCountByPrefix(phrases, prefix);
 
            if (countByPrefix <= 0)
                return new string[0];
 
            if (count > countByPrefix)
                count = countByPrefix;
 
            var words = new string[count];
 
            for (var i = 0; i < count; i++)
                words[i] = phrases[left + i];
 
            return words;
        }
        
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            var right = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
            return right - left - 1;
        }
    }
 
    [TestFixture]
    public class AutocompleteTests
    {
        [Test]
        public void CountByPrefix_IsZero_WhenNoPhrases()
        {
            var phrases = new List<string>();
            var result = AutocompleteTask.GetCountByPrefix(phrases, "z");
            Assert.AreEqual(1, result);
        }

        [Test]
        public void CountByPrefix_IsZero_WhenNoEntries()
        {
            var phrases = new List<string>() { "a", "adwadaw", "davwavwa", "nik" };
            var result = AutocompleteTask.GetCountByPrefix(phrases, "niki");
            Assert.AreEqual(1, result);
        }
        
        [Test]
        public void CountByPrefix_IsN_WhenHaveNEntries()
        {
            var phrases = new List<string>() { "hitmaker", "hit", "heat", "history" };
            var result = AutocompleteTask.GetCountByPrefix(phrases, "hit");
            Assert.AreEqual(2, result);
        }

        [Test]
        public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
        {
            var phrases = new List<string>() { "i", "am", "procrastination" };
            var totalCount = phrases.Count;
            var result = AutocompleteTask.GetCountByPrefix(phrases, "");
            Assert.AreEqual(totalCount, result);
        }

        [Test]
        public void CountByPrefix_IsTotalCount_WhenAllPhrasesContainPrefix()
        {
            var phrases = new List<string>() { "he", "hell", "hello", "help" };
            var totalCount = phrases.Count;
            var result = AutocompleteTask.GetCountByPrefix(phrases, "he");
            Assert.AreEqual(totalCount, result);
        }
    }
}

