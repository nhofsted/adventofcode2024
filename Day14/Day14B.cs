using System.Text.RegularExpressions;

namespace Day14
{
    internal class Day14B : Day14
    {
        class Robot
        {
            long x0;
            long y0;
            long vx;
            long vy;
            long width;
            long height;

            public Robot(long x0, long y0, long vx, long vy, long width, long height)
            {
                this.x0 = x0;
                this.y0 = y0;
                this.vx = vx;
                this.vy = vy;
                this.width = width;
                this.height = height;
            }

            public long GetXPos(long steps)
            {
                return ((x0 + steps * vx) % width + width) % width;
            }

            public long GetYPos(long steps)
            {
                return ((y0 + steps * vy) % height + height) % height;
            }
        }

        override public long GetSampleSolution()
        {
            return 31;
        }

        public override long Solve(StreamReader input, bool sample)
        {
            Regex numbers = new Regex("[^-0-9]*([-0-9]+)[^-0-9]*([-0-9]+)[^-0-9]*([-0-9]+)[^-0-9]*([-0-9]+)");

            long width = GetWidth(sample);
            long height = GetHeight(sample);

            List<Robot> robots = new List<Robot>();
            string? line = null;
            while ((line = input.ReadLine()) != null)
            {
                Match match = numbers.Match(line);
                long x0 = long.Parse(match.Groups[1].Value);
                long y0 = long.Parse(match.Groups[2].Value);
                long vx = long.Parse(match.Groups[3].Value);
                long vy = long.Parse(match.Groups[4].Value);

                robots.Add(new Robot(x0, y0, vx, vy, width, height));
            }

            long minVariance = long.MaxValue;
            long minVarianceStep = -1;
            for (int step = 0; step < width * height; ++step)
            {
                // Calculate Average
                long sumX = 0;
                long sumY = 0;
                foreach (Robot r in robots)
                {
                    sumX += r.GetXPos(step);
                    sumY += r.GetYPos(step);
                }
                long avgX = sumX / robots.Count;
                long avgY = sumY / robots.Count;

                // Calculate Clustering Heuristic (Variance-ish)
                long variance = 0;
                foreach (Robot r in robots)
                {
                    long xDiff = r.GetXPos(step) - avgX;
                    long yDiff = r.GetYPos(step) - avgY;
                    variance += xDiff * xDiff;
                    variance += yDiff * yDiff;
                }

                // Remember lowest variance
                if (variance < minVariance)
                {
                    minVariance = variance;
                    minVarianceStep = step;
                }
            }

            PrintPositions(width, height, robots, minVarianceStep);

            return minVarianceStep;
        }

        private static void PrintPositions(long width, long height, List<Robot> robots, long minVarianceStep)
        {
            char[][] grid = new char[height][];
            for (int y = 0; y < height; ++y)
            {
                grid[y] = new char[width];
                for (int x = 0; x < width; ++x)
                {
                    grid[y][x] = '.';
                }
            }
            foreach (Robot r in robots)
            {
                grid[(int)r.GetYPos(minVarianceStep)][(int)r.GetXPos(minVarianceStep)] = 'X';
            }
            Console.WindowHeight = 130;
            Console.WindowWidth = 120;
            for (int l = 0; l < grid.Length; ++l)
            {
                string s = new(grid[l]);
                Console.WriteLine(s);
            }
        }
    }
}