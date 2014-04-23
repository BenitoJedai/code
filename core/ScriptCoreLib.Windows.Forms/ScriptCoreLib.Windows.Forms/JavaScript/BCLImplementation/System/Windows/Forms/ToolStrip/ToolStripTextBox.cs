using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ToolStripTextBox))]
    public class __ToolStripTextBox : __ToolStripItem
    {
        public IHTMLInput InternalElement = new IHTMLInput
        {

            className =
                " " + typeof(__ToolStripTextBox).Name
        };


        public __ToolStripTextBox()
        {
            this.TextChanged += delegate
            {
                this.InternalElement.value = this.InternalText;
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
