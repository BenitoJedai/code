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
using TestdecodeURIComponent;
using TestdecodeURIComponent.Design;
using TestdecodeURIComponent.HTML.Pages;

namespace TestdecodeURIComponent
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
            new { }.With(
                async delegate
                 {
                     // {{ Length = 7, value = 7 }}
                     var a = await GetByteArray();

                     new IHTMLPre {
                    new { a.Length,
                        value = a[0] }
                     }.AttachToDocument();

                     var m = await GetMemory();


                     // {{ Length = 0, value = 0 }}

                     new IHTMLPre {
                    new { m.Length,
                        value = m.ReadByte() }
                     }.AttachToDocument();

                 }
           );

        }

    }
}
