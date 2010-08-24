using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ScriptCoreLib.Extensions;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace MultitouchFingerTools.Library
{
    public static class Accelerate
    {
        public static void AccelerateAndFade(this UIElement e)
        {

            var x = Canvas.GetLeft(e);
            var y = Canvas.GetTop(e);

            var a = 0.0;
            var i = 1;

            (1000 / 15).AtInterval(
                delegate
                {
                    a += i;

                    x += a;

                    e.MoveTo(x, y);
                }
            );
        }
    }
}
