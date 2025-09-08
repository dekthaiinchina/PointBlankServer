using PointBlank.Auth.Network.ServerPacket;
using PointBlank.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointBlank.Auth.Data.Model;
using PointBlank.Core;

namespace PointBlank.Auth.Network.ClientPacket
{
    public class PROTOCOL_BASE_DAILY_RECORD_REQ : ReceivePacket
    {
        public PROTOCOL_BASE_DAILY_RECORD_REQ(AuthClient Client, byte[] Buffer)
        {
            makeme(Client, Buffer);
        }

        public override void read()
        {

        }

        public override void run()
        {
            try
            {
                Account Player = _client._player;
                if (Player.Daily != null)
                {
                    _client.SendPacket(new PROTOCOL_BASE_DAILY_RECORD_ACK(Player.Daily));
                }
            }
            catch (Exception ex)
            {
                Logger.error("PROTOCOL_BASE_DAILY_RECORD_REQ: " + ex.ToString());
            }
        }
    }
}
