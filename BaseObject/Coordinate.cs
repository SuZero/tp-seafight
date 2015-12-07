namespace BaseObject
{
    public class Coordinate
    {
        private int x;
        private int y;

        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Coordinate(Coordinate coord)
        {
            this.x = coord.x;
            this.y = coord.y;
        }
        public Coordinate()
        {
            
        }
    }
}