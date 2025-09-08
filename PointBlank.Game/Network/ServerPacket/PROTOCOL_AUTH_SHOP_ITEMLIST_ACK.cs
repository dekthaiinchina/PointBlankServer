using PointBlank.Core.Managers;
using PointBlank.Core.Network;

namespace PointBlank.Game.Network.ServerPacket
{
    public class PROTOCOL_AUTH_SHOP_ITEMLIST_ACK : SendPacket
    {
        private int _tudo;
        private ShopData data;

        public PROTOCOL_AUTH_SHOP_ITEMLIST_ACK(ShopData data, int tudo)
        {
            this.data = data;
            _tudo = tudo;
        }

        public override void write()
        {
            writeH(1038);
            writeD(_tudo);
            writeD(data.ItemsCount);
            writeD(data.Offset);
            writeB(data.Buffer);
        }
    }
}