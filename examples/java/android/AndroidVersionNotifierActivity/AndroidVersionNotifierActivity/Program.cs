using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using AndroidVersionNotifierActivity.Activities;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace AndroidVersionNotifierActivity
{
    class Program
    {
        [STAThread]
        public static void Main(string[] e)
        {
            // Mixed mode assembly is built against version 'v2.0.50727' of the runtime and cannot be loaded in the 4.0 runtime without additional configuration information.

            //---------------------------
            //Microsoft Visual Studio Express 2012 RC for Web
            //---------------------------
            //NuGet Package Manager

            //Do you want to configure this solution to download and restore missing NuGet packages during build? A .nuget folder will be added to the root of the solution that contains files that enable package restore.

            //Packages installed into Website projects will not be restored during build. Consider converting those into Web application projects if necessary.
            //---------------------------
            //Yes   No   
            //---------------------------


            global::jsc.AndroidLauncher.Launch(
                 typeof(AndroidVersionNotifierActivity.Activities.AndroidVersionNotifierActivity)
            );
        }
    }
}
