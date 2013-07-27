using VirtualConsoleExperiment.Design;
using VirtualConsoleExperiment.HTML.Pages;
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
using Abstractatech.ConsoleFormPackage.Library;

namespace VirtualConsoleExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationSprite sprite = new ApplicationSprite();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // Initialize ApplicationSprite
            sprite.AttachSpriteTo(page.Content);

            sprite.WhenReady(
                delegate
                {
                    Console.WriteLine("WhenReady!");
                }
            );

            new ConsoleForm().With(
                f =>
                {
                    f.HandleFormClosing = false;
                    f.InitializeConsoleFormWriter();
                    f.PopupInsteadOfClosing();

                    f.Shown +=
                        delegate
                        {
                            Console.WriteLine("you are looking at VirtualConsoleExperiment");

                            //Action<string> Console_Write = x => Console.Write(x);
                            //Action<string> Console_WriteLine = x => Console.WriteLine(x);
                            sprite.InitializeConsoleFormWriter(
                                Console.Write,
                                Console.WriteLine
                            );
                            Console.WriteLine("you are looking at VirtualConsoleExperiment!!");
                        };

                    f.Show();


                }
            );

            page._Hi.onclick +=
                delegate
                {
                    Console.WriteLine("hi");
                };


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
