namespace Day7
{
    internal class Day7B : Day7A
    {
        override public long GetSampleSolution()
        {
            return 11387;
        }

        override public Func<long, long, long>[] GetOperations()
        {
            return new Func<long, long, long>[]
            {
                Multiply,
                Concatenate,
                Add
            };
        }

        protected long Concatenate(long a, long b)
        {
            return long.Parse(a.ToString() + b.ToString());
        }
    }
}