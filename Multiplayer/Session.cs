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
        //Для записи и чтения пакетов данных
        PacketWriter packetWriter;
        PacketReader packetReader;

        private AvailableNetworkSessionCollection availNetSessions = NetworkSession.Find(NetworkSessionType.SystemLink, 2, null);



        public void createRoom()
        {
            networkSession = NetworkSession.Create(NetworkSessionType.SystemLink, 1, 2);
            //Подключаем обработчики событий
            joinEventHandlers();
        }

        public void joinInRoom()
        {
            networkSession = NetworkSession.Join(availNetSessions[0]);
            joinEventHandlers();
        }


        //Подключение обработчиков событий
        void joinEventHandlers()
        {
            //Игрок вошел в комнату
            networkSession.GamerJoined += new EventHandler<GamerJoinedEventArgs>(networkSession_GamerJoined);
            //Сессия закрылась
            networkSession.SessionEnded += new EventHandler<NetworkSessionEndedEventArgs>(networkSession_SessionEnded);
        }
        //Обработчик события, происходящего при присоединении нового игрока
        void networkSession_GamerJoined(object sender, GamerJoinedEventArgs e)
        {
            // Номер игрока
            int player = networkSession.AllGamers.IndexOf(e.Gamer);

            // e.Gamer.Tag = Создать объект
        }

        //Обработчик события, происходящего при закрытии сессии
        void networkSession_SessionEnded(object sender, NetworkSessionEndedEventArgs e)
        {
            //уничтожим сессию
            networkSession.Dispose();
            networkSession = null;
        }

        //Процедура обработки событий в течение сессии
        void WorkWithSession()
        {

        }



    }
}
