using PointBlank.Core.Network;
using PointBlank.Game.Data.Managers;
using PointBlank.Game.Data.Model;

namespace PointBlank.Game.Data.Sync.Client
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

            Account player = AccountManager.getAccount(playerId, true);
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