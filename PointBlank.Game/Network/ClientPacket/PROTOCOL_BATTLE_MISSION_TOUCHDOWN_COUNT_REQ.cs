using PointBlank.Core;
using PointBlank.Core.Models.Enums;

using PointBlank.Core.Models.Room;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Data.Utils;
using PointBlank.Game.Network.ServerPacket;
using System;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ : ReceivePacket
    {
        private int weaponId, TRex;

        public PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ(GameClient client, byte[] data)
        {
            makeme(client, data);
        }

        public override void read()
        {
            TRex = readC();
            weaponId = readD();
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
                if (room != null && room.round.Timer == null && room.RoomState == RoomState.Battle && room.TRex == TRex)
                {
                    Slot slot = room.getSlot(p._slotId);
                    if (slot == null || slot.state != SlotState.BATTLE)
                    {
                        return;
                    }
                    if (slot._team == 0)
                    {
                        room.red_dino += 5;
                    }
                    else
                    {
                        room.blue_dino += 5;
                    }
                    using (PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK packet = new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK(room))
                    {
                        room.SendPacketToPlayers(packet, SlotState.BATTLE, 0);
                    }
                    AllUtils.CompleteMission(room, p, slot, MissionType.DEATHBLOW, weaponId);
                }
            }
            catch (Exception ex)
            {
                Logger.info("PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ: " + ex.ToString());
            }
        }
    }
}