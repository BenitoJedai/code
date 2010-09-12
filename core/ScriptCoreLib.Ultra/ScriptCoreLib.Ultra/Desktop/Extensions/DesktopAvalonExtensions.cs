using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using System.Threading;

namespace ScriptCoreLib.Desktop.Extensions
{
    public static class DesktopAvalonExtensions
    {
        public static void Launch<T>(Func<T> Create) where T : Canvas
        {
            var t = new Thread(
                delegate()
                {
                    var w = Create().ToWindow();

                    w.ShowDialog();
                }
            )
            {
                ApartmentState = ApartmentState.STA
            };

            t.Start();
            t.Join();

        }
    }
}
