using System;

namespace PointBlank.Core.Models.Enums
{
    [Flags]
    public enum ResultIcon
    {
        None = 0,
        Pc = 1,
        PcPlus = 2,
        Item = 4,
        Event = 8
    }
}