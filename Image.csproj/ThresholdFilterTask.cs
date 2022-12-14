using System.Linq;
using System;
namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var x = original.GetLength(0);
            var y = original.GetLength(1);
            double T= (int)(original.Length * whitePixelsFraction);

            if (T > 0 && T <= original.Length)
                T = original.Cast<double>().OrderBy(n => n).
                    ElementAt(original.Length - (int)T);
            else
                T = int.MaxValue;

            for (var i = 0; i < x; i++)
            for (var j = 0; j < y; j++)
                original[i, j] = Convert.ToInt32(original[i, j] >= T);

            return original;
        }
    }
}
