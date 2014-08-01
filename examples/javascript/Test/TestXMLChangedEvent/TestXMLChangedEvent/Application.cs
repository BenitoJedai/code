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
using TestXMLChangedEvent;
using TestXMLChangedEvent.Design;
using TestXMLChangedEvent.HTML.Pages;

namespace TestXMLChangedEvent
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
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier

            this.Header.Changed += (send, arg) =>
            {
                Console.WriteLine("Sender "+send.ToString());

                if (arg.ObjectChange == XObjectChange.Value)
                {
                    var t = (XAttribute)send;
                    Console.WriteLine("Attr " + t.Name + " val " + t.Value);
                }
               
                Console.WriteLine("Args " + arg.ToString());

                Console.WriteLine("Header changed");
            };
            page.Test = Header;

            WebMethod2();

            page.Test = Header;

           



            //this.Header.Changing += delegate
            //{
            //    Console.WriteLine("Header changing");
            //};
        }

    }
}
