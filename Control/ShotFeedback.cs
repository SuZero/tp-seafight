namespace Control
{
    public class ShotFeedback
    {
        private int hits;
        private int sunkShips;

        public int Hits { get { return hits; } }
        public int SunkShips { get { return sunkShips; } }

        public ShotFeedback(int hits, int sunkShips)
        {
            hits = hits;
            sunkShips = sunkShips;
        }
    }
}