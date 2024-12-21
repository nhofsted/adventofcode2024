using Common;

namespace Day20
{
    public abstract class Day20 : Puzzle
    {
        public override long Solve(StreamReader input, bool sample)
        {
            Coordinate? start = null;
            Coordinate? end = null;
            Grid grid = Grid.BuildGrid(input, ref start, ref end);

            List<Coordinate> track = ExtractTrack(grid, start, end);
            Dictionary<Coordinate, int> trackDistances = IndexTrackDistances(track);
            Dictionary<Tuple<Coordinate, Coordinate>, int> shortcuts = DiscoverShortcuts(grid, track, trackDistances, GetCheatDistance());

            long result = 0;
            foreach (Tuple<Coordinate, Coordinate> key in shortcuts.Keys)
            {
                if (shortcuts[key] >= 100) result++;
            }

            return result;
        }

        protected abstract int GetCheatDistance();

        private Dictionary<Tuple<Coordinate, Coordinate>, int> DiscoverShortcuts(Grid grid, List<Coordinate> track, Dictionary<Coordinate, int> trackDistances, int cheatDistance)
        {
            Dictionary<Tuple<Coordinate, Coordinate>, int> shortcuts = new Dictionary<Tuple<Coordinate, Coordinate>, int>();
            for (int i = 0; i < track.Count; ++i)
            {
                Coordinate position = track[i];
                foreach (Coordinate cheatTarget in position.GetNeighbors(grid, 2, cheatDistance))
                {
                    int stepsSaved = trackDistances[cheatTarget] - trackDistances[position] - position.DistanceTo(cheatTarget);
                    if (stepsSaved > 0) shortcuts[new Tuple<Coordinate, Coordinate>(position, cheatTarget)] = stepsSaved;
                }
            }
            return shortcuts;
        }

        private Dictionary<Coordinate, int> IndexTrackDistances(List<Coordinate> track)
        {
            Dictionary<Coordinate, int> retVal = new Dictionary<Coordinate, int>();
            for (int i = 0; i < track.Count; i++)
            {
                retVal[track[i]] = i;
            }
            return retVal;
        }

        private List<Coordinate> ExtractTrack(Grid grid, Coordinate? pos, Coordinate? end)
        {
            Coordinate prevPos = pos;
            List<Coordinate> track = new List<Coordinate>();
            while (pos != end)
            {
                track.Add(pos);
                foreach (Coordinate neighgor in pos.GetNeighbors(grid))
                {
                    if (neighgor == prevPos) continue;
                    prevPos = pos;
                    pos = neighgor;
                    break;
                }
            }
            track.Add(end);
            return track;
        }

        static void Main(string[] args)
        {
            new Day20A().run();
            new Day20B().run();
        }
    }
}