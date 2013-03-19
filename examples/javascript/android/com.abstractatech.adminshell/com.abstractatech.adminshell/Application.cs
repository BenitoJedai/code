using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using com.abstractatech.adminshell.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;
using System.Drawing;

namespace com.abstractatech.adminshell
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
            page.LoginButton.onclick +=
                delegate
                {
                    page.LoginButton.Orphanize();

                    // ask for credentials for new ui

                    var s = new IHTMLScript { src = "/a" };

                    // http://stackoverflow.com/questions/538745/how-to-tell-if-a-script-tag-failed-to-load
                    s.onload +=
                        delegate
                        {
                            Console.WriteLine("onload");
                        };

                    s.AttachToDocument();

                };
        }


        public sealed class a
        {


            public a(IApp e)
            {

                var c = new ShellWithPing.Library.ConsoleWindow
                {
                    Text = "Administrator Shell (Logged in as " + new Cookie("foo").Value + ")",
                    Color = Color.Red,
                    BackColor = Color.Black
                };

                c.AppendLine(
@" *** WARNING *** be careful!
example:
 am start -a android.intent.action.CALL tel:245007
");

                c.Show();


                global::CSSMinimizeFormToSidebar.ApplicationExtension.InitializeSidebarBehaviour(
                    c, HandleClosed: false
                );

                Native.Document.body.style.backgroundColor = "#185D7B";

                c.PopupInsteadOfClosing();

                //c.AtCommand += applicationWebService1.ShellAsync;
            }
        }
    }
}
