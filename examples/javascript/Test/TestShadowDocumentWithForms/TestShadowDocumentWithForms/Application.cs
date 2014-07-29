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
using TestShadowDocumentWithForms;
using TestShadowDocumentWithForms.Design;
using TestShadowDocumentWithForms.HTML.Pages;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;

namespace TestShadowDocumentWithForms
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
            // used by
            // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionShadowExperiment\ChromeExtensionShadowExperiment\Application.cs



            // X:\jsc.svn\examples\javascript\Test\TestShadowBody\TestShadowBody\Application.cs
            //var s = new ShadowLayout().AttachTo(Native.document.documentElement.shadow);
            var s = new ShadowLayout().AttachTo(Native.shadow);

            // forms shall use position fixed
            // to prevent overflow!?

            __Form.InternalHTMLTargetAttachToDocument =
                (that, yield) =>
                {
                    if (that.HTMLTarget.parentNode == null)
                    {
                        //that.HTMLTarget.AttachTo(Native.document.documentElement.shadow);
                        that.HTMLTarget.AttachTo(Native.shadow);
                    }

                    // animate!
                    yield(true);
                };


            new FooUserControl().AttachControlTo(
                s.TopSideBar
            );






        }

    }
}
