using System;
using BaseObject;

namespace BattleField
{
    public class GameView : IPlayerView
    {
        private Gameboard board;
        private Player player;

        public GameView(Gameboard board, Player player)
        {
            this.board = board;
            this.player = player;
        }

        public int GetXMax()
        {
            return this.board.XMax;
        }

        public int GetYMax()
        {
            return board.YMax;
        }

        public bool PutShip(IPlacement placement)
        {
          /*  if (!(placement.Ship is Ship))
                throw new ArgumentException("Объект не является кораблем", "placement");

            return board.PlacePlayerShip(player, placement);
           * */
            return true;
        }
    }
}