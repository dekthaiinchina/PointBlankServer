using PointBlank.Core.Models.Room;
using PointBlank.Core.Network;
using PointBlank.Game.Data.Model;

namespace PointBlank.Game.Network.ServerPacket
{
    public class PROTOCOL_BATTLE_SENDPING_ACK : SendPacket
    {
        private byte[] Pings;

        public PROTOCOL_BATTLE_SENDPING_ACK(byte[] Pings)
        {
            this.Pings = Pings;
        }

        public override void write()
        {
            writeH(4123);
            writeB(Pings);
        }
    }
}