using android.database;
using android.net;
using ScriptCoreLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.content
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/content/DialogInterface.java
    // http://developer.android.com/reference/android/content/DialogInterface.html

    [Script(IsNative = true)]
    public interface DialogInterface
    {
        // X:\jsc.svn\core\ScriptCoreLibAndroid.Windows.Forms\ScriptCoreLibAndroid.Windows.Forms\Android\BCLImplementation\System\Windows\Forms\MessageBox.cs



    }

    [Script(IsNative = true)]
    public interface DialogInterface_OnClickListener
    {
        void onClick(DialogInterface arg0, int arg1);

    }

    [Script(IsNative = true)]
    public interface DialogInterface_OnDismissListener
    {
         void onDismiss(DialogInterface value);

    }
}
