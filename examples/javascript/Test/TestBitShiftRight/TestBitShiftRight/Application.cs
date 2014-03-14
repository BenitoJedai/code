using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestBitShiftRight;
using TestBitShiftRight.Design;
using TestBitShiftRight.HTML.Pages;

namespace TestBitShiftRight
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // 0:32ms CreatePaddedBuffer { i = 4, offset = 60, value = 40, sizeMsg = 40 } 
            var i = 4;
            //var sizeMsg = 40;
            ulong sizeMsg = 40;
            var e = ((8 - i) * 8);


            //c = 4;
            //d = 40;
            //e = ((8 - c) * 8);
            //f = (~~(((d >> ((e & 31) >>> 0)) & 255) >>> 0));

            //var value = (byte)(sizeMsg >> ii & 0x00000000000000ff);
            var x = sizeMsg >> e;

            var value = (byte)(x & 255);


            new IHTMLPre { new { ii = e, sizeMsg, value } }.AttachToDocument();

        }

    }
}
