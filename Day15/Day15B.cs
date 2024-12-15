namespace Day15
{
    internal class Day15B : Day15
    {
        override public long GetSampleSolution()
        {
            return 9021;
        }

        override protected char[][] ReadGrid(StreamReader input, ref Tuple<int, int> lanternfish)
        {
            char[][] grid = base.ReadGrid(input, ref lanternfish);
            List<char[]> transformedGrid = new List<char[]>();
            for (int y = 0; y < grid.Length; ++y)
            {
                char[] transformedLine = new char[grid[y].Length * 2];
                for (int x = 0; x < grid[y].Length; ++x)
                {
                    char item = grid[y][x];
                    transformedLine[2 * x] = item;
                    if (item == 'O')
                    {
                        transformedLine[2 * x] = '[';
                        transformedLine[2 * x + 1] = ']';
                    }
                    else if (item == '@') transformedLine[2 * x + 1] = '.';
                    else transformedLine[2 * x + 1] = item;
                }
                transformedGrid.Add(transformedLine);
            }
            lanternfish = new Tuple<int, int>(lanternfish.Item1 * 2, lanternfish.Item2);
            return transformedGrid.ToArray();
        }
    }
}