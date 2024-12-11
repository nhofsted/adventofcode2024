using Common;

namespace Day10
{
    public abstract class Day10 : Puzzle
    {
        public override long Solve(StreamReader input)
        {
            int[][] map;
            string? line = null;
            List<int[]> mapBuilder = new List<int[]>();
            while ((line = input.ReadLine()) != null)
            {
                mapBuilder.Add(Array.ConvertAll<char, int>(line.ToCharArray(), c => c - '0'));
            }
            map = mapBuilder.ToArray();

            long result = 0;
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    result += Rate(GetTrailEnds(map, y, x, 0));
                }
            }

            return result;
        }

        protected abstract long Rate(IEnumerable<Tuple<int, int>> enumerable);

        private IEnumerable<Tuple<int, int>> GetTrailEnds(int[][] map, int y, int x, int h)
        {
            if (map[y][x] == h)
            {
                if (h == 9)
                {
                    return new Tuple<int, int>[] { new Tuple<int, int>(y, x) };
                }
                else
                {
                    List<Tuple<int, int>> trailEnds = new List<Tuple<int, int>>();
                    if (y > 0) trailEnds.AddRange(GetTrailEnds(map, y - 1, x, h + 1));
                    if (x < map[y].Length - 1) trailEnds.AddRange(GetTrailEnds(map, y, x + 1, h + 1));
                    if (y < map.Length - 1) trailEnds.AddRange(GetTrailEnds(map, y + 1, x, h + 1));
                    if (x > 0) trailEnds.AddRange(GetTrailEnds(map, y, x - 1, h + 1));
                    return trailEnds;
                }
            }
            else
            {
                return Enumerable.Empty<Tuple<int, int>>();
            }
        }

        static void Main(string[] args)
        {
            new Day10A().run();
            new Day10B().run();
        }
    }
}