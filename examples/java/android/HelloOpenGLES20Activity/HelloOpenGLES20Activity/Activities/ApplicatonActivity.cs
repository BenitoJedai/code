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

namespace HelloOpenGLES20Activity.Activities
{
    using gl = GLES20;

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

        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r "y:\jsc.svn\examples\java\android\HelloOpenGLES20Activity\HelloOpenGLES20Activity\staging\bin\HelloOpenGLES20Activity-debug.apk"



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


        [Script]
        public partial class HelloOpenGLES20Renderer : GLSurfaceView.Renderer
        {

            public void onSurfaceCreated(GL10 unused, EGLConfig config)
            {
                // Set the background frame color
                gl.glClearColor(0.5f, 0.5f, 0.5f, 1.0f);

                // initialize the triangle vertex array
                initShapes();




                int vertexShader = loadShader(gl.GL_VERTEX_SHADER, vertexShaderCode);
                int fragmentShader = loadShader(gl.GL_FRAGMENT_SHADER, fragmentShaderCode);

                mProgram = gl.glCreateProgram();             // create empty OpenGL Program
                gl.glAttachShader(mProgram, vertexShader);   // add the vertex shader to program
                gl.glAttachShader(mProgram, fragmentShader); // add the fragment shader to program
                gl.glLinkProgram(mProgram);                  // creates OpenGL program executables

                // get handle to the vertex shader's vPosition member
                maPositionHandle = gl.glGetAttribLocation(mProgram, "vPosition");
            }

            public void onDrawFrame(GL10 unused)
            {

                // Redraw background color
                gl.glClear(gl.GL_COLOR_BUFFER_BIT | gl.GL_DEPTH_BUFFER_BIT);



                // Add program to OpenGL environment
                gl.glUseProgram(mProgram);

                // Prepare the triangle data
                gl.glVertexAttribPointer(maPositionHandle, 3, gl.GL_FLOAT, false, 12, triangleVB);
                gl.glEnableVertexAttribArray(maPositionHandle);



    



                // Create a rotation for the triangle (Boring! Comment this out:)
                // long time = SystemClock.uptimeMillis() % 4000L;
                // float angle = 0.090f * ((int) time);

                // Use the mAngle member as the rotation value
                Matrix.setRotateM(mMMatrix, 0, mAngle, 0, 0, 1.0f);
                Matrix.multiplyMM(mMVPMatrix, 0, mVMatrix, 0, mMMatrix, 0);
                Matrix.multiplyMM(mMVPMatrix, 0, mProjMatrix, 0, mMVPMatrix, 0);

                // Apply a ModelView Projection transformation
                #region [uniform] uMVPMatrix <- mMVPMatrix
                gl.glUniformMatrix4fv(muMVPMatrixHandle, 1, false, mMVPMatrix, 0);
                #endregion

                // Draw the triangle
                gl.glDrawArrays(gl.GL_TRIANGLES, 0, 3);
            }

            public void onSurfaceChanged(GL10 unused, int width, int height)
            {
                gl.glViewport(0, 0, width, height);

                float ratio = (float)width / height;

                // this projection matrix is applied to object coodinates
                // in the onDrawFrame() method
                Matrix.frustumM(mProjMatrix, 0, -ratio, ratio, -1, 1, 3, 7);

                muMVPMatrixHandle = gl.glGetUniformLocation(mProgram, "uMVPMatrix");

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

            public const string fragmentShaderCode =
        "precision mediump float;  \n" +
        "void main(){              \n" +
        " gl_FragColor = vec4 (0.63671875, 0.76953125, 0.22265625, 1.0); \n" +
        "}                         \n";


            private int loadShader(int type, String shaderCode)
            {

                // create a vertex shader type (GLES20.GL_VERTEX_SHADER)
                // or a fragment shader type (GLES20.GL_FRAGMENT_SHADER)
                int shader = gl.glCreateShader(type);

                // add the source code to the shader and compile it
                gl.glShaderSource(shader, shaderCode);
                gl.glCompileShader(shader);

                return shader;
            }


            private int mProgram;
            private int maPositionHandle;
        }
        #endregion




        #region Apply Projection and Camera View
        partial class HelloOpenGLES20Renderer
        {
            private int muMVPMatrixHandle;
            private float[] mMVPMatrix = new float[16];
            private float[] mVMatrix = new float[16];
            private float[] mProjMatrix = new float[16];


            private const string vertexShaderCode =
                // This matrix member variable provides a hook to manipulate
                // the coordinates of the objects that use this vertex shader
        "uniform mat4 uMVPMatrix;   \n" +

        "attribute vec4 vPosition;  \n" +
        "void main(){               \n" +

        // the matrix must be included as a modifier of gl_Position
        " gl_Position = uMVPMatrix * vPosition; \n" +

        "}  \n";
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
