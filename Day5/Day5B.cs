namespace Day5
{
    internal class Day5B : Day5
    {
        override public long GetSampleSolution()
        {
            return 123;
        }

        override protected long scoreUpdate(Dictionary<int, List<int>> requirements, long result, int[] pages)
        {
            if (checkPageOrder(pages, requirements)) return 0;

            HashSet<int> todo = new HashSet<int>(pages);
            int middlePage = 0;
            while (todo.Count > pages.Length / 2)
            {
                foreach (int page in todo)
                {
                    // pick and remove a page without dependend pages
                    if (!requirements.TryGetValue(page, out var dependendPages) || dependendPages.TrueForAll(p => !todo.Contains(p)))
                    {
                        todo.Remove(page);
                        middlePage = page;
                        goto outer;
                    }
                }
                throw new Exception("circular dependency");
            outer:;
            }
            return middlePage;
        }
    }
}