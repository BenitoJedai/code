using System;
using System.Collections.Generic;
using System.Text;

namespace LightsOut.ServertTest
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Nonoba.DevelopmentServer.Server.StartWithDebugging();
        }
    }
}
