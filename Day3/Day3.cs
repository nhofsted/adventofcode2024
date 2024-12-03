using Common;
using System.Text.RegularExpressions;

namespace Day3
{
    public abstract class Day3 : Puzzle
    {
        override public long Solve(StreamReader input)
        {
            Regex mul = new Regex("mul\\(([0-9]{1,3}),([0-9]{1,3})\\)|don't\\(\\)|do\\(\\)");
            long result = 0;
            string? line;
            bool interpretConditionals = GetInterpretConditionals();
            bool condition = true;
            while ((line = input.ReadLine()) != null)
            {
                MatchCollection matches = mul.Matches(line);
                for (int i = 0; i < matches.Count; i++)
                {
                    if (interpretConditionals && condition || !interpretConditionals)
                    {
                        if (matches[i].Value[0] == 'm')
                        {
                            result += int.Parse(matches[i].Groups[1].Value) * int.Parse(matches[i].Groups[2].Value);
                        }
                    }
                    if (interpretConditionals)
                    {
                        if (matches[i].Value.Equals("do()"))
                        {
                            condition = true;
                        }
                        else if (matches[i].Value.Equals("don't()"))
                        {
                            condition = false;
                        }
                    }
                }
            }
            return result;
        }

        public abstract bool GetInterpretConditionals();
        static void Main(string[] args)
        {
            new Day3A().run();
            new Day3B().run();
        }
    }
}