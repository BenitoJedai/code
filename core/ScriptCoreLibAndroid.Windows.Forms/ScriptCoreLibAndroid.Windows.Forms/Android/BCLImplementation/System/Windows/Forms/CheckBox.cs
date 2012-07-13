using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.view;
using android.widget;
using java.lang;

namespace ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.CheckBox))]
    internal class __CheckBox : __ButtonBase
    {
        public CheckBox InternalElement;

        public override View InternalGetElement()
        {
            return InternalElement;
        }

        public string InternalText;
        public override void InternalSetText(string value)
        {
            if (InternalElement != null)
                InternalElement.setText((CharSequence)(object)value);
            else
                InternalText = value;
        }

        public override void InternalBeforeSetContext(android.content.Context c)
        {
            InternalElement = new CheckBox(c);
            InternalSetText(InternalText);
        }

    }
}
