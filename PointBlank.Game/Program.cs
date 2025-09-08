using PointBlank.Core;
using PointBlank.Core.Managers;
using PointBlank.Core.Managers.Events;
using PointBlank.Core.Managers.Server;
using PointBlank.Core.Network;
using PointBlank.Core.Xml;
using PointBlank.Game.Data.Xml;
using PointBlank.Game.Data.Managers;
using PointBlank.Game.Data.Sync;
using System;
using System.Diagnostics;
using System.Reflection;
using PointBlank.Core.Filters;
using PointBlank.Game.Data.Configs;
using System.Text;
using PointBlank.Game.Network.ServerPacket;
using PointBlank.Game.Data.Chat;
using PointBlank.Game.Data.Model;
using PointBlank.Core.Models.Enums;
using PointBlank.Core.Models.Room;
using PointBlank.Core.Models.Account.Players;
using PointBlank.Game.Rcon;
using System.Threading;
using PointBlank.Game.Data.Command;

namespace PointBlank.Game
{
    public class Programm
    {
        public static void Main(string[] args)
        {
            string Date = ComDiv.GetLinkerTime(Assembly.GetExecutingAssembly(), null).ToString("dd/MM/yyyy HH:mm");
            Logger.StartedFor = "Game";
            Logger.checkDirectorys();
            Console.Clear();
            Logger.Cyan(@"________________________________________________________________________________");
            Logger.Cyan(@"          ____                       _       _____  __     _______ _   _");
            Logger.Cyan(@"      ___|  _ \ _   _ _______  _ __ (_)_  __|__  /  \ \   / /___  / | / |");
            Logger.Cyan(@"     |_  / |_) | | | |_  / _ \| '_ \| \ \/ /  / /____\ \ / /   / /| | | |");
            Logger.Cyan(@"      / /|  _ <| |_| |/ / (_) | | | | |>  <  / /|_____\ V /   / /_| |_| |");
            Logger.Cyan(@"     /___|_| \_\\__, /___\___/|_| |_|_/_/\_\/____|     \_/   /_/(_)_(_)_|");
            Logger.Cyan(@"                |___/");
            Logger.Cyan(@"_______________________________ " + Date + " _______________________________");
            Logger.Cyan("[Release Server: version 3.68]");
            GameConfig.Load();
            BasicInventoryXml.Load();
            ServerConfigSyncer.GenerateConfig(GameConfig.configId);
            ServersXml.Load();
            ChannelsXml.Load(GameConfig.serverId);
            EventLoader.LoadAll();
            TitlesXml.Load();
            TitleAwardsXml.Load();
            ClanManager.Load();
            NickFilter.Load();
            MissionCardXml.LoadBasicCards(1);
            RankXml.Load();
            BattleServerXml.Load();
            RankXml.LoadAwards();
            ClanRankXml.Load();
            MissionAwardsXml.Load();
            MissionsXml.Load();
            Translation.Load();
            ShopManager.Load(1);
            MapsXml.Load();
            RandomBoxXml.LoadBoxes();
            CouponEffectManager.LoadCouponFlags();
            GameSync.Start();
            PermissionManager.Load();
            CommandManager.Load();

            if(Logger.erro)
            {
                Logger.error("Check your configuration.");
                Thread.Sleep(5000);
                Environment.Exit(0);
            }

            if (GameManager.Start())
                Game.Update();

            if (GameConfig.RconEnable)
                RconManager.Instance();

            Process.GetCurrentProcess().WaitForExit();
        }
    }
}