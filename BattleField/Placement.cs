﻿using BaseObject;
namespace BattleField
{
    public class Placement : Coordinate,IPlacement
    {
        private Orientation orientation;
        private IShip ship;

        public Orientation Orientation { get { return orientation; } set { orientation = value; } }
        public IShip Ship { get { return ship; } set { ship = value; } }

        public Placement(IShip ship, int x, int y, Orientation orientation):base(x,y)
        {
            this.ship = ship;
            this.orientation = orientation;
        }
        public Placement(IShip ship, Coordinate coord, Orientation orientation) : this(ship, coord.X, coord.Y, orientation)
        {
        }
        public Placement()
        {
            this.ship = null;
            this.orientation = Orientation.Horizontal;
        }
    }
}