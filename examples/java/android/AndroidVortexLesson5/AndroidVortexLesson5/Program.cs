using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace AndroidVortexLesson5
{
    class Program
    {
        [STAThread]
        public static void Main(string[] e)
        {
            var w = new Window { Title = "JSC - Android Project" }.SizeTo(400 + 48, 300);

            var c = new Canvas();

            c.AttachTo(w);


            new global::AndroidVortexLesson5.Avalon.Images.jsc().AttachTo(c).MoveTo(300, 16);

            var button1 = new Button { Content = "Debug in Android Emulator" };

            button1.MoveTo(16, 128).SizeTo(400, 32);
            button1.AttachTo(c);

            var button2 = new Button { Content = "Debug on Device" };

            button2.MoveTo(16, 128 + 48).SizeTo(400, 32);
            button2.AttachTo(c);

            w.ShowDialog();
        }
    }
}
