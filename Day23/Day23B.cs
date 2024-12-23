namespace Day23
{
    internal class Day23B : Day23
    {
        override public long GetSampleSolution()
        {
            return 4;
        }

        public override long Solve(StreamReader input, bool sample)
        {
            Dictionary<string, HashSet<string>> network = new Dictionary<string, HashSet<string>>();
            string? conn = null;
            while ((conn = input.ReadLine()) != null)
            {
                string[] link = conn.Split('-');
                if (!network.ContainsKey(link[0]))
                {
                    network.Add(link[0], new HashSet<string>());
                }
                if (!network.ContainsKey(link[1]))
                {
                    network.Add(link[1], new HashSet<string>());
                }
                network[link[0]].Add(link[1]);
                network[link[1]].Add(link[0]);
            }

            int maxSize = 0;
            string? maxDescription = null;
            foreach (string node in network.Keys)
            {
                HashSet<string> group = CalculateMaxGroup(new HashSet<string>([node]), new HashSet<string>(), new HashSet<string>(network[node]), network);
                if (group.Count > maxSize)
                {
                    maxSize = group.Count;
                    List<String> nodes = group.ToList();
                    nodes.Sort();
                    maxDescription = String.Join(",", nodes.ToArray());
                }
            }
            Console.WriteLine(maxDescription);
            return maxSize;
        }

        private HashSet<string> CalculateMaxGroup(HashSet<string> group, HashSet<string> rejected, HashSet<string> candidates, Dictionary<string, HashSet<string>> network)
        {
            HashSet<string> maxGroup = group;

            foreach (string candidate in candidates)
            {
                if (rejected.Contains(candidate)) continue;
                if (IsConnected(group, candidate, network))
                {
                    HashSet<string> biggerGroup = new HashSet<string>(group);
                    biggerGroup.Add(candidate);
                    HashSet<string> newCandidates = new HashSet<string>(candidates);
                    newCandidates.Remove(candidate);
                    foreach (string neighbour in network[candidate])
                    {
                        if (!biggerGroup.Contains(neighbour) && !rejected.Contains(neighbour))
                        {
                            newCandidates.Add(neighbour);
                        }
                    }
                    HashSet<string> expandedGroup = CalculateMaxGroup(biggerGroup, new HashSet<string>(rejected), newCandidates, network);
                    if (expandedGroup.Count > maxGroup.Count)
                    {
                        maxGroup = expandedGroup;
                    }
                    rejected.UnionWith(expandedGroup);
                }
                else
                {
                    rejected.Add(candidate);
                }
            }

            return maxGroup;
        }

        private bool IsConnected(HashSet<string> group, string candidate, Dictionary<string, HashSet<string>> network)
        {
            HashSet<string> connections = network[candidate];
            return group.IsSubsetOf(connections);
        }
    }
}