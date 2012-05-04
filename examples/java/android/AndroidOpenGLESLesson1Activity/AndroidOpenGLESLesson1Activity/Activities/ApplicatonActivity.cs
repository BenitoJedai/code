using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.opengl;
using android.os;
using android.provider;
using android.view;
using android.webkit;
using android.widget;
using AndroidOpenGLESLesson1Activity.Library;
using java.lang;
using java.nio;
using javax.microedition.khronos.egl;
using javax.microedition.khronos.opengles;
using ScriptCoreLib;

namespace AndroidOpenGLESLesson1Activity.Activities
{
    using gl = GLES20;

    [Script]
    public class AndroidOpenGLESLesson1Activity : Activity
    {
        // port from http://developer.android.com/resources/tutorials/opengl/opengl-es20.html

        // C:\util\android-sdk-windows\tools\android.bat create project --package AndroidOpenGLESLesson1Activity.Activities --activity AndroidOpenGLESLesson1Activity  --target 2  --path y:\jsc.svn\examples\java\android\AndroidOpenGLESLesson1Activity\AndroidOpenGLESLesson1Activity\staging

        // http://developer.android.com/guide/developing/device.html#setting-up
        // Caution: OpenGL ES 2.0 is currently not supported by the Android Emulator. You must have a physical test device running Android 2.2 (API Level 8) or higher in order to run and test the example code in this tutorial.

        // note: rebuild could auto reinstall

        // running it on device:
        // attach device to usb
        // C:\util\android-sdk-windows\platform-tools\adb.exe devices
        //List of devices attached
        //3330A17632C000EC        device 

        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r "y:\jsc.svn\examples\java\android\AndroidOpenGLESLesson1Activity\AndroidOpenGLESLesson1Activity\staging\bin\AndroidOpenGLESLesson1Activity-debug.apk"


        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);


        }

    }



}
