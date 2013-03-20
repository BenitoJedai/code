using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;

namespace android.util
{
    // http://developer.android.com/reference/android/util/SparseArray.html
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/util/SparseArray.java
    [Script(IsNative = true)]
    public class SparseArray
    {
        public int size()
        {
            throw null;
        }

        public int keyAt(int index)
        {
            throw null;
        }

        public void put(int key, object value)
        {
        }

        public object get(int key)
        {
            throw null;

        }

        public void remove(int key)
        {
        }
    }
}
