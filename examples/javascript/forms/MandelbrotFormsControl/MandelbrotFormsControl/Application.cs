using MandelbrotFormsControl.Design;
using MandelbrotFormsControl.HTML.Pages;
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
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MandelbrotFormsControl
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            //impl:type: ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing.__Image 8ebb56a7-9ca1-3c92-908a-696d9c757606  - System.Drawing.Image 451dbf16-b46b-3b4f-993c-efd8b01553a0
            //script: error JSC1000: No implementation found for this native method, please implement [System.Drawing.Image.Dispose()]
            //script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
            //script: error JSC1000: error at MandelbrotFormsControl.Library.MandelbrotComponent+<>c__DisplayClass6.<MandelbrotComponent_Load>b__0,

            content.AttachControlToDocument();


        }

    }
}
