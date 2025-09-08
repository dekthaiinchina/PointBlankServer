using PointBlank.Core.Network;

namespace PointBlank.Game.Network.ServerPacket
{
    public class PROTOCOL_LOBBY_LEAVE_ACK : SendPacket
    {
        private uint _erro;

        public PROTOCOL_LOBBY_LEAVE_ACK(uint erro)
        {
            _erro = erro;
        }

        public override void write()
        {
            writeH(3076);
            writeD(_erro);
        }
    }
}