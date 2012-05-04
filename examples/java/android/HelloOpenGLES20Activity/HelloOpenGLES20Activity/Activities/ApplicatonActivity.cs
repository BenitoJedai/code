using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.opengl;
using android.provider;
using android.view;
using android.webkit;
using android.widget;
using HelloOpenGLES20Activity.Library;
using java.lang;
using java.nio;
using javax.microedition.khronos.egl;
using javax.microedition.khronos.opengles;
using ScriptCoreLib;

namespace HelloOpenGLES20Activity.Activities
{
    [Script]
    public class HelloOpenGLES20Activity : Activity
    {
        // port from http://developer.android.com/resources/tutorials/opengl/opengl-es20.html

        // C:\util\android-sdk-windows\tools\android.bat create project --package HelloOpenGLES20Activity.Activities --activity HelloOpenGLES20Activity  --target 2  --path y:\jsc.svn\examples\java\android\HelloOpenGLES20Activity\HelloOpenGLES20Activity\staging

        // http://developer.android.com/guide/developing/device.html#setting-up

        // running it in emulator:
        // C:\util\android-sdk-windows\tools\android.bat avd
        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r "Z:\jsc.svn\examples\java\android\HelloOpenGLES20Activity\HelloOpenGLES20Activity\staging\bin\HelloOpenGLES20Activity-debug.apk"

        // note: rebuild could auto reinstall

        // running it on device:
        // attach device to usb
        // C:\util\android-sdk-windows\platform-tools\adb.exe devices
        //List of devices attached
        //3330A17632C000EC        device 

 

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);
            setContentView(_vortexView);

        }





    }
}
