using Test453Forms;
using Test453Forms.Design;
using Test453Forms.HTML.Pages;
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

namespace Test453Forms
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public readonly ApplicationControl content = new ApplicationControl();


        //02000050 ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.__Control
        //script: error JSC1000: Method: InternalChildrenAnchorUpdate, Type: ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.__Control; emmiting failed : System.Exception: recursion detected at stack 32
        //   at jsc.RecursionGuard..ctor(RecursionGuard parent) in x:\jsc.internal.git\compiler\jsc\RecursionGuard.cs:line 31
        //   at jsc.RecursionGuard.get_Lock() in x:\jsc.internal.git\compiler\jsc\RecursionGuard.cs:line 47
        //   at jsc.ILBlock.Prestatement.ValidateInlineAssigment(Prestatement p) in x:\jsc.internal.git\compiler\jsc\CodeModel\ILBlock.cs:line 727
        //   at jsc.ILBlock.PrestatementBlock.Populate() in x:\jsc.internal.git\compiler\jsc\CodeModel\ILBlock.cs:line 1491

        //        at System.ComponentModel.Design.Serialization.CodeDomDesignerLoader.EnsureDocument(IDesignerSerializationManager manager)
        //at System.ComponentModel.Design.Serialization.CodeDomDesignerLoader.PerformLoad(IDesignerSerializationManager manager)
        //at Microsoft.VisualStudio.Design.Serialization.CodeDom.VSCodeDomDesignerLoader.PerformLoad(IDesignerSerializationManager serializationManager)
        //at Microsoft.VisualStudio.Design.Serialization.CodeDom.VSCodeDomDesignerLoader.DeferredLoadHandler.Microsoft.VisualStudio.TextManager.Interop.IVsTextBufferDataEvents.OnLoadCompleted(Int32 fReload)

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            content.AttachControlToDocument();
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            this.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
