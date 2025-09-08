using PointBlank.Core.Models.Enums;
using PointBlank.Core.Network;
using PointBlank.Game.Data.Model;

namespace PointBlank.Game.Network.ServerPacket
{
    public class PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK : SendPacket
    {
        private Account p;
        private ulong status;

        public PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(Account player)
        {
            p = player;
            status = ComDiv.GetClanStatus(player._status, player._isOnline);
        }

        public PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(Account player, FriendState st)
        {
            p = player;
            if (st == 0)
            {
                status = ComDiv.GetClanStatus(player._status, player._isOnline);
            }
            else
            {
                status = ComDiv.GetClanStatus(st);
            }
        }

        public override void write()
        {
            writeH(1875);
            writeQ(p.player_id);
            writeQ(status);
        }
    }
}