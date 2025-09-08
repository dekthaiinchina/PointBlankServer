using PointBlank.Core.Managers;
using PointBlank.Core.Network;

namespace PointBlank.Game.Network.ServerPacket
{
    public class PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK : SendPacket
    {
        private int _tudo;
        private ShopData data;

        public PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK(ShopData data, int tudo)
        {
            this.data = data;
            _tudo = tudo;
        }

        public override void write()
        {
            writeH(1040);
            writeD(_tudo);
            writeD(data.ItemsCount);
            writeD(data.Offset);
            writeB(data.Buffer);
            writeD(777);
            writeC(0);
        }
    }
}