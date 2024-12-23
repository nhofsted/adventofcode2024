namespace Day22
{
    internal class Day22B : Day22
    {
        override public long GetSampleSolution()
        {
            return 23;
        }

        override public string GetSampleFile()
        {
            return "sampleB.txt";
        }

        public override long Solve(StreamReader input, bool sample)
        {
            HashSet<int> sequences = new HashSet<int>();
            List<Dictionary<int, int>> sequencePrices = new List<Dictionary<int, int>>();

            long retVal = 0;
            string? secretString = null;
            while ((secretString = input.ReadLine()) != null)
            {
                long secret = long.Parse(secretString);
                CalculateSequencePrices(secret, sequences, sequencePrices);
            }

            long maxTotalPrice = 0;
            foreach (int sequence in sequences)
            {
                long totalPrice = 0;
                foreach (Dictionary<int, int> sequencePrice in sequencePrices)
                {
                    if (sequencePrice.TryGetValue(sequence, out int price)) totalPrice += price;
                }
                maxTotalPrice = Math.Max(maxTotalPrice, totalPrice);
            }

            return maxTotalPrice;
        }

        private void CalculateSequencePrices(long secret, HashSet<int> sequences, List<Dictionary<int, int>> sequencePrices)
        {
            Dictionary<int, int> sequencePrice = new Dictionary<int, int>();
            int sequence = 0;
            int price = 0;
            int previousPrice = (int)(secret % 10);
            int difference = 0;
            // initialize sequence and diff
            for (int i = 0; i < 3; ++i)
            {
                secret = GenerateNextRandom(secret);
                price = (int)(secret % 10);
                difference = price - previousPrice;
                previousPrice = price;
                sequence = UpdateSequence(sequence, difference);

            }
            for (int i = 0; i < 2000 - 3; i++)
            {
                secret = GenerateNextRandom(secret);
                price = (int)(secret % 10);
                difference = price - previousPrice;
                previousPrice = price;
                sequence = UpdateSequence(sequence, difference);
                sequences.Add(sequence);
                if (!sequencePrice.ContainsKey(sequence))
                {
                    sequencePrice.Add(sequence, price);
                }
            }
            sequencePrices.Add(sequencePrice);
        }

        private int UpdateSequence(int sequence, int difference)
        {
            sequence <<= 8;
            sequence |= difference & 0xff;
            return sequence;
        }
    }
}