using PointBlank.Auth.Network.ServerPacket;
using PointBlank.Core;
using System;

namespace PointBlank.Auth.Network.ClientPacket
{
    public class PROTOCOL_BASE_USER_ENTER7681_REQ : ReceivePacket
    {
        public PROTOCOL_BASE_USER_ENTER7681_REQ(AuthClient client, byte[] data)
        {
            makeme(client, data);
        }

        public override void read()
        {

        }

        public override void run()
        {
            try
            {
                _client.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK("(7671) 0xc0000PZDC \nNot implemented... \n Work in progress :) (C) EmpireB \n Blayt' connection close..."));
                _client.Close(5, true);
            }
            catch (Exception ex)
            {
                Logger.warning(ex.ToString());
            }
        }
    }
}