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
        // Caution: OpenGL ES 2.0 is currently not supported by the Android Emulator. You must have a physical test device running Android 2.2 (API Level 8) or higher in order to run and test the example code in this tutorial.

        // note: rebuild could auto reinstall

        // running it on device:
        // attach device to usb
        // C:\util\android-sdk-windows\platform-tools\adb.exe devices
        //List of devices attached
        //3330A17632C000EC        device 




        private GLSurfaceView mGLView;

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);


            // Create a GLSurfaceView instance and set it
            // as the ContentView for this Activity
            mGLView = new HelloOpenGLES20SurfaceView(this);
            setContentView(mGLView);

        }

        protected override void onPause()
        {
            base.onPause();
            // The following call pauses the rendering thread.
            // If your OpenGL application is memory intensive,
            // you should consider de-allocating objects that
            // consume significant memory here.
            mGLView.onPause();
        }

        protected override void onResume()
        {
            base.onResume();
            // The following call resumes a paused rendering thread.
            // If you de-allocated graphic objects for onPause()
            // this is a good place to re-allocate them.
            mGLView.onResume();
        }


        [Script]
        public class HelloOpenGLES20SurfaceView : GLSurfaceView
        {

            public HelloOpenGLES20SurfaceView(Context context)
                : base(context)
            {

                // Create an OpenGL ES 2.0 context.
                setEGLContextClientVersion(2);
                // Set the Renderer for drawing on the GLSurfaceView
                setRenderer(new HelloOpenGLES20Renderer());
            }
        }


        [Script]
        public class HelloOpenGLES20Renderer : GLSurfaceView.Renderer
        {

            public void onSurfaceCreated(GL10 unused, EGLConfig config)
            {
                // Set the background frame color
                GLES20.glClearColor(0.5f, 0.5f, 0.5f, 1.0f);

                // initialize the triangle vertex array
                initShapes();
            }

            public void onDrawFrame(GL10 unused)
            {

                // Redraw background color
                GLES20.glClear(GLES20.GL_COLOR_BUFFER_BIT | GLES20.GL_DEPTH_BUFFER_BIT);
            }

            public void onSurfaceChanged(GL10 unused, int width, int height)
            {
                GLES20.glViewport(0, 0, width, height);
            }



        }

        #region Define a Triange
        partial class HelloOpenGLES20Renderer
        {
            private FloatBuffer triangleVB;


            private void initShapes()
            {

                float[] triangleCoords = {
                    // X, Y, Z
                    -0.5f, -0.25f, 0,
                     0.5f, -0.25f, 0,
                     0.0f,  0.559016994f, 0
                };

                // initialize vertex Buffer for triangle  
                ByteBuffer vbb = ByteBuffer.allocateDirect(
                    // (# of coordinate values * 4 bytes per float)
                        triangleCoords.Length * 4);
                vbb.order(ByteOrder.nativeOrder());// use the device hardware's native byte order
                triangleVB = vbb.asFloatBuffer();  // create a floating point buffer from the ByteBuffer
                triangleVB.put(triangleCoords);    // add the coordinates to the FloatBuffer
                triangleVB.position(0);            // set the buffer to read the first coordinate

            }
        }
        #endregion

    }



}
