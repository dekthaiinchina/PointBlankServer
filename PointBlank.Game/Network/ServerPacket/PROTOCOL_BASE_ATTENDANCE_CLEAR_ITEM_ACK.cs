using PointBlank.Core.Models.Enums;
using PointBlank.Core.Network;

namespace PointBlank.Game.Network.ServerPacket
{
    public class PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK : SendPacket
    {
        private uint _erro;

        public PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK(EventErrorEnum erro)
        {
            _erro = (uint)erro;
        }

        public override void write()
        {
            writeH(547);
            writeD(_erro);
        }
    }
}