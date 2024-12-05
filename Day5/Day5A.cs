namespace Day5
{
    internal class Day5A : Day5
    {
        override public long GetSampleSolution()
        {
            return 143;
        }

        override protected long scoreUpdate(Dictionary<int, List<int>> requirements, long result, int[] pages)
        {
            return checkPageOrder(pages, requirements) ? pages[pages.Length / 2] : 0;
        }
    }
}