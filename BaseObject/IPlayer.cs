using System.Collections.Generic;
using BattleField;

namespace seafight
{
    public interface IPlayer
    {
        string Name { get; }
        void PlaceShips(IPlayerView playerView, ICollection<IShip> ships);
        Shot YourTurn(IPlayerView playerView);
        void ShotFeedback(int hits, int sunkShips);
    }
}