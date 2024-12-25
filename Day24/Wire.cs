namespace Day24
{
    internal class Wire : Component
    {
        Component input;

        public Wire(Component input)
        {
            this.input = input;
        }

        public bool GetValue()
        {
            return input.GetValue();
        }
    }
}