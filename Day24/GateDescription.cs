namespace Day24
{
    internal class GateDescription
    {
        public enum Types { AND, OR, XOR };

        public Types Type { get; }
        public string In1 { get; }
        public string In2 { get; }
        public string Output { get; }

        public GateDescription(Types type, string in1, string in2, string output)
        {
            this.Type = type;
            this.In1 = in1;
            this.In2 = in2;
            this.Output = output;
        }
    }
}