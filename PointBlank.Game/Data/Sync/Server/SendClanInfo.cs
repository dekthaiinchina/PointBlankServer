using PointBlank.Core.Models.Account.Clan;
using PointBlank.Core.Models.Servers;
using PointBlank.Core.Network;
using PointBlank.Core.Xml;
using PointBlank.Game.Data.Configs;
using PointBlank.Game.Data.Model;

namespace PointBlank.Game.Data.Sync.Server
{
    public class SendClanInfo
    {
        public static void Load(Account pl, Account member, int type)
        {
            if (pl == null)
            {
                return;
            }
            GameServerModel gs = GameSync.GetServer(pl._status);
            if (gs == null)
            {
                return;
            }

            using (SendGPacket pk = new SendGPacket())
            {
                pk.writeH(16);
                pk.writeQ(pl.player_id);
                pk.writeC((byte)type);
                if (type == 1) //adicionar
                {
                    pk.writeQ(member.player_id);
                    pk.writeC((byte)(member.player_name.Length + 1));
                    pk.writeS(member.player_name, member.player_name.Length + 1);
                    pk.writeB(member._status.buffer);
                    pk.writeC((byte)member._rank);
                }
                else if (type == 2) //remover
                {
                    pk.writeQ(member.player_id);
                }
                else if (type == 3) //atualizar id do clã e aux
                {
                    pk.writeD(pl.clanId);
                    pk.writeC((byte)pl.clanAccess);
                }
                GameSync.SendPacket(pk.mstream.ToArray(), gs.Connection);
            }
        }

        public static void Update(Clan clan, int type)
        {
            for (int i = 0; i < ServersXml._servers.Count; i++)
            {
                GameServerModel gs = ServersXml._servers[i];
                if (gs._id == 0 || gs._id == GameConfig.serverId)
                {
                    continue;
                }

                using (SendGPacket pk = new SendGPacket())
                {
                    pk.writeH(22);
                    pk.writeC((byte)type);
                    if (type == 0)
                    {
                        pk.writeQ(clan.owner_id);
                    }
                    else if (type == 1)
                    {
                        pk.writeC((byte)(clan._name.Length + 1));
                        pk.writeS(clan._name, clan._name.Length + 1);
                    }
                    else if (type == 2)
                    {
                        pk.writeC((byte)clan._name_color);
                    }
                    GameSync.SendPacket(pk.mstream.ToArray(), gs.Connection);
                }
            }
        }

        public static void Load(Clan clan, int type)
        {
            for (int i = 0; i < ServersXml._servers.Count; i++)
            {
                GameServerModel gs = ServersXml._servers[i];
                if (gs._id == 0 || gs._id == GameConfig.serverId)
                {
                    continue;
                }

                using (SendGPacket pk = new SendGPacket())
                {
                    pk.writeH(21);
                    pk.writeC((byte)type);
                    pk.writeD(clan._id);
                    if (type == 0)
                    {
                        pk.writeQ(clan.owner_id);
                        pk.writeD(clan.creationDate);
                        pk.writeC((byte)(clan._name.Length + 1));
                        pk.writeS(clan._name, clan._name.Length + 1);
                        pk.writeC((byte)(clan._info.Length + 1));
                        pk.writeS(clan._info, clan._info.Length + 1);
                    }
                    GameSync.SendPacket(pk.mstream.ToArray(), gs.Connection);
                }
            }
        }
    }
}