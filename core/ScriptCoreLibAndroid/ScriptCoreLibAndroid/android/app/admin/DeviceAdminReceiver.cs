using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using android.content;

namespace android.app.admin
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/app/admin/DeviceAdminReceiver.java
    // http://developer.android.com/reference/android/app/admin/DeviceAdminReceiver.html

    [Script(IsNative = true)]
    public class DeviceAdminReceiver : BroadcastReceiver
    {
        // https://developer.android.com/guide/topics/admin/device-admin.html

        // members and types are to be extended by jsc at release build
    }
}
