using Common;

namespace Day6
{
    public abstract class Day6 : Puzzle
    {
        protected Space[][] grid;
        protected int startX, startY;
        protected void Parse(StreamReader input)
        {
            ReadGrid(input);
        }

        private void ReadGrid(StreamReader input)
        {
            List<Space[]> gridBuilder = new List<Space[]>();
            string? line = null;
            int y = 0;
            while ((line = input.ReadLine()) != null)
            {
                int x = 0;
                if ((x = line.IndexOf('^')) != -1)
                {
                    startX = x;
                    startY = y;
                }
                gridBuilder.Add(Array.ConvertAll<char, Space>(line.ToCharArray(), c => new Space(c == '^', c == '#')));
                y++;
            }
            grid = gridBuilder.ToArray();
        }

        static void Main(string[] args)
        {
            new Day6A().run();
            new Day6B().run();
        }
    }
}