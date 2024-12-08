namespace Day6
{
    public class Space
    {
        const int STARTPOS = 0x10;
        const int OBSTACLE = 0x20;

        private char space = (char)0;

        public Space(bool startPos, bool obstacle)
        {
            if (startPos) space = (char)((int)space | (int)Space.STARTPOS);
            if (obstacle) space = (char)((int)space | (int)Space.OBSTACLE);
        }

        public Space(Space other)
        {
            space = other.space;
        }

        public void Visit(Direction direction)
        {
            space = (char)((int)space | (int)direction);
        }

        public bool IsVisited(Direction direction)
        {
            return ((int)space & (int)direction) != 0;
        }

        public bool IsVisited()
        {
            return ((int)space & 0x0f) != 0;
        }

        public bool IsStartpos()
        {
            return ((int)space & STARTPOS) != 0;
        }

        public bool IsObstacle()
        {
            return ((int)space & OBSTACLE) != 0;
        }

        internal void MakeObstacle()
        {
            space = (char)((int)space | (int)Space.OBSTACLE);
        }
    }
}