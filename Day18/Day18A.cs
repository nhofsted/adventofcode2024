namespace Day18
{
    internal class Day18A : Day18
    {
        override public long GetSampleSolution()
        {
            return 22;
        }

        override protected long Solve(bool sample, List<int[]> coordinates)
        {
            int iterations = sample ? 12 : 1024;
            return FindShortestPath(sample, coordinates, iterations);
        }
    }
}