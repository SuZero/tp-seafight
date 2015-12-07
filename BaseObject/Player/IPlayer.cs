using System.Collections.Generic;

namespace BaseObject
{
    public interface IPlayer
    {
        string Name { get; }
        void PlaceShips(IPlayerView playerView, ICollection<IShip> ships);
        IShot YourTurn(IPlayerView playerView);
        void ShotFeedback(int hits, int sunkShips);
    }
}