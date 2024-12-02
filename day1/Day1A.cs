namespace Day1
{
    public class Day1A : Day1
    {
        override public long GetSampleSolution()
        {
            return 11;
        }

        override protected long Calculate(List<int> left, List<int> right)
        {
            left.Sort();
            right.Sort();

            long result = 0;
            int lines = 0;
            List<int>.Enumerator leftEnumerator = left.GetEnumerator();
            List<int>.Enumerator rightEnumerator = right.GetEnumerator();
            do
            {
                result += Math.Abs(rightEnumerator.Current - leftEnumerator.Current);
                lines++;
            } while (leftEnumerator.MoveNext() && rightEnumerator.MoveNext());

            return result;
        }
    }
}