using Microsoft.VisualBasic.Devices;
using PointBlank.Auth.Data.Managers;
using PointBlank.Core.Network;
using PointBlank.Core.Xml;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace PointBlank.Auth
{
    public class Auth
    {
        public static async void Update()
        {
            while (true)
            {
                Console.Title = "Point Blank - Auth [Users: " + AuthManager._socketList.Count + " Online: " + ServersXml.getServer(0)._LastCount + " Used RAM: " + (GC.GetTotalMemory(true) / 1024) + " KB]";
                await Task.Delay(1000);
            }
        }
    }
}