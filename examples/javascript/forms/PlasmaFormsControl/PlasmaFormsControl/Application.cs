using PlasmaFormsControl.Design;
using PlasmaFormsControl.HTML.Pages;
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

namespace PlasmaFormsControl
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
            // public static T AttachControlTo<T>(this T that, IHTMLElement parent) where T : Control;
            // Error	1	The call is ambiguous between the following methods or properties: 
            // 'ScriptCoreLib.JavaScript.Windows.Forms.WindowsFormsExtensions.AttachControlTo<PlasmaFormsControl.ApplicationControl>(PlasmaFormsControl.ApplicationControl, ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement)' and
            // 'ScriptCoreLib.JavaScript.Extensions.FormExtensions.AttachControlTo<PlasmaFormsControl.ApplicationControl>(PlasmaFormsControl.ApplicationControl, ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement)'	
            // X:\jsc.svn\examples\javascript\forms\PlasmaFormsControl\PlasmaFormsControl\Application.cs	36	13	PlasmaFormsControl

            // X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\JavaScript\Windows\Forms\WindowsFormsExtensions.cs
            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\JavaScript\Extensions\FormExtensions.cs


            content.AttachControlTo(page.Content);
            //content.AutoSizeControlTo(page.ContentSize);
         
        }

    }
}
