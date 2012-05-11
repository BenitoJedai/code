using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace TestSQLiteFromNuGet
{
    class Program
    {
        // later we need to make it running in java and android and AIR

        [STAThread]
        public static void Main(string[] e)
        {
            SQLiteConnection.CreateFile("MyDatabase.sqlite");

        }
    }
}
