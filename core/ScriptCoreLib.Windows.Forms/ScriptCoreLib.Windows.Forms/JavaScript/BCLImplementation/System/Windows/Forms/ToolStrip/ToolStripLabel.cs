using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib.JavaScript.Extensions;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ToolStripLabel))]
    public class __ToolStripLabel : __ToolStripItem
    {
        public IHTMLSpan InternalElement = new IHTMLSpan
        {

            className =
                " " + typeof(__ToolStripLabel).Name
        };


        public __ToolStripLabel()
        {
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
