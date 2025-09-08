using PointBlank.Core;
using PointBlank.Core.Models.Enums;
using PointBlank.Core.Models.Room;
using PointBlank.Game.Data.Model;

namespace PointBlank.Game.Data.Chat
{
    public static class LatencyAnalyze
    {
        public static string StartAnalyze(Account player, Room room)
        {
            if (room == null)
            {
                return Translation.GetLabel("GeneralRoomInvalid");
            }

            Slot slot = room.getSlot(player._slotId);
            if (slot.state == SlotState.BATTLE)
            {
                player.DebugPing = !player.DebugPing;
                if (player.DebugPing)
                {
                    return Translation.GetLabel("LatencyInfoOn");
                }
                else
                {
                    return Translation.GetLabel("LatencyInfoOff");
                }
            }
            else
            {
                return Translation.GetLabel("LatencyInfoError");
            }
        }
    }
}