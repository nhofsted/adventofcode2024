namespace Common
{
    public abstract class Puzzle
    {
        public void run()
        {
            StreamReader sample = new StreamReader("..\\..\\..\\data\\sample.txt");
            long sampleResult = Solve(sample);
            Console.WriteLine("Sample solution: " + sampleResult);

            if (sampleResult == GetSampleSolution())
            {
                StreamReader data = new StreamReader("..\\..\\..\\data\\data.txt");
                Console.WriteLine("Puzzle solution: " + Solve(data));
            }
            else
            {
                Console.WriteLine("Sample solution " + sampleResult + " differs from expected " + GetSampleSolution());
            }
        }
        public abstract long GetSampleSolution();

        public abstract long Solve(StreamReader input);
    }
}