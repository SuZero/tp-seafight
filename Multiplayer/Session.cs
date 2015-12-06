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
        //��� ������ � ������ ������� ������
        PacketWriter packetWriter;
        PacketReader packetReader;

        private AvailableNetworkSessionCollection availNetSessions = NetworkSession.Find(NetworkSessionType.SystemLink, 2, null);



        public void createRoom()
        {
            networkSession = NetworkSession.Create(NetworkSessionType.SystemLink, 1, 2);
            //���������� ����������� �������
            joinEventHandlers();
        }

        public void joinInRoom()
        {
            networkSession = NetworkSession.Join(availNetSessions[0]);
            joinEventHandlers();
        }


        //����������� ������������ �������
        void joinEventHandlers()
        {
            //����� ����� � �������
            networkSession.GamerJoined += new EventHandler<GamerJoinedEventArgs>(networkSession_GamerJoined);
            //������ ���������
            networkSession.SessionEnded += new EventHandler<NetworkSessionEndedEventArgs>(networkSession_SessionEnded);
        }
        //���������� �������, ������������� ��� ������������� ������ ������
        void networkSession_GamerJoined(object sender, GamerJoinedEventArgs e)
        {
            // ����� ������
            int player = networkSession.AllGamers.IndexOf(e.Gamer);

            // e.Gamer.Tag = ������� ������
        }

        //���������� �������, ������������� ��� �������� ������
        void networkSession_SessionEnded(object sender, NetworkSessionEndedEventArgs e)
        {
            //��������� ������
            networkSession.Dispose();
            networkSession = null;
        }

        //��������� ��������� ������� � ������� ������
        void WorkWithSession()
        {

        }



    }
}
