using PointBlank.Core;
using PointBlank.Core.Managers;
using PointBlank.Game.Data.Managers;
using PointBlank.Game.Data.Model;
using PointBlank.Game.Data.Sync.Server;
using PointBlank.Game.Network.ServerPacket;

namespace PointBlank.Game.Rcon.Packets
{
    public class RconSendMoney : RconReceive
    {      
        public override void Run()
        {
            // Get infos
            long Id = PopLong();
            int Money = PopInt();

            // Execute the command
            Account Player = AccountManager.getAccount(Id, 0);
            if (Player != null)
            {
                if (PlayerManager.updateAccountCash(Player.player_id, Player._money + Money))
                {
                    Player._money += Money;
                    Player.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0, Player._gp, Player._money), false);
                    SendItemInfo.LoadGoldCash(Player);
                    Logger.debug("Player " + Id + " received " + Money + " money");
                }
            }
        }
    }
}
