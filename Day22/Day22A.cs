namespace Day22
{
    internal class Day22A : Day22
    {
        override public long GetSampleSolution()
        {
            return 37327623;
        }

        public override long Solve(StreamReader input, bool sample)
        {
            long retVal = 0;
            string? secretString = null;
            while ((secretString = input.ReadLine()) != null)
            {
                long secret = long.Parse(secretString);
                for (int i = 0; i < 2000; ++i)
                {
                    secret = GenerateNextRandom(secret);
                }
                retVal += secret;
            }

            return retVal;
        }
    }
}