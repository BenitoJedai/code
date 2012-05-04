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
    using android.content.pm;
    using gl = GLES20;

    [Script]
    public class AndroidOpenGLESLesson1Activity : Activity
    {
        // port from http://www.learnopengles.com/android-lesson-one-getting-started/

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




        /** Hold a reference to our GLSurfaceView */
        private GLSurfaceView mGLSurfaceView;


        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            mGLSurfaceView = new GLSurfaceView(this);

            // Check if the system supports OpenGL ES 2.0.
            ActivityManager activityManager = (ActivityManager)getSystemService(Context.ACTIVITY_SERVICE);
            ConfigurationInfo configurationInfo = activityManager.getDeviceConfigurationInfo();
            var supportsEs2 = configurationInfo.reqGlEsVersion >= 0x20000;

            if (supportsEs2)
            {
                // Request an OpenGL ES 2.0 compatible context.
                mGLSurfaceView.setEGLContextClientVersion(2);

                // Set the renderer to our demo renderer, defined below.
                mGLSurfaceView.setRenderer(new LessonOneRenderer());
            }
            else
            {
                // This is where you could create an OpenGL ES 1.x compatible
                // renderer if you wanted to support both ES 1 and ES 2.
                return;
            }

            setContentView(mGLSurfaceView);


        }

        #region pause

        protected override void onResume()
        {
            // The activity must call the GL surface view's onResume() on activity onResume().
            base.onResume();
            mGLSurfaceView.onResume();
        }


        protected override void onPause()
        {
            // The activity must call the GL surface view's onPause() on activity onPause().
            base.onPause();
            mGLSurfaceView.onPause();
        }

        #endregion

        [Script]
        class LessonOneRenderer : GLSurfaceView.Renderer
        {

            // New class members
            /** Store our model data in a float buffer. */
            private FloatBuffer mTriangle1Vertices;
            private FloatBuffer mTriangle2Vertices;
            private FloatBuffer mTriangle3Vertices;

            /** How many bytes per float. */
            private int mBytesPerFloat = 4;

            /**
             * Initialize the model data.
             */
            public LessonOneRenderer()
            {
                // This triangle is red, green, and blue.
                float[] triangle1VerticesData = {
                    // X, Y, Z,
                    // R, G, B, A
                    -0.5f, -0.25f, 0.0f,
                    1.0f, 0.0f, 0.0f, 1.0f,
 
                    0.5f, -0.25f, 0.0f,
                    0.0f, 0.0f, 1.0f, 1.0f,
 
                    0.0f, 0.559016994f, 0.0f,
                    0.0f, 1.0f, 0.0f, 1.0f
                                                };


                // Initialize the buffers.
                mTriangle1Vertices = ByteBuffer.allocateDirect(triangle1VerticesData.Length * mBytesPerFloat)
                .order(ByteOrder.nativeOrder()).asFloatBuffer();


                mTriangle1Vertices.put(triangle1VerticesData).position(0);

            }



            
        }
    }



}
