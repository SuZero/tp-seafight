using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;

namespace Multiplayer
{
    public class Session : Microsoft.Xna.Framework.Game
    {
        NetworkSession networkSession;
        //ƒБE заБEси БEчтенБE БEБEтоБEдаыLых
        PacketWriter packetWriter;
        PacketReader packetReader;

        private AvailableNetworkSessionCollection availNetSessions = NetworkSession.Find(NetworkSessionType.SystemLink, 2, null);



        public void createRoom()
        {
            networkSession = NetworkSession.Create(NetworkSessionType.SystemLink, 1, 2);
            //ѕьCБEючаем ь@работчикБEсобытиБE
            joinEventHandlers();
        }

        public void joinInRoom()
        {
            networkSession = NetworkSession.Join(availNetSessions[0]);
            joinEventHandlers();
        }


        //ѕьCБEючеыGБEь@работчикьA событиБE
        void joinEventHandlers()
        {
            //»грьI вошеБEБEБEБEатБE
            networkSession.GamerJoined += new EventHandler<GamerJoinedEventArgs>(networkSession_GamerJoined);
            //—есси€ заБEыласБE
            networkSession.SessionEnded += new EventHandler<NetworkSessionEndedEventArgs>(networkSession_SessionEnded);
        }
        //ќбрабъCчиБEсобыти€, БEьGсхьC€щего БEБEБEисьDдиыDыGБEыMвого игроБE
        void networkSession_GamerJoined(object sender, GamerJoinedEventArgs e)
        {
            // ЌьKер игроБE
            int player = networkSession.AllGamers.IndexOf(e.Gamer);

            // e.Gamer.Tag = —ьFдать ь@ъеБE
        }

        //ќбрабъCчиБEсобыти€, БEьGсхьC€щего БEБEзаБEытии сессии
        void networkSession_SessionEnded(object sender, NetworkSessionEndedEventArgs e)
        {
            //уничтожиБEсессБE
            networkSession.Dispose();
            networkSession = null;
        }

        //ѕроцедура ь@работкБEсобытиБEБEтечеыGБEсессии
        void WorkWithSession()
        {

        }



    }
}
