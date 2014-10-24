using android.os;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.hardware.display
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/hardware/display/WifiDisplay.java
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/services/java/com/android/server/display/HeadlessDisplayAdapter.java
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/services/java/com/android/server/display/VirtualDisplayAdapter.java
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/services/java/com/android/server/display/WifiDisplayAdapter.java

    [Script(IsNative = true)]
    public class WifiDisplay : Parcelable
    {
        // tested by ?

        //* A public virtual display behaves just like most any other display that is connected
        //* to the system such as an HDMI or Wireless display.  Applications can open
        //* windows on the display and the system may mirror the contents of other displays
        //* onto it.



        public virtual void writeToParcel(Parcel dest, int flags)
        { 
        }


        public virtual int describeContents()
        {
            return 0;
        }
    }
}
