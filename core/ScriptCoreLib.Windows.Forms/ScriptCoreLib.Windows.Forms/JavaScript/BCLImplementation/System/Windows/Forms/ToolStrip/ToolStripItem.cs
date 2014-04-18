using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ToolStripItem))]
    public abstract class __ToolStripItem : __Component
    {
        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.ToolStripItem.set_TextAlign(System.Drawing.ContentAlignment)]
        public virtual ContentAlignment TextAlign { get; set; }

        public string InternalText;
        public virtual string Text
        {
            get { return InternalText; }
            set
            {
                InternalText = value;
                if (TextChanged != null)
                    TextChanged(this, new EventArgs());
            }
        }
        public event EventHandler TextChanged;


        public event EventHandler Click;

        public void RaiseClick()
        {
            if (this.Click != null)
                this.Click(this, new EventArgs());
        }


        public string Name { get; set; }

        public virtual Size Size { get; set; }



        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.ToolStripItem.set_DisplayStyle(System.Windows.Forms.ToolStripItemDisplayStyle)]

        public virtual ToolStripItemDisplayStyle DisplayStyle { get; set; }

        // X:\jsc.svn\examples\javascript\p2p\SharedBrowserSessionExperiment\SharedBrowserSessionExperiment\TheBrowserTab.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140412
        // do we support forms images yet?
        public virtual Image BackgroundImage { get; set; }

        public virtual Image Image { get; set; }

        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.ToolStripItem.set_RightToLeftAutoMirrorImage(System.Boolean)]
        public bool RightToLeftAutoMirrorImage { get; set; }

        public string ToolTipText { get; set; }

        public string AccessibleName { get; set; }

        public bool AutoSize { get; set; }

        public virtual bool Enabled { get; set; }


        public static implicit operator __ToolStripItem(global::System.Windows.Forms.ToolStripItem e)
        {
            return (__ToolStripItem)(object)e;
        }


        public ToolStrip Owner { get; set; }

        public event Action InternalAfterSetOwner;
        public virtual void InternalSetOwner(__ToolStrip __ToolStrip)
        {

            this.Owner = __ToolStrip;

            if (InternalAfterSetOwner != null)
                InternalAfterSetOwner();

            //new IHTMLButton { this.Name }.AttachTo(
            //__ToolStrip.InternalElement
            //);

        }
    }
}
