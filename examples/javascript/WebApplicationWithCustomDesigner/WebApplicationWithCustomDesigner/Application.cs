using FormsWebServiceWithDesigner.Library;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WebApplicationWithCustomDesigner.Design;
using WebApplicationWithCustomDesigner.HTML.Pages;

namespace WebApplicationWithCustomDesigner
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : XAppFromDocument
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public Application()
        {

        }

        public Application(IApp page = null)
        {

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }

    [Designer(typeof(XComponentDesigner), typeof(IRootDesigner))]
    public class XAppFromDocument :
        Component
    {

    }

}
