using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Drawing;


namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ToolStripLabel))]
    public class __ToolStripLabel : __ToolStripItem
    {
        public IHTMLSpan InternalElement = typeof(__ToolStripLabel);

        static IStyle InternalStyle = new IStyle(typeof(__ToolStripLabel))
        {

            font = __Control.DefaultFont.ToCssString()

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
