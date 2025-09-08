using PointBlank.Core;
using PointBlank.Core.Managers;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Network.ServerPacket;
using System;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_CS_REQUEST_INFO_REQ : ReceivePacket
    {
        private long pId;

        public PROTOCOL_CS_REQUEST_INFO_REQ(GameClient client, byte[] data)
        {
            makeme(client, data);
        }

        public override void read()
        {
            pId = readQ();
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
                _client.SendPacket(new PROTOCOL_CS_REQUEST_INFO_ACK(pId, PlayerManager.getRequestText(player.clanId, pId)));
            }
            catch (Exception ex)
            {
                Logger.info(ex.ToString());
            }
        }
    }
}