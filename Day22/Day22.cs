using Common;

namespace Day22
{
    public abstract class Day22 : Puzzle
    {
        protected long GenerateNextRandom(long secret)
        {
            secret ^= secret << 6;
            secret &= 0xffffff;
            secret ^= secret >> 5;
            secret ^= secret << 11;
            secret &= 0xffffff;
            return secret;
        }

        static void Main(string[] args)
        {
            //new Day22A().run();
            new Day22B().run();
        }
    }
}