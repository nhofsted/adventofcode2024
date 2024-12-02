using Common;

namespace Day2
{
    public abstract class Day2 : Puzzle
    {
        override public long Solve(StreamReader input)
        {
            int score = 0;
            string? line;
            while ((line = input.ReadLine()) != null)
            {
                int[] levels = Array.ConvertAll<string, int>(line.Split(' '), int.Parse);
                bool safe = isSafe(levels, GetDampener());
                if (safe) score++;
            }

            return score;
        }

        private bool isSafe(int[] levels, int dampener)
        {
            int dir = 0;
            for (int i = 0; i < levels.Length - 1; i++)
            {
                int diff = levels[i + 1] - levels[i];
                dir = Math.Sign(Math.Sign(diff) + dir);
                if (diff == 0 || Math.Abs(diff) > 3 || dir == 0)
                {
                    if (dampener == 0) return false;
                    return isSafe(removeLevel(levels, i), dampener - 1) ||
                        isSafe(removeLevel(levels, i + 1), dampener - 1) ||
                        ((i > 0) && isSafe(removeLevel(levels, i - 1), dampener - 1));
                }
            }
            return true;
        }

        private int[] removeLevel(int[] levels, int itemIndex)
        {
            int[] retVal = new int[levels.Length - 1];
            int i = 0;
            for (int j = 0; j < levels.Length; j++)
            {
                if (j != itemIndex) retVal[i++] = levels[j];
            }
            return retVal;
        }

        public abstract int GetDampener();

        static void Main(string[] args)
        {
            new Day2A().run();
            new Day2B().run();
        }
    }
}