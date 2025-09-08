using PointBlank.Battle.Data.Models.SubHead;
using System;

namespace PointBlank.Battle.Network.Actions.SubHead
{
    public class ControledObj
    {
        public static ControledInfo ReadSyncInfo(ReceivePacket p, bool genLog)
        {
            ControledInfo info = new ControledInfo
            {
                _unk = p.readB(9)
            }; //160 existe no mapa OUTPOST
            if (genLog)
            {
                Logger.warning("[ControledObj] " + BitConverter.ToString(info._unk));
            }
            return info;
        }

        public static void WriteInfo(SendPacket s, ReceivePacket p, bool genLog)
        {
            ControledInfo info = ReadSyncInfo(p, genLog);
            s.writeB(info._unk);
            info = null;
        }
    }
}