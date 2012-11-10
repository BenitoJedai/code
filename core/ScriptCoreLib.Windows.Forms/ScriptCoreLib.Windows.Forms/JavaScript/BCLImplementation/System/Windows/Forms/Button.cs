using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    using ScriptCoreLib.JavaScript.DOM.HTML;
    using System.Drawing;

    [Script(Implements = typeof(global::System.Windows.Forms.Button))]
    internal class __Button : __ButtonBase
    {
        object __ButtonTypeHint;

        public IHTMLButton HTMLTarget { get; set; }

        public __Button()
        {
            HTMLTarget = new IHTMLButton();
            HTMLTarget.style.padding = "0";

            this.InternalSetDefaultFont();

            var FlatAppearance = new __FlatButtonAppearance();
            FlatAppearance.InternalBorderColorChanged +=
                delegate
                {
                    var a = this.FlatAppearance;
                    var BorderColor = a.BorderColor;

                    this.HTMLTarget.style.borderBottomColor = BorderColor.ToString();
                    this.HTMLTarget.style.borderStyle = "solid";
                    this.HTMLTarget.style.borderWidth = "1px";
                };
            this.FlatAppearance = (FlatButtonAppearance)(object)FlatAppearance;

        }


        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTarget;
            }
        }

        public override bool Enabled
        {
            get
            {
                return !HTMLTarget.disabled;
            }
            set
            {
                HTMLTarget.disabled = !value;
            }
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
            }
        }


      

        #region operators
        static public implicit operator Button(__Button e)
        {
            return (Button)(object)e;
        }

        static public implicit operator __Button(Button e)
        {
            return (__Button)(object)e;
        }
        #endregion

    }
}
