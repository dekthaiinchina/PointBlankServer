using PointBlank.Game.Data.Model;

namespace PointBlank.Game.Data.Command
{
    public interface ICommand
    {
        public string Execute(string[] Params, GameClient Client, Account Account);
    }
}
