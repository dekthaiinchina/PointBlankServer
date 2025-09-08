using PointBlank.Core.Models.Enums;
using PointBlank.Core.Network;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Data.Utils;

namespace PointBlank.Game.Network.ServerPacket
{
    public class PROTOCOL_BATTLE_MISSION_ROUND_START_ACK : SendPacket
    {
        private Room _r;

        public PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(Room r)
        {
            _r = r;
        }

        public override void write()
        {
            writeH(4129);
            writeC((byte)_r.rounds);
            writeD(_r.getInBattleTimeLeft());
            writeH(AllUtils.getSlotsFlag(_r, true, false));
            writeC(0);
        }
    }
}