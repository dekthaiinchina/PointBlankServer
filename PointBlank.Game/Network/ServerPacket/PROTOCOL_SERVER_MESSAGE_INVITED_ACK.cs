using PointBlank.Core.Network;
using PointBlank.Game.Data.Model;

namespace PointBlank.Game.Network.ServerPacket
{
    public class PROTOCOL_SERVER_MESSAGE_INVITED_ACK : SendPacket
    {
        private Account sender;
        private Room room;

        public PROTOCOL_SERVER_MESSAGE_INVITED_ACK(Account sender, Room room)
        {
            this.sender = sender;
            this.room = room;
        }

        public override void write()
        {
            writeH(2565);
            writeUnicode(sender.player_name, 66);
            writeD(room._roomId);
            writeQ(sender.player_id);
            writeS(room.password, 4);
        }
    }
}