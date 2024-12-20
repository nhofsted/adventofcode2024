namespace Day18
{
    internal class Day18B : Day18
    {
        override public long GetSampleSolution()
        {
            return 20;
        }

        override protected long Solve(bool sample, List<int[]> coordinates)
        {
            int min = 0;
            int max = coordinates.Count;
            while (min + 1 != max)
            {
                int test = (min + max) / 2;
                if (FindShortestPath(sample, coordinates, test) == -1)
                {
                    max = test;
                }
                else
                {
                    min = test;
                }
            }
            Console.WriteLine(coordinates[min][0] + "," + coordinates[min][1]);
            return min;
        }
    }
}