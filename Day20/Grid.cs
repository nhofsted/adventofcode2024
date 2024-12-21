namespace Day20
{
    public class Grid
    {
        private char[][] data;

        public Grid(char[][] data)
        {
            this.data = data;
        }

        public char GetContent(int x, int y)
        {
            if (x < 0 || y < 0 || y > data.Length - 1 || x > data[y].Length - 1) return '#';
            return data[y][x];
        }

        public static Grid BuildGrid(StreamReader input, ref Coordinate? start, ref Coordinate? end)
        {
            List<char[]> gridBuilder = new List<char[]>();
            int y = 0;
            string? line = null;
            while ((line = input.ReadLine()) != null)
            {
                int x = 0;
                if ((x = line.IndexOf('S')) != -1)
                {
                    start = new Coordinate(x, y);
                }
                if ((x = line.IndexOf('E')) != -1)
                {
                    end = new Coordinate(x, y);
                }
                gridBuilder.Add(line.ToCharArray());
                y++;
            }
            return new Grid(gridBuilder.ToArray());
        }
    }
}