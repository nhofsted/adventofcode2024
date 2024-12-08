namespace Day7
{
    internal class Day7A : Day7
    {
        override public long GetSampleSolution()
        {
            return 3749;
        }
        override public Func<long, long, long>[] GetOperations()
        {
            return new Func<long, long, long>[]
            {
                Multiply,
                Add
            };
        }

        protected long Multiply(long a, long b)
        {
            return a * b;
        }

        protected long Add(long a, long b)
        {
            return a + b;
        }

    }
}