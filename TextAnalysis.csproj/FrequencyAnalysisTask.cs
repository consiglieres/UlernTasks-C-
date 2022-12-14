using System;
using System.Collections.Generic;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = GetFinals(text);
            return result;
        }
        
        private static Dictionary<string, string> GetFinals(List<List<string>> initialList)
        {
            var countOfBigramsAndThreegrams = GetThreegramsWithCount(initialList);
            return GetLastDict(countOfBigramsAndThreegrams);
        }

        private static Dictionary<string,Dictionary<string,int>> GetThreegramsWithCount(List<List<string>> initialList)
        {
            var countOfBigrams = new Dictionary<string, Dictionary<string, int>>();
            for (var amountOfNgrams = 1; amountOfNgrams < 3; amountOfNgrams++)
            {
                foreach (var t in initialList)
                {
                    for (var j = 0; j < t.Count - amountOfNgrams; j++)
                    {
                        var nGramKey = "";
                        switch (amountOfNgrams)
                        {
                            case 1:
                                nGramKey = t[j];
                                break;
                            case 2:
                                nGramKey = $"{t[j]} {t[j + 1]}";
                                break;
                        }
                        var howManySymbolInSentence = 1;
                        if (countOfBigrams.ContainsKey(nGramKey))
                        {
                            if (countOfBigrams[nGramKey].ContainsKey(t[j + amountOfNgrams]))
                            {
                                countOfBigrams[nGramKey].TryGetValue(t[j + amountOfNgrams], out howManySymbolInSentence);
                                howManySymbolInSentence++;
                                countOfBigrams[nGramKey][t[j + amountOfNgrams]] = howManySymbolInSentence;
                            }
                            else
                            {
                                countOfBigrams[nGramKey].Add(t[j + amountOfNgrams], howManySymbolInSentence);
                            }
                        }
                        else
                        {
                            var result2 = new Dictionary<string, int>();
                            result2.Add(t[j + amountOfNgrams], howManySymbolInSentence);
                            countOfBigrams.Add(nGramKey, result2);
                        }
                    }
                }
            }
            return countOfBigrams;
        }
        
        private static Dictionary<string,string> GetLastDict(Dictionary<string,Dictionary<string,int>> dictionaryWithThreeAndDigrams)
        {
            var finalDict = new Dictionary<string, string>();
            var arrOfKeys = new string [dictionaryWithThreeAndDigrams.Count];
            dictionaryWithThreeAndDigrams.Keys.CopyTo(arrOfKeys,0);
            foreach (var t in arrOfKeys)
            {
                var arrOfKeys2 = new string [dictionaryWithThreeAndDigrams[t].Count];
                dictionaryWithThreeAndDigrams[t].Keys.CopyTo(arrOfKeys2,0);
                if (arrOfKeys2.Length == 1)
                {
                    finalDict.Add(t,arrOfKeys2[0]);
                }
                else if (CheckAll(dictionaryWithThreeAndDigrams, t,arrOfKeys2))
                {
                    var value = arrOfKeys2[0];
                    for (var j = 0; j < arrOfKeys2.Length - 1; j++)
                    {
                        if (string.CompareOrdinal(value, arrOfKeys2[j + 1]) > 0)
                            value = arrOfKeys2[j + 1];
                    }
                    finalDict.Add(t,value);
                }
                else if(!CheckAll(dictionaryWithThreeAndDigrams, t,arrOfKeys2))
                {
                    var numberOfKey = 0;
                    for (var j = 0; j < arrOfKeys2.Length-1; j++)
                    {
                        if (dictionaryWithThreeAndDigrams[t][arrOfKeys2[numberOfKey]] < dictionaryWithThreeAndDigrams[t][arrOfKeys2[j+1]])
                        {
                            numberOfKey = j+1;
                        }
                        else if (dictionaryWithThreeAndDigrams[t][arrOfKeys2[numberOfKey]] ==
                                 dictionaryWithThreeAndDigrams[t][arrOfKeys2[j+1]])
                        {
                            if (string.CompareOrdinal(arrOfKeys2[numberOfKey], arrOfKeys2[j + 1]) > 0)
                                numberOfKey = j + 1;
                        }
                    }
                    finalDict.Add(t,arrOfKeys2[numberOfKey]);
                }
            }
            return finalDict;
        }

        private static bool CheckAll(Dictionary<string, Dictionary<string, int>> dictionaryWithThreeAndDigrams, string firstKeyDict, string[] arrOfKeys2)
        {
            var checkout = true;
            foreach (var t in arrOfKeys2)
            {
                if (dictionaryWithThreeAndDigrams[firstKeyDict][t] != 1) checkout = false;
            }
            return checkout;
        }
    }
}