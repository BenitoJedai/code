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
using RoslynPrimaryConstructorService;
using RoslynPrimaryConstructorService.Design;
using RoslynPrimaryConstructorService.HTML.Pages;

namespace RoslynPrimaryConstructorService
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application() : ApplicationWebService(
        ClientTag: "goo",
        yield:
            delegate
    {
        // how would this be useful if jsc supported this?
    }
        )
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page) : this()
        // this wont work yet
        //: base("goo")
        {
            //this.Invoke("base");




            // jsc wont like this yet.
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140512
            // this wont work yet
            //new ApplicationWebService(ClientTag: "CCC").Invoke(InvokeTag: "A");

            // http://programmers.stackexchange.com/questions/177528/does-async-await-makes-simple-thing-unnecessary-complicated

            new ApplicationWebService
            {
                ClientTag = "CCC",

                yield = x => new IHTMLPre { new { x } }.AttachToDocument()
            }.With(
                async service =>
                {
                    var service2 = await service.Invoke(InvokeTag: "A");

                    // reuse
                    service2.yield = service.yield;

                    service2.Invoke("B");
                }
             );

            //.Invoke(InvokeTag: "B");


        }

    }
}
