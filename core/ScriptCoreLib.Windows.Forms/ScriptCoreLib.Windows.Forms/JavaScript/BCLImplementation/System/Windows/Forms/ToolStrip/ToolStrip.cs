using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ToolStrip))]
    public class __ToolStrip : __ScrollableControl
    {
        // X:\jsc.svn\examples\javascript\forms\HashForBindingSource\HashForBindingSource\ApplicationControl.Designer.cs
        // what if we would like to actually see our toolbar? HTMLTargetRef

        public ToolStripGripStyle GripStyle { get; set; }

        public override DOM.HTML.IHTMLElement HTMLTargetRef
        {
            get
            {
                return InternalElement;
            }
        }

        public IHTMLDiv InternalElement = new IHTMLDiv();

        public __ToolStrip()
        {
            // these look like generic .css rules. not instance specific.
            InternalElement.style.whiteSpace = DOM.IStyle.WhiteSpaceEnum.nowrap;
            InternalElement.style.overflow = DOM.IStyle.OverflowEnum.hidden;

            // shall css talk to the class name asigned to it?
            InternalElement.css.children.style.margin = "0px";
            InternalElement.css.children.style.marginLeft = "0.1em";
            InternalElement.css.children.style.marginRight = "0.1em";

            InternalElement.css.children.style.verticalAlign = "middle";

            // X:\jsc.svn\examples\javascript\forms\FormsNICWithDataSource\FormsNICWithDataSource\ApplicationControl.Designer.cs

            Items = new __ToolStripItemCollection
            {
                InternalOwner = this
            };


            // set the default. designer may omit it.
            this.Dock = DockStyle.Top;
        }

        public __ToolStripItemCollection Items { get; set; }

        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.ToolStrip.set_RenderMode(System.Windows.Forms.ToolStripRenderMode)]
        public ToolStripRenderMode RenderMode { get; set; }

        public static implicit operator ToolStrip(__ToolStrip x)
        {
            return (ToolStrip)(object)x;
        }

        public static implicit operator __ToolStrip(ToolStrip x)
        {
            return (__ToolStrip)(object)x;
        }
    }
}
