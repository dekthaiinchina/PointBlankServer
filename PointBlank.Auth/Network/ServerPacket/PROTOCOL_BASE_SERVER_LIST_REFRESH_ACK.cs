using PointBlank.Core.Models.Servers;
using PointBlank.Core.Network;
using PointBlank.Core.Xml;

namespace PointBlank.Auth.Network.ServerPacket
{
    public class PROTOCOL_BASE_SERVER_LIST_REFRESH_ACK : SendPacket
    {
        public PROTOCOL_BASE_SERVER_LIST_REFRESH_ACK()
        {

        }

        public override void write()
        {
            writeH(698);
            writeD(ServersXml._servers.Count);
            for (int i = 0; i < ServersXml._servers.Count; i++)
            {
                GameServerModel server = ServersXml._servers[i];
                writeD(server._state);
                writeIP(server.Connection.Address);
                writeH(server._port);
                writeC((byte)server._type);
                writeH((ushort)server._maxPlayers);
                writeD(server._LastCount);
            }
        }
    }
}