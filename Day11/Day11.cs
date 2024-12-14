using Common;

namespace Day11
{
    public abstract class Day11 : Puzzle
    {
        class MemoryKeyComparer : EqualityComparer<Tuple<string, int>>
        {
            public override bool Equals(Tuple<string, int> x, Tuple<string, int> y)
            {
                return System.Collections.StructuralComparisons.StructuralEqualityComparer
                    .Equals(x, y);
            }

            public override int GetHashCode(Tuple<string, int> obj)
            {
                return System.Collections.StructuralComparisons.StructuralEqualityComparer
                    .GetHashCode(obj);
            }
        }

        Dictionary<Tuple<string, int>, long> memory = new Dictionary<Tuple<string, int>, long>(new MemoryKeyComparer());

        public override long Solve(StreamReader input, bool sample)
        {

            long retVal = 0;
            List<string> stones = new List<string>(input.ReadLine().Split(' '));
            foreach (string stone in stones)
            {
                retVal += CountStones(stone, GetNumberOfBlinks());
            }
            return retVal;
        }

        abstract protected int GetNumberOfBlinks();

        private long CountStones(string stone, int blinks)
        {
            if (blinks == 0) return 1;

            Tuple<string, int> memoryKey = new Tuple<string, int>(stone, blinks);
            long value = 0;
            if (memory.TryGetValue(memoryKey, out value)) return value;

            foreach (string mutatedStone in Mutate(stone))
            {
                value += CountStones(mutatedStone, blinks - 1);
            }
            memory.Add(memoryKey, value);
            return value;
        }

        protected IEnumerable<string> Mutate(string stone)
        {
            if (stone == "0") return ["1"];
            if (stone.Length % 2 == 0) return [stone.Substring(0, stone.Length / 2), long.Parse(stone.Substring(stone.Length / 2)).ToString()];
            return [(2024 * long.Parse(stone)).ToString()];
        }

        static void Main(string[] args)
        {
            new Day11A().run();
            new Day11B().run();
        }
    }
}