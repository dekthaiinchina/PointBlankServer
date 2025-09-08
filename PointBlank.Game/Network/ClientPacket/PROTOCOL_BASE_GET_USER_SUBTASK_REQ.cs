using PointBlank.Game.Data.Model;
using PointBlank.Game.Network.ServerPacket;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_BASE_GET_USER_SUBTASK_REQ : ReceivePacket
    {
        private uint SessionId;
        public PROTOCOL_BASE_GET_USER_SUBTASK_REQ(GameClient client, byte[] data)
        {
            makeme(client, data);
        }

        public override void read()
        {
            SessionId = readUD();
        }

        public override void run()
        {
            Account player = _client._player;
            _client.SendPacket(new PROTOCOL_BASE_GET_USER_SUBTASK_ACK(SessionId));
        }
    }
}