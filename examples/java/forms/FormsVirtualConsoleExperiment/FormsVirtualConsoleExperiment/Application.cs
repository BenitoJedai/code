using FormsVirtualConsoleExperiment.Design;
using FormsVirtualConsoleExperiment.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using SQLiteWithDataGridView.Library;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FormsVirtualConsoleExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationApplet applet = new ApplicationApplet();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // http://stackoverflow.com/questions/6275953/how-to-display-div-over-a-java-applet-in-chrome



            new ConsoleForm().With(
                f =>
                {
                    f.InitializeConsoleFormWriter();

                    f.Show();

                    //var archive = applet.ToHTMLElement().AsXElement().Attribute("archive");
                    //archive.Value = ("" + Native.Document.location).TakeUntilLastOrEmpty("/") + "/" + archive.Value;

                    Console.WriteLine("calling applet.InitializeConsoleFormWriter");
                    applet.InitializeConsoleFormWriter(
                          Console.Write,
                          Console.WriteLine
                      );

                    Console.WriteLine("you are looking at FormsVirtualConsoleExperiment!!");

                    applet.AttachAppletToDocument();

                    var shadow = new IHTMLIFrame { src = "about:blank", frameBorder = "0", scrolling = "no" };

                    shadow.AttachToDocument();

                    shadow.style.SetLocation(120, 48);
                    shadow.style.zIndex = 0;

                    Action Update =
                        delegate
                        {
                            shadow.style.SetLocation(f.Left, f.Top, f.Width, f.Height);
                        };

                    f.LocationChanged +=
                        delegate
                        {
                            Update();
                        };

                    f.SizeChanged +=
                        delegate
                        {
                            Update();
                        };

                    Update();

                    Native.Window.onresize +=
                        delegate
                        {
                            applet.ToHTMLElement().style.SetSize(
                                 Native.Window.Width,
                                Native.Window.Height
                            );
                        };
                }
            );

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
