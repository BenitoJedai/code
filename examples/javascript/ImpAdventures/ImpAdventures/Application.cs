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
using ImpAdventures.Design;
using ImpAdventures.HTML.Pages;
using NatureBoy.js;

namespace ImpAdventures
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // would a service worker be of use
        // to download
        // unpack and cache all assets?




        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {

            // Error	3	The type or namespace name 'DiagnosticsConsole' could not be found in the global namespace (are you missing an assembly reference?)	X:\jsc.svn\examples\javascript\ImpAdventures\ImpAdventures\Application.cs	32	21	ImpAdventures
            //global::DiagnosticsConsole.ApplicationContent.BindKeyboardToDiagnosticsConsole();

            new Class4();


        }

    }
}
