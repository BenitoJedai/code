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
using SignedWebApplicationCommunicationTest;
using SignedWebApplicationCommunicationTest.Design;
using SignedWebApplicationCommunicationTest.HTML.Pages;

namespace SignedWebApplicationCommunicationTest
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
            var temp = new signedResponse();
            temp.resp = "hello";


            Action test = async delegate {
                var i = await this.getResp(temp);
                Console.WriteLine(new {i.resp, i.signed });
                temp = i;
                var i2 = await this.getResp(temp);
                Console.WriteLine(new { i2.resp, i2.signed });
                temp = i2;
                temp.resp = "newIn";
                var i3 = await this.getResp(temp);
                Console.WriteLine(new { i3.resp, i3.signed });
            };

            test();
           
        }

    }
}
