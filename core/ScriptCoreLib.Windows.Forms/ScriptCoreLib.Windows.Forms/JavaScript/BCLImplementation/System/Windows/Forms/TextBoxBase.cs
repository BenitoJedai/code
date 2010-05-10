using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    using ScriptCoreLib.JavaScript.DOM.HTML;


    [Script(Implements = typeof(global::System.Windows.Forms.TextBoxBase))]
    internal class __TextBoxBase : __Control
    {
        public bool Multiline { get; set; }

        public IHTMLTextArea HTMLTarget { get; set; }

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTarget;
            }
        }


        public __TextBoxBase()
        {
            HTMLTarget = new IHTMLTextArea();

			// http://www.electrictoolbox.com/disable-textarea-resizing-safari-chrome/
			HTMLTarget.style.resize = "none";
			HTMLTarget.onchange +=
				delegate
				{
					this.InternalRaiseTextChanged();
				};

            this.Size = new global::System.Drawing.Size(100, 20);
			
			// fixme: we should be switching between HTMLTextArea and HTMLInput...
			this.InternalSetDefaultFont();
        }

        public bool ReadOnly
        {
            get { return this.HTMLTarget.readOnly; }
            set { this.HTMLTarget.readOnly = value; }
        }

		public void AppendText(string text)
		{
			this.Text += text;
		}

        public void Clear()
        {
            this.Text = "";
        }

        public override string Text
        {
            get
            {
                return this.HTMLTarget.value;
            }
            set
            {
                this.HTMLTarget.value = value;
            }
        }

        public bool WordWrap
        {
            get
            {
                return false;

                //return this.HTMLTarget.style.whiteSpace == ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.normal;
            }
            set
            {
                //if (value)
                //    this.HTMLTarget.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.normal;
                //else
                //    this.HTMLTarget.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;

            }
        }

        #region
        static public implicit operator TextBoxBase(__TextBoxBase e)
        {
            return (TextBoxBase)(object)e;
        }

        static public implicit operator __TextBoxBase(TextBoxBase e)
        {
            return (__TextBoxBase)(object)e;
        }
        #endregion
    }
}
