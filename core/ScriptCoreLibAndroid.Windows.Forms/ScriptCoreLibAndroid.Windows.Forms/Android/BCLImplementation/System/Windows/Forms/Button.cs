using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.view;
using android.widget;
using java.lang;

namespace ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms
{
    // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\Button.cs
    // http://referencesource.microsoft.com/#System.Windows.Forms/ndp/fx/src/winforms/Managed/System/WinForms/Button.cs

    [Script(Implements = typeof(global::System.Windows.Forms.Button))]
    public class __Button : __ButtonBase
    {
        // X:\jsc.svn\examples\java\android\forms\AndroidFormsActivity\AndroidFormsActivity\ApplicationActivity.cs

        public Button InternalElement;

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
            InternalElement = new Button(c);
            InternalSetText(InternalText);
            InternalAddClick();
        }

    
    }
}
