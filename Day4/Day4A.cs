namespace Day4
{
    internal class Day4A : Day4
    {
        override public long GetSampleSolution()
        {
            return 18;
        }

        protected override long findPattern()
        {
            char[] search = "XMAS".ToCharArray();
            long result = 0;
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    for (int ydir = -1; ydir <= 1; ++ydir)
                    {
                        for (int xdir = -1; xdir <= 1; ++xdir)
                        {
                            if (ydir == 0 && xdir == 0) continue;
                            for (int c = 0; c < search.Length; ++c)
                            {
                                if (getLetter(x + c * xdir, y + c * ydir) != search[c]) goto OUTER;
                            }
                            result++;
                        OUTER:;
                        }
                    }
                }
            }

            return result;
        }
    }
}