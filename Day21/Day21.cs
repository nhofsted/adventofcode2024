using Common;

namespace Day20
{
    public abstract class Day21 : Puzzle
    {
        private Dictionary<char, Tuple<int, int>> numpadCoordinates = InitializeNumpadCoordinates();
        private Dictionary<char, Tuple<int, int>> dirpadCoordinates = InitializeDirpadCoordinates();

        public override long Solve(StreamReader input, bool sample)
        {
            List<Dictionary<char, Tuple<int, int>>> transforms = new List<Dictionary<char, Tuple<int, int>>>();
            transforms.Add(numpadCoordinates);
            for (int i = 0; i < GetNumberOfDirectionalKeypads(); ++i)
            {
                transforms.Add(dirpadCoordinates);
            }

            long retVal = 0;
            string? target = null;
            while ((target = input.ReadLine()) != null)
            {
                long length = CalculateMoveLength(target, transforms, 0, new Dictionary<Tuple<string, int>, long>());
                int numPart = extractNumpart(target);
                retVal += length * numPart;
            }

            return retVal;
        }

        protected abstract int GetNumberOfDirectionalKeypads();

        private long CalculateMoveLength(string target, List<Dictionary<char, Tuple<int, int>>> transforms, int i, Dictionary<Tuple<string, int>, long> memory)
        {
            if(memory.TryGetValue(new Tuple<string, int>(target, i), out long value)) return value;
            if (i == transforms.Count) return target.Length;
            Dictionary<char, Tuple<int, int>> coords = transforms[i];
            char pos = 'A';
            long moveLength = 0;
            foreach (char c in target)
            {
                long minMoveLength = long.MaxValue;
                string minMoves = "";
                foreach (string moves in CalculateMoves(coords, pos, c))
                {
                    long possibleLength = CalculateMoveLength(moves, transforms, i + 1, memory);
                    if (possibleLength < minMoveLength)
                    {
                        minMoveLength = possibleLength;
                        minMoves = moves;
                    }
                }
                moveLength += minMoveLength;
                pos = c;
            }

            memory[new Tuple<string, int>(target, i)] = moveLength;
            return moveLength;
        }

        private List<string> CalculateMoves(Dictionary<char, Tuple<int, int>> coords, char pos, char c)
        {
            Tuple<int, int> start = coords[pos];
            Tuple<int, int> end = coords[c];
            int dx = end.Item1 - start.Item1;
            int dy = end.Item2 - start.Item2;

            // Not sure which is faster, but alternating is always worse
            List<string> options = new List<string>();

            string moves = "";
            // vertical first
            if (start.Item1 != 0 || end.Item2 != coords['A'].Item2)
            {
                if (dy > 0) moves += new String('v', dy);
                else moves += new String('^', -dy);
                if (dx > 0) moves += new String('>', dx);
                else moves += new String('<', -dx);
                moves += 'A';
                options.Add(moves);
            }

            // horizontal first
            if (end.Item1 != 0 || start.Item2 != coords['A'].Item2)
            {
                moves = "";
                if (dx > 0) moves += new String('>', dx);
                else moves += new String('<', -dx);
                if (dy > 0) moves += new String('v', dy);
                else moves += new String('^', -dy);
                moves += 'A';
                options.Add(moves);
            }

            return options;
        }

        private int extractNumpart(string target)
        {
            int number = 0;
            foreach (char ch in target)
            {
                if (Char.IsDigit(ch))
                {
                    number *= 10;
                    number += ch - '0';
                }
            }
            return number;
        }

        private static Dictionary<char, Tuple<int, int>> InitializeNumpadCoordinates()
        {
            Dictionary<char, Tuple<int, int>> numpadCoordinates = new Dictionary<char, Tuple<int, int>>();
            numpadCoordinates['7'] = new Tuple<int, int>(0, 0);
            numpadCoordinates['8'] = new Tuple<int, int>(1, 0);
            numpadCoordinates['9'] = new Tuple<int, int>(2, 0);
            numpadCoordinates['4'] = new Tuple<int, int>(0, 1);
            numpadCoordinates['5'] = new Tuple<int, int>(1, 1);
            numpadCoordinates['6'] = new Tuple<int, int>(2, 1);
            numpadCoordinates['1'] = new Tuple<int, int>(0, 2);
            numpadCoordinates['2'] = new Tuple<int, int>(1, 2);
            numpadCoordinates['3'] = new Tuple<int, int>(2, 2);
            numpadCoordinates['0'] = new Tuple<int, int>(1, 3);
            numpadCoordinates['A'] = new Tuple<int, int>(2, 3);
            return numpadCoordinates;
        }

        private static Dictionary<char, Tuple<int, int>> InitializeDirpadCoordinates()
        {
            Dictionary<char, Tuple<int, int>> dirpadCoordinates = new Dictionary<char, Tuple<int, int>>();
            dirpadCoordinates['^'] = new Tuple<int, int>(1, 0);
            dirpadCoordinates['A'] = new Tuple<int, int>(2, 0);
            dirpadCoordinates['<'] = new Tuple<int, int>(0, 1);
            dirpadCoordinates['v'] = new Tuple<int, int>(1, 1);
            dirpadCoordinates['>'] = new Tuple<int, int>(2, 1);
            return dirpadCoordinates;
        }

        static void Main(string[] args)
        {
            new Day21A().run();
            new Day21B().run();
        }
    }
}