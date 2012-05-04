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
        public partial class HelloOpenGLES20Renderer : GLSurfaceView.Renderer
        {

            public void onSurfaceCreated(GL10 unused, EGLConfig config)
            {
                // Set the background frame color
                GLES20.glClearColor(0.5f, 0.5f, 0.5f, 1.0f);

                // initialize the triangle vertex array
                initShapes();




                int vertexShader = loadShader(GLES20.GL_VERTEX_SHADER, vertexShaderCode);
                int fragmentShader = loadShader(GLES20.GL_FRAGMENT_SHADER, fragmentShaderCode);

                mProgram = GLES20.glCreateProgram();             // create empty OpenGL Program
                GLES20.glAttachShader(mProgram, vertexShader);   // add the vertex shader to program
                GLES20.glAttachShader(mProgram, fragmentShader); // add the fragment shader to program
                GLES20.glLinkProgram(mProgram);                  // creates OpenGL program executables

                // get handle to the vertex shader's vPosition member
                maPositionHandle = GLES20.glGetAttribLocation(mProgram, "vPosition");
            }

            public void onDrawFrame(GL10 unused)
            {

                // Redraw background color
                GLES20.glClear(GLES20.GL_COLOR_BUFFER_BIT | GLES20.GL_DEPTH_BUFFER_BIT);



                // Add program to OpenGL environment
                GLES20.glUseProgram(mProgram);

                // Prepare the triangle data
                GLES20.glVertexAttribPointer(maPositionHandle, 3, GLES20.GL_FLOAT, false, 12, triangleVB);
                GLES20.glEnableVertexAttribArray(maPositionHandle);



                // Apply a ModelView Projection transformation
                Matrix.multiplyMM(mMVPMatrix, 0, mProjMatrix, 0, mVMatrix, 0);
                GLES20.glUniformMatrix4fv(muMVPMatrixHandle, 1, false, mMVPMatrix, 0);

                // Draw the triangle
                GLES20.glDrawArrays(GLES20.GL_TRIANGLES, 0, 3);
            }

            public void onSurfaceChanged(GL10 unused, int width, int height)
            {
                GLES20.glViewport(0, 0, width, height);

                float ratio = (float)width / height;

                // this projection matrix is applied to object coodinates
                // in the onDrawFrame() method
                Matrix.frustumM(mProjMatrix, 0, -ratio, ratio, -1, 1, 3, 7);


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
                int shader = GLES20.glCreateShader(type);

                // add the source code to the shader and compile it
                GLES20.glShaderSource(shader, shaderCode);
                GLES20.glCompileShader(shader);

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
            private float[] mMMatrix = new float[16];
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

    }



}
