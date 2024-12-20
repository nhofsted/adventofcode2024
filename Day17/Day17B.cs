namespace Day17
{
    internal class Day17B : Day17
    {
        // This only works if the last 3 bits are discarded after every output and the output
        // is influenced by A and at most the previous 10 bits (which - luckily - is also true
        // for the sample)

        override public long GetSampleSolution()
        {
            return 117440;
        }

        override public string GetSampleFile()
        {
            return "sampleB.txt";
        }

        override protected long Solve(int registerA, int registerB, int registerC, int[] program)
        {
            for (int i = 0; i < 0b1111111111; i++)
            {
                List<int> bits = new List<int>();
                if (Generate(program, program.Length - 1, i, bits))
                {
                    long result = i;
                    result <<= 3 * bits.Count;
                    result |= toNumber(bits);
                    return result;
                }
            }
            throw new Exception("No solution found");
        }

        private bool Generate(int[] program, int v, long prefix, List<int> bits)
        {
            if (v < 0) return true;

            for (int i = 0; i < 8; ++i)
            {
                long startValue = prefix;
                startValue <<= 3;
                startValue |= (long)i;
                Computer c = new Computer(startValue, 0, 0, program);
                bool match = false;
                c.Run(o => { match = (o == program[v]); return false; });
                if (match)
                {
                    bits.Add(i);
                    if (Generate(program, v - 1, startValue, bits))
                    {
                        return true;
                    };
                    bits.RemoveAt(bits.Count - 1);
                }
            }
            return false;
        }

        private long toNumber(List<int> triplets)
        {
            long result = 0;
            for (int i = 0; i < triplets.Count; ++i)
            {
                result <<= 3;
                result |= (long)triplets[i];
            }
            return result;
        }
    }
}