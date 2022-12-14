using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]

        public void TestCase(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var quotesTextLength = 1;
            var quotesText = new StringBuilder();
            var quoteType = line[startIndex];
            
            for (var i = startIndex + 1; i < line.Length;i++)
            {
                if (line[i] == '\\' && i < line.Length - 1)
                {
                    quotesText.Append(line[i + 1]);
                    quotesTextLength += 2;
                    i++;
                }
                
                else if (line[i] == quoteType)
                {
                    quotesTextLength++;
                    break;
                }
                
                else if (line[i] != '\\')
                {
                    quotesText.Append(line[i]);
                    quotesTextLength++;
                }
            }
            return new Token(quotesText.ToString(), startIndex, quotesTextLength);
        }
    }
}