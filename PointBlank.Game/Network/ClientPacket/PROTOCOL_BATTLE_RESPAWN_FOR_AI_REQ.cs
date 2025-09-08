using PointBlank.Core;
using PointBlank.Core.Models.Enums;
using PointBlank.Core.Models.Room;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Network.ServerPacket;
using System;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ : ReceivePacket
    {
        private int slotIdx;

        public PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ(GameClient gc, byte[] data)
        {
            makeme(gc, data);
        }

        public override void read()
        {
            slotIdx = readD();
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
                if (room != null && room.RoomState == RoomState.Battle && p._slotId == room._leader)
                {
                    Slot slot = room.getSlot(slotIdx);
                    slot.aiLevel = room.IngameAiLevel;
                    room.spawnsCount++;
                    using (PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK packet = new PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK(slotIdx))
                    {
                        room.SendPacketToPlayers(packet, SlotState.BATTLE, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.info("PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ: " + ex.ToString());
            }
        }
    }
}