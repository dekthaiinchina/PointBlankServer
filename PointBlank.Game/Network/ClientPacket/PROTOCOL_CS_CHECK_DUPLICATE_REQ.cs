using PointBlank.Game.Data.Managers;
using PointBlank.Game.Network.ServerPacket;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_CS_CHECK_DUPLICATE_REQ : ReceivePacket
    {
        private string clanName;

        public PROTOCOL_CS_CHECK_DUPLICATE_REQ(GameClient client, byte[] data)
        {
            makeme(client, data);
        }

        public override void read()
        {
            clanName = readUnicode(readC() * 2);
        }

        public override void run()
        {
            if (_client == null || _client._player == null)
            {
                return;
            }
            try
            {
                _client.SendPacket(new PROTOCOL_CS_CHECK_DUPLICATE_ACK(!ClanManager.isClanNameExist(clanName) ? 0 : 0x80000000));
            }
            catch
            {

            }
        }
    }
}