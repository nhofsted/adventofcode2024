
namespace Day10
{
    internal class Day10B : Day10
    {
        override public long GetSampleSolution()
        {
            return 81;
        }

        override protected long Rate(IEnumerable<Tuple<int, int>> trails)
        {
            return Enumerable.Count(trails);
        }
    }
}