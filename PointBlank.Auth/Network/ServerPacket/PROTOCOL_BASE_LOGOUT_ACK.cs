using PointBlank.Core.Network;

namespace PointBlank.Auth.Network.ServerPacket
{
    public class PROTOCOL_BASE_LOGOUT_ACK : SendPacket
    {
        public PROTOCOL_BASE_LOGOUT_ACK()
        {

        }

        public override void write()
        {
            writeH(516);
            writeD(0);
        }
    }
}