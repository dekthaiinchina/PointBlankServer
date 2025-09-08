using PointBlank.Core;
using PointBlank.Core.Models.Enums;
using PointBlank.Core.Models.Room;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Data.Utils;
using System;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_ROOM_LOADING_START_REQ : ReceivePacket
    {
        private string name;

        public PROTOCOL_ROOM_LOADING_START_REQ(GameClient client, byte[] data)
        {
            makeme(client, data);
        }

        public override void read()
        {
            name = readS(readC());
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
                Slot slot;
                if (room != null && room.isPreparing() && room.getSlot(p._slotId, out slot) && slot.state == SlotState.LOAD)
                {
                    slot.preLoadDate = DateTime.Now;
                    room.StartCounter(0, p, slot);
                    room.changeSlotState(slot, SlotState.RENDEZVOUS, true);
                    room._mapName = name;
                    if (slot._id == room._leader)
                    {
                        room.RoomState = RoomState.Rendezvous;
                        room.updateRoomInfo();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.info("PROTOCOL_ROOM_LOADING_START_REQ: " + ex.ToString());
            }
        }
    }
}