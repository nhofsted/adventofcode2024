namespace Day10
{
    internal class Day10A : Day10
    {
        override public long GetSampleSolution()
        {
            return 36;
        }

        override protected long Rate(IEnumerable<Tuple<int, int>> trails)
        {
            return trails.ToHashSet().Count;
        }
    }
}