using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    // http://referencesource.microsoft.com/#System.Windows.Forms/ndp/fx/src/winforms/Managed/System/WinForms/DateTimePicker.cs

    [Script(Implements = typeof(global::System.Windows.Forms.DateTimePicker))]
    internal class __DateTimePicker : __Control
    {
        public IHTMLInput HTMLTarget { get; set; }

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTarget;
            }
        }

        public __DateTimePicker()
        {
            this.HTMLTarget = new IHTMLInput
            {
                type = Shared.HTMLInputTypeEnum.date
            };
            this.HTMLTarget.style.border = "1px solid gray";

            this.Size = new global::System.Drawing.Size(80, 20);
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
                OnTextChanged(this, new EventArgs());
            }
        }
        //valueAsNumber: 1390003200000

        public DateTime Value { get { return this.HTMLTarget; } set { throw new NotImplementedException(); } }

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

        static public implicit operator DateTimePicker(__DateTimePicker e)
        {
            return (DateTimePicker)(object)e;
        }

        static public implicit operator __DateTimePicker(DateTimePicker e)
        {
            return (__DateTimePicker)(object)e;
        }
    }
}
