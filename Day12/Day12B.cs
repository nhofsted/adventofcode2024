namespace Day12
{
    internal class Day12B : Day12
    {
        override public long GetSampleSolution()
        {
            return 1206;
        }

        override protected long CalculatePrice(int[][] igrid)
        {
            Dictionary<int, int> areas = new Dictionary<int, int>();
            Dictionary<int, int> corners = new Dictionary<int, int>();
            // Corners + Area
            for (int y = -1; y < igrid.Length; ++y)
            {
                int currentType = -1;
                for (int x = -1; x < igrid[0].Length; ++x)
                {
                    int typeA = GetType(igrid, y, x);
                    int typeB = GetType(igrid, y, x + 1);
                    int typeC = GetType(igrid, y + 1, x);
                    int typeD = GetType(igrid, y + 1, x + 1);
                    // Area
                    if (typeA != -1) AddOne(areas, typeA);
                    // IsCorner
                    if (typeA != -1 && (typeA != typeB && typeA != typeC || typeA == typeB && typeA == typeC && typeA != typeD)) AddOne(corners, typeA);
                    if (typeB != -1 && (typeB != typeA && typeB != typeD || typeB == typeA && typeB == typeD && typeB != typeC)) AddOne(corners, typeB);
                    if (typeC != -1 && (typeC != typeD && typeC != typeA || typeC == typeD && typeC == typeA && typeC != typeB)) AddOne(corners, typeC);
                    if (typeD != -1 && (typeD != typeC && typeD != typeB || typeD == typeC && typeD == typeB && typeD != typeA)) AddOne(corners, typeD);
                }
            }

            long retVal = 0;
            foreach (char type in areas.Keys)
            {
                retVal += areas[type] * corners[type];
            }
            return retVal;
        }

        private int GetType(int[][] igrid, int y, int x)
        {
            if (y < 0 || y >= igrid.Length) return -1;
            if (x < 0 || x >= igrid[y].Length) return -1;
            return igrid[y][x];
        }
    }
}