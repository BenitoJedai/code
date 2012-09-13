using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TestPHPMemoryStream.Design;
using TestPHPMemoryStream.HTML.Pages;

namespace TestPHPMemoryStream
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            var m = new MemoryStream();

            m.WriteByte((byte)'H');
            m.WriteByte((byte)'E');
            m.WriteByte((byte)'L');
            m.WriteByte((byte)'L');
            m.WriteByte((byte)'O');

            var a = m.ToArray();

            //Native.API.var_dump(a);

            var w = new StringBuilder();

            foreach (var item in a)
            {
                w.Append(item.ToString("x2"));
            }

            w.Append(", " + Convert.ToBase64String(a));

            // {48454c4c4f, SEVMTE8=}
            var e = w.ToString();

            new IHTMLPre { innerText = e }.AttachToDocument();

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
