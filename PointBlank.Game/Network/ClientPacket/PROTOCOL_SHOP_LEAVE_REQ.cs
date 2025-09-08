using PointBlank.Core;
using PointBlank.Core.Managers;
using PointBlank.Core.Models.Account.Players;
using PointBlank.Core.Models.Enums;
using PointBlank.Core.Network;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Data.Utils;
using PointBlank.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_SHOP_LEAVE_REQ : ReceivePacket
    {
        public PROTOCOL_SHOP_LEAVE_REQ(GameClient client, byte[] data)
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
                if (_client == null)
                {
                    return;
                }
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
                _client.SendPacket(new PROTOCOL_SHOP_LEAVE_ACK());
                /*if (erro > 0)
                {
                    _client.SendPacket(new PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK(p, 3));
                }*/
            }
            catch (Exception ex)
            {
                Logger.info("PROTOCOL_SHOP_LEAVE_REQ: " + ex.ToString());
            }
        }
    }
}