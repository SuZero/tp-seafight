using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BaseObject;
using BattleField;

namespace AI
{
    public class StdPlayer : IPlayer
    {
        public void PlaceShips(ICollection<IShip> ships)
        {
            throw new NotImplementedException();
        }

        public IShot YourTurn(IPlayer player)
        {
            throw new NotImplementedException();
        }

        public void ShotFeedback(int hits, int sunkShips)
        {
            throw new NotImplementedException();
        }
    }
}
