using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using System.Text;
using System;
using System.Linq;
using System.Collections.Specialized;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ConvertASToCS.js.Any;


namespace ConvertASToCS.js
{
    partial class ConvertASToCS
    {

        public static void RenderProxyTo(ProxyProvider r, IHTMLElement Target)
        {
            Func<Color, Action<string>> ToColorWrite =
                color =>
                    text =>
                    {
                        var s = new IHTMLSpan { innerText = text };

                        s.style.color = color;
                        s.AttachTo(Target);
                    };

            Action<string> Write = text => new IHTMLSpan(text).AttachTo(Target);

            ProxyConverter.RenderProxyTo(r, ToColorWrite, Write, null);
        }


    }

}
