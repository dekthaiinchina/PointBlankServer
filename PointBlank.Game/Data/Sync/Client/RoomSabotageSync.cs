using PointBlank.Core;
using PointBlank.Core.Models.Enums;
using PointBlank.Core.Models.Room;
using PointBlank.Core.Network;
using PointBlank.Game.Data.Utils;
using PointBlank.Game.Data.Xml;
using PointBlank.Game.Network.ServerPacket;
using PointBlank.Game.Data.Model;
using System;

namespace PointBlank.Game.Data.Sync.Client
{
    public class RoomSabotageSync
    {
        public static void Load(ReceiveGPacket p)
        {
            int roomId = p.readH();
            int channelId = p.readH();
            byte killerIdx = p.readC();
            ushort redObjective = p.readUH();
            ushort blueObjective = p.readUH();
            int barNumber = p.readC();
            ushort damage = p.readUH();
            if (p.getBuffer().Length > 14)
            {
                Logger.warning("Invalid Sabotage: " + BitConverter.ToString(p.getBuffer()));
            }
            Channel ch = ChannelsXml.getChannel(channelId);
            if (ch == null)
            {
                return;
            }
            Room room = ch.getRoom(roomId);
            Slot killer;
            if (room != null && room.round.Timer == null && room.RoomState == RoomState.Battle && !room.swapRound && room.getSlot(killerIdx, out killer))
            {
                room.Bar1 = redObjective;
                room.Bar2 = blueObjective;
                RoomType type = (RoomType)room.RoomType;
                int times = 0;
                if (barNumber == 1)
                {
                    killer.damageBar1 += damage;
                    times += killer.damageBar1 / 600;
                }
                else if (barNumber == 2)
                {
                    killer.damageBar2 += damage;
                    times += killer.damageBar2 / 600;
                }
                killer.earnedXP = times;
                if (type == RoomType.Destroy)
                {
                    using (PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK packet = new PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK(room))
                    {
                        room.SendPacketToPlayers(packet, SlotState.BATTLE, 0);
                    }
                    if (room.Bar1 == 0)
                    {
                        EndRound(room, 1);
                    }
                    else if (room.Bar2 == 0)
                    {
                        EndRound(room, 0);
                    }
                }
                else if (type == RoomType.Defense)
                {
                    using (PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK packet = new PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK(room))
                    {
                        room.SendPacketToPlayers(packet, SlotState.BATTLE, 0);
                    }
                    if (room.Bar1 == 0 && room.Bar2 == 0)
                    {
                        EndRound(room, 0);
                    }
                }
            }
        }
        public static void EndRound(Room room, byte winner)
        {
            room.swapRound = true;
            if (winner == 0)
            {
                room.red_rounds++;
            }
            else if (winner == 1)
            {
                room.blue_rounds++;
            }
            AllUtils.BattleEndRound(room, winner, RoundEndType.Normal);
        }
    }
}