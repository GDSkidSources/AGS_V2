using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGS_V2.Utils;
namespace AGS_V2.Server
{
    class Functions
    {
        public static async Task SwitchLevel(Player TargetPlayer, String Map)
        {
            var Writer = new BitWriter();
            Writer.Write("SwitchLevel");
            Writer.Write(Map);
            await TargetPlayer.SendAsync(Writer.Dump());
        }

        public static async Task InitPlayer(Player TargetPlayer)
        {
            var Writer = new BitWriter();
            Writer.Write("InitPlayer");
            Writer.Write(TargetPlayer.PlayerName);
            await TargetPlayer.SendAsync(Writer.Dump());
        }

        public static async Task PlayerTick(Player TargetPlayer)
        {
            var Writer = new BitWriter();

            Writer.Write("Tick");
            Writer.Write(TargetPlayer.PlayerName);
            //Location (FVector)
            Writer.Write(TargetPlayer.Location.X);
            Writer.Write(TargetPlayer.Location.Y);
            Writer.Write(TargetPlayer.Location.Z);
            //Rotation (FRotator)
            Writer.Write(TargetPlayer.Rotation.Yaw);
            Writer.Write(TargetPlayer.Rotation.Pitch);
            Writer.Write(TargetPlayer.Rotation.Roll);
            await TargetPlayer.SendAsync(Writer.Dump());
        }

        public static async Task Jump(Player TargetPlayer)
        {
            var Writer = new BitWriter();
            Writer.Write("Jump");

            await TargetPlayer.SendAsync(Writer.Dump());
        }

        public static async Task Crouch(Player TargetPlayer)
        {
            var Writer = new BitWriter();
            Writer.Write("Crouch");
            Writer.Write(TargetPlayer.PlayerName);
            if(TargetPlayer.IsCrouched == true)
            {
                Writer.Write("Crouch");
            }
            else
            {
                Writer.Write("UnCrouch");
            }
            await TargetPlayer.SendAsync(Writer.Dump());
        }

        public static async Task StartEvent(Player TargetPlayer)
        {
            var Writer = new BitWriter();

            Writer.Write("StartEvent");
            await TargetPlayer.SendAsync(Writer.Dump());
        }
    }
}
