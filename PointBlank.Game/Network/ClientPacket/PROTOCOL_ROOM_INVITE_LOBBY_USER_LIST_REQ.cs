using PointBlank.Game.Data.Managers;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Network.ServerPacket;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ : ReceivePacket
    {
        private int count;
        private uint erro;

        public PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ(GameClient client, byte[] data)
        {
            makeme(client, data);
        }

        public override void read()
        {
            count = readD();
        }

        public override void run()
        {
            Account p = _client._player;
            if (p != null && p._room != null)
            {
                Channel ch = p.getChannel();
                if (ch != null)
                {
                    using (PROTOCOL_SERVER_MESSAGE_INVITED_ACK packet = new PROTOCOL_SERVER_MESSAGE_INVITED_ACK(p, p._room))
                    {
                        byte[] data = packet.GetCompleteBytes("PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ");
                        for (int i = 0; i < count; i++)
                        {
                            try
                            {
                                Account ps = AccountManager.getAccount(ch.getPlayer(readUD())._playerId, true);
                                if (ps != null)
                                {
                                    ps.SendCompletePacket(data);
                                }
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }
            else
            {
                erro = 0x80000000;
            }
            _client.SendPacket(new PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK(erro));
        }
    }
}