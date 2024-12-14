using Common;

namespace Day9
{
    public abstract class Day9 : Puzzle
    {
        public override long Solve(StreamReader input, bool sample)
        {
            string layout = input.ReadLine();
            return Defrag(layout);
        }

        abstract protected long Defrag(string layout);

        static void Main(string[] args)
        {
            new Day9A().run();
            new Day9B().run();
        }
    }
}