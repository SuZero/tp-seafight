using System;
using System.Collections.Generic;


namespace BaseObject
{
    public class Player: IPlayer
    {
        public PlayerNumber numberPlayer { get; set; }
        public List<IShip> ships { get; set; }
        public String name { get; set; }
        public PlayerType playerType { get; set; }


        public Player(String name, PlayerNumber number, PlayerType type, List<IShip> ships)
        {
            this.name = name;
            this.ships = ships;
            this.playerType = type;
            this.numberPlayer = number;
       
                this.ships = ships;

        }

  

        public void PlaceShips(ICollection<IShip> ships)
        {
            throw new NotImplementedException();
        }

        public IShot YourTurn(IPlayer playerView)
        {
            throw new NotImplementedException();
        }

        public void ShotFeedback(int hits, int sunkShips)
        {
            throw new NotImplementedException();
        }
    }

    public enum PlayerNumber
    {
        One, Two
    }

}