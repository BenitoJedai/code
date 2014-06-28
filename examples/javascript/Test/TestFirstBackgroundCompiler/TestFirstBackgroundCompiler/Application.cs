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
using TestFirstBackgroundCompiler;
using TestFirstBackgroundCompiler.Design;
using TestFirstBackgroundCompiler.HTML.Pages;

namespace TestFirstBackgroundCompiler
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
            // c:/util/jsc/bin/jsc.bc.exe $(ProjectPath) /rewrite /clear /run $(TargetPath) "C:\util\jsc\bin\ScriptCoreLib.Extensions.dll"
            // X:\jsc.svn\examples\javascript\LINQ\test\AutoRefreshTesting\AutoRefreshTestingHost\bin\Debug\AutoRefreshTestingHost.exe $(SolutionPath) $(ProjectPath) /rewrite /clear /run $(TargetPath) "C:\util\jsc\bin\ScriptCoreLib.Extensions.dll"

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier




            new IHTMLPre { "we are looking at backgroun compilation!" }.AttachToDocument();
            new IHTMLPre { "it works on .cs file changes and then reruns the app. ctrl. s." }.AttachToDocument();

            new IHTMLPre { "its now renamed as jsc.bc.exe" }.AttachToDocument();
            new IHTMLPre { "a rebuild will run this as a server." }.AttachToDocument();
            new IHTMLPre { "but we will d a full cycle. if only one type was changed" }.AttachToDocument();
            new IHTMLPre { "a localized change then could we update running app without restart?" }.AttachToDocument();

            page.AddingAButtonCtrlsWillCauseAssetsToBeRebuilt.style.color = "blue";
            page.AddingAButtonCtrlsWillCauseAssetsToBeRebuilt.onclick +=
                delegate
                {
                    page.AddingAButtonCtrlsWillCauseAssetsToBeRebuilt.Orphanize();
                    // ctrl s to test this new feature.
                    // what if there is no change on IL level?
                    // just some comments?
                };

            //xx
            //xx
            //x


            // why aint it working yet?
            this.WebMethod2(
                @"ctrl. s!! ",
                value => value.ToDocumentTitle()
            );
        }

    }
}
