using PointBlank.Core;
using PointBlank.Core.Managers;
using PointBlank.Core.Models.Account.Players;
using PointBlank.Core.Network;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Data.Sync.Server;
using System;
using System.Collections.Generic;

namespace PointBlank.Game.Network.ServerPacket
{
    public class PROTOCOL_AUTH_SHOP_CAPSULE_ACK : SendPacket
    {
        private List<ItemsModel> Rewards;
        private int CouponId, Index;

        public PROTOCOL_AUTH_SHOP_CAPSULE_ACK(List<ItemsModel> Rewards, int CouponId, int Index)
        {
            this.CouponId = CouponId;
            this.Index = Index;
            this.Rewards = Rewards;
        }

        public override void write()
        {
            writeH(1064);
            writeH(0);
            writeC((byte)Rewards.Count);
            for (int i = 0; i < Rewards.Count; i++)
            {
                ItemsModel Item = Rewards[i];
                writeD(Item._id);
                writeD((int)Item._count);
            }
            writeC((byte)Index);
            writeD(CouponId);
        }
    }
}