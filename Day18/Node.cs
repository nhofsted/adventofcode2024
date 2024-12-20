namespace Day18
{
    public class Node
    {
        public Node(int x, int y, Node parent, int distance)
        {
            X = x;
            Y = y;
            Parent = parent;
            Distance = distance;
        }

        public IEnumerable<Node> GetChildren(bool[][] grid)
        {
            List<Node> children = new List<Node>(4);
            if (X > 0 && !grid[Y][X - 1]) children.Add(new Node(X - 1, Y, this, Distance + 1));
            if (X < grid[Y].Length - 1 && !grid[Y][X + 1]) children.Add(new Node(X + 1, Y, this, Distance + 1));
            if (Y > 0 && !grid[Y - 1][X]) children.Add(new Node(X, Y - 1, this, Distance + 1));
            if (Y < grid.Length - 1 && !grid[Y + 1][X]) children.Add(new Node(X, Y + 1, this, Distance + 1));
            return children;
        }

        public int X { get; }
        public int Y { get; }
        public Node Parent { get; }
        public int Distance { get; internal set; }
    }
}