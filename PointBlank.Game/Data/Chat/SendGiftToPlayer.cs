using PointBlank.Core;
using PointBlank.Core.Managers;
using PointBlank.Core.Models.Account;
using PointBlank.Game.Network.ServerPacket;
using PointBlank.Game.Data.Managers;
using PointBlank.Game.Data.Model;
using System;
using PointBlank.Core.Models.Shop;

namespace PointBlank.Game.Data.Chat
{
    public static class SendGiftToPlayer
    {
        public static string SendGiftById(string str)
        {
            if (!GameManager.Config.GiftSystem)
            {
                return Translation.GetLabel("SendGift_SystemOffline");
            }
            string txt = str.Substring(str.IndexOf(" ") + 1);
            string[] split = txt.Split(' ');
            long player_id = Convert.ToInt64(split[0]);
            int shopGiftId = Convert.ToInt32(split[1]);

            Account pR = AccountManager.getAccount(player_id, 0);
            if (pR == null)
            {
                return Translation.GetLabel("SendGift_Fail4");
            }
            GoodItem good = ShopManager.getGood(shopGiftId);
            if (good != null && (good.visibility == 0 || good.visibility == 4))
            {
                Message gift = new Message(30) { sender_name = "GM", sender_id = shopGiftId, state = 0, type = 2 };
                if (MessageManager.CreateMessage(player_id, gift))
                {
                    pR.SendPacket(new PROTOCOL_AUTH_SHOP_RECV_GIFT_ACK(gift), false);
                    return Translation.GetLabel("SendGift_Success", good._item._name, pR.player_name);
                }
                else
                {
                    return Translation.GetLabel("SendGift_Fail1");
                }
            }
            else
            {
                return good == null ? Translation.GetLabel("SendGift_Fail2") : Translation.GetLabel("SendGift_Fail3", good._item._name);
            }
        }
    }
}