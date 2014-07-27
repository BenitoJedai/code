using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;
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
using TestFormsShadowSplit;
using TestFormsShadowSplit.Design;
using TestFormsShadowSplit.HTML.Pages;

namespace TestFormsShadowSplit
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

            var c = new Form1();

            // Error	2	The type 'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLContent' cannot be used as type parameter 'T' in the generic type or method 'ScriptCoreLib.Extensions.FormsExtensions.AttachTo<T>(T, System.Windows.Forms.Control)'. There is no implicit reference conversion from 'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLContent' to 'System.Windows.Forms.Control'.	X:\jsc.svn\examples\javascript\test\TestFormsShadowSplit\TestFormsShadowSplit\Application.cs	37	13	TestFormsShadowSplit
            new IHTMLContent { }.AttachTo(
                //c.splitContainer1.Panel2
                c.splitContainer1.Panel2.GetHTMLTargetContainer()
            );

            var s = new IHTMLPre { "the shadow dom" }.AttachTo(Native.document.documentElement.shadow);

            // forms shall use position fixed
            // to prevent overflow!?

            __Form.InternalHTMLTargetAttachToDocument =
                (that, yield) =>
                {
                    if (that.HTMLTarget.parentNode == null)
                    {
                        that.HTMLTarget.AttachTo(Native.document.documentElement.shadow);
                    }

                    // animate!
                    yield(true);
                };

            c.Show();


        }

    }
}
