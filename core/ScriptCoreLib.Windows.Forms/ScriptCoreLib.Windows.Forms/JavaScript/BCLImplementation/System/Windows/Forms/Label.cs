using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.JavaScript.Extensions;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    using ScriptCoreLib.JavaScript.DOM.HTML;
    using ScriptCoreLib.JavaScript.DOM;

    [Script(Implements = typeof(global::System.Windows.Forms.Label))]
    internal class __Label : __Control
    {
        public IHTMLDiv HTMLTargetContainer;

        public IHTMLLabel HTMLTarget { get; set; }

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTargetContainer;
            }
        }


        public __Label()
        {
            this.HTMLTargetContainer = new IHTMLDiv();
            this.HTMLTarget = new IHTMLLabel().AttachTo(this.HTMLTargetContainer);
            this.HTMLTarget.style.whiteSpace = DOM.IStyle.WhiteSpaceEnum.pre;
            this.HTMLTarget.style.display = IStyle.DisplayEnum.inline_block;

            this.Size = new global::System.Drawing.Size(100, 18);
			this.InternalSetDefaultFont();
        }

        public override string Text
        {
            get
            {
                return HTMLTarget.innerText;
            }
            set
            {
                HTMLTarget.innerText = value;

                if (this.AutoSize)
                {
                    this.HTMLTargetContainer.style.SetSize(
                        this.HTMLTarget.scrollWidth,
                        this.HTMLTarget.scrollHeight
                    );
                }
            }
        }

        #region
        static public implicit operator Label(__Label e)
        {
            return (Label)(object)e;
        }

        static public implicit operator __Label(Label e)
        {
            return (__Label)(object)e;
        }
        #endregion

		public bool TabStop { get; set; }

        // is there a style we can use?
        public bool AutoEllipsis { get; set; }

        // how flat should we look? :)
        public FlatStyle FlatStyle { get; set; }

        BorderStyle _BorderStyle;
        public BorderStyle BorderStyle
        {
            get
            {
                return this._BorderStyle;
            }
            set
            {
                this._BorderStyle = value;

                this.HTMLTargetRef.ApplyBorderStyle(value);
            }
        }
    }
}
