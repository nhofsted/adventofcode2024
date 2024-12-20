using Common;

namespace Day18
{
    public abstract class Day18 : Puzzle
    {
        public override long Solve(StreamReader input, bool sample)
        {
            List<int[]> coordinates = new List<int[]>();
            string? block = null;
            while ((block = input.ReadLine()) != null)
            {
                coordinates.Add(Array.ConvertAll<string, int>(block.Split(','), s => int.Parse(s)));
            }

            return Solve(sample, coordinates);
        }

        protected abstract long Solve(bool sample, List<int[]> coordinates);

        protected long FindShortestPath(bool sample, List<int[]> coordinates, int iterations)
        {
            int size = sample ? 7 : 71;

            List<bool[]> gridBuilder = new List<bool[]>();
            for (int i = 0; i < size; ++i)
            {
                gridBuilder.Add(new bool[size]);
            }
            bool[][] grid = gridBuilder.ToArray();

            for (int i = 0; i < iterations; ++i)
            {
                grid[coordinates[i][1]][coordinates[i][0]] = true;
            }

            return FindShortestPath(grid, new Node(0, 0, null, 0), (Node n) => 2 * size - n.X - n.Y);
        }

        protected int FindShortestPath(bool[][] grid, Node pos, Func<Node, int> heuristic)
        {
            PriorityQueue<Node, int> toVisit = new PriorityQueue<Node, int>();
            Dictionary<Tuple<int, int>, int> visited = new Dictionary<Tuple<int, int>, int>();
            toVisit.Enqueue(pos, heuristic(pos));

            while ((pos.X != grid.Length - 1) || (pos.Y != grid.Length - 1))
            {
                foreach (Node n in pos.GetChildren(grid))
                {
                    Tuple<int, int> p = new Tuple<int, int>(n.X, n.Y);
                    if (!visited.TryGetValue(p, out int distance) || distance > n.Distance) toVisit.Enqueue(n, n.Distance + heuristic(n));
                }
                if (toVisit.Count == 0) return -1;
                visited[new Tuple<int, int>(pos.X, pos.Y)] = pos.Distance;
                pos = toVisit.Dequeue();
            }

            return pos.Distance;
        }

        static void Main(string[] args)
        {
            new Day18A().run();
            new Day18B().run();
        }
    }
}