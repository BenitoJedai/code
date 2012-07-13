using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.view;
using android.widget;
using java.lang;

namespace ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.Button))]
    internal class __Button : __ButtonBase
    {
        public Button InternalButton;

        public override View InternalGetElement()
        {
            return InternalButton;
        }

        public string InternalText;
        public override void InternalSetText(string value)
        {
            if (InternalButton != null)
                InternalButton.setText((CharSequence)(object)value);
            else
                InternalText = value;
        }

        public override void InternalBeforeSetContext(android.content.Context c)
        {
            InternalButton = new Button(c);
            InternalSetText(InternalText);
        }

    
    }
}
