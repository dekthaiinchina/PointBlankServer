using System;

namespace PointBlank.Core.Models.Enums
{
    [Flags]
    public enum InventoryFlag
    {
        Character = 1,
        Weapon = 2,
        Item = 4
    }
}