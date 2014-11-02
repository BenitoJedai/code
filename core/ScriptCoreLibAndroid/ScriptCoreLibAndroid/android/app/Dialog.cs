using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using android.content;

namespace android.app
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/app/Dialog.java
    // http://developer.android.com/reference/android/app/Dialog.html

    [Script(IsNative = true)]
    public class Dialog : DialogInterface
    {
        
        public void setOnDismissListener(DialogInterface_OnDismissListener listener)
        { 
        }

        public void dismiss()
        {
        }

        public void show()
        {
        }
    }
}
