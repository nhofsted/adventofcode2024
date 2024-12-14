using Common;
using System.Text.RegularExpressions;

namespace Day13
{
    public abstract class Day13 : Puzzle
    {
        public override long Solve(StreamReader input, bool sample)
        {
            Regex numbers = new Regex("[^0-9]*([0-9]+)[^0-9]*([0-9]+)");
            long retVal = 0;

            string? line = null;
            while ((line = input.ReadLine()) != null)
            {
                Match match = numbers.Match(line);
                long A = int.Parse(match.Groups[1].Value);
                long D = int.Parse(match.Groups[2].Value);
                line = input.ReadLine();
                match = numbers.Match(line);
                long B = int.Parse(match.Groups[1].Value);
                long E = int.Parse(match.Groups[2].Value);
                line = input.ReadLine();
                match = numbers.Match(line);
                long C = GetOffset() + int.Parse(match.Groups[1].Value);
                long F = GetOffset() + int.Parse(match.Groups[2].Value);
                line = input.ReadLine();
                if (E * A != B * D)
                {
                    long X = (E * C - B * F) / (E * A - B * D);
                    long Y = (A * F - C * D) / (E * A - B * D);
                    if (A * X + B * Y == C && D * X + E * Y == F)
                    {
                        retVal += 3 * X;
                        retVal += Y;
                    }
                }
            }
            return retVal;
        }

        abstract protected long GetOffset();

        static void Main(string[] args)
        {
            new Day13A().run();
            new Day13B().run();
        }
    }
}