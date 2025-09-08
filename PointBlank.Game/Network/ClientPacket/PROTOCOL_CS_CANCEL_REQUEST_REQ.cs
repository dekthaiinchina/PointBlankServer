using PointBlank.Core;
using PointBlank.Core.Managers;
using PointBlank.Game.Network.ServerPacket;
using System;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_CS_CANCEL_REQUEST_REQ : ReceivePacket
    {
        private uint erro;

        public PROTOCOL_CS_CANCEL_REQUEST_REQ(GameClient client, byte[] data)
        {
            makeme(client, data);
        }

        public override void run()
        {
            try
            {
                if (_client == null || !PlayerManager.DeleteInviteDb(_client.player_id))
                {
                    erro = 2147487835;
                }
                _client.SendPacket(new PROTOCOL_CS_CANCEL_REQUEST_ACK(erro));
            }
            catch (Exception ex)
            {
                Logger.info(ex.ToString());
            }
        }

        public override void read()
        {

        }
    }
}