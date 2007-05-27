using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements=typeof(RadioButton))]
    class __RadioButton : __ButtonBase
    {
        public bool TabStop { get; set; }


        public IHTMLDiv HTMLTarget { get; set; }

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTarget;
            }
        }

        IHTMLInput check;
        IHTMLLabel label;



        public __RadioButton()
        {
            HTMLTarget = new IHTMLDiv();

            check = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.radio, "");
            label = new IHTMLLabel("", check);

            HTMLTarget.appendChild(check, label);
        }

        public override bool Enabled
        {
            get
            {
                return !check.disabled;
            }
            set
            {
                check.disabled = !value;
            }
        }
        public override string Text
        {
            get
            {
                return label.innerText;
            }
            set
            {
                label.innerText = value;
            }
        }


        public bool Checked
        {
            get { return check.@checked; }
            set { check.@checked = value; }
        }
    }
}
