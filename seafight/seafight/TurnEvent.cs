﻿using BattleField;

namespace seafight

{
    public class TurnEvent
    {
        private IPlayer player;
        private Player playerNr;
        private Shot shot;
        private bool hit;
        private int turn;

        public IPlayer Player { get { return this.player; } set { this.player = value; } }
        public Player PlayerNr { get { return this.playerNr; } set { this.playerNr = value; } }
        public Shot Shot { get { return this.shot; } set { this.shot = value; } }
        public bool Hit { get { return this.hit; } set { this.hit = value; } }
        public int Turn { get { return this.turn; } set { this.turn = value; } }
    }
}