using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms;

namespace ScriptCoreLib.Extensions.Android
{
    [Script]
    public static class AndroidFormsExtensions
    {
        public static void AttachTo(this System.Windows.Forms.Control e, Activity Container)
        {
            var i = (__Control)(object)e;

            i.InternalSetContext(Container);

            var Child = i.InternalGetElement();

            Container.setContentView(Child);
        }
    }
}
