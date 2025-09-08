using PointBlank.Core.Models.Enums;

namespace PointBlank.Game.Data.Model
{
    public class SlotMatch
    {
        public SlotMatchState state;
        public long _playerId, _id;

        public SlotMatch(int slot)
        {
            _id = slot;
        }
    }
}