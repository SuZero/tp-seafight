using System;
using System.Collections.Generic;
using Control;
using seafight;
using BaseObject;
using BattleField;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace seafight
{
    public class Gameboard
    {
        public enum State
        {
            DEAD,
            BLOOMER,
            LIVE
        }

        private int xMax;
        private int yMax;
        public Dictionary<Rectangle,State> cellList { get; set; }
     
        public Player player { get; set; }

        public int XMax { get { return xMax; } }
        public int YMax { get { return yMax; } }

    public Gameboard(int gridXSize, int gridYSize, Player player)
        {
            xMax = gridXSize;
            yMax = gridYSize;
            this.player = player;
             cellList = new Dictionary<Rectangle, State>();
        }

        public bool PutShip(Placement placement)
        {
            if (!(placement.Ship is Ship))
                  throw new ArgumentException("Объект не является кораблем", "placement");

              return this.PlacePlayerShip(player, placement);
             
            return true;
        }

        internal bool PlacePlayerShip(Player player, Placement placement)
        {
            return true;
        }

        public bool IsSelectedCell(Coordinate placement)
        {
           Rectangle mouse = new Rectangle(placement.X, placement.Y,1,1);

            foreach (var cell in cellList)
            {
                if (cell.Key.Intersects(mouse) && cell.Value==State.LIVE)
                    return true;
         
            }
            return false;

        }

        private bool IsInTheWayOf(Placement placedShip, Placement placement)
        {
            Coordinate shipTopLeft, shipBottomRight, endPoint, tmpSegment;

            // TODO: This should be BottomLeft and TopRight if going away from origo.
            shipTopLeft = new Coordinate(placedShip.X - 1, placedShip.Y + 1);
            endPoint = ShipEndPoint(placedShip);
            shipBottomRight = new Coordinate(endPoint.X + 1, endPoint.Y - 1);

            for (int i = 0; i <= placement.Ship.Length; i++)
            {
                if (placement.Orientation == Orientation.Horizontal)
                    tmpSegment = new Coordinate(placement.X + i, placement.Y);
                else //if (placement.Orientation == Orientation.Vertical)
                    tmpSegment = new Coordinate(placement.X, placement.Y - i);

                if (CoordinateInSquare(tmpSegment, shipTopLeft, shipBottomRight))
                    return true;
            }

            return false;
        }
        private Coordinate ShipEndPoint(Placement placement)
        {
            Coordinate end;
            if (placement.Orientation == Orientation.Horizontal)
                end = new Coordinate(placement.X + (placement.Ship.Length - 1), placement.Y);
            else //if (placement.Orientation == Orientation.Vertical)
                end = new Coordinate(placement.X, placement.Y - (placement.Ship.Length - 1));  // TODO: This should be changed.

            return end;
        }
        // TODO: This should be rewritten to work with BottomLeft and TopRight.
        private bool CoordinateInSquare(Coordinate testCoord, Coordinate topLeft, Coordinate bottomRight)
        {
            if ((testCoord.X >= topLeft.X && testCoord.X <= bottomRight.X) &&
                (testCoord.Y <= topLeft.Y && testCoord.Y >= bottomRight.Y))
                return true;

            return false;
        }

        internal bool SetUpComplete()
        {
          /*  List<int> shipCheckList = new List<int>(_shipSizes.Length);

            foreach (KeyValuePair<Player, List<Placement>> pair in _playerShips)
            {
                if (pair.Value.Count != _shipSizes.Length)
                    return false;

                shipCheckList.AddRange(_shipSizes);

                foreach (Placement placedShip in pair.Value)
                {
                    shipCheckList.Remove(placedShip.Vessel.Length);
                }

                if (shipCheckList.Count != 0)
                    return false;
            }*/

            return true;
        }

        internal ShotFeedback FireShot(Player whosTurn, Shot playerShot)
        {
            /*List<Placement> targetFleet = _playerShips[whosTurn == Player.One ? Player.Two : Player.One];
            Vessel ship;
            int start, end, shot, segment;
            int hits = 0;
            int sunkShips = 0;

            foreach (Placement shipPlacement in targetFleet)
            {
                ship = (Vessel)shipPlacement.Vessel;

                if (shipPlacement.Orientation == Orientation.Horizontal && shipPlacement.Y == playerShot.Y)
                {
                    start = shipPlacement.X;
                    end = shipPlacement.X + (ship.Length - 1);
                    shot = playerShot.X;
                }
                else if (shipPlacement.Orientation == Orientation.Vertical && shipPlacement.X == playerShot.X)
                {
                    start = shipPlacement.Y - (ship.Length - 1);  // TODO: This should be taken care of.
                    end = shipPlacement.Y;
                    shot = playerShot.Y;
                }
                else
                    continue;

                segment = shot - start;

                if (segment < ship.Length && segment >= 0)
                {
                    ship.Hit(segment);
                    hits++;

                    if (ship.IsSunk)
                        sunkShips++;
                }

            }
            */
            return null; //new ShotFeedback(hits, sunkShips);
        }


    }


}
