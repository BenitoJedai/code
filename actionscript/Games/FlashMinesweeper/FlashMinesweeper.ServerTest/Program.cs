using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashMinesweeper.ServerTest
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
