using Common;

namespace Day1
{
    public abstract class Day1 : Puzzle
    {
        override public long Solve(StreamReader input)
        {
            List<int> left = new List<int>();
            List<int> right = new List<int>();

            string? line;
            while ((line = input.ReadLine()) != null)
            {
                string[] halves = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                left.Add(int.Parse(halves[0]));
                right.Add(int.Parse(halves[1]));
            }

            return Calculate(left, right);
        }

        protected abstract long Calculate(List<int> left, List<int> right);

        static void Main(string[] args)
        {
            new Day1A().run();
            new Day1B().run();
        }
    }
}
