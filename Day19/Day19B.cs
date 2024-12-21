namespace Day19
{
    internal class Day19B : Day19
    {
        override public long GetSampleSolution()
        {
            return 16;
        }

        override protected long Match(string pattern, string[] towels, Dictionary<string, long> memory)
        {
            if (pattern.Length == 0) return 1;
            if (memory.TryGetValue(pattern, out long result)) return result;
            long matches = 0;
            foreach (string towel in towels)
            {
                if (pattern.StartsWith(towel))
                {
                    matches += Match(pattern.Substring(towel.Length), towels, memory);
                }
            }
            memory[pattern] = matches;
            return matches;
        }
    }
}