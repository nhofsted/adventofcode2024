using Common;

namespace Day12
{
    public abstract class Day12 : Puzzle
    {
        public override long Solve(StreamReader input)
        {
            List<char[]> gridBuilder = new List<char[]>();

            string? line = null;
            while ((line = input.ReadLine()) != null)
            {
                gridBuilder.Add(line.ToCharArray());
            }
            char[][] grid = gridBuilder.ToArray();

            int[][] igrid = new int[grid.Length][];
            for (int i = 0; i < igrid.Length; ++i)
            {
                igrid[i] = new int[grid[i].Length];
            }

            int regionId = 0;
            for (int y = 0; y < grid.Length; ++y)
            {
                for (int x = 0; x < grid[y].Length; ++x)
                {
                    if (grid[y][x] != '.') MoveRegion(grid, igrid, y, x, regionId++);
                }
            }

            return CalculatePrice(igrid);
        }

        abstract protected long CalculatePrice(int[][] igrid);

        private static void MoveRegion(char[][] from, int[][] to, int y, int x, int id)
        {
            char type = from[y][x];
            from[y][x] = '.';
            to[y][x] = id;
            if (y > 0 && from[y - 1][x] == type) MoveRegion(from, to, y - 1, x, id);
            if (y < from.Length - 1 && from[y + 1][x] == type) MoveRegion(from, to, y + 1, x, id);
            if (x > 0 && from[y][x - 1] == type) MoveRegion(from, to, y, x - 1, id);
            if (x < from[y].Length - 1 && from[y][x + 1] == type) MoveRegion(from, to, y, x + 1, id);
        }

        protected static void AddOne(Dictionary<int, int> map, int key)
        {
            if (!map.ContainsKey(key)) map[key] = 0;
            map[key]++;
        }

        static void Main(string[] args)
        {
            new Day12A().run();
            new Day12B().run();
        }
    }
}