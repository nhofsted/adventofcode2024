using Common;

namespace Day5
{
    public abstract class Day5 : Puzzle
    {
        override public long Solve(StreamReader input)
        {
            Dictionary<int, List<int>> requirements = parseRequirements(input);

            long result = 0;
            string? line = null;
            while ((line = input.ReadLine()) != null)
            {
                int[] pages = Array.ConvertAll<string, int>(line.Split(','), int.Parse);
                result += scoreUpdate(requirements, result, pages);
            }

            return result;
        }

        protected abstract long scoreUpdate(Dictionary<int, List<int>> requirements, long result, int[] pages);

        protected static bool checkPageOrder(int[] pages, Dictionary<int, List<int>> requirements)
        {
            HashSet<int> seen = new HashSet<int>();
            HashSet<int> pagesSet = new HashSet<int>(pages);
            for (int i = 0; i < pages.Length; i++)
            {
                // check whether a page that should come after this page isn't already seen in front of this page
                if (requirements.TryGetValue(pages[i], out var req) && req.Exists(p => pagesSet.Contains(p) && !seen.Contains(p))) return false;
                seen.Add(pages[i]);
            }
            return true;
        }

        private Dictionary<int, List<int>> parseRequirements(StreamReader input)
        {
            Dictionary<int, List<int>> requirements = new Dictionary<int, List<int>>();
            string? line = null;
            while ((line = input.ReadLine()).Length > 0)
            {
                int[] order = Array.ConvertAll<string, int>(line.Split('|'), int.Parse);
                if (!requirements.ContainsKey(order[1]))
                {
                    requirements.Add(order[1], new List<int>());
                }
                requirements[order[1]].Add(order[0]);
            }
            return requirements;
        }

        static void Main(string[] args)
        {
            new Day5A().run();
            new Day5B().run();
        }
    }
}