using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace AndroidWebViewActivity
{
    class Program
    {
        [STAThread]
        public static void Main(string[] e)
        {
            global::jsc.AndroidLauncher.Launch(
                 typeof(AndroidWebViewActivity.Activities.AndroidWebViewActivity)
            );


        }
    }
}
