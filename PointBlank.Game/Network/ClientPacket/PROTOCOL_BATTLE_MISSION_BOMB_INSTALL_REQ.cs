using PointBlank.Core;
using PointBlank.Core.Models.Enums;
using PointBlank.Core.Models.Room;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Data.Sync.Client;
using System;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ : ReceivePacket
    {
        private int slotIdx;
        private float x, y, z;
        private byte area;

        public PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ(GameClient client, byte[] data)
        {
            makeme(client, data);
        }

        public override void read()
        {
            slotIdx = readD();
            area = readC();
            x = readT();
            y = readT();
            z = readT();
        }

        public override void run()
        {
            try
            {
                Account player = _client._player;
                Room room = player == null ? null : player._room;
                if (room != null && room.round.Timer == null && room.RoomState == RoomState.Battle && !room.C4_actived)
                {
                    Slot slot = room.getSlot(slotIdx);
                    if (slot == null || slot.state != SlotState.BATTLE)
                    {
                        return;
                    }
                    RoomC4.InstallBomb(room, slot, area, x, y, z);
                }
            }
            catch (Exception ex)
            {
                Logger.info("PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ: " + ex.ToString());
            }
        }
    }
}