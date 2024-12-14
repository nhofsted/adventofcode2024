using Common;

namespace Day14
{
    public abstract class Day14 : Puzzle
    {
        protected long GetWidth(bool sample)
        {
            if (sample) return 11;
            return 101;
        }

        protected long GetHeight(bool sample)
        {
            if (sample) return 7;
            return 103;
        }

        static void Main(string[] args)
        {
            new Day14A().run();
            new Day14B().run();
        }
    }
}