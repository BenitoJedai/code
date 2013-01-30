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
using IsometricTycoonViewWithToolbar.Design;
using IsometricTycoonViewWithToolbar.HTML.Pages;
using IsometricTycoonViewWithToolbar.HTML.Audio.FromAssets;

namespace IsometricTycoonViewWithToolbar
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
        public Application(IDefaultPage page)
        {
            global::DiagnosticsConsole.ApplicationContent.BindKeyboardToDiagnosticsConsole();


            new gong().AttachToDocument().play();
            new ThreeDStuff.js.Tycoon4();

       
        }

    }
}
