using PointBlank.Core;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Network.ServerPacket;
using System;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_CS_REQUEST_CONTEXT_REQ : ReceivePacket
    {
        public PROTOCOL_CS_REQUEST_CONTEXT_REQ(GameClient client, byte[] data)
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
                Account player = _client._player;
                if (player == null)
                {
                    return;
                }
                _client.SendPacket(new PROTOCOL_CS_REQUEST_CONTEXT_ACK(player.clanId));
            }
            catch (Exception ex)
            {
                Logger.info(ex.ToString());
            }
        }
    }
}