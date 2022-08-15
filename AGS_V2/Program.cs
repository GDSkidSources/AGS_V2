using System;
using System.Collections.Generic;
using System.Text;

namespace AGS_V2
{
    class Program
    {
        static void Main(string[] args)
        {
            Server.Server.Start(Globals.DefaultPort);
            Globals.StartPlayerLoop();
            Globals.Start();
        }
    }
}
