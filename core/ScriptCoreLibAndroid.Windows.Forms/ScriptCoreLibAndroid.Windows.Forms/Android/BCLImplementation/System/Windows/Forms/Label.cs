using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.view;
using android.widget;
using java.lang;

namespace ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.Label))]
    internal class __Label  : __Control
    {
        public TextView InternalTextView;

        public override View InternalGetElement()
        {
            return InternalTextView;
        }

        public string InternalText;
        public override void InternalSetText(string value)
        {
            if (InternalTextView != null)
                InternalTextView.setText((CharSequence)(object)value);
            else
                InternalText = value;
        }

        public override void InternalBeforeSetContext(android.content.Context c)
        {
            InternalTextView = new TextView(c);
            InternalSetText(InternalText);
        }
    }
}
