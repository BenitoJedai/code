
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
using WebServicePDFGenerator;
using WebServicePDFGenerator.Design;
using WebServicePDFGenerator.HTML.Pages;

namespace WebServicePDFGenerator
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

            //         { SourceMethod = Void Page_InitializeInternalFontFace() }
            //         { SourceMethod = ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement Initialize_0_html(WebServicePDFGenerator.HTML.Pages.App, ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement) }
            //     script: error JSC1000: Method: Initialize_0_html, Type: WebServicePDFGenerator.HTML.Pages.App; emmiting failed : System.IndexOutOfRangeException: Index was outside the bounds of the array.
            //at jsc.ILInstruction.get_TargetParameter() in X:\jsc.internal.git\compiler\jsc\CodeModel\ILInstruction.cs:line 1389

            new IHTMLButton { "pdf" }.AttachToDocument().WhenClicked(
                async button =>
                {
                    var base64 = await base.Invoke();

                    // { Length = 1228 }

                    new IHTMLPre {
                        new { base64.Length }
                    }.AttachToDocument();

                    new IHTMLIFrame
                    {

                        src = "data:application/pdf;base64," + base64
                        //bytes =
                    }.AttachToDocument();
                }
             );

        }

    }
}
