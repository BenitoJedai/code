using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace AndroidSpyDroidActivity
{
    class Program
    {
        [STAThread]
        public static void Main(string[] e)
        {
            global::jsc.AndroidLauncher.Launch(
                 //typeof(net.majorkernelpanic.spydroid.ApplicationActivity)
                 typeof(AndroidSpyDroidActivity.Activities.ApplicationActivity)
            );
        }
    }
}
