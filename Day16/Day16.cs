using Common;

namespace Day16
{
    public abstract class Day16 : Puzzle
    {
        public override long Solve(StreamReader input, bool sample)
        {
            Position? reindeer = null;
            Position? end = null;
            char[][] grid = ReadGrid(input, ref reindeer, ref end);

            PathNode path = new PathNode(reindeer, null, 0);
            Func<PathNode, int> heuristic = (PathNode path) => Math.Abs(path.X - end.X) + Math.Abs(path.Y - end.Y);

            PriorityQueue<PathNode, long> openNodes = new PriorityQueue<PathNode, long>();
            HashSet<PathNode> visitedPositions = new HashSet<PathNode>();
            HashSet<Tuple<int, int>> visitedCoordinates = new HashSet<Tuple<int, int>>();
            long leastCost = long.MaxValue;
            while (path.Cost <= leastCost)
            {
                if (path.X == end.X && path.Y == end.Y)
                {
                    leastCost = path.Cost;
                    PathNode iterator = path;
                    while (iterator != null)
                    {
                        visitedCoordinates.Add(new Tuple<int, int>(iterator.X, iterator.Y));
                        iterator = iterator.Parent;
                    }
                }
                visitedPositions.Add(path);
                foreach (PathNode child in path.Children(grid))
                {
                    if (!visitedPositions.TryGetValue(child, out PathNode previousPosition) || previousPosition.Cost >= child.Cost) openNodes.Enqueue(child, child.Cost + heuristic(child));
                }
                path = openNodes.Dequeue();
            }
            return Score(leastCost, visitedCoordinates);
        }

        protected abstract long Score(long leastCost, HashSet<Tuple<int, int>> visitedCoordinates);

        private static char[][] ReadGrid(StreamReader input, ref Position? start, ref Position? end)
        {
            List<char[]> listBuilder = new List<char[]>();
            string? line = null;
            while ((line = input.ReadLine()) != null)
            {
                int x = 0;
                if ((x = line.IndexOf('S')) != -1)
                {
                    start = new Position(x, listBuilder.Count, Direction.EAST);
                }
                if ((x = line.IndexOf('E')) != -1)
                {
                    end = new Position(x, listBuilder.Count, Direction.EAST);
                }
                listBuilder.Add(line.ToCharArray());
            }
            return listBuilder.ToArray();
        }

        static void Main(string[] args)
        {
            new Day16A().run();
            new Day16B().run();
        }
    }
}