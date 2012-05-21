using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using AndroidNuGetSQLiteActivity.Activities;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace AndroidNuGetSQLiteActivity
{
    class Program
    {
        [STAThread]
        public static void Main(string[] e)
        {
            MyDatabase.Write();

            var c = MyDatabase.Read("-");

            Console.WriteLine(c);

            // 
            global::jsc.AndroidLauncher.Launch(
                 typeof(AndroidNuGetSQLiteActivity.Activities.AndroidNuGetSQLiteActivity)
            );
        }
    }
}
