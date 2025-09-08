using PointBlank.Core;
using PointBlank.Core.Models.Enums;
using PointBlank.Core.Models.Room;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Data.Utils;
using PointBlank.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Threading;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_ROOM_TOTAL_TEAM_CHANGE_REQ : ReceivePacket
    {
        private List<SlotChange> changeList = new List<SlotChange>();

        public PROTOCOL_ROOM_TOTAL_TEAM_CHANGE_REQ(GameClient client, byte[] data)
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
                Room r = p == null ? null : p._room;
                if (r != null && r._leader == p._slotId && r.RoomState == RoomState.Ready && !r.changingSlots)
                {
                    Monitor.Enter(r._slots);
                    r.changingSlots = true;
                    foreach (int slotIdx in r.RED_TEAM)
                    {
                        int NewId = (slotIdx + 1);
                        if (slotIdx == r._leader)
                        {
                            r._leader = NewId;
                        }
                        else if (NewId == r._leader)
                        {
                            r._leader = slotIdx;
                        }
                        r.SwitchSlots(changeList, NewId, slotIdx, true);
                    }
                    if (changeList.Count > 0)
                    {
                        using (PROTOCOL_ROOM_TEAM_BALANCE_ACK packet = new PROTOCOL_ROOM_TEAM_BALANCE_ACK(changeList, r._leader, 2))
                        {
                            byte[] data = packet.GetCompleteBytes("PROTOCOL_ROOM_CHANGE_TEAM_REQ");
                            for (int idx = 0; idx < r.getAllPlayers().Count; idx++)
                            {
                                Account ac = r.getAllPlayers()[idx];
                                ac._slotId = AllUtils.getNewSlotId(ac._slotId);
                                ac.SendCompletePacket(data);
                            }
                        }
                    }
                    r.changingSlots = false;
                    Monitor.Exit(r._slots);
                }
            }
            catch (Exception ex)
            {
                Logger.info("PROTOCOL_ROOM_CHANGE_TEAM_REQ: " + ex.ToString());
            }
        }
    }
}