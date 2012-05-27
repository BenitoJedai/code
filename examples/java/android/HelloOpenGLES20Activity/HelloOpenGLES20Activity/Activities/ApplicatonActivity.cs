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
using HelloOpenGLES20Activity.Library;
using java.lang;
using java.nio;
using javax.microedition.khronos.egl;
using javax.microedition.khronos.opengles;
using ScriptCoreLib;
using ScriptCoreLib.Android;

namespace HelloOpenGLES20Activity.Activities
{
    using gl = __WebGLRenderingContext;
    using opengl = GLES20;

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

        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r "y:\jsc.svn\examples\java\android\HelloOpenGLES20Activity\HelloOpenGLES20Activity\staging\bin\HelloOpenGLES20Activity-debug.apk"



        private GLSurfaceView mGLView;

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);


            // Create a GLSurfaceView instance and set it
            // as the ContentView for this Activity
            mGLView = new HelloOpenGLES20SurfaceView(this);
            setContentView(mGLView);

            this.ShowToast("http://jsc-solutions.net");
        }

        #region pause
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
        #endregion


        public partial class HelloOpenGLES20SurfaceView : GLSurfaceView
        {

            public HelloOpenGLES20SurfaceView(Context context)
                : base(context)
            {

                // Create an OpenGL ES 2.0 context.
                setEGLContextClientVersion(2);

                // set the mRenderer member
                mRenderer = new HelloOpenGLES20Renderer();
                setRenderer(mRenderer);

                // Render the view only when there is a change
                setRenderMode(GLSurfaceView.RENDERMODE_WHEN_DIRTY);
            }
        }


        public partial class HelloOpenGLES20Renderer : GLSurfaceView.Renderer
        {
            __WebGLRenderingContext gl = new __WebGLRenderingContext();

            public void onSurfaceCreated(GL10 unused, EGLConfig config)
            {
                // Set the background frame color
                gl.clearColor(0.5f, 0.5f, 0.5f, 1.0f);

                // initialize the triangle vertex array
                initShapes();


                mProgram = gl.createAndLinkProgram(
                    new Shaders.TriangleVertexShader(),
                    new Shaders.TriangleFragmentShader()
                );

           
                // get handle to the vertex shader's vPosition member
                maPositionHandle = gl.getAttribLocation(mProgram, "vPosition");
            }

            public void onDrawFrame(GL10 unused)
            {

                // Redraw background color
                gl.clear(opengl.GL_COLOR_BUFFER_BIT | opengl.GL_DEPTH_BUFFER_BIT);



                // Add program to OpenGL environment
                gl.useProgram(mProgram);

                // Prepare the triangle data
                opengl.glVertexAttribPointer(maPositionHandle, 3, opengl.GL_FLOAT, false, 12, triangleVB);
                opengl.glEnableVertexAttribArray(maPositionHandle);



    



                // Create a rotation for the triangle (Boring! Comment this out:)
                // long time = SystemClock.uptimeMillis() % 4000L;
                // float angle = 0.090f * ((int) time);

                // Use the mAngle member as the rotation value
                Matrix.setRotateM(mMMatrix, 0, mAngle, 0, 0, 1.0f);
                Matrix.multiplyMM(mMVPMatrix, 0, mVMatrix, 0, mMMatrix, 0);
                Matrix.multiplyMM(mMVPMatrix, 0, mProjMatrix, 0, mMVPMatrix, 0);

                // Apply a ModelView Projection transformation
                #region [uniform] uMVPMatrix <- mMVPMatrix
                gl.uniformMatrix4fv(muMVPMatrixHandle, 1, false, mMVPMatrix, 0);
                #endregion

                // Draw the triangle
                opengl.glDrawArrays(opengl.GL_TRIANGLES, 0, 3);
            }

            public void onSurfaceChanged(GL10 unused, int width, int height)
            {
                gl.viewport(0, 0, width, height);

                float ratio = (float)width / height;

                // this projection matrix is applied to object coodinates
                // in the onDrawFrame() method
                Matrix.frustumM(mProjMatrix, 0, -ratio, ratio, -1, 1, 3, 7);

                muMVPMatrixHandle = gl.getUniformLocation(mProgram, "uMVPMatrix");

                Matrix.setLookAtM(mVMatrix, 0, 0, 0, -3, 0f, 0f, 0f, 0f, 1.0f, 0.0f);
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



        #region Draw the Triange
        partial class HelloOpenGLES20Renderer
        {
            private __WebGLProgram mProgram;
            private int maPositionHandle;
        }
        #endregion




        #region Apply Projection and Camera View
        partial class HelloOpenGLES20Renderer
        {
            private __WebGLUniformLocation muMVPMatrixHandle;
            private float[] mMVPMatrix = new float[16];
            private float[] mVMatrix = new float[16];
            private float[] mProjMatrix = new float[16];

        }
        #endregion


        #region Add Motion
        partial class HelloOpenGLES20Renderer
        {
            private float[] mMMatrix = new float[16];
        }
        #endregion


        #region Respond to Touch Events
        partial class HelloOpenGLES20Renderer
        {

            public float mAngle;



        }

        partial class HelloOpenGLES20SurfaceView
        {

            private const float TOUCH_SCALE_FACTOR = 180.0f / 320;
            private HelloOpenGLES20Renderer mRenderer;
            private float mPreviousX;
            private float mPreviousY;



            public override bool onTouchEvent(MotionEvent e)
            {
                // MotionEvent reports input details from the touch screen
                // and other input controls. In this case, you are only
                // interested in events where the touch position changed.

                float x = e.getX();
                float y = e.getY();

                if (e.getAction() == MotionEvent.ACTION_MOVE)
                {

                    float dx = x - mPreviousX;
                    float dy = y - mPreviousY;

                    // reverse direction of rotation above the mid-line
                    if (y > getHeight() / 2)
                    {
                        dx = dx * -1;
                    }

                    // reverse direction of rotation to left of the mid-line
                    if (x < getWidth() / 2)
                    {
                        dy = dy * -1;
                    }

                    mRenderer.mAngle += (dx + dy) * TOUCH_SCALE_FACTOR;
                    requestRender();
                }

                mPreviousX = x;
                mPreviousY = y;
                return true;
            }
        }
        #endregion
    }



}
