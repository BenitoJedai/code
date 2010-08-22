// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using MultitouchTransformAvalonFingers.HTML.Pages;
using MultitouchTransformAvalonFingers;
using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using System.Diagnostics;
using System.IO;

namespace MultitouchTransformAvalonFingers
{


    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// In debug build you can just hit F5 and debug the server side code.
        /// </summary>
        /// <param name="args">Commandline arguments</param>
        [STAThread]
        public static void Main(string[] args)
        {
#if DEBUG
            //{ var n = typeof(System.Windows.Input.TouchPoint).AssemblyQualifiedName; Debugger.Break(); }

            new ApplicationCanvas().ToWindow().ShowDialog();
#else

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
#endif
        }

    }

    #region default Browser Application implementation for release build testing
    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class Application
    {
        public class RemoteWriter : TextWriter
        {
            public override void Write(string e)
            {
                // cache?
                new ApplicationWebService().Console_Write(e);
            }

            public override Encoding Encoding
            {
                get
                {
                    throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            Console.SetOut(new RemoteWriter());
            new ApplicationCanvas().AttachToContainer(page.PageContainer);
        }

    }

    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        public void Console_Write(string e)
        {
            Console.Write(e);
        }
    }

    #endregion
}
