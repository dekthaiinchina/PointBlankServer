using PointBlank.Core.Models.Enums;
using PointBlank.Core.Models.Room;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Data.Sync.Client;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_REQ : ReceivePacket
    {
        private int slotIdx;

        public PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_REQ(GameClient client, byte[] data)
        {
            makeme(client, data);
        }

        public override void read()
        {
            slotIdx = readD();
        }

        public override void run()
        {
            Account player = _client._player;
            Room room = player == null ? null : player._room;
            if (room != null && room.round.Timer == null && room.RoomState == RoomState.Battle && room.C4_actived)
            {
                Slot slot = room.getSlot(slotIdx);
                if (slot == null || slot.state != SlotState.BATTLE)
                {
                    return;
                }
                RoomC4.UninstallBomb(room, slot);
            }
        }
    }
}