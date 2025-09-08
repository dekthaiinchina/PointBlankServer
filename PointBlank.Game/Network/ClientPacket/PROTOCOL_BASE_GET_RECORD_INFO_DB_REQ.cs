using PointBlank.Core;
using PointBlank.Game.Data.Managers;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Network.ServerPacket;
using System;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_BASE_GET_RECORD_INFO_DB_REQ : ReceivePacket
    {
        private long objId;

        public PROTOCOL_BASE_GET_RECORD_INFO_DB_REQ(GameClient client, byte[] data)
        {
            makeme(client, data);
        }

        public override void read()
        {
            objId = readQ();
        }

        public override void run()
        {
            if (_client._player == null)
            {
                return;
            }
            try
            {
                Account player = AccountManager.getAccount(objId, 0);
                _client.SendPacket(new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(player != null ? player._statistic : null));
            }
            catch (Exception ex)
            {
                Logger.info(ex.ToString());
            }
        }
    }
}