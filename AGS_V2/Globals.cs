using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using AGS_V2.Server;
using AGS_V2.Utils;

namespace AGS_V2
{
    public struct FVector
    {
        public float X;
        public float Y;
        public float Z;
    }

    public struct FRotator
    {
        public float Yaw;
        public float Pitch;
        public float Roll;
    }

    enum E_Direction : int
    {
        Forward = 0,
        Backwards = 1,
        Left = 2,
        Right = 3,
        NotMoving = 4
    }
    class Globals
    {
        public static int DefaultPort = 7777;
        public static string DefaultMap = "Athena_Terrain";

        public static UdpClient Udp;
        public static List<Player> Players = new List<Player>();
        public static int TickSpeed = 120;  // Milliseconds
        public static string GPlayerName = "";
        static long lastTime = 0;
        public static double GetDeltaTime()
        {
            long now = DateTime.Now.Millisecond;
            double dT = (now - lastTime);
            lastTime = now;
            return dT;
        }



        public static async void HandleCommand(String Command)
        {
            if(Command == "StartEvent")
            {
                foreach (var _P in Globals.Players)
                {
                    if (_P != null)
                    {
                        await Functions.StartEvent(_P);
                        Console.WriteLine("\n");
                    }
                }
            }
        }

        public static void Start()
        {
            new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    Console.WriteLine("Enter a Command: \n");
                    String Command = Console.ReadLine();
                    HandleCommand(Command);
                }
            })).Start();
        }

        public static void StartPlayerLoop()
        {
            new Thread(new ThreadStart(async () =>
            {
                while (true)
                {
                    foreach(var _Player in Globals.Players)
                    {
                        var Writer = new BitWriter();

                        Writer.Write("Tick");
                        Writer.Write(_Player.PlayerName);
                        Writer.Write(_Player.Location.X);
                        Writer.Write(_Player.Location.Y);
                        Writer.Write(_Player.Location.Z);
                        Writer.Write(_Player.Rotation.Pitch);
                        Writer.Write(_Player.Rotation.Yaw);
                        Writer.Write(_Player.Rotation.Roll);
                        foreach(var __Player in Globals.Players)
                        {
                            if( __Player.PlayerName != _Player.PlayerName && __Player.IsLoaded == true)
                            {
                                await __Player.SendAsync(Writer.Dump());
                            }
                        }
                        Thread.Sleep(500);
                    }
                    Thread.Sleep(1000);
                }
            })).Start();
        }
    }
}
