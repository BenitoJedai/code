using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    using ScriptCoreLib.JavaScript.DOM.HTML;
    [Script(Implements = typeof(global::System.Windows.Forms.UserControl))]
    internal class __UserControl : __ContainerControl
    {

        public IHTMLDiv HTMLTarget { get; set; }

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTarget;
            }
        }


        public __UserControl()
        {
            HTMLTarget = new IHTMLDiv();

            //HTMLTarget.style.border = "1px dotted gray";

            this.Size = new global::System.Drawing.Size(150, 150);
            this.BackColor = SystemColors.ButtonFace;
        }

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

                if (value == BorderStyle.None)
                {
                    this.HTMLTargetRef.style.border = "";
                }
                else if (value == BorderStyle.FixedSingle)
                {
                    this.HTMLTargetRef.style.borderStyle = "solid";
                    this.HTMLTargetRef.style.borderWidth = "1px";
                    this.HTMLTargetRef.style.borderColor = Shared.Drawing.Color.System.ActiveBorder;
                }
                else
                {
                    this.HTMLTargetRef.style.borderStyle = "inset";
                    this.HTMLTargetRef.style.borderWidth = "1px";
                    this.HTMLTargetRef.style.borderColor = Shared.Drawing.Color.System.ActiveBorder;                    
                }
            }
        }

        #region
        static public implicit operator UserControl(__UserControl e)
        {
            return (UserControl)(object)e;
        }

        static public implicit operator __UserControl(UserControl e)
        {
            return (__UserControl)(object)e;
        }
        #endregion
    }
}
