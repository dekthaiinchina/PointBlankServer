using PointBlank.Core;
using PointBlank.Core.Managers.Server;
using PointBlank.Game.Data.Model;

namespace PointBlank.Game.Data.Chat
{
    public static class EnableMissions
    {
        public static string genCode1(string str, Account player)
        {
            bool activate = bool.Parse(str.Substring(8));
            bool result = ServerConfigSyncer.updateMission(GameManager.Config, activate);
            if (result)
            {
                Logger.warning(Translation.GetLabel("ActivateMissionsWarn", activate, player.player_name));
                return Translation.GetLabel("ActivateMissionsMsg1");
            }
            else
            {
                return Translation.GetLabel("ActivateMissionsMsg2");
            }
        }
    }
}