using PointBlank.Battle.Data.Configs;
using PointBlank.Battle.Data.Items;
using PointBlank.Battle.Data.Sync;
using PointBlank.Battle.Data.Xml;
using PointBlank.Battle.Network;
using PointBlank.Battle;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace PointBlank.Battle
{
    class Program
    {
        public static void Main(string[] args)
        {
            //string Date = ComDiv.GetLinkerTime(Assembly.GetExecutingAssembly(), null).ToString("dd/MM/yyyy HH:mm");
            BattleConfig.Load();
            Logger.checkDirectory();
            Console.Clear();
            Logger.Cyan(@"________________________________________________________________________________");
            Logger.Cyan(@"          ____                       _       _____  __     _______ _   _");
            Logger.Cyan(@"      ___|  _ \ _   _ _______  _ __ (_)_  __|__  /  \ \   / /___  / | / |");
            Logger.Cyan(@"     |_  / |_) | | | |_  / _ \| '_ \| \ \/ /  / /____\ \ / /   / /| | | |");
            Logger.Cyan(@"      / /|  _ <| |_| |/ / (_) | | | | |>  <  / /|_____\ V /   / /_| |_| |");
            Logger.Cyan(@"     /___|_| \_\\__, /___\___/|_| |_|_/_/\_\/____|     \_/   /_/(_)_(_)_|");
            Logger.Cyan(@"                |___/");
            Logger.Cyan(@"________________________________________________________________________________");
            Logger.Cyan("[Release Server: version 3.68]");
            MapXml.Load();
            CharaXml.Load();
            MeleeExceptionsXml.Load();
            ServersXml.Load();
            ItemManager.Load();
            BattleSync.Start();
            BattleManager.Connect();

            Update();

            Process.GetCurrentProcess().WaitForExit();
        }

        protected static async void Update()
        {
            while (true)
            {
                Console.Title = "Point Blank - Battle [Used RAM: " + (GC.GetTotalMemory(true) / 1024) + " KB]";

                await Task.Delay(5000);
            }
        }
    }
}