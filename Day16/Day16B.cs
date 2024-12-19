namespace Day16
{
    internal class Day16B : Day16
    {
        override public long GetSampleSolution()
        {
            return 45;
        }

        protected override long Score(long leastCost, HashSet<Tuple<int, int>> visitedCoordinates)
        {
            return visitedCoordinates.Count;
        }
    }
}