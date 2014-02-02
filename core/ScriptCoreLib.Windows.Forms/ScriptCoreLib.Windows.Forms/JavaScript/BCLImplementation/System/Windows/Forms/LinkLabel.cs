using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.LinkLabel))]
    internal partial class __LinkLabel : __Label, __IButtonControl
    {
        public IHTMLAnchor InternalAnchor;

        public CSSStyleRuleMonkier css_active;
        public CSSStyleRuleMonkier css;

        public __LinkLabel()
        {
            // X:\jsc.svn\examples\javascript\forms\Test\TestHyperlink\TestHyperlink\ApplicationControl.cs

            this.InternalAnchor = new IHTMLAnchor { }.AttachTo(this.HTMLTargetContainer);

            this.InternalLabel.Orphanize().AttachTo(this.InternalAnchor);

            this.HTMLTarget = this.InternalAnchor;

            // should the rules be static?
            this.InternalAnchor.css.hover.style.textDecoration = "underline";

            this.css_active = this.InternalAnchor.css.active;
            this.css_active.style.color = "red";

            // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.LinkLabel.set_LinkColor(System.Drawing.Color)]

            this.css = this.InternalAnchor.css;

            this.css.style.color = "blue";
            this.css.style.cursor = DOM.IStyle.CursorEnum.pointer;

            // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.LinkLabel.set_ActiveLinkColor(System.Drawing.Color)]


            // we might use a real A element instead
            //this.HTMLTargetRef.style.color = ScriptCoreLib.JavaScript.Runtime.JSColor.Blue;

            // http://www.w3schools.com/Css/pr_text_text-decoration.asp
            //this.HTMLTargetRef.style.textDecoration = "underline";
            //this.HTMLTargetRef.style.cursor = ScriptCoreLib.JavaScript.DOM.IStyle.CursorEnum.pointer;

            this.InternalAnchor.onclick +=
                delegate
                {
                    // we could do a lazy bind here instead
                    // but we assume a link will have a handler anyway

                    if (this.LinkClicked != null)
                        this.LinkClicked(this, new LinkLabelLinkClickedEventArgs(null));
                };
        }

        public Color ActiveLinkColor
        {
            get
            {
                return Color.Red;
            }
            set
            {
                this.css_active.style.color = value.ToString();
            }
        }

        public Color LinkColor
        {
            get
            {
                return Color.Blue;
            }
            set
            {
                this.css.style.color = value.ToString();
            }
        }

        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.LinkLabel.set_LinkBehavior(System.Windows.Forms.LinkBehavior)]
        public LinkBehavior LinkBehavior { get; set; }

        public event LinkLabelLinkClickedEventHandler LinkClicked;


        public LinkArea LinkArea { get; set; }

        public bool UseCompatibleTextRendering { get; set; }

    }
}
