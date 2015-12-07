using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using seafight;
using BattleField;

namespace AI
{
    public class StdPlayer : IPlayer
    {
        private static int _objCount;
        private string _name;
        private Random _rand;

        public string Name
        {
            get { return _name; }
        }

        public StdPlayer()
        {
            _objCount++;
            _name = String.Concat("StdPlayer #", _objCount);
            _rand = new Random(Environment.TickCount + _objCount);
        }

        public void PlaceShips(IPlayerView playerView, ICollection<IShip> ships)
        {
            int maxX = playerView.GetXMax();
            int maxY = playerView.GetYMax();

            Placement placement;
            int xPos;
            int yPos;
            Orientation direction;

            foreach (Ship ship in ships)
            {
                
                {
                    xPos = _rand.Next(maxX) + 1;
                    yPos = _rand.Next(maxY) + 1;
                    direction = _rand.NextDouble() > 0.5 ? Orientation.Horizontal : Orientation.Vertical;
                    placement = new Placement(ship, xPos, yPos, direction);
                } 
            }
        }

        public Shot YourTurn(IPlayerView playerView)
        {
            int x = _rand.Next(playerView.GetXMax()) + 1;
            int y = _rand.Next(playerView.GetYMax()) + 1;
            return new Shot(x, y);
        }

        public void ShotFeedback(int hits, int sunkShips)
        {
            // TODO: Maybe use this information to plan a better next move.
        }
    }
}
