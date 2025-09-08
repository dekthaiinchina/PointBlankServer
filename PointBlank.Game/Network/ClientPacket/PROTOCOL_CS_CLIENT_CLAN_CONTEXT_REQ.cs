using PointBlank.Core;
using PointBlank.Game.Data.Managers;
using PointBlank.Game.Network.ServerPacket;
using System;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_CS_CLIENT_CLAN_CONTEXT_REQ : ReceivePacket
    {
        public PROTOCOL_CS_CLIENT_CLAN_CONTEXT_REQ(GameClient client, byte[] data)
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
                 _client.SendPacket(new PROTOCOL_CS_CLIENT_CLAN_CONTEXT_ACK(ClanManager._clans.Count));
            }
            catch (Exception ex)
            {
                Logger.info(ex.ToString());
            }
        }
    }
}