using System.Collections.Generic;

namespace BaseObject
{
    public interface IPlayer
    {
   
        void PlaceShips(ICollection<IShip> ships);
        IShot YourTurn(IPlayer player);
        void ShotFeedback(int hits, int sunkShips);
    }
}