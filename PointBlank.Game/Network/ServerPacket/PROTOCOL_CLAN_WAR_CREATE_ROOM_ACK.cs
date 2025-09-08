using PointBlank.Core.Network;
using PointBlank.Game.Data.Model;

namespace PointBlank.Game.Network.ServerPacket
{
    public class PROTOCOL_CLAN_WAR_CREATE_ROOM_ACK : SendPacket
    {
        public Match _mt;
        public PROTOCOL_CLAN_WAR_CREATE_ROOM_ACK(Match match)
        {
            _mt = match;
        }

        public override void write()
        {
            writeH(1564);
            writeH((short)_mt._matchId);
            writeD(_mt.getServerInfo());
            writeH((short)_mt.getServerInfo());
            writeC(10);
        }
    }
}