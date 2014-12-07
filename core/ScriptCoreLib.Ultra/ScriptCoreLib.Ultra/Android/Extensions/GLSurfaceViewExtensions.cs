extern alias globalandroid;
using globalandroid::android.opengl;
using globalandroid::android.content;
using globalandroid::android.widget;
using globalandroid::java.lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.Extensions
{
    public static class GLSurfaceViewExtensions
    {
        class queueEvent_Handler : Runnable
        {
            public Action h;

            public void run()
            {
                h();
            }
        }

        // X:\jsc.svn\examples\java\android\gles\AndroidOpenGLESLesson5Activity\AndroidOpenGLESLesson5Activity\ApplicatonActivity.cs

        public static void queueEvent(this GLSurfaceView that, Action h)
        {
            that.queueEvent(
                new queueEvent_Handler { h = h }
            );
        }
    }
}
