namespace Day9
{
    internal class Day9A : Day9
    {
        override public long GetSampleSolution()
        {
            return 1928;
        }

        override protected long Defrag(string layout)
        {
            int freePointer = 0;
            int dataPointer = layout.Length;

            int emptyBuffer = 0;
            int dataBuffer = 0;

            int blockIndex = 0;

            long checksum = 0;

            while (dataPointer > freePointer)
            {
                // move to next free space
                int data = layout[freePointer++] - '0';
                for (int i = 0; i < data; ++i)
                {
                    checksum += freePointer / 2 * blockIndex++;
                }
                if (dataPointer > freePointer)
                {
                    emptyBuffer = layout[freePointer++] - '0';
                }
                while (emptyBuffer > 0 && dataPointer >= freePointer)
                {
                    if (dataBuffer == 0)
                    {
                        dataPointer--;
                        dataBuffer = layout[dataPointer--] - '0';
                    }
                    // move
                    while (dataBuffer > 0 && emptyBuffer > 0)
                    {
                        checksum += (dataPointer / 2 + 1) * blockIndex++;
                        dataBuffer--;
                        emptyBuffer--;
                    }
                }
            }
            // process trailing bit
            while (dataBuffer > 0)
            {
                checksum += (dataPointer / 2 + 1) * blockIndex++;
                dataBuffer--;
            }

            return checksum;
        }
    }
}