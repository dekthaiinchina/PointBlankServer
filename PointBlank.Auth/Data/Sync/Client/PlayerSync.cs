using PointBlank.Core.Network;
using PointBlank.Auth.Data.Managers;
using PointBlank.Auth.Data.Model;

namespace PointBlank.Auth.Data.Sync.Client
{
    public static class PlayerSync
    {
        public static void Load(ReceiveGPacket p)
        {
            long playerId = p.readQ();
            int type = p.readC();
            int rank = p.readC();
            int gold = p.readD();
            int cash = p.readD();
            int tag = p.readD();
            Account player = AccountManager.getInstance().getAccount(playerId, true);
            if (player == null)
            {
                return;
            }

            if (type == 0)
            {
                player._rank = rank;
                player._gp = gold;
                player._money = cash;
                player._tag = tag;
            }
        }
    }
}