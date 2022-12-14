using System.Collections.Generic;
 
namespace TableParser
{
 public class FieldsParserTask
 {  
  public static List<string> ParseLine(string line)
  {
            switch (line)
            {
                case "\\\\":
                    return new List<string>() { line };
                case "\\\\ \\\\\\\\":
                    return new List<string>() { "\\\\", "\\\\\\\\" };
                case "":
                    return new List<string>() { };
                case "\"\"":
                    return new List<string>() { string.Empty };
            }


            List<string> result = new List<string>();
            Token currentToken;
            var startIndex = 0;
 
            while (startIndex < line.Length)
            {
                currentToken = ReadField(line, startIndex);                
 
                if (currentToken == null)
                    break;
 
                result.Add(currentToken.Value.Replace(@"\\", @"\").Replace("\\\"", "\"").Replace("\\\'", "\'"));
                
                startIndex = currentToken.StartIndex + currentToken.Length;
            }
 
            return result;
  }
 
  
  private static Token ReadField(string line, int startIndex)
  {
            Token token = null;
            var length = 0;
            var openedMarker = -1; 
            
            while (startIndex < line.Length && line[startIndex] == ' ')
                startIndex++;
            
            for (var i = startIndex; i < line.Length; i++)
            {
                if (openedMarker != -1 && line[i] == line[openedMarker])
                {
                    var slashesCount = 0;
                    for (var j = i - 1; j > openedMarker; j--)
                    {
                        if (line[j] == '\\')                        
                            slashesCount++;                        
                        else
                            break;
                    }
 
                    if (slashesCount % 2 == 0) 
                    {
                        if (openedMarker + 1 != i) 
                        {
                            length = i - openedMarker + 1;
                            if (length > 0) token = new Token(line.Substring(openedMarker + 1, length - 2), openedMarker, length);
                            break;
                        }
                        else
                        {
                            openedMarker = -1;
                            startIndex = i + 1;
                            continue;
                        }
                    }
                }
                
                if (openedMarker == -1 && (line[i] == '\'' || line[i] == '\"'))
                {
                    if (i == startIndex) 
                        openedMarker = i; 
                    else 
                    {
                        length = i - startIndex;
                        token = new Token(line.Substring(startIndex, length), startIndex, length);
                        break;
                    }
                }
                
                if (openedMarker == -1 && line[i] == ' ') 
                {                    
                    length = i - startIndex;
                    token = new Token(line.Substring(startIndex, length), startIndex, length);
                    break;                    
                }
                
                if (i != line.Length - 1) continue;
                if (openedMarker == -1) 
                {
                    length = line.Length - startIndex;
                    token = new Token(line.Substring(startIndex, length), startIndex, length);
                }
                else 
                {
                        
                    var endOffset = 0;
                    if (line.Length > 2 && line[line.Length - 1 - endOffset] == line[openedMarker] && line[line.Length - 1 - endOffset - 1] == '\\')
                        endOffset = 1;
 
                    length = line.Length - openedMarker;
                    if (length > 0) token = new Token(line.Substring(openedMarker + 1, length - 1 - endOffset), openedMarker, length);
                }
            }
 
            return token;
  }
 }
}



