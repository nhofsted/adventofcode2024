namespace Day16
{
    class Position : IEquatable<Position>
    {
        public int X { get; }
        public int Y { get; }
        public Direction CurrentDirection { get; }

        public Position(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            CurrentDirection = direction;
        }

        public Position(Position position)
        {
            this.X = position.X;
            this.Y = position.Y;
            this.CurrentDirection = position.CurrentDirection;
        }

        public Position TurnClockwise()
        {
            return new Position(X, Y, TurnClockwise(CurrentDirection));
        }

        public Position TurnCounterClockwise()
        {
            return new Position(X, Y, TurnCounterClockwise(CurrentDirection));
        }

        public Position Advance()
        {
            switch (CurrentDirection)
            {
                case Direction.NORTH: return new Position(X, Y - 1, CurrentDirection);
                case Direction.EAST: return new Position(X + 1, Y, CurrentDirection);
                case Direction.SOUTH: return new Position(X, Y + 1, CurrentDirection);
                default: return new Position(X - 1, Y, CurrentDirection);
            }
        }

        public bool Equals(Position? other)
        {
            if (other is null)
            {
                return false;
            }

            return X == other.X && Y == other.Y && CurrentDirection == other.CurrentDirection;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 73;
                hash = hash * 97 + X.GetHashCode();
                hash = hash * 83 + Y.GetHashCode();
                hash = hash * 79 + CurrentDirection.GetHashCode();
                return hash;
            }
        }

        private static Direction TurnClockwise(Direction direction)
        {
            switch (direction)
            {
                case Direction.NORTH: return Direction.EAST;
                case Direction.EAST: return Direction.SOUTH;
                case Direction.SOUTH: return Direction.WEST;
                default: return Direction.NORTH;
            }
        }

        private static Direction TurnCounterClockwise(Direction direction)
        {
            switch (direction)
            {
                case Direction.NORTH: return Direction.WEST;
                case Direction.EAST: return Direction.NORTH;
                case Direction.SOUTH: return Direction.EAST;
                default: return Direction.SOUTH;
            }
        }
    }
}