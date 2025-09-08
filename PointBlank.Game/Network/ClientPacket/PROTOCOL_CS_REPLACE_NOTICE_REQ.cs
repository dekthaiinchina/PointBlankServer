using PointBlank.Core.Network;
using PointBlank.Core.Models.Account.Clan;
using PointBlank.Game.Data.Managers;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Network.ServerPacket;
using PointBlank.Core;
using System.Text;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_CS_REPLACE_NOTICE_REQ : ReceivePacket
    {
        private string clan_news;
        private uint erro;

        public PROTOCOL_CS_REPLACE_NOTICE_REQ(GameClient client, byte[] data)
        {
            makeme(client, data);
        }

        public override void read()
        {
            clan_news = readUnicode(readC() * 2);
        }

        public override void run()
        {
            try
            {
                Account p = _client._player;
                if (p != null)
                {
                    Clan c = ClanManager.getClan(p.clanId);
                    if (c._id > 0 && c._news != clan_news && (c.owner_id == _client.player_id || p.clanAccess >= 1 && p.clanAccess <= 2))
                    {
                        if (ComDiv.updateDB("clans", "clan_news", clan_news, "clan_id", c._id))
                        {
                            c._news = clan_news;
                        }
                        else
                        {
                            erro = 2147487859;
                        }
                    }
                    else
                    {
                        erro = 2147487835;
                    }
                }
                else
                {
                    erro = 2147487835;
                }
            }
            catch
            {
                erro = 2147487859;
            }
            _client.SendPacket(new PROTOCOL_CS_REPLACE_NOTICE_ACK(erro));
        }
    }
}