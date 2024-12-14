namespace Day6
{
    internal class Day6A : Day6
    {
        override public long GetSampleSolution()
        {
            return 41;
        }

        override public long Solve(StreamReader input, bool sample)
        {
            Parse(input);

            Guard guard = new Guard(startX, startY);
            while (guard.StampAndMove(grid) != Guard.MoveResult.ESCAPED) ;

            long result = 0;
            for (int y = 0; y < grid.Length; ++y)
            {
                for (int x = 0; x < grid[y].Length; ++x)
                {
                    if (grid[y][x].IsVisited())
                    {
                        result++;
                    }
                }
            }
            return result;
        }
    }
}