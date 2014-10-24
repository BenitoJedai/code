using android.os;
using android.widget;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.appwidget
{

    // http://developer.android.com/reference/android/appwidget/AppWidgetProviderInfo.html
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/appwidget/AppWidgetProviderInfo.java

    [Script(IsNative = true)]
    public class AppWidgetProviderInfo : Parcelable
    {
        public virtual void writeToParcel(Parcel dest, int flags)
        {
        }


        public virtual int describeContents()
        {
            return 0;
        }
    }
}
