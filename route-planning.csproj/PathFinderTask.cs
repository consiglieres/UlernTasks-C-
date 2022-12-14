using System;
using System.Drawing;
 
namespace RoutePlanning
{
    public static class PathFinderTask
    {
        private static double minLength;
 
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            var size = checkpoints.Length;
 
            var bestOrder = new int[size];
            for (var i = 0; i < size; i++)
                bestOrder[i] = i;
 
            if (size == 1) return bestOrder;
 
            minLength = double.MaxValue;
 
            MakeTrivialPermutation(checkpoints, new int[size], 1, new double[size], ref bestOrder);
 
            return bestOrder;
        }        
 
        private static void MakeTrivialPermutation(Point[] checkpoints, int[] positions, int currentPosition, double[] lengths, ref int[] bestOrder)
        {
            if (checkpoints == null) throw new ArgumentNullException(nameof(checkpoints));
            if (lengths == null) throw new ArgumentNullException(nameof(lengths));
            if (currentPosition == checkpoints.Length)
            {
                if (!(lengths[currentPosition - 1] < minLength)) return;
                minLength = lengths[currentPosition - 1];
                bestOrder = positions.Clone() as int[];
                return;
            }
 
            for (var i = 1; i < positions.Length; i++)
            {
                var index = Array.IndexOf(positions, i, 1, currentPosition - 1);

                if (index != -1) continue;
                lengths[currentPosition] = lengths[currentPosition - 1] + GetDistanceBetween(checkpoints[positions[currentPosition - 1]], checkpoints[i]);

                if (!(lengths[currentPosition] < minLength)) continue;
                positions[currentPosition] = i;
                MakeTrivialPermutation(checkpoints, positions, currentPosition + 1, lengths, ref bestOrder);
            }
        }

        private static double GetDistanceBetween(this Point a, Point b)
        {
            var dx = a.X - b.X;
            var dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
