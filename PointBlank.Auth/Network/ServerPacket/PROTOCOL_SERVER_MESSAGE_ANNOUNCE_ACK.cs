using PointBlank.Core.Models.Enums;
using PointBlank.Core.Network;

namespace PointBlank.Auth.Network.ServerPacket
{
    public class PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK : SendPacket
    {
        private string _message;

        public PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(string msg)
        {
            _message = msg;
        }

        public override void write()
        {
            writeH(2567);
            writeH(0);
            writeD(0);
            writeH((ushort)(_message.Length));
            writeUnicode(_message, false);
            writeD(2);
        }
    }
}