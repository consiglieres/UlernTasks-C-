using System;
 
namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];
 
            var compensate = sx.GetLength(0) / 2;
            var constant = sx.GetLength(0);
 
            for (var x = compensate; x < width - compensate; x++)
            {
                for (var y = compensate; y < height - compensate; y++)
                {
                    var gCropped = GetCroppedMatrix(g, x, y, constant);
                    var gx = GetConvolution(gCropped, sx);
                    var sRotated = GetRotated(sx);
                    var gy = GetConvolution(gCropped, sRotated);
 
                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            }
 
            return result;
        }
 
        private static double[,] GetCroppedMatrix(double[,] g, int x, int y, int length)
        {
            var result = new double[length, length];
            var compensate = length / 2;

            for (var i = -compensate; i <= compensate; i++)
            {
                for (var j = -compensate; j <= compensate; j++)
                    result[i + compensate, j + compensate] = g[x + j, y + i];
            }

            return result;
        }        
 
        private static double[,] GetRotated(double[,] sx)
        {
            var length = sx.GetLength(0);
            var result = new double[length, length];

            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < length; j++)
                    result[j, i] = sx[i, j];
            }

            return result;
        }
 
        private static double GetConvolution(double[,] a, double[,] b)
        {
            var length = a.GetLength(0);
            double result = 0;

            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < length; j++)
                    result += a[i, j] * b[i, j];
            }

            return result;
        }
    }
}
