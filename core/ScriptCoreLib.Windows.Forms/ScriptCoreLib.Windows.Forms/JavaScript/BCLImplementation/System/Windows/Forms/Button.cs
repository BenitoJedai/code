using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System.Drawing;
using ScriptCoreLib.JavaScript.Drawing;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    // http://referencesource.microsoft.com/#System.Windows.Forms/ndp/fx/src/winforms/Managed/System/WinForms/Button.cs
    // X:\jsc.svn\core\ScriptCoreLibAndroid.Windows.Forms\ScriptCoreLibAndroid.Windows.Forms\Android\BCLImplementation\System\Windows\Forms\Button.cs

    [Script(Implements = typeof(global::System.Windows.Forms.Button))]
    public class __Button : __ButtonBase
    {
        object __ButtonTypeHint;

        public IHTMLButton InternalButton;

        public IHTMLDiv HTMLTarget { get; set; }

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTarget;
            }
        }

        public __Button()
        {
            // shadow element?
            this.HTMLTarget = new IHTMLDiv
            {

                // name: "CSSButton"
                // it works, yet does not apply css
                name = "CSSButton"
                //className = "CSSButton"
            };

            //this.HTMLTarget.setAttribute("name", "CSSButton");

            //this.HTMLTarget.setAttribute("style-id", x);
            //this.

            this.HTMLTarget.style.display = DOM.IStyle.DisplayEnum.inline_block;

            this.InternalButton = new IHTMLButton().AttachTo(this.HTMLTarget);
            this.InternalButton.style.padding = "0";

            this.InternalButton.style.position = DOM.IStyle.PositionEnum.absolute;
            this.InternalButton.style.left = "0";
            this.InternalButton.style.top = "0";

            // button is special like iframe??
            // width: 40px;
            this.InternalButton.style.width = "100%";
            this.InternalButton.style.height = "100%";


            this.InternalButton.style.font = DefaultFont.ToCssString();


            var FlatAppearance = new __FlatButtonAppearance();
            FlatAppearance.InternalBorderColorChanged +=
                delegate
                {
                    var a = this.FlatAppearance;
                    var BorderColor = a.BorderColor;

                    this.InternalButton.style.borderBottomColor = BorderColor.ToString();
                    this.InternalButton.style.borderStyle = "solid";
                    this.InternalButton.style.borderWidth = "1px";
                };
            this.FlatAppearance = (FlatButtonAppearance)(object)FlatAppearance;

        }




        public override bool Enabled
        {
            get
            {
                return !InternalButton.disabled;
            }
            set
            {
                InternalButton.disabled = !value;
            }
        }
        public override string Text
        {
            get
            {
                return InternalButton.innerText;
            }
            set
            {
                InternalButton.innerText = value;
            }
        }
        public void PerformClick()
        {
            InternalButton.click();
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




//        arg[0] is typeof System.Windows.Forms.AutoSizeMode
//script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.Button.set_AutoSizeMode(System.Windows.Forms.AutoSizeMode)]


        public AutoSizeMode AutoSizeMode { get; set; }

    }
}
