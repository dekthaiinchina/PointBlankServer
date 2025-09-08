using PointBlank.Battle.Data.Enums;
using PointBlank.Battle.Data.Models;
using PointBlank.Battle.Data.Models.Event;
using System;

namespace PointBlank.Battle.Network.Actions.Event
{
    public class ActionState
    {
        public static ActionStateInfo ReadInfo(ReceivePacket p, ActionModel ac, bool genLog)
        {
            ActionStateInfo info = new ActionStateInfo
            {
                Action = (ACTION_STATE) p.readUH(),
                Value = p.readC(),
                Flag = (WEAPON_SYNC_TYPE)p.readC()
            };
            if (genLog)
            {

            }
            return info;
        }

        public static void WriteInfo(SendPacket s, ActionModel ac, ReceivePacket p, bool genLog)
        {
            ActionStateInfo info = ReadInfo(p, ac, genLog);
            WriteInfo(s, info);
            info = null;
        }

        public static void WriteInfo(SendPacket s, ActionStateInfo info)
        {
            s.writeH((ushort)info.Action);
            s.writeC(info.Value);
            s.writeC((byte)info.Flag);
        }
    }
}