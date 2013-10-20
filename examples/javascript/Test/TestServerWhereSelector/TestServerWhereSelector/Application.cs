using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestServerWhereSelector;
using TestServerWhereSelector.Design;
using TestServerWhereSelector.HTML.Pages;

namespace TestServerWhereSelector
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


            new __closure { __i = 3 }.With(
                async c =>
                {

                    var data = await this.GetData(c);

                    foreach (var item in data)
                    {
                        new IHTMLPre { innerText = new { item }.ToString() }.AttachToDocument();

                    }
                }
            );


            //new __closure { __i = 3 }.With(
            //     async c =>
            {
                //                <.ctor>b__4(int) : bool
                //Analysis
                //Attributes
                //Signature Types
                //Declaring Module
                //Declaring Type
                //arg.0 x : int
                //loc.0 <- 0x0002 cgt 
                //maxstack 2
                //IL Code (7)
                //0x0000 . ldarg.0        arg.0 x : int
                //0x0001 . . ldc.i4.3     3 (0x00000003)
                //0x0002 . cgt 
                //0x0004 stloc.0          loc.0 : bool
                //0x0005 br.s 
                //0x0007 . ldloc.0        loc.0 : bool
                //0x0008 ret 


                var data = new Mashup().GetData(x => x > 3);

                foreach (var item in data)
                {
                    new IHTMLPre { innerText = new { item }.ToString() }.AttachToDocument().style.color = "red";

                }
            }

            {
                var xx = 3;
                var data = new Mashup().GetData(x => x > xx);

                foreach (var item in data)
                {
                    new IHTMLPre { innerText = new { item }.ToString() }.AttachToDocument().style.color = "cyan";

                }
            }
            //);

        }

    }
}
