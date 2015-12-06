﻿using System;
using System.Linq;

namespace BattleField
{
    public class Ship : IShip
    {
        private int length;
        private bool[] sectionDestroyed;

        public int Length { get { return length; } }
        public bool IsSunk { get { return sectionDestroyed.All(b => b); } }

        public Ship(int shipSize)
        {
            length = shipSize;
            sectionDestroyed = (bool[])Array.CreateInstance(typeof(Boolean), shipSize);
        }
        public void Hit(int section)
        {
            sectionDestroyed[section] = true;
        }
        public Placement SailTo(int x, int y, Orientation orientation)
        {
            return new Placement(this, x, y, orientation);
        }
        public Placement SailTo(Coordinate coord, Orientation orientation)
        {
            return new Placement(this, coord, orientation);
        }


    }
}