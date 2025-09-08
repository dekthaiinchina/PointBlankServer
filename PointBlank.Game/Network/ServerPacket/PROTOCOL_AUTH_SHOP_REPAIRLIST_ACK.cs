using PointBlank.Core.Managers;
using PointBlank.Core.Network;

namespace PointBlank.Game.Network.ServerPacket
{
    public class PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK : SendPacket
    {
        private int Total;
        private ShopData Data;

        public PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK(ShopData Data, int Total)
        {
            this.Data = Data;
            this.Total = Total;
        }

        public override void write()
        {
            writeH(1070);
            writeD(Total);
            writeD(Data.ItemsCount);
            writeD(Data.Offset);
            writeB(Data.Buffer);
        }
    }
}