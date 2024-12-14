using Common;

namespace Day8
{
    public abstract class Day8 : Puzzle
    {
        public override long Solve(StreamReader input, bool sample)
        {
            int height = 0;
            int width = 0;
            Dictionary<char, List<Tuple<int, int>>> antennae = ParseInput(input, ref height, ref width);
            HashSet<Tuple<int, int>> antinodes = CalculateAntinodes(height, width, antennae);

            return antinodes.Count;
        }

        private HashSet<Tuple<int, int>> CalculateAntinodes(int height, int width, Dictionary<char, List<Tuple<int, int>>> antennae)
        {
            HashSet<Tuple<int, int>> antinodes = new HashSet<Tuple<int, int>>();

            foreach (char frequency in antennae.Keys)
            {
                List<Tuple<int, int>> frequencyAntennae = antennae[frequency];
                foreach (Tuple<int, int> antenna1 in frequencyAntennae)
                {
                    foreach (Tuple<int, int> antenna2 in frequencyAntennae)
                    {
                        if (antenna1 == antenna2) continue;
                        Tuple<int, int> distance = Subtract(antenna2, antenna1);
                        int[] bounds = GetHarmonicBounds();
                        int minHarmonic = bounds[0];
                        int maxHarmonic = bounds[1];
                        for (int c = minHarmonic; c <= maxHarmonic; c++)
                        {
                            Tuple<int, int> node = Multiply(c, distance);
                            Tuple<int, int> resonance = Add(antenna2, node);
                            if (OnMap(resonance, width, height))
                            {
                                antinodes.Add(resonance);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return antinodes;
        }

        abstract protected int[] GetHarmonicBounds();

        private static Dictionary<char, List<Tuple<int, int>>> ParseInput(StreamReader input, ref int height, ref int width)
        {
            Dictionary<char, List<Tuple<int, int>>> antennae = new Dictionary<char, List<Tuple<int, int>>>();
            string? line;
            while ((line = input.ReadLine()) != null)
            {
                for (int x = 0; x < line.Length; x++)
                {
                    char frequency = line[x];
                    if (frequency != '.')
                    {
                        if (!antennae.ContainsKey(line[x]))
                        {
                            antennae.Add(line[x], new List<Tuple<int, int>>());
                        }
                        antennae[line[x]].Add(new Tuple<int, int>(x, height));
                    }
                }
                height++;
                width = Math.Max(width, line.Length);
            }

            return antennae;
        }

        private bool OnMap(Tuple<int, int> position, int width, int height)
        {
            return position.Item1 >= 0 && position.Item2 >= 0 && position.Item1 < width && position.Item2 < height;
        }

        private Tuple<int, int> Add(Tuple<int, int> a, Tuple<int, int> b)
        {
            return new Tuple<int, int>(a.Item1 + b.Item1, a.Item2 + b.Item2);
        }

        private Tuple<int, int> Subtract(Tuple<int, int> a, Tuple<int, int> b)
        {
            return new Tuple<int, int>(a.Item1 - b.Item1, a.Item2 - b.Item2);
        }

        private Tuple<int, int> Multiply(int c, Tuple<int, int> a)
        {
            return new Tuple<int, int>(c * a.Item1, c * a.Item2);
        }

        static void Main(string[] args)
        {
            new Day8A().run();
            new Day8B().run();
        }
    }
}