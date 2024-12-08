
namespace Day6
{
    internal class Day6B : Day6
    {
        override public long GetSampleSolution()
        {
            return 6;
        }

        override public long Solve(StreamReader input)
        {
            Parse(input);

            Space[][] workGrid = CopyGrid(grid);

            Guard guard = new Guard(startX, startY);
            while (guard.StampAndMove(workGrid) != Guard.MoveResult.ESCAPED) ;

            long result = 0;
            for (int y = 0; y < workGrid.Length; ++y)
            {
                for (int x = 0; x < workGrid[y].Length; ++x)
                {
                    if (x == startX && y == startY) continue;
                    if (workGrid[y][x].IsVisited())
                    {
                        if (ObstacleCausesLoop(x, y))
                        {
                            result++;
                        };
                    }
                }
            }
            return result;
        }

        private Space[][] CopyGrid(Space[][] grid)
        {
            Space[][] copy = new Space[grid.Length][];

            for (int i = 0; i < grid.Length; i++)
            {
                copy[i] = new Space[grid[i].Length];
                for (int j = 0; j < grid[i].Length; j++)
                {
                    copy[i][j] = new Space(grid[i][j]);
                }
            }
            return copy;
        }

        private bool ObstacleCausesLoop(int x, int y)
        {
            Space[][] workGrid = CopyGrid(grid);
            workGrid[y][x].MakeObstacle();
            Guard guard = new Guard(startX, startY);
            Guard.MoveResult result;
            while ((result = guard.StampAndMove(workGrid)) == Guard.MoveResult.MOVED) ;
            return result == Guard.MoveResult.LOOPED;
        }
    }
}