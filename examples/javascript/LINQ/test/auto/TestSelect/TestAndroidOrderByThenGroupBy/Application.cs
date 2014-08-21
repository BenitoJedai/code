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
using TestAndroidOrderByThenGroupBy;
using TestAndroidOrderByThenGroupBy.Design;
using TestAndroidOrderByThenGroupBy.HTML.Pages;

namespace TestAndroidOrderByThenGroupBy
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
            // E/Web Console( 3214): Uncaught RangeError: Invalid array length at http://10.96.4.122:16624/view-source:38225

            // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__MemoryStream.set_Capacity
            //          type$_96zhglf59TaVhJ5CtuAtgw.uRUABlf59TaVhJ5CtuAtgw = function(b)
            //{
            //              var a = [this], c, d, e;

            //              c = a[0].InternalBuffer;
            //              a[0].InternalBuffer = new Uint8ClampedArray(b);

            new IHTMLButton { "invoke" }.AttachToDocument().onclick +=
                 //async delegate
                 delegate
            {
                //var x = await this.WebMethod2();
                this.WebMethod2(
                    x =>
                {

                    new IHTMLPre { new { x } }.AttachToDocument();
                }
                    );


            };
        }

    }
}
