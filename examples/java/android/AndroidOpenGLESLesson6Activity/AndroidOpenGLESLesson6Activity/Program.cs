using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace AndroidOpenGLESLesson6Activity
{
    class Program
    {
        [STAThread]
        public static void Main(string[] e)
        {
            global::jsc.AndroidLauncher.Launch(
                  typeof(AndroidOpenGLESLesson6Activity.Activities.AndroidOpenGLESLesson6Activity)
             );
        }
    }
}
