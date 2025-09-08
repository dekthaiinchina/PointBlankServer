using PointBlank.Core.Network;
using PointBlank.Game.Data.Managers;
using PointBlank.Game.Data.Model;

namespace PointBlank.Game.Data.Sync.Client
{
    public static class ClanSync
    {
        public static void Load(ReceiveGPacket p)
        {
            long playerId = p.readQ();
            int type = p.readC();
            Account player = AccountManager.getAccount(playerId, true);
            if (player == null)
            {
                return;
            }

            if (type == 3)
            {
                int clanId = p.readD();
                int clanAccess = p.readC();
                player.clanId = clanId;
                player.clanAccess = clanAccess;
            }
        }
    }
}