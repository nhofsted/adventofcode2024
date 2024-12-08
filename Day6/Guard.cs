namespace Day6
{
    public class Guard
    {
        public enum MoveResult { MOVED, LOOPED, ESCAPED };

        private int x;
        private int y;
        private Direction direction;

        public Guard(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.direction = Direction.Up;
        }

        public MoveResult StampAndMove(Space[][] grid)
        {
            grid[y][x].Visit(direction);
            for (int i = 0; i < 4; ++i)
            {
                switch (direction)
                {
                    case Direction.Up:
                        if (y == 0) return MoveResult.ESCAPED;
                        if (grid[y - 1][x].IsObstacle())
                        {
                            direction = Direction.Right;
                            break;
                        }
                        else
                        {
                            y = y - 1;
                            return grid[y][x].IsVisited(direction) ? MoveResult.LOOPED : MoveResult.MOVED;
                        }
                    case Direction.Right:
                        if (x == grid[y].Length - 1) return MoveResult.ESCAPED;
                        if (grid[y][x + 1].IsObstacle())
                        {
                            direction = Direction.Down;
                            break;
                        }
                        else
                        {
                            x = x + 1;
                            return grid[y][x].IsVisited(direction) ? MoveResult.LOOPED : MoveResult.MOVED;
                        }
                    case Direction.Down:
                        if (y == grid.Length - 1) return MoveResult.ESCAPED;
                        if (grid[y + 1][x].IsObstacle())
                        {
                            direction = Direction.Left;
                            break;
                        }
                        else
                        {
                            y = y + 1;
                            return grid[y][x].IsVisited(direction) ? MoveResult.LOOPED : MoveResult.MOVED;
                        }
                    case Direction.Left:
                        if (x == 0) return MoveResult.ESCAPED;
                        if (grid[y][x - 1].IsObstacle())
                        {
                            direction = Direction.Up;
                            break;
                        }
                        else
                        {
                            x = x - 1;
                            return grid[y][x].IsVisited(direction) ? MoveResult.LOOPED : MoveResult.MOVED;
                        }
                }
            }
            throw new Exception("Locked up");
        }
    }
}