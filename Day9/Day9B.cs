namespace Day9
{
    internal class Day9B : Day9
    {
        override public long GetSampleSolution()
        {
            return 2858;
        }

        override protected long Defrag(string layout)
        {
            // Create filesystem layout
            LinkedList<Block> filesystem = new LinkedList<Block>();
            int start = 0;
            for (int i = 0; i < layout.Length; ++i)
            {
                int length = layout[i] - '0';
                filesystem.AddLast(i % 2 == 0 ? new Block(i / 2, start, length) : new Block(start, length));
                start += length;
            }

            // Create shortcuts to first free space capable of containing a data block of a specific size
            LinkedList<LinkedListNode<Block>>[] emptyBlocks = new LinkedList<LinkedListNode<Block>>[9];
            for (int i = 0; i < emptyBlocks.Length; ++i)
            {
                emptyBlocks[i] = new LinkedList<LinkedListNode<Block>>();
            }
            LinkedListNode<Block>? freeSpaceFinder = filesystem.First;
            while (freeSpaceFinder != null)
            {
                if (freeSpaceFinder.Value.IsEmpty() && freeSpaceFinder.Value.Size > 0)
                {
                    emptyBlocks[freeSpaceFinder.Value.Size - 1].AddLast(freeSpaceFinder);
                }
                freeSpaceFinder = freeSpaceFinder.Next;
            }

            // Try moving blocks forward
            LinkedListNode<Block>? dataPointer = filesystem.Last;
            while (dataPointer != null)
            {
                if (!dataPointer.Value.IsEmpty())
                {
                    LinkedListNode<Block>? freeSpace = null;
                    for (int i = dataPointer.Value.Size - 1; i < 9; ++i)
                    {
                        if (emptyBlocks[i].Count > 0)
                        {
                            if (freeSpace == null && emptyBlocks[i].First().Value.Start < dataPointer.Value.Start) freeSpace = emptyBlocks[i].First();
                            else if (freeSpace != null && emptyBlocks[i].First().Value.Start < freeSpace.Value.Start && freeSpace.Value.Start < dataPointer.Value.Start) freeSpace = emptyBlocks[i].First();
                        }
                    }
                    if (freeSpace != null)
                    {
                        filesystem.AddBefore(freeSpace, new Block(dataPointer.Value.FileId, freeSpace.Value.Start, dataPointer.Value.Size));
                        if (freeSpace.Value.Size > dataPointer.Value.Size)
                        {
                            LinkedListNode<Block> newFreeSpace = filesystem.AddBefore(freeSpace, new Block(freeSpace.Value.Start + dataPointer.Value.Size, freeSpace.Value.Size - dataPointer.Value.Size));
                            LinkedListNode<LinkedListNode<Block>>? insertPos = emptyBlocks[newFreeSpace.Value.Size - 1].First;
                            while (insertPos != null && insertPos.Value.Value.Start < newFreeSpace.Value.Start) insertPos = insertPos.Next;
                            if (insertPos == null) emptyBlocks[newFreeSpace.Value.Size - 1].AddLast(newFreeSpace);
                            else emptyBlocks[newFreeSpace.Value.Size - 1].AddBefore(insertPos, newFreeSpace);
                        }
                        emptyBlocks[freeSpace.Value.Size - 1].Remove(freeSpace);
                        filesystem.Remove(freeSpace);
                        LinkedListNode<Block> newDataPointer = filesystem.AddBefore(dataPointer, new Block(dataPointer.Value.Start, dataPointer.Value.Size));
                        filesystem.Remove(dataPointer);
                        dataPointer = newDataPointer;
                    }
                }
                dataPointer = dataPointer.Previous;
            }

            // Calculate checksum
            long checksum = 0;
            LinkedListNode<Block>? checksumIterator = filesystem.First;
            int position = 0;
            while (checksumIterator != null)
            {
                if (checksumIterator.Value.IsEmpty())
                {
                    position += checksumIterator.Value.Size;
                }
                else
                {
                    for (int i = 0; i < checksumIterator.Value.Size; ++i)
                    {
                        checksum += position * checksumIterator.Value.FileId;
                        position++;
                    }
                }
                checksumIterator = checksumIterator.Next;
            }

            return checksum;
        }

        public class Block
        {
            public int FileId { get; }
            public int Start { get; }
            public int Size { get; }

            public Block(int fileId, int start, int size)
            {
                this.FileId = fileId;
                this.Start = start;
                this.Size = size;
            }

            public Block(int start, int size)
            {
                this.FileId = -1;
                this.Start = start;
                this.Size = size;
            }

            public bool IsEmpty()
            {
                return FileId == -1;
            }
        }
    }
}