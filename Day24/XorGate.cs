namespace Day24
{
    internal class XorGate : Gate
    {
        public XorGate(Dictionary<String, Component> components, String input1, string input2) : base(components, input1, input2)
        {
        }

        public override bool GetValue()
        {
            return components[input1].GetValue() ^ components[input2].GetValue();
        }
    }
}
