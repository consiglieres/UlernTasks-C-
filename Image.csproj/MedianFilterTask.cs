using System;
using System.Collections.Generic;
 
namespace Recognizer
{
    internal static class MedianFilterTask
    {
        public static double[,] MedianFilter(double[,] original)
        {
            var rowse = original.GetLength(0);
            var columns = original.GetLength(1);                        
            var result = new double[rowse, columns];            
 
            for (var x = 0; x < rowse; x++)
            {
                for (var y = 0; y < columns; y++)
                {
                    var window = new List<double>();
 
                    var xFrom = x > 0 ? x - 1 : 0;
                    var xTo = x < rowse - 1 ? x + 1 : rowse - 1;
                    var yFrom = y > 0 ? y - 1 : 0;
                    var yTo = y < columns - 1 ? y + 1 : columns - 1;

                    for (var fx = xFrom; fx <= xTo; fx++)
                        for (var fy = yFrom; fy <= yTo; fy++)
                        {
                            window.Add(original[fx, fy]);
                        }

                    window.Sort();
 
                    var count = window.Count;
                    var fuck = count % 2 != 0 ? window[count / 2] : (window[count / 2] + window[count / 2 - 1]) / 2;
 
                    result[x, y] = fuck;
                }
            }
 
            return result;
        }
    }
}