using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace AndroidFormsActivity
{
    class Program
    {
        [STAThread]
        public static void Main(string[] e)
        {
            //An unhandled exception of type 'System.BadImageFormatException' occurred in Microsoft.VisualStudio.HostingProcess.Utilities.dll

            //Additional information: Could not load file or assembly 'ScriptCoreLibAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. An attempt was made to load a program with an incorrect format.

            global::jsc.AndroidLauncher.Launch(
                 typeof(AndroidFormsActivity.Activities.ApplicationActivity)
            );
        }
    }
}
