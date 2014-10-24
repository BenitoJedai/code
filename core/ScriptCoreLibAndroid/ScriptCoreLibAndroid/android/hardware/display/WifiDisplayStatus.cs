using android.os;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.hardware.display
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/hardware/display/WifiDisplayStatus.java

    // hidden?
    [Script(IsNative = true)]
    public class WifiDisplayStatus : Parcelable
    {
        // tested by ?

            public virtual void writeToParcel(Parcel dest, int flags)
        { 
        }


        public virtual int describeContents()
        {
            return 0;
        }
    }
}
