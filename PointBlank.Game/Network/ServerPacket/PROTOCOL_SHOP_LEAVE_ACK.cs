using PointBlank.Core.Network;

namespace PointBlank.Game.Network.ServerPacket
{
    public class PROTOCOL_SHOP_LEAVE_ACK : SendPacket
    {
        public PROTOCOL_SHOP_LEAVE_ACK()
        {

        }

        public override void write()
        {
            writeH(1028);
            writeH(0);
            writeD(0);
        }
    }
}