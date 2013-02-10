using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace PopupWebView
{
    class Program
    {
        [STAThread]
        public static void Main(string[] e)
        {
            global::jsc.AndroidLauncher.Launch(
                 typeof(PopupWebView.Activities.ApplicationActivity)
            );
        }
    }
}
