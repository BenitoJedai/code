using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using android.os;

namespace android.app
{
    // http://developer.android.com/reference/android/app/WallpaperInfo.html
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/app/WallpaperInfo.java

    [Script(IsNative = true)]
    public class WallpaperInfo : Parcelable
    {
        // members and types are to be extended by jsc at release build


        public virtual void writeToParcel(Parcel dest, int flags)
        {
        }


        public virtual int describeContents()
        {
            return 0;
        }
    }
}
