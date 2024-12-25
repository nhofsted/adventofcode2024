namespace Day24
{
    internal abstract class Gate : Component
    {
        protected Dictionary<String, Component> components;
        protected string input1;
        protected string input2;

        public Gate(Dictionary<String, Component> components, String input1, string input2)
        {
            this.components = components;
            this.input1 = input1;
            this.input2 = input2;
        }

        public abstract bool GetValue();
    }
}