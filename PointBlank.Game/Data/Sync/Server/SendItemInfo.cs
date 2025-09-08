using PointBlank.Core.Models.Account.Players;
using PointBlank.Core.Models.Servers;
using PointBlank.Core.Network;
using PointBlank.Game.Data.Model;

namespace PointBlank.Game.Data.Sync.Server
{
    public class SendItemInfo
    {
        public static void LoadItem(Account player, ItemsModel item)
        {
            if (player == null || player._status.serverId == 0)
            {
                return;
            }
            GameServerModel gs = GameSync.GetServer(player._status);
            if (gs == null)
            {
                return;
            }

            using (SendGPacket pk = new SendGPacket())
            {
                pk.writeH(18);
                pk.writeQ(player.player_id);
                pk.writeQ(item._objId);
                pk.writeD(item._id);
                pk.writeC((byte)item._equip);
                pk.writeC((byte)item._category);
                pk.writeQ(item._count);
                GameSync.SendPacket(pk.mstream.ToArray(), gs.Connection);
            }
        }

        public static void LoadGoldCash(Account player)
        {
            if (player == null)
            {
                return;
            }
            GameServerModel gs = GameSync.GetServer(player._status);
            if (gs == null)
            {
                return;
            }

            using (SendGPacket pk = new SendGPacket())
            {
                pk.writeH(19);
                pk.writeQ(player.player_id);
                pk.writeC(0);
                pk.writeC((byte)player._rank);
                pk.writeD(player._gp);
                pk.writeD(player._money);
                GameSync.SendPacket(pk.mstream.ToArray(), gs.Connection);
            }
        }
    }
}