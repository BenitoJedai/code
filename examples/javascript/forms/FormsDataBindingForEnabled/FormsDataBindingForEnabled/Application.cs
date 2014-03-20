using FormsDataBindingForEnabled;
using FormsDataBindingForEnabled.Design;
using FormsDataBindingForEnabled.HTML.Pages;
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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FormsDataBindingForEnabled
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //            No implementation found for this native method, please implement [System.Windows.Forms.Control.get_DataBindings()]
            //: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
            //error at FormsDataBindingForEnabled.ApplicationControl.ApplicationControl_Load,
            //BindingForEnabled.Application.exe
            //ForEnabled.ApplicationControl, FormsDataBindingForEnabled.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

            //onControl_Load(System.Object, System.EventArgs)

            content.AttachControlToDocument();

        }

    }
}
