namespace Day24
{
    internal class Input : Component
    {
        bool value;

        public Input(bool value)
        {
            this.value = value;
        }

        public bool GetValue()
        {
            return value;
        }
    }
}