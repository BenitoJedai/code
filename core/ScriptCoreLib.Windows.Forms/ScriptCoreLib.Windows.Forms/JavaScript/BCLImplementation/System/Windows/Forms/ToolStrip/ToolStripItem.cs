using ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing;
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

        #region Text
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
        #endregion


        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.ToolStripItem.set_ImageTransparentColor(System.Drawing.Color)]
        public Color ImageTransparentColor { get; set; }


        #region Click

        public event EventHandler Click;

        public void RaiseClick()
        {
            if (this.Click != null)
                this.Click(this, new EventArgs());
        }

        #endregion



        public string Name { get; set; }

        public virtual Size Size { get; set; }



        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.ToolStripItem.set_DisplayStyle(System.Windows.Forms.ToolStripItemDisplayStyle)]

        public virtual ToolStripItemDisplayStyle DisplayStyle { get; set; }

        // X:\jsc.svn\examples\javascript\p2p\SharedBrowserSessionExperiment\SharedBrowserSessionExperiment\TheBrowserTab.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140412
        // do we support forms images yet?
        public virtual Image BackgroundImage { get; set; }



        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.ToolStripItem.set_Font(System.Drawing.Font)]

        #region Image
        public event Action InternalImageChanged;
        public Image InternalImage;
        public virtual Image Image
        {
            get { return InternalImage; }
            set
            {
                this.InternalImage = value;

                if (InternalImageChanged != null)
                    InternalImageChanged();
            }
        }
        #endregion


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





        #region Font
        public virtual void InternalSetFont(Font value)
        {
        }

        private Font InternalFont;
        public Font Font
        {
            get { return InternalFont; }
            set
            {
                InternalFont = value;

                //this.HTMLTargetRef.style.font = value.ToCssString();
                InternalSetFont(value);

                //OnFontChanged(new EventArgs());


            }
        }
        #endregion

        public virtual Color ForeColor { get; set; }

    }
}
