using PointBlank.Core;
using PointBlank.Core.Models.Servers;
using PointBlank.Core.Xml;
using PointBlank.Game;
using PointBlank.Game.Data.Configs;

namespace PointBlank.Game.Data.Chat
{
    public static class PlayersCountInServer
    {
        public static string GetMyServerPlayersCount()
        {
            return Translation.GetLabel("UsersCount", GameManager._socketList.Count, GameConfig.serverId);
        }

        public static string GetServerPlayersCount(string str)
        {
            int serverId = int.Parse(str.Substring(9));
            GameServerModel server = ServersXml.getServer(serverId);
            if (server != null)
            {
                return Translation.GetLabel("UsersCount2", server._LastCount, server._maxPlayers, serverId);
            }
            else
            {
                return Translation.GetLabel("UsersInvalid");
            }
        }
    }
}