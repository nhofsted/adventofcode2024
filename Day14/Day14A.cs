using System.Text.RegularExpressions;

namespace Day14
{
    internal class Day14A : Day14
    {
        override public long GetSampleSolution()
        {
            return 12;
        }

        public override long Solve(StreamReader input, bool sample)
        {
            Regex numbers = new Regex("[^-0-9]*([-0-9]+)[^-0-9]*([-0-9]+)[^-0-9]*([-0-9]+)[^-0-9]*([-0-9]+)");

            long width = GetWidth(sample);
            long height = GetHeight(sample);

            long topleft = 0;
            long topright = 0;
            long bottomleft = 0;
            long bottomright = 0;

            string? line = null;
            while ((line = input.ReadLine()) != null)
            {
                Match match = numbers.Match(line);
                long x0 = long.Parse(match.Groups[1].Value);
                long y0 = long.Parse(match.Groups[2].Value);
                long vx = long.Parse(match.Groups[3].Value);
                long vy = long.Parse(match.Groups[4].Value);

                long xe = ((x0 + 100 * vx) % width + width) % width;
                long ye = ((y0 + 100 * vy) % height + height) % height;

                bool top = ye < height / 2;
                bool bottom = ye > height / 2;
                bool left = xe < width / 2;
                bool right = xe > width / 2;

                if (top && left) topleft++;
                if (top && right) topright++;
                if (bottom && left) bottomleft++;
                if (bottom && right) bottomright++;
            }

            return topleft * topright * bottomleft * bottomright;
        }
    }
}