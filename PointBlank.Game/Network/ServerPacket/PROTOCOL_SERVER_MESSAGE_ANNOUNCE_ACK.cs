using PointBlank.Core;
using PointBlank.Core.Models.Enums;
using PointBlank.Core.Network;

namespace PointBlank.Game.Network.ServerPacket
{
    public class PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK : SendPacket
    {
        private string _message;

        public PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(string msg)
        {
            _message = msg;
            if (msg.Length >= 1024)
            {
                Logger.error("Message with size bigger than 1024 sent!!");
            }
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