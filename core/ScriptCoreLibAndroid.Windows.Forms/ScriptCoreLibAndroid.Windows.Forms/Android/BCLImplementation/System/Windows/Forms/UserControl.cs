using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.widget;

namespace ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.UserControl))]
    public class __UserControl : __ContainerControl
    {
        // see also: X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\UserControl.cs


        public ScrollView InternalScrollView;
        public LinearLayout InternalLinearLayout;

        public override android.view.View InternalGetElement()
        {
            return InternalScrollView;
        }

        public override android.view.ViewGroup InternalGetContainer()
        {
            return InternalLinearLayout;
        }

        // called by?
        public override void InternalBeforeSetContext(android.content.Context c)
        {
            InternalScrollView = new ScrollView(c);
            InternalLinearLayout = new LinearLayout(c);

            InternalLinearLayout.setOrientation(1);

            InternalScrollView.addView(InternalLinearLayout);
        }
    }
}
