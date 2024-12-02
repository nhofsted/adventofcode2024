namespace Day1
{
    public class Day1B : Day1
    {
        override public long GetSampleSolution()
        {
            return 31;
        }

        override protected long Calculate(List<int> left, List<int> right)
        {
            right.Sort();
            Dictionary<int, int> counts = new Dictionary<int, int>();

            int lastValue = 0;
            int count = 0;
            if (right.Count > 0)
            {
                lastValue = right[0];
                count = 1;
            }
            for (int i = 1; i < right.Count; i++)
            {
                int currentValue = right[i];
                if (currentValue != lastValue)
                {
                    counts.Add(lastValue, count);
                    lastValue = currentValue;
                    count = 0;
                }
                count++;
            }
            if (right.Count > 0)
            {
                counts.Add(lastValue, count);
            }

            long result = 0;
            for (int i = 0; i < left.Count; i++)
            {
                int number = left[i];
                if (counts.ContainsKey(number))
                {
                    result += left[i] * counts[number];
                }
            }

            return result;
        }
    }
}