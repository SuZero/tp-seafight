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
        //āE ���E�� �E����E �E�E��E���L��
        PacketWriter packetWriter;
        PacketReader packetReader;

        private AvailableNetworkSessionCollection availNetSessions = NetworkSession.Find(NetworkSessionType.SystemLink, 2, null);



        public void createRoom()
        {
            networkSession = NetworkSession.Create(NetworkSessionType.SystemLink, 1, 2);
            //��C�E����� �@��������E������E
            joinEventHandlers();
        }

        public void joinInRoom()
        {
            networkSession = NetworkSession.Join(availNetSessions[0]);
            joinEventHandlers();
        }


        //��C�E����G�E�@���������A ������E
        void joinEventHandlers()
        {
            //����I ����E�E�E�E��E
            networkSession.GamerJoined += new EventHandler<GamerJoinedEventArgs>(networkSession_GamerJoined);
            //������ ���E����E
            networkSession.SessionEnded += new EventHandler<NetworkSessionEndedEventArgs>(networkSession_SessionEnded);
        }
        //������C��E�������, �E�G���C����� �E�E�E���D���D�G�E�M���� ����E
        void networkSession_GamerJoined(object sender, GamerJoinedEventArgs e)
        {
            // ��K�� ����E
            int player = networkSession.AllGamers.IndexOf(e.Gamer);

            // e.Gamer.Tag = ��F���� �@��E
        }

        //������C��E�������, �E�G���C����� �E�E���E���� ������
        void networkSession_SessionEnded(object sender, NetworkSessionEndedEventArgs e)
        {
            //��������E����E
            networkSession.Dispose();
            networkSession = null;
        }

        //��������� �@������E������E�E�����G�E������
        void WorkWithSession()
        {

        }



    }
}
