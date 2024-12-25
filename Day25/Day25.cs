using Common;
using System.ComponentModel;

namespace Day25
{
    public class Day25 : Puzzle
    {
        public override long GetSampleSolution()
        {
            return 3;
        }

        public override long Solve(StreamReader input, bool sample)
        {
            Dictionary<string, Component> wires = new Dictionary<string, Component>();
            List<int[]> keys = new List<int[]>();
            List<int[]> locks = new List<int[]>();

            string? line = null;
            do
            {
                int[] pattern = new int[5];
                char type = '.';
                for (int i = 0; i < 7; ++i)
                {
                    line = input.ReadLine();
                    if (i == 0) type = line[0];
                    for (int j = 0; j < 5; ++j)
                    {
                        if (line[j] != type && pattern[j] == 0) pattern[j] = i;
                    }
                }
                if (type == '.') locks.Add(pattern);
                else keys.Add(pattern);
            }
            while (input.ReadLine() != null);

            long retVal = 0;
            foreach (int[] key in keys)
            {
                foreach (int[] alock in locks)
                {
                    if (IsMatch(key, alock)) retVal++;
                }
            }

            return retVal;
        }

        private static bool IsMatch(int[] key, int[] alock)
        {
            for (int i = 0; i < 5; ++i)
            {
                if (key[i] - alock[i] > 0) return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            new Day25().run();
        }
    }
}