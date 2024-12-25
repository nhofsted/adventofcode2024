namespace Day24
{
    internal class Day24A : Day24
    {
        override public long GetSampleSolution()
        {
            return 4;
        }

        public override long Solve(StreamReader input, bool sample)
        {
            Dictionary<string, Component> wires = new Dictionary<string, Component>();
            string? inputWire = null;
            while ((inputWire = input.ReadLine()) != "")
            {
                string[] w = inputWire.Split(": ");
                wires.Add(w[0], new Input(w[1] == "1"));
            }

            List<Wire?> output = new List<Wire?>();
            string? gateDesc;
            while ((gateDesc = input.ReadLine()) != null)
            {
                string[] g = gateDesc.Split(" ");
                Gate? gate = null;
                switch (g[1])
                {
                    case "AND":
                        gate = new AndGate(wires, g[0], g[2]);
                        break;
                    case "OR":
                        gate = new OrGate(wires, g[0], g[2]);
                        break;
                    case "XOR":
                        gate = new XorGate(wires, g[0], g[2]);
                        break;
                    default:
                        throw new Exception("Unknown Gate");
                }
                Wire w = new Wire(gate);
                wires[g[4]] = w;

                if (g[4].StartsWith("z"))
                {
                    int pos = int.Parse(g[4].Substring(1));
                    while (output.Count <= pos)
                    {
                        output.Add(null);
                    }
                    output[pos] = w;
                }
            }

            long retVal = 0;
            for (int i = output.Count; i-- > 0;)
            {
                retVal *= 2;
                if (output[i] != null && output[i].GetValue()) retVal += 1;
            }

            return retVal;
        }
    }
}