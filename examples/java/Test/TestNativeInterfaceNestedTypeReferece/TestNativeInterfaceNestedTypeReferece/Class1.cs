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

        }
    }

}
