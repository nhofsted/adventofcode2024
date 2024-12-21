namespace Day19
{
    internal class Day19A : Day19
    {
        override public long GetSampleSolution()
        {
            return 6;
        }

        override protected long Match(string pattern, string[] towels, Dictionary<string, long> memory)
        {
            if (pattern.Length == 0) return 1;
            if (memory.TryGetValue(pattern, out long result)) return result;
            foreach (string towel in towels)
            {
                if (pattern.StartsWith(towel) && Match(pattern.Substring(towel.Length), towels, memory) == 1)
                {
                    memory[pattern] = 1;
                    return 1;
                }
            }
            memory[pattern] = 0;
            return 0;
        }
    }
}