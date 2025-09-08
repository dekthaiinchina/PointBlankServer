using PointBlank.Core.Models.Enums;
using PointBlank.Core.Models.Room;
using PointBlank.Core.Network;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Data.Utils;
using System.Collections.Generic;

namespace PointBlank.Game.Network.ServerPacket
{
    public class PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK : SendPacket
    {
        private Room _r;
        private List<int> _dinos;
        private bool isBotMode;

        public PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(Room r, List<int> dinos, bool isBotMode)
        {
            _r = r;
            _dinos = dinos;
            this.isBotMode = isBotMode;
        }

        public PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(Room r)
        {
            _r = r;
            _dinos = AllUtils.getDinossaurs(r, false, -1);
            isBotMode = _r.isBotMode();
        }

        public override void write()
        {
            writeH(4127);
            writeH(AllUtils.getSlotsFlag(_r, false, false));
            if (isBotMode)
            {
                writeB(new byte[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255 });
            }
            else if (_r.RoomType == RoomType.Boss || _r.RoomType == RoomType.CrossCounter)
            {
                int TRex = _dinos.Count == 1 || _r.RoomType == RoomType.CrossCounter ? 255 : _r.TRex;
                writeC((byte)TRex);
                writeC(10);
                for (int i = 0; i < _dinos.Count; i++)
                {
                    int slotId = _dinos[i];
                    if (slotId != _r.TRex && _r.RoomType == RoomType.Boss || _r.RoomType == RoomType.CrossCounter)
                    {
                        writeC((byte)slotId);
                    }
                }
                int Fault = 8 - _dinos.Count - (TRex == 255 ? 1 : 0);
                for (int i = 0; i < Fault; i++)
                {
                    writeC(255);
                }
                writeC(255);
            }
            else
            {
                writeB(new byte[10]);
            }
            writeC(0);
        }
    }
}