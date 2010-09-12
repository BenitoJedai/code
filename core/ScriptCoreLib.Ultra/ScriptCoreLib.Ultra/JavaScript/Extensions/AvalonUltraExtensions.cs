using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Controls;

namespace ScriptCoreLib.JavaScript.Extensions
{
    public static class AvalonUltraExtensions
    {
        public static T AutoSizeTo<T>(this T e, IHTMLElement shadow) where T : Panel
        {
            Action Update =
                delegate
                {
                    var w = shadow.scrollWidth;
                    var h = shadow.scrollHeight;

                    e.SizeTo(w, h);
                };


            Native.Window.onresize +=
                delegate
                {
                    Update();
                };

            Update();

            return e;
        }
    }
}
