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
using InteractiveDAESceneB.HTML.Pages;
using InteractiveDAESceneB;
using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.CSharp.Avalon.Extensions;

namespace InteractiveDAESceneB
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
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            new ApplicationCanvas().AttachToContainer(page.PageContainer);
        }

    }

    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {

    }

    #endregion
}
