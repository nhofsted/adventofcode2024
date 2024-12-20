using Common;

namespace Day17
{
    public abstract class Day17 : Puzzle
    {
        public override long Solve(StreamReader input, bool sample)
        {
            int registerA = int.Parse(input.ReadLine().Substring(12));
            int registerB = int.Parse(input.ReadLine().Substring(12));
            int registerC = int.Parse(input.ReadLine().Substring(12));
            input.ReadLine();
            int[] program = Array.ConvertAll<string, int>(input.ReadLine().Substring(9).Split(','), s => int.Parse(s));

            return Solve(registerA, registerB, registerC, program);
        }

        abstract protected long Solve(int registerA, int registerB, int registerC, int[] program);

        static void Main(string[] args)
        {
            new Day17A().run();
            new Day17B().run();
        }
    }
}