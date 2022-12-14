namespace Recognizer
{
    public static class GrayscaleTask
    {
        public static double[,] ToGrayscale(Pixel[,] original)
        {
            var rows = original.GetLength(0);
            var columns = original.GetLength(1);
 
            var result = new double[rows, columns];

            for (var i = 0; i < rows; i++)
                for (var j = 0; j < columns; j++)
                {
                    var p = original[i, j];
                    result[i, j] = (0.299 * p.R + 0.587 * p.G + 0.114 * p.B) / 255;
                }
            
 
            return result;            
        }
    }
}