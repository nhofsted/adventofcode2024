namespace Day4
{
    internal class Day4B : Day4
    {
        override public long GetSampleSolution()
        {
            return 9;
        }

        protected override long findPattern()
        {
            long result = 0;
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    if (getLetter(x, y) != 'A') continue;

                    char tl = getLetter(x - 1, y - 1);
                    char tr = getLetter(x + 1, y - 1);
                    char bl = getLetter(x - 1, y + 1);
                    char br = getLetter(x + 1, y + 1);

                    if ((tl != 'M' || br != 'S') && (tl != 'S' || br != 'M') || (tr != 'M' || bl != 'S') && (tr != 'S' || bl != 'M')) continue;

                    result++;
                }
            }
            return result;
        }
    }
}