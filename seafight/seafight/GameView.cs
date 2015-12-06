using System;
using BattleField;

namespace seafight
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

        public bool PutShip(Placement placement)
        {
            if (!(placement.Ship is Ship))
                throw new ArgumentException("Объект не является кораблем", "placement");

            return board.PlacePlayerShip(player, placement);
        }
    }
}