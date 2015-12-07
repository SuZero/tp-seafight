using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleField;
using Microsoft.Xna.Framework.Input;
using seafight;

namespace Multiplayer
{
   /* class PlayerMP : IPlayer
    {
        private Random _rand;

        public Shot YourTurn(IPlayerView playerView)
        {
            MouseState mouseState;
            MouseState previousMouseState;
            mouseState = Mouse.GetState();
            if (previousMouseState.LeftButton == ButtonState.Pressed &&
                mouseState.LeftButton == ButtonState.Released)
            {

                MouseClicked(mouseState.X, mouseState.Y);

            }
            int x = _rand.Next(playerView.GetXMax()) + 1;
            int y = _rand.Next(playerView.GetYMax()) + 1;
            return new Shot(x, y);
        }

    }
    */
}
