namespace Day12
{
    internal class Day12A : Day12
    {
        override public long GetSampleSolution()
        {
            return 1930;
        }

        override protected long CalculatePrice(int[][] igrid)
        {
            Dictionary<int, int> areas = new Dictionary<int, int>();
            Dictionary<int, int> fences = new Dictionary<int, int>();
            // Horizontal + Area
            int currentType;
            for (int y = 0; y < igrid.Length; ++y)
            {
                currentType = -1;
                for (int x = 0; x < igrid[y].Length; ++x)
                {
                    int type = igrid[y][x];
                    if (type != currentType)
                    {
                        if (currentType != -1) AddOne(fences, currentType);
                        AddOne(fences, type);
                        currentType = type;
                    }
                    AddOne(areas, type);
                }
                AddOne(fences, currentType);
            }
            // Vertical
            for (int x = 0; x < igrid[0].Length; ++x)
            {
                currentType = -1;
                for (int y = 0; y < igrid.Length; ++y)
                {
                    int type = igrid[y][x];
                    if (type != currentType)
                    {
                        if (currentType != -1) AddOne(fences, currentType);
                        AddOne(fences, type);
                        currentType = type;
                    }
                }
                AddOne(fences, currentType);
            }

            long retVal = 0;
            foreach (char type in areas.Keys)
            {
                retVal += areas[type] * fences[type];
            }
            return retVal;
        }
    }
}