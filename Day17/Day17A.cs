namespace Day17
{
    internal class Day17A : Day17
    {
        override public long GetSampleSolution()
        {
            return 0;
        }

        override protected long Solve(int registerA, int registerB, int registerC, int[] program)
        {
            Computer c = new Computer(registerA, registerB, registerC, program);
            List<int> output = new List<int>();
            c.Run(i => { output.Add(i); return true; });
            Console.WriteLine(String.Join(',', output));
            return 0;
        }
    }
}