using PointBlank.Core;
using PointBlank.Core.Managers;
using PointBlank.Core.Models.Account.Clan;
using PointBlank.Core.Models.Enums;
using PointBlank.Game.Data.Managers;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Network.ServerPacket;
using System;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_CS_CLIENT_ENTER_REQ : ReceivePacket
    {
        private int id;

        public PROTOCOL_CS_CLIENT_ENTER_REQ(GameClient client, byte[] data)
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
                    room.changeSlotState(p._slotId, SlotState.CLAN, false);
                    room.StopCountDown(p._slotId);
                    room.updateSlotsInfo();
                }
                Clan clan = ClanManager.getClan(p.clanId);
                if (p.clanId == 0 && p.player_name.Length > 0)
                {
                    id = PlayerManager.getRequestClanId(p.player_id);
                }
                _client.SendPacket(new PROTOCOL_CS_CLIENT_ENTER_ACK(id > 0 ? id : clan._id, p.clanAccess));
                if (clan._id > 0 && id == 0)
                {
                    _client.SendPacket(new PROTOCOL_CS_DETAIL_INFO_ACK(0, clan));
                }
            }
            catch (Exception ex)
            {
                Logger.info("PROTOCOL_CS_CLIENT_ENTER_REQ: " + ex.ToString());
            }
        }
    }
}