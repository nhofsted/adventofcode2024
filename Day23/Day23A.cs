namespace Day23
{
    internal class Day23A : Day23
    {
        override public long GetSampleSolution()
        {
            return 7;
        }

        public override long Solve(StreamReader input, bool sample)
        {
            Dictionary<string, List<string>> network = new Dictionary<string, List<string>>();
            string? conn = null;
            while ((conn = input.ReadLine()) != null)
            {
                string[] link = conn.Split('-');
                if (!network.ContainsKey(link[0]))
                {
                    network.Add(link[0], new List<string>());
                }
                if (!network.ContainsKey(link[1]))
                {
                    network.Add(link[1], new List<string>());
                }
                network[link[0]].Add(link[1]);
                network[link[1]].Add(link[0]);
            }

            HashSet<string> triplets = new HashSet<string>();
            foreach (string node1 in network.Keys)
            {
                if (node1.StartsWith("t"))
                {
                    foreach (string node2 in network[node1])
                    {
                        if (node2 != node1)
                        {
                            foreach (string node3 in network[node2])
                            {
                                if (node3 != node1 && node3 != node2)
                                {
                                    foreach (string nodet in network[node3])
                                    {
                                        if (node1 == nodet)
                                        {
                                            List<string> triplet = new List<string>([node1, node2, node3]);
                                            triplet.Sort();
                                            triplets.Add(String.Join(',', triplet.ToArray()));
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return triplets.Count;
        }
    }
}