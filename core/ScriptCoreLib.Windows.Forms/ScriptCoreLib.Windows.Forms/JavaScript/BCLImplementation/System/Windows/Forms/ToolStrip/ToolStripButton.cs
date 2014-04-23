using ScriptCoreLib.JavaScript.DOM;
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
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\HTML\IHTMLButton.cs
        public IHTMLButton InternalElement = typeof(__ToolStripButton);

        [Script]
        static class Styles
        {
            static IStyle idle = new IStyle(Native.css[typeof(__ToolStripButton)])
            {
                border = "1px solid transparent"
            };

            static IStyle hover = new IStyle(Native.css[typeof(__ToolStripButton)].hover)
            {
                border = "1px solid rgba(0,0,255, 0.5)",
                backgroundColor = "rgba(0,0,255, 0.1)"
            };

            static IStyle active = new IStyle(Native.css[typeof(__ToolStripButton)].active)
            {
                border = "1px solid rgba(0,0,255, 0.7)",
                backgroundColor = "rgba(0,0,255, 0.3)"
            };
        }


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
