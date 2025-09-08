using PointBlank.Core;
using PointBlank.Core.Models.Enums;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Network.ServerPacket;
using System;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_CS_CLIENT_LEAVE_REQ : ReceivePacket
    {
        public PROTOCOL_CS_CLIENT_LEAVE_REQ(GameClient client, byte[] data)
        {
            makeme(client, data);
        }

        public override void read()
        {

        }

        public override void run()
        {
            try
            {
                Account p = _client._player;
                if (p == null)
                {
                    return;
                }
                Room room = p._room;
                if (room != null)
                {
                    room.changeSlotState(p._slotId, SlotState.NORMAL, true);
                }
                _client.SendPacket(new PROTOCOL_CS_CLIENT_LEAVE_ACK());
            }
            catch (Exception ex)
            {
                Logger.info("PROTOCOL_CS_CLIENT_LEAVE_REQ: " + ex.ToString());
            }
        }
    }
}