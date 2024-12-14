using Common;

namespace Day4
{
    public abstract class Day4 : Puzzle
    {
        protected char[][]? grid = null;
        protected int width = 0;
        protected int height = 0;

        override public long Solve(StreamReader input, bool sample)
        {
            List<char[]> gridBuilder = new List<char[]>();
            string? line;
            while ((line = input.ReadLine()) != null)
            {
                gridBuilder.Add(line.ToCharArray());
            }

            grid = gridBuilder.ToArray();
            height = grid.Length;
            if (height > 0) width = grid[0].Length;

            return findPattern();
        }

        protected abstract long findPattern();

        protected char getLetter(int x, int y)
        {
            if (x < 0 || y < 0 || x >= width || y >= height) return '.';
            return grid[y][x];
        }

        static void Main(string[] args)
        {
            new Day4A().run();
            new Day4B().run();
        }
    }
}