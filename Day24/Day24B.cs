namespace Day24
{
    internal class Day24B : Day24
    {
        override public long GetSampleSolution()
        {
            return 0;
        }
        public override long Solve(StreamReader input, bool sample)
        {
            if (sample) return 0;
            while (input.ReadLine() != "") ;

            List<GateDescription> gates = new List<GateDescription>();
            Dictionary<string, GateDescription> source = new Dictionary<string, GateDescription>();

            string? gate;
            while ((gate = input.ReadLine()) != null)
            {
                string[] g = gate.Split(" ");
                GateDescription.Types type;
                switch (g[1])
                {
                    case "AND":
                        type = GateDescription.Types.AND;
                        break;
                    case "OR":
                        type = GateDescription.Types.OR;
                        break;
                    case "XOR":
                        type = GateDescription.Types.XOR;
                        break;
                    default:
                        throw new Exception("Unknown Gate");
                }
                GateDescription description = new GateDescription(type, g[0], g[2], g[4]);
                gates.Add(description);
                source[g[4]] = description;
            }

            List<string> problems = new List<string>();

            HashSet<string> R = new HashSet<string>();
            foreach (GateDescription g in gates)
            {
                if ((g.In1.StartsWith("x") && g.In2.StartsWith("y")) || ((g.In1.StartsWith("y") && g.In2.StartsWith("x"))))
                {
                    if (g.In1.Substring(1) == g.In2.Substring(1))
                    {
                        int n = ExtractNumber(g.In1);
                        if (g.Type == GateDescription.Types.XOR)
                        {
                            R.Add(g.Output);
                        }
                    }
                }
            }

            // Some tests after observations of the graph implementing the Full Adder
            foreach (GateDescription g in gates)
            {
                if (g.Output.StartsWith("z") && g.Output != "z01")
                {
                    if (g.Type != GateDescription.Types.XOR && g.Output != "z" + R.Count)
                    {
                        problems.Add(g.Output);
                    }
                    else if (source.ContainsKey(g.In1) && source.ContainsKey(g.In2))
                    {
                        if (source[g.In1].Type != GateDescription.Types.XOR && source[g.In2].Type != GateDescription.Types.XOR)
                        {
                            if (source[g.In1].Type == GateDescription.Types.OR && source[g.In2].Type != GateDescription.Types.OR) problems.Add(g.In2);
                            if (source[g.In2].Type == GateDescription.Types.OR && source[g.In1].Type != GateDescription.Types.OR) problems.Add(g.In1);
                        }

                        if (source[g.In1].Type != GateDescription.Types.OR && source[g.In2].Type != GateDescription.Types.OR)
                        {
                            if (source[g.In1].Type == GateDescription.Types.XOR && source[g.In2].Type == GateDescription.Types.XOR)
                            {
                                if (R.Contains(g.In1)) problems.Add(g.In2);
                                if (R.Contains(g.In2)) problems.Add(g.In1);
                            }
                            if (source[g.In1].Type == GateDescription.Types.XOR && source[g.In2].Type != GateDescription.Types.XOR) problems.Add(g.In2);
                            if (source[g.In2].Type == GateDescription.Types.XOR && source[g.In1].Type != GateDescription.Types.XOR) problems.Add(g.In1);
                        }
                    }
                }
                if (g.Type == GateDescription.Types.OR)
                {
                    if (source[g.In1].Type != GateDescription.Types.AND) problems.Add(g.In1);
                    if (source[g.In2].Type != GateDescription.Types.AND) problems.Add(g.In2);
                }
            }

            problems.Sort();
            Console.WriteLine(String.Join(",", problems.ToArray()));

            return 0;
        }

        private int ExtractNumber(string label)
        {
            return int.Parse(label.Substring(1));
        }
    }
}