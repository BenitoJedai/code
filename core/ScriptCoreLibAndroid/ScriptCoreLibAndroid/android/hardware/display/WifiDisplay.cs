using android.os;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.hardware.display
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/hardware/display/WifiDisplay.java

    [Script(IsNative = true)]
    public class WifiDisplay : Parcelable
    {
        // tested by ?

        //* A public virtual display behaves just like most any other display that is connected
        //* to the system such as an HDMI or Wireless display.  Applications can open
        //* windows on the display and the system may mirror the contents of other displays
        //* onto it.
    }
}
