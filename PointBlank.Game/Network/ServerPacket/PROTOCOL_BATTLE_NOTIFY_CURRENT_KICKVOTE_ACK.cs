using PointBlank.Core.Models.Room;
using PointBlank.Core.Network;

namespace PointBlank.Game.Network.ServerPacket
{
    public class PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK : SendPacket
    {
        private VoteKick _k;

        public PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK(VoteKick vote)
        {
            _k = vote;
        }

        public override void write()
        {
            writeH(3407);
            writeC((byte)_k.kikar);
            writeC((byte)_k.deixar);
        }
    }
}