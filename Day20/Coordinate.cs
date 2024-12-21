namespace Day20
{
    public class Coordinate : IEquatable<Coordinate>
    {
        public int X { get; }
        public int Y { get; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        internal IEnumerable<Coordinate> GetNeighbors(Grid grid)
        {
            return GetNeighbors(grid, 1, 1);
        }

        internal IEnumerable<Coordinate> GetNeighbors(Grid grid, int minDistance, int cheatDistance)
        {
            for (int dy = -cheatDistance; dy <= cheatDistance; ++dy)
            {
                for (int dx = -cheatDistance; dx <= cheatDistance; ++dx)
                {
                    int distance = Math.Abs(dx) + Math.Abs(dy);
                    if (distance >= minDistance && distance <= cheatDistance)
                    {
                        if (grid.GetContent(X + dx, Y + dy) != '#') yield return new Coordinate(X + dx, Y + dy);
                    }
                }
            }
        }

        public override int GetHashCode()
        {
            return 97 * X + 83 * Y;
        }

        public override bool Equals(object obj)
        {
            return obj is Coordinate && Equals((Coordinate)obj);
        }

        public bool Equals(Coordinate c)
        {
            return X == c.X && Y == c.Y;
        }

        internal int DistanceTo(Coordinate cheatTarget)
        {
            return Math.Abs(cheatTarget.X - X) + Math.Abs(cheatTarget.Y - Y);
        }

        public static bool operator ==(Coordinate obj1, Coordinate obj2)
        {
            if (ReferenceEquals(obj1, obj2))
                return true;
            if (ReferenceEquals(obj1, null))
                return false;
            if (ReferenceEquals(obj2, null))
                return false;
            return obj1.Equals(obj2);
        }

        public static bool operator !=(Coordinate obj1, Coordinate obj2) => !(obj1 == obj2);
    }
}