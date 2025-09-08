using PointBlank.Core;
using PointBlank.Core.Managers;
using PointBlank.Core.Models.Account;
using PointBlank.Core.Models.Account.Clan;
using PointBlank.Game.Data.Managers;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace PointBlank.Game.Network.ClientPacket
{
    public class PROTOCOL_CS_NOTE_REQ : ReceivePacket
    {
        private int type;
        private string message;

        public PROTOCOL_CS_NOTE_REQ(GameClient client, byte[] data)
        {
            makeme(client, data);
        }

        public override void read()
        {
            type = readC();
            message = readUnicode(readC() * 2);
        }

        public override void run()
        {
            try
            {
                Account player = _client._player;
                if (message.Length > 120 || player == null)
                {
                    return;
                }
                Clan clan = ClanManager.getClan(player.clanId);
                int playersLoaded = 0;
                if (clan._id > 0 && clan.owner_id == _client.player_id)
                {
                    List<Account> players = ClanManager.getClanPlayers(clan._id, _client.player_id, true);
                    for (int i = 0; i < players.Count; i++)
                    {
                        Account member = players[i];
                        if ((type == 0 || member.clanAccess == 2 && type == 1 || member.clanAccess == 3 && type == 2) && MessageManager.getMsgsCount(member.player_id) < 100)
                        {
                            ++playersLoaded;
                            Message msg = CreateMessage(clan, member.player_id, _client.player_id);
                            if (msg != null && member._isOnline)
                            {
                                member.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(msg), false);
                            }
                        }
                    }
                }
                _client.SendPacket(new PROTOCOL_CS_NOTE_ACK(playersLoaded));
                if (playersLoaded > 0)
                {
                    _client.SendPacket(new PROTOCOL_MESSENGER_NOTE_SEND_ACK(0));
                }
            }
            catch (Exception ex)
            {
                Logger.info("PROTOCOL_CS_NOTE_REQ: " + ex.ToString());
            }
        }

        private Message CreateMessage(Clan clan, long owner, long senderId)
        {
            Message msg = new Message(15)
            {
                sender_name = clan._name,
                sender_id = senderId,
                clanId = clan._id,
                type = 4,
                text = message,
                state = 1
            };
            return MessageManager.CreateMessage(owner, msg) ? msg : null;
        }
    }
}