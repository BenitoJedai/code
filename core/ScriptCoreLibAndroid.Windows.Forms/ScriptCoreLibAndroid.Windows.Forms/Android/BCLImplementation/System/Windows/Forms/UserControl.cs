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

        public event EventHandler Load;

        // called by?
        public override void InternalBeforeSetContext(android.content.Context c)
        {
            InternalScrollView = new ScrollView(c);
            InternalLinearLayout = new LinearLayout(c);

            var p = this.Padding;
            InternalLinearLayout.setPadding(
                          p.Left,
                          p.Top,
                          p.Right,
                          p.Bottom
                          );


            // shal we allow dynamic change after we have been shown?
            //InternalLinearLayout.setPadding(
            //    this.Padding.Left,
            //    this.Padding.Top,
            //    this.Padding.Right,
            //    this.Padding.Bottom
            //    );

            InternalLinearLayout.setOrientation(1);

            InternalScrollView.addView(InternalLinearLayout);


            // X:\jsc.svn\examples\java\android\forms\InteractivePortForwarding\InteractivePortForwarding\UserControl1.cs
            if (Load != null)
                Load(this, new EventArgs());
        }
    }
}
