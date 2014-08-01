using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    // http://referencesource.microsoft.com/#System.Windows.Forms/ndp/fx/src/winforms/Managed/System/WinForms/StatusStrip.cs

    [Script(Implements = typeof(global::System.Windows.Forms.StatusStrip))]
    internal class __StatusStrip : __ToolStrip
    {
        public IHTMLDiv InternalElement = new IHTMLDiv();

        public override DOM.HTML.IHTMLElement HTMLTargetRef
        {
            get
            {
                return this.InternalElement;
            }
        }
    }
}
