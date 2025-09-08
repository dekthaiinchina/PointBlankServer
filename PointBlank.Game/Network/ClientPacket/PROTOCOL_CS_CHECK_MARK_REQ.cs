using PointBlank.Game.Data.Managers;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Network.ServerPacket;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_CS_CHECK_MARK_REQ : ReceivePacket
    {
        private uint logo, erro;

        public PROTOCOL_CS_CHECK_MARK_REQ(GameClient client, byte[] data)
        {
            makeme(client, data);
        }

        public override void read()
        {
            logo = readUD();
        }

        public override void run()
        {
            Account p = _client._player;
            if (p == null || ClanManager.getClan(p.clanId)._logo == logo || ClanManager.isClanLogoExist(logo))
            {
                erro = 0x80000000;
            }
            _client.SendPacket(new PROTOCOL_CS_CHECK_MARK_ACK(erro));
        }
    }
}