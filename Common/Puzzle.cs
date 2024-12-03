namespace Common
{
    public abstract class Puzzle
    {
        public void run()
        {
            StreamReader sample = new StreamReader("..\\..\\..\\data\\" + GetSampleFile());
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

        virtual public string GetSampleFile()
        {
            return "sample.txt";
        }

        public abstract long GetSampleSolution();

        public abstract long Solve(StreamReader input);
    }
}