
namespace Day16
{
    class PathNode : Position, IEquatable<PathNode>
    {
        public PathNode Parent { get; }
        public long Cost { get; }

        public PathNode(Position position, PathNode parent, long cost) : base(position)
        {
            Parent = parent;
            Cost = cost;
        }

        internal IEnumerable<PathNode> Children(char[][] grid)
        {
            List<PathNode> children = new List<PathNode>(3);
            Position advancedPosition = Advance();
            if (grid[advancedPosition.Y][advancedPosition.X] != '#') children.Add(new PathNode(advancedPosition, this, Cost + 1));
            children.Add(new PathNode(TurnClockwise(), this, Cost + 1000));
            children.Add(new PathNode(TurnCounterClockwise(), this, Cost + 1000));
            return children;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(PathNode? other)
        {
            return base.Equals(other);
        }
    }
}