using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

[assembly: Script]
[assembly: ScriptTypeFilter(ScriptType.Java)]

namespace android.content
{
    [Script(IsNative = true)]
    public interface DialogInterface
    {
        void cancel();
        void dismiss();
    }
}

namespace android.view
{
    [Script(IsNative = true)]
    public interface WindowManager
    { }

    [Script(IsNative = true)]
    public class WindowManager_LayoutParams
    {
        public static int FLAG_FULLSCREEN;
    }

    [Script(IsNative = true)]
    public interface WindowManager_LayoutParamsX
    {
    }

}

namespace android.content
{
    [Script(IsNative = true)]
    public interface DialogInterface_OnClickListener
    {

        void onClick(DialogInterface dialog, int item);
    }


}


namespace TestNativeInterfaceNestedTypeReferece
{
    [Script]
    class lesson_six_min_filter_types_onclick : android.content.DialogInterface_OnClickListener
    {
        public void onClick(android.content.DialogInterface dialog, int item)
        {
            var i = android.view.WindowManager_LayoutParams.FLAG_FULLSCREEN;

            android.view.WindowManager_LayoutParamsX x;
        }
    }

}
