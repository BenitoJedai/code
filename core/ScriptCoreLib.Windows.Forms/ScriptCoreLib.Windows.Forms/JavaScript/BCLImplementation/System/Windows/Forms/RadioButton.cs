using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
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

        internal IHTMLInput button;
        internal IHTMLLabel label;



        public __RadioButton()
        {
            // http://msdn2.microsoft.com/en-us/library/system.windows.forms.radiobutton.aspx
            /* Use the Checked property to get or set the state of a RadioButton. 
             * The option button's appearance can be altered to appear as a toggle-style 
             * button or as a standard option button by setting the Appearance property.
             */

            // http://javascript.about.com/library/blradio2.htm
            /* The solution here is to give all of the radio buttons within the group 
             * the same name but different values. Here is the code used to code just 
             * radio button themselves on the previous page that shows you how this is done.
             */

            // http://www.thescripts.com/forum/thread468483.html

            // IE support
            // http://www.gtalbot.org/DHTMLSection/DynamicallyCreateRadioButtons.html

            HTMLTarget = new IHTMLDiv();
            HTMLTarget.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;

            button = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.radio);
            label = new IHTMLLabel("", button);

            HTMLTarget.appendChild(button, label);
        }

        protected override void OnParentChanged(EventArgs e)
        {
            // calling base method that was overriden is not supported at this time

            __Control c = this.Parent;

            var v = button.value;
            button.Dispose();
            this.button = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.radio, c.ControlGroupName,  v);
            // we need to rewire
            this.button.id = this.label.htmlFor;
            
            

            InternalUpdate();

            base.RaiseParentChanged(e);
        }


        #region CheckAlign
        private ContentAlignment _CheckAlign;

        public ContentAlignment CheckAlign
        {
            get { return _CheckAlign; }
            set
            {
                _CheckAlign = value;

                InternalUpdate();
            }
        }

        private void InternalUpdate()
        {
            if (_CheckAlign == ContentAlignment.MiddleRight)
            {
                HTMLTarget.appendChild(label, button);
                HTMLTarget.style.textAlign = ScriptCoreLib.JavaScript.DOM.IStyle.TextAlignEnum.right;
            }
            else
            {
                HTMLTarget.appendChild(button, label);
                HTMLTarget.style.textAlign = ScriptCoreLib.JavaScript.DOM.IStyle.TextAlignEnum.left;
            }
        }
        #endregion

        public override bool Enabled
        {
            get
            {
                return !button.disabled;
            }
            set
            {
                button.disabled = !value;
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
                button.value = value;
                label.innerText = value;
            }
        }


        public bool Checked
        {
            get { return button.@checked; }
            set { button.@checked = value; }
        }


        #region
        static public implicit operator RadioButton(__RadioButton e)
        {
            return (RadioButton)(object)e;
        }

        static public implicit operator __RadioButton(RadioButton e)
        {
            return (__RadioButton)(object)e;
        }
        #endregion
    }
}
