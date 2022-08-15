using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using AGS_V2.Utils;
namespace AGS_V2.Server
{
    class Player : Client
    {
        public Player(IPEndPoint Endpoint) : base(Endpoint) => Globals.Players.Add(this);

        public bool IsLoaded = false;

        public String PlayerName = "";

        public bool IsCrouched = false;

        public FVector Location = new FVector();
        public FRotator Rotation = new FRotator();

        public async Task Process(byte[] Data)
        {
            if (Data != null)
            {
                var Reader = new BitReader(Data);
                String Function = "";
                Function = Reader.ReadString();

                if (Function == "Join")
                {
                    PlayerName = Reader.ReadString();

                    Console.WriteLine(PlayerName + " Joined!");

                    await Functions.SwitchLevel(this, Globals.DefaultMap);
                }

                if (Function == "ReadyToStartMatch")
                {
                    foreach (var _Player in Globals.Players)
                    {
                        if (_Player != this)
                        {
                            await Functions.InitPlayer(this);
                            await Functions.InitPlayer(_Player);
                            IsLoaded = true;
                        }
                    }
                }

                if (Function == "PlayerTick")
                {
                    var PlayerName = Reader.ReadString();
                    var X = Reader.ReadFloat();
                    var Y = Reader.ReadFloat();
                    var Z = Reader.ReadFloat();
                    var Pitch = Reader.ReadFloat();
                    var Yaw = Reader.ReadFloat();
                    var Roll = Reader.ReadFloat();

                    FVector N_Loc = new FVector();
                    N_Loc.X = X;
                    N_Loc.Y = Y;
                    N_Loc.Z = Z;

                    FRotator N_Rot;
                    N_Rot.Pitch = Pitch;
                    N_Rot.Yaw = Yaw;
                    N_Rot.Roll = Roll;

                    foreach(var _Player in Globals.Players)
                    {
                        if (_Player.PlayerName == PlayerName) {
                            _Player.Location = N_Loc;
                            _Player.Rotation = N_Rot;
                            break;
                        }
                    }
                }
            }
        }
    }
}
