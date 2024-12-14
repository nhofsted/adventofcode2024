using Common;

namespace Day7
{
    public abstract class Day7 : Puzzle
    {
        public override long Solve(StreamReader input, bool sample)
        {
            long result = 0;
            string? line;
            while ((line = input.ReadLine()) != null)
            {
                string[] testValues = line.Split(": ");
                long testValue = long.Parse(testValues[0]);
                long[] numbers = Array.ConvertAll<string, long>(testValues[1].Split(' '), s => long.Parse(s));
                long subResult = numbers[0];
                result += CreateTestValue(testValue, numbers, subResult, 1);
            }
            return result;
        }

        private long CreateTestValue(long testValue, long[] numbers, long subResult, int depth)
        {
            // stop
            if (depth == numbers.Length)
            {
                if (subResult == testValue) return testValue;
                else return 0;
            }

            // return early
            if (subResult > testValue) return 0;

            Func<long, long, long>[] operations = GetOperations();
            foreach (Func<long, long, long> operation in operations)
            {
                long result = CreateTestValue(testValue, numbers, operation(subResult, numbers[depth]), depth + 1);
                if (result != 0) return result;
            }

            return 0;
        }
        abstract public Func<long, long, long>[] GetOperations();

        static void Main(string[] args)
        {
            new Day7A().run();
            new Day7B().run();
        }
    }
}