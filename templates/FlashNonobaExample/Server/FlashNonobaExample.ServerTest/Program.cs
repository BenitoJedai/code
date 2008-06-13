using System;
using System.Collections.Generic;
using System.Text;

namespace FlashNonobaExample.ServerTest
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Start the server and wait for incomming connection
            Nonoba.DevelopmentServer.Server.StartWithDebugging();

            // Start the server, and make it simulate one user connecting for 15seconds
            // (this is an easy way to debug serverside only code that runs every X timeslot...)
            //
            // Nonoba.DevelopmentServer.Server.StartWithDebugging(15000);
        }
    }
}
