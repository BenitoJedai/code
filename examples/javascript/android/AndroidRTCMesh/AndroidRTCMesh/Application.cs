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
using AndroidRTCMesh;
using AndroidRTCMesh.Design;
using AndroidRTCMesh.HTML.Pages;

namespace AndroidRTCMesh
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
            // based on
            // X:\jsc.svn\examples\javascript\android\AndroidRTC\AndroidRTC\Application.cs

            Native.body.Clear();

            // are we running on android?
            // are we the first?
            // if we are the first we should start asking others to join us..
            // lets connect inputs..

        }

    }
}
