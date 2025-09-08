using PointBlank.Core;
using PointBlank.Core.Network;
using PointBlank.Game.Data.Configs;

namespace PointBlank.Game.Network.ServerPacket
{
    public class PROTOCOL_CS_CREATE_CLAN_CONDITION_ACK : SendPacket
    {
        public PROTOCOL_CS_CREATE_CLAN_CONDITION_ACK()
        {

        }

        public override void write()
        {
            writeH(1937);
            writeC((byte)GameConfig.minCreateRank);
            writeD(GameConfig.minCreateGold);
        }
    }
}