using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ChromeTCPServer;
using ChromeTCPServer.Design;
using ChromeTCPServer.HTML.Pages;
using chrome;

namespace ChromeTCPServer
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
        public Application(IApp page)
        {


            chrome.app.runtime.Launched +=
                delegate
                {
                    new Notification
                    {
                        Title = "ChromeTCPServer",
                        Message = "Launched!"
                    };
                };

            chrome.app.runtime.Restarted +=
            delegate
            {
                new Notification
                {
                    Title = "ChromeTCPServer",
                    Message = "Restarted!"
                };
            };


            chrome.runtime.Installed += delegate
            {
                new Notification
                {
                    Title = "ChromeTCPServer",
                    Message = "Installed!"
                };
            };

            chrome.runtime.Startup +=
                delegate
                {
                    new Notification
                    {
                        Title = "ChromeTCPServer",
                        Message = "Startup!"
                    };
                };

            chrome.runtime.Suspend +=
                delegate
                {
                    new Notification
                    {
                        Title = "ChromeTCPServer",
                        Message = "Suspend!"
                    };
                };

            //            getNetworkList: 
            //{ name = {CE7A76DF-BCB0-4C3B-8466-D712A03F10A0}, address = fe80::55cc:63eb:5b4:60b4 }
            //{ name = {CE7A76DF-BCB0-4C3B-8466-D712A03F10A0}, address = 192.168.43.252 }
            //{ name = {4E818D17-30DD-46D2-9592-9E1F497D3D82}, address = 2001:0:5ef5:79fb:24f2:176e:3f57:d403 }
            //{ name = {4E818D17-30DD-46D2-9592-9E1F497D3D82}, address = fe80::24f2:176e:3f57:d403 }


            chrome.socket.getNetworkList().ContinueWithResult(
                n =>
                {
                    var m = new StringBuilder();

                    m.AppendLine("getNetworkList: ");

                    foreach (var item in n.OrderBy(k => k.address.Contains(":")))
                    {
                        m.AppendLine(new { item.name, item.address }.ToString());
                    }

                    Console.WriteLine(m);

                    new Notification
                    {
                        Title = "ChromeTCPServer",
                        Message = m.ToString()
                    };
                }
            );




        }

    }
}
