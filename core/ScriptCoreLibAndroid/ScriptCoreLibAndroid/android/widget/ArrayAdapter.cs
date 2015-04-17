using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using android.view;
using ScriptCoreLib;

namespace android.widget
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/widget/ArrayAdapter.java
    // http://developer.android.com/reference/android/widget/ArrayAdapter.html
    [Script(IsNative = true)]
    public class ArrayAdapter<T> : BaseAdapter
    {
        // members and types are to be extended by jsc at release build

        public ArrayAdapter(Context c, int resource, T[] objects)
        {

        }
    }
}
