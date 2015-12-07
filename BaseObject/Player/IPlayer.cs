using System.Collections.Generic;
using BaseObject;

namespace BattleField
{
    public interface IPlayer
    {
        string Name { get; }
        void PlaceShips(IPlayerView playerView, ICollection<IShip> ships);
        IShot YourTurn(IPlayerView playerView);
        void ShotFeedback(int hits, int sunkShips);
    }
}