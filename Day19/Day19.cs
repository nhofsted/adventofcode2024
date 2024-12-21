using Common;

namespace Day19
{
    public abstract class Day19 : Puzzle
    {
        public override long Solve(StreamReader input, bool sample)
        {
            string[] towels = input.ReadLine().Split(", ");
            input.ReadLine();

            long result = 0;
            string? pattern = null;
            while ((pattern = input.ReadLine()) != null)
            {
                result += Match(pattern, towels, new Dictionary<string, long>());
            }
            return result;
        }

        abstract protected long Match(string pattern, string[] towels, Dictionary<string, long> memory);

        static void Main(string[] args)
        {
            new Day19A().run();
            new Day19B().run();
        }
    }
}