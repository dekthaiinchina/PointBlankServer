using PointBlank.Core;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Network.ServerPacket;
using System;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_NEW_MYINFO_REQ : ReceivePacket
    {
        public PROTOCOL_NEW_MYINFO_REQ(GameClient client, byte[] data)
        {
            makeme(client, data);
        }

        public override void read()
        {
            //no read here
        }

        public override void run()
        {
            try
            {
                Account player = _client._player;
                if (_client == null || _client._player == null)
                {
                    return;
                }
                else
                    _client.SendPacket(new PROTOCOL_NEW_MYINFO_ACK(player.player_id));
            }
            catch (Exception ex)
            {
                Logger.info("PROTOCOL_NEW_MYINFO_REQ: " + ex.ToString());
            }
        }
    }
}