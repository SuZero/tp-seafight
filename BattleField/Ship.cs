using System;
using System.Linq;
using BaseObject;
using Microsoft.Xna.Framework.Graphics;

namespace BattleField
{
    public class Ship : IShip
    {
        protected Texture2D deck_1 ;
        protected Texture2D deck_2;
        protected Texture2D deck_3;
        protected Texture2D deck_4;

        private int length;

    
        private bool[] sectionDestroyed;

        public int Length { get { return length; } }
        public bool IsSunk { get { return sectionDestroyed.All(b => b); } }

        public Ship(int shipSize)
        {
            length = shipSize;
            sectionDestroyed = (bool[])Array.CreateInstance(typeof(Boolean), shipSize);
        }
        public Ship()
        {
            length = 0;
        }
        public void Hit(int section)
        {
            sectionDestroyed[section] = true;
        }
        public IPlacement SailTo(int x, int y, Orientation orientation)
        {
            return new Placement(this, x, y, orientation);
        }
        public IPlacement SailTo(Coordinate coord, Orientation orientation)
        {
            return new Placement(this, coord, orientation);
        }


    }
}