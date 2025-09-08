using PointBlank.Core.Models.Enums;
using PointBlank.Core.Network;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Data.Utils;
using PointBlank.Game.Network.ServerPacket;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_REQ : ReceivePacket
    {
        public PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_REQ(GameClient client, byte[] data)
        {
            makeme(client, data);
        }

        public override void read()
        {

        }

        public override void run()
        {
            if (_client == null || _client._player == null)
            {
                return;
            }
            Room Room = _client._player._room;
            Room.RoomState = RoomState.BattleEnd;
            _client.SendPacket(new PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_ACK(Room));
            _client.SendPacket(new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(_client._player._room, 0, RoundEndType.Tutorial));
            _client.SendPacket(new PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(Room));
            if (Room.RoomState == RoomState.BattleEnd)
            {
                Room.RoomState = RoomState.Ready;
                _client.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(_client._player));
                _client.SendPacket(new PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(Room));
            }
            AllUtils.resetBattleInfo(Room);
            _client.SendPacket(new PROTOCOL_ROOM_GET_SLOTINFO_ACK(Room));
        }
    }
}