using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ContextMenuStrip))]
    internal class __ContextMenuStrip : __ToolStripDropDownMenu
    {
        // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionContextMenu\ChromeExtensionContextMenu\Application.cs
        // what about chromeos?

        public IHTMLDiv InternalElement;

        public override DOM.HTML.IHTMLElement HTMLTargetRef
        {
            get
            {
                return InternalElement;
            }
        }

        public __ContextMenuStrip(IContainer container)
        {
            InternalElement = new IHTMLDiv();
            InternalElement.style.background = JSColor.White;
            InternalElement.style.border = JSColor.Gray;
            InternalElement.style.borderWidth = "1px";
            InternalElement.style.borderStyle = "solid";

        }
    }
}
