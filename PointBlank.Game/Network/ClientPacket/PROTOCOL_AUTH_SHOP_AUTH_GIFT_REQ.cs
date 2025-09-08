using PointBlank.Core;
using PointBlank.Core.Managers;
using PointBlank.Core.Models.Account;
using PointBlank.Core.Models.Shop;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Network.ServerPacket;
using System;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ : ReceivePacket
    {
        private int msgId;

        public PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ(GameClient client, byte[] data)
        {
            makeme(client, data);
        }

        public override void read()
        {
            msgId = readD();
        }

        public override void run()
        {
            try
            {
                if (_client == null)
                {
                    return;
                }
                Account p = _client._player;
                if (p == null)
                {
                    return;
                }
                if (p._inventory._items.Count >= 500)
                {
                    _client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(2147487785));
                    _client.SendPacket(new PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(0x80000000));
                }
                else
                {
                    Message msg = MessageManager.getMessage(msgId, p.player_id);
                    if (msg != null && msg.type == 2)
                    {
                        GoodItem good = ShopManager.getGood((int)msg.sender_id);
                        if (good != null)
                        {
                            _client.SendPacket(new PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(1, good._item, p));
                            MessageManager.DeleteMessage(msgId, p.player_id);
                        }
                    }
                    else
                    {
                        _client.SendPacket(new PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(0x80000000));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.info("PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ: " + ex.ToString());
            }
        }
    }
}