using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ToolStripButton))]
    public class __ToolStripButton : __ToolStripItem
    {
        public IHTMLButton InternalElement = new IHTMLButton();

        public __ToolStripButton()
        {
            this.InternalElement.onclick +=
                delegate
                {
                    this.RaiseClick();
                };

            this.TextChanged += delegate
            {
                this.InternalElement.innerText = this.InternalText;
            };

            this.InternalAfterSetOwner +=
                delegate
                {
                    __ToolStrip o = this.Owner;

                    // or contaner?
                    InternalElement.AttachTo(o.InternalElement);

                };

        }
    }
}
