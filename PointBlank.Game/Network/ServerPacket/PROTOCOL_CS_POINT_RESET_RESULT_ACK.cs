using PointBlank.Core.Network;

namespace PointBlank.Game.Network.ServerPacket
{
    public class PROTOCOL_CS_POINT_RESET_RESULT_ACK : SendPacket
    {
        public PROTOCOL_CS_POINT_RESET_RESULT_ACK()
        {

        }

        public override void write()
        {
            writeH(1930);
        }
    }
}