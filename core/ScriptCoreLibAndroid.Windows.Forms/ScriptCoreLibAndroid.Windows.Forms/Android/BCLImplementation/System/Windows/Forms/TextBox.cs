using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.view;
using android.widget;
using java.lang;

namespace ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.TextBox))]
    internal class __TextBox : __TextBoxBase
    {
        public EditText InternalEditText;


        public override View InternalGetElement()
        {
            return InternalEditText;
        }

        public string InternalText;
        public override void InternalSetText(string value)
        {
            if (InternalEditText != null)
                InternalEditText.setText((CharSequence)(object)value);
            else
                InternalText = value;
        }

        public override void InternalBeforeSetContext(android.content.Context c)
        {
            InternalEditText = new EditText(c);
            InternalSetText(InternalText);
        }
    }
}
