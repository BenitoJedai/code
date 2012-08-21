using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;
using android.widget;

namespace android.webkit
{
    // http://developer.android.com/reference/android/webkit/ValueCallback.html
    [Script(IsNative = true)]
    public interface ValueCallback<T>
    {
        void onReceiveValue(T value);
    }
}
