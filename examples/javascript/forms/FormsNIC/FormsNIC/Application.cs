using FormsNIC;
using FormsNIC.Design;
using FormsNIC.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FormsNIC
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
        public Application(IApp page)
        {
            //I/Web Console(  361): InternalFieldsFromTypeInitializer
            //I/Web Console(  361):  at http://192.168.43.7:1317/view-source:28802
            //I/Web Console(  361): Cookie GetValues { value =  }
            //I/Web Console(  361):  at http://192.168.43.7:1317/view-source:28802
            //I/Web Console(  361): Time to load fields from the cookie, were they even sent?
            //I/Web Console(  361):  at http://192.168.43.7:1317/view-source:28802
            //I/Web Console(  361): DataGridView ready
            //I/Web Console(  361):  at http://192.168.43.7:1317/view-source:28802
            //I/Web Console(  361): Time to load fields from the cookie, were they even sent?
            //I/Web Console(  361):  at http://192.168.43.7:1317/view-source:28802
            //E/Web Console(  361): Uncaught Error: SYNTAX_ERR: DOM Exception 12 at http://192.168.43.7:1317/view-source:11723


            content.AttachControlToDocument();

        }

    }
}
