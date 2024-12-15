using Common;

namespace Day15
{
    public abstract class Day15 : Puzzle
    {
        public override long Solve(StreamReader input, bool sample)
        {
            Tuple<int, int> lanternfish = new Tuple<int, int>(0, 0);
            char[][] grid = ReadGrid(input, ref lanternfish);
            char[] movements = ReadMovements(input);

            foreach (char move in movements)
            {
                lanternfish = ConditionalMove(grid, lanternfish, move);
            }

            return GPSSum(grid);
        }

        private long GPSSum(char[][] grid)
        {
            long score = 0;
            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[y].Length; x++)
                {
                    if (grid[y][x] == 'O' || grid[y][x] == '[')
                    {
                        score += 100 * y + x;
                    }
                }
            }
            return score;
        }

        private Tuple<int, int> ConditionalMove(char[][] grid, Tuple<int, int> item, char move)
        {
            if (CanMove(grid, item, move))
            {
                return Move(grid, item, move);
            }
            return item;
        }

        private bool CanMove(char[][] grid, Tuple<int, int> item, char move)
        {
            Tuple<int, int> destination = GetDestination(item, move);
            char destinationItem = grid[destination.Item2][destination.Item1];
            if (destinationItem == '#')
            {
                return false;
            }
            else if ((move == '^' || move == 'v') && (destinationItem == '[' || destinationItem == ']'))
            {
                int companionOffset = destinationItem == '[' ? 1 : -1;
                Tuple<int, int> otherItem = new Tuple<int, int>(item.Item1 + companionOffset, item.Item2);
                Tuple<int, int> otherDestination = new Tuple<int, int>(destination.Item1 + companionOffset, destination.Item2);
                return (destinationItem == '.' || CanMove(grid, destination, move)) &&
                    (grid[otherDestination.Item2][otherDestination.Item1] == '.' || CanMove(grid, otherDestination, move));
            }
            else if (destinationItem == '.' || CanMove(grid, destination, move))
            {
                return true;
            }
            return false;
        }

        private Tuple<int, int> Move(char[][] grid, Tuple<int, int> item, char move)
        {
            Tuple<int, int> destination = GetDestination(item, move);
            char gridItem = grid[item.Item2][item.Item1];
            if ((move == '^' || move == 'v') && (gridItem == '[' || gridItem == ']'))
            {
                int companionOffset = gridItem == '[' ? 1 : -1;
                Tuple<int, int> otherItem = new Tuple<int, int>(item.Item1 + companionOffset, item.Item2);
                Tuple<int, int> otherDestination = new Tuple<int, int>(destination.Item1 + companionOffset, destination.Item2);
                if (grid[destination.Item2][destination.Item1] != '.')
                {
                    Move(grid, destination, move);
                }
                if (grid[otherDestination.Item2][otherDestination.Item1] != '.')
                {
                    Move(grid, otherDestination, move);
                }
                grid[destination.Item2][destination.Item1] = gridItem;
                grid[otherDestination.Item2][otherDestination.Item1] = grid[otherItem.Item2][otherItem.Item1];
                grid[item.Item2][item.Item1] = '.';
                grid[otherItem.Item2][otherItem.Item1] = '.';
            }
            else
            {
                if (grid[destination.Item2][destination.Item1] != '.')
                {
                    Move(grid, destination, move);
                }
                grid[destination.Item2][destination.Item1] = gridItem;
                grid[item.Item2][item.Item1] = '.';
            }
            return destination;
        }

        private static Tuple<int, int> GetDestination(Tuple<int, int> item, char move)
        {
            Tuple<int, int> destination = null;
            switch (move)
            {
                case '<': destination = new Tuple<int, int>(item.Item1 - 1, item.Item2); break;
                case '>': destination = new Tuple<int, int>(item.Item1 + 1, item.Item2); break;
                case '^': destination = new Tuple<int, int>(item.Item1, item.Item2 - 1); break;
                case 'v': destination = new Tuple<int, int>(item.Item1, item.Item2 + 1); break;
                default: throw new Exception();
            }

            return destination;
        }

        private static char[] ReadMovements(StreamReader input)
        {
            string? line;
            string movementsBuilder = "";
            while ((line = input.ReadLine()) != null)
            {
                movementsBuilder += line;
            }
            return movementsBuilder.ToArray();
        }

        virtual protected char[][] ReadGrid(StreamReader input, ref Tuple<int, int> lanternfish)
        {
            List<char[]> listBuilder = new List<char[]>();
            string? line = null;
            while ((line = input.ReadLine()) != "")
            {
                int x = 0;
                if ((x = line.IndexOf('@')) != -1)
                {
                    lanternfish = new Tuple<int, int>(x, listBuilder.Count);
                }
                listBuilder.Add(line.ToCharArray());
            }
            return listBuilder.ToArray();
        }

        static void Main(string[] args)
        {
            new Day15A().run();
            new Day15B().run();
        }
    }
}