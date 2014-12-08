using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using android.content;
using android.view;

namespace android.app
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/app/AlertDialog.java
    // http://developer.android.com/reference/android/app/AlertDialog.html

    [Script(IsNative = true)]
    public class AlertDialog : Dialog
    {
        // X:\jsc.svn\core\ScriptCoreLibAndroid.Windows.Forms\ScriptCoreLibAndroid.Windows.Forms\Android\BCLImplementation\System\Windows\Forms\MessageBox.cs

        // members and types are to be extended by jsc at release build





        // http://developer.android.com/reference/android/app/AlertDialog.Builder.html
        [Script(IsNative = true)]
        public class Builder
        {
            // X:\jsc.svn\core\ScriptCoreLibAndroid.Windows.Forms\ScriptCoreLibAndroid.Windows.Forms\Android\BCLImplementation\System\Windows\Forms\MessageBox.cs

            public Builder(Context c)
            {

            }

            public virtual AlertDialog create()
            {
                return default(AlertDialog);
            }

            public virtual Builder setView(View view)
            {
                return default(Builder);
            }

            public virtual Builder setMessage(string title)
            {
                return default(Builder);
            }

            public virtual Builder setTitle(string title)
            {
                return default(Builder);
            }

            public virtual Builder setPositiveButton(string text, DialogInterface_OnClickListener listener)
            {
                return default(Builder);
            }

            // X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Android\Extensions\AlertDialogBuilderExtensions.cs
            public virtual Builder setItems(java.lang.CharSequence[] text, DialogInterface_OnClickListener listener)
            {
                return default(Builder);
            }
        }
    }
}
