using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using android.content;
using android.os;
using android.view;

namespace android.app
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/app/Service.java
    // http://developer.android.com/reference/android/app/Service.html

    [Script(IsNative = true)]
    public abstract class Service : ContextWrapper
    {
        // https://developer.android.com/tools/sdk/ndk/index.html
        // You cannot access features such as Services and Content Providers natively, 
        // so if you want to use them or any other framework API, you can still write JNI code to do so.
        // what if we want to do a service from native code?
        // emit .dx code to do that?

        // http://developer.android.com/guide/components/services.html#Foreground
        // http://developer.android.com/guide/topics/manifest/service-element.html

        // members and types are to be extended by jsc at release build

        public static int START_NOT_STICKY;


        public void startForeground(int id, Notification notification)
        { }
        public void stopForeground(bool removeNotification)
        {
        }


        public void stopSelf()
        {
        }


        public virtual IBinder onBind(Intent value)
        {
            throw null;
        }
        public virtual void onCreate()
        {
            throw null;
        }

        public virtual void onDestroy()
        {
            throw null;
        }
        public virtual int onStartCommand(Intent intent, int flags, int startId)
        {
            throw null;
        }

    }
}
