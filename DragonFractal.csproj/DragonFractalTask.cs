using System;
 
namespace Fractals
{
    internal static class DragonFractalTask
    {
        public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
        {
            const double a1 = Math.PI * 45 / 180;
            const double a2 = Math.PI * 135 / 180;
 
            var x = 1.0; 
            var y = 0.0;            
 
            var r = new Random(seed);
 
            for (var i = 0; i < iterationsCount; i++)
            {
                double newX;
                double newY;
 
                if (r.Next(2) == 0)
                {
                    newX = (x * Math.Cos(a1) - y * Math.Sin(a1)) / Math.Sqrt(2);
                    newY = (x * Math.Sin(a1) + y * Math.Cos(a1)) / Math.Sqrt(2);                    
                }
                else                
                {
                    newX = (x * Math.Cos(a2) - y * Math.Sin(a2)) / Math.Sqrt(2) + 1;
                    newY = (x * Math.Sin(a2) + y * Math.Cos(a2)) / Math.Sqrt(2);                    
                }
 
                x = newX;
                y = newY;
 
                pixels.SetPixel(x, y);
            }
        }
    }
}