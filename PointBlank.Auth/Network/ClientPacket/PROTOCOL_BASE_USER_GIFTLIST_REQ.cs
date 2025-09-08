using PointBlank.Core;
using PointBlank.Core.Managers;
using PointBlank.Core.Models.Account;
using PointBlank.Auth.Data.Model;
using PointBlank.Auth.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace PointBlank.Auth.Network.ClientPacket
{
    public class PROTOCOL_BASE_USER_GIFTLIST_REQ : ReceivePacket
    {
        public PROTOCOL_BASE_USER_GIFTLIST_REQ(AuthClient lc, byte[] buff)
        {
            makeme(lc, buff);
        }

        public override void read()
        {

        }

        public override void run()
        {
            try
            {
                Account player = _client._player;
                if (player == null || !AuthManager.Config.GiftSystem)
                {
                    return;
                }
                List<Message> gifts = MessageManager.getGifts(player.player_id);
                if (gifts.Count > 0)
                {
                    MessageManager.RecicleMessages(player.player_id, gifts);
                    if (gifts.Count > 0)
                    {
                        _client.SendPacket(new PROTOCOL_BASE_USER_GIFTLIST_ACK(0, gifts));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.warning(ex.ToString());
            }
        }
    }
}