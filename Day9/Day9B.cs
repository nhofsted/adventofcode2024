
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
            for (int i = 0; i < layout.Length; ++i)
            {
                int length = layout[i] - '0';
                filesystem.AddLast(i % 2 == 0 ? new Block(i / 2, length) : new Block(length));
            }

            // Create shortcuts to first free space capable of containing a data block of a specific size
            LinkedListNode<Block>[] firstFreeSpaceOfSize = new LinkedListNode<Block>[9];
            CreateShortcuts(firstFreeSpaceOfSize, filesystem.First, filesystem.Last);

            // Try moving data blocks forward
            LinkedListNode<Block> dataIterator = filesystem.Last;
            while (dataIterator != null)
            {
                if (dataIterator.Value.IsEmpty())
                {
                    // If we pass shortcuts, make them unusable
                    for (int i = 0; i < 9; ++i)
                    {
                        if (firstFreeSpaceOfSize[i] == dataIterator) firstFreeSpaceOfSize[i] = null;
                    }
                }
                else
                {
                    int size = dataIterator.Value.Size;
                    for (int i = size - 1; i < 9; ++i)
                    {
                        if (firstFreeSpaceOfSize[i] != null)
                        {
                            // Copy block to target and adjust remaining space
                            LinkedListNode<Block> insertionPoint = firstFreeSpaceOfSize[i];
                            filesystem.AddBefore(insertionPoint, dataIterator.Value);
                            int remaining = insertionPoint.Value.Size - dataIterator.Value.Size;
                            LinkedListNode<Block> remainingPointer = filesystem.AddBefore(insertionPoint, new Block(remaining));
                            filesystem.Remove(insertionPoint);

                            // Adjust shortcuts
                            for (int d = 0; d < 9; ++d)
                            {
                                if (firstFreeSpaceOfSize[d] == insertionPoint)
                                {
                                    firstFreeSpaceOfSize[d] = null;
                                }
                            }
                            for (int r = remaining; r-- > 0;)
                            {
                                if (firstFreeSpaceOfSize[r] == null)
                                {
                                    firstFreeSpaceOfSize[r] = remainingPointer;
                                }
                            }
                            CreateShortcuts(firstFreeSpaceOfSize, remainingPointer, dataIterator);

                            // Delete block from source location
                            LinkedListNode<Block> toDelete = dataIterator;
                            dataIterator = filesystem.AddBefore(dataIterator, new Block(dataIterator.Value.Size));
                            filesystem.Remove(toDelete);
                            break;
                        }
                    }
                }
                dataIterator = dataIterator.Previous;
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

        private static void CreateShortcuts(LinkedListNode<Block>[] firstFreeSpaceOfSize, LinkedListNode<Block> iterator, LinkedListNode<Block> end)
        {
            int toFill = 0;
            for (int i = 0; i < firstFreeSpaceOfSize.Length; ++i)
            {
                if (firstFreeSpaceOfSize[i] == null) toFill++;
            }
            int filled = 0;
            while (filled < toFill && iterator != end)
            {
                if (iterator.Value.IsEmpty())
                {
                    int freeSpace = iterator.Value.Size;
                    for (int i = freeSpace; i-- > 0;)
                    {
                        if (firstFreeSpaceOfSize[i] == null)
                        {
                            firstFreeSpaceOfSize[i] = iterator;
                            ++filled;
                        }
                    }
                }
                iterator = iterator.Next;
            }
        }

        public class Block
        {
            public int FileId { get; }
            public int Size { get; }

            public Block(int fileId, int size)
            {
                this.FileId = fileId;
                this.Size = size;
            }

            public Block(int size)
            {
                this.FileId = -1;
                this.Size = size;
            }

            public bool IsEmpty()
            {
                return FileId == -1;
            }
        }
    }
}