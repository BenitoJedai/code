using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.content.pm;
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
        // port from http://www.learnopengles.com/android-lesson-one-getting-started/
        // see also: "Y:\opensource\github\Learn-OpenGLES-Tutorials\android\AndroidOpenGLESLessons\src\com\learnopengles\android\lesson1\LessonOneActivity.java"

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

        // screenshot: home+back
        // at "F:\ScreenCapture\SC20120504-153450.png"


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
            /**
                 * Store the model matrix. This matrix is used to move models from object space (where each model can be thought
                 * of being located at the center of the universe) to world space.
                 */
            private float[] mModelMatrix = new float[16];

            /**
             * Store the view matrix. This can be thought of as our camera. This matrix transforms world space to eye space;
             * it positions things relative to our eye.
             */
            private float[] mViewMatrix = new float[16];

            /** Store the projection matrix. This is used to project the scene onto a 2D viewport. */
            private float[] mProjectionMatrix = new float[16];

            /** Allocate storage for the final combined matrix. This will be passed into the shader program. */
            private float[] mMVPMatrix = new float[16];

            /** Store our model data in a float buffer. */
            private readonly FloatBuffer mTriangle1Vertices;
            private readonly FloatBuffer mTriangle2Vertices;
            private readonly FloatBuffer mTriangle3Vertices;

            /** This will be used to pass in the transformation matrix. */
            private int mMVPMatrixHandle;

            /** This will be used to pass in model position information. */
            private int mPositionHandle;

            /** This will be used to pass in model color information. */
            private int mColorHandle;

            /** How many bytes per float. */
            private const int mBytesPerFloat = 4;

            /** How many elements per vertex. */
            private const int mStrideBytes = 7 * mBytesPerFloat;

            /** Offset of the position data. */
            private const int mPositionOffset = 0;

            /** Size of the position data in elements. */
            private const int mPositionDataSize = 3;

            /** Offset of the color data. */
            private const int mColorOffset = 3;

            /** Size of the color data in elements. */
            private const int mColorDataSize = 4;

            /**
             * Initialize the model data.
             */
            public LessonOneRenderer()
            {
                // Define points for equilateral triangles.

                // This triangle is red, green, and blue.
                float[] triangle1VerticesData = {
				// X, Y, Z, 
				// R, G, B, A
	            -0.5f, -0.25f, 0.0f, 
	            1.0f, 0.0f, 0.0f, 1.0f,
	            
	            0.5f, -0.25f, 0.0f,
	            0.0f, 0.0f, 1.0f, 1.0f,
	            
	            0.0f, 0.559016994f, 0.0f, 
	            0.0f, 1.0f, 0.0f, 1.0f};

                // This triangle is yellow, cyan, and magenta.
                float[] triangle2VerticesData = {
				// X, Y, Z, 
				// R, G, B, A
	            -0.5f, -0.25f, 0.0f, 
	            1.0f, 1.0f, 0.0f, 1.0f,
	            
	            0.5f, -0.25f, 0.0f, 
	            0.0f, 1.0f, 1.0f, 1.0f,
	            
	            0.0f, 0.559016994f, 0.0f, 
	            1.0f, 0.0f, 1.0f, 1.0f};

                // This triangle is white, gray, and black.
                float[] triangle3VerticesData = {
				// X, Y, Z, 
				// R, G, B, A
	            -0.5f, -0.25f, 0.0f, 
	            1.0f, 1.0f, 1.0f, 1.0f,
	            
	            0.5f, -0.25f, 0.0f, 
	            0.5f, 0.5f, 0.5f, 1.0f,
	            
	            0.0f, 0.559016994f, 0.0f, 
	            0.0f, 0.0f, 0.0f, 1.0f};

                // Initialize the buffers.
                mTriangle1Vertices = ByteBuffer.allocateDirect(triangle1VerticesData.Length * mBytesPerFloat)
                .order(ByteOrder.nativeOrder()).asFloatBuffer();
                mTriangle2Vertices = ByteBuffer.allocateDirect(triangle2VerticesData.Length * mBytesPerFloat)
                .order(ByteOrder.nativeOrder()).asFloatBuffer();
                mTriangle3Vertices = ByteBuffer.allocateDirect(triangle3VerticesData.Length * mBytesPerFloat)
                .order(ByteOrder.nativeOrder()).asFloatBuffer();

                mTriangle1Vertices.put(triangle1VerticesData).position(0);
                mTriangle2Vertices.put(triangle2VerticesData).position(0);
                mTriangle3Vertices.put(triangle3VerticesData).position(0);
            }

            public void onSurfaceCreated(GL10 glUnused, EGLConfig config)
            {
                // Set the background clear color to gray.
                gl.glClearColor(0.5f, 0.5f, 0.5f, 0.5f);

                // Position the eye behind the origin.
                const float eyeX = 0.0f;
                const float eyeY = 0.0f;
                const float eyeZ = 1.5f;

                // We are looking toward the distance
                const float lookX = 0.0f;
                const float lookY = 0.0f;
                const float lookZ = -5.0f;

                // Set our up vector. This is where our head would be pointing were we holding the camera.
                const float upX = 0.0f;
                const float upY = 1.0f;
                const float upZ = 0.0f;

                // Set the view matrix. This matrix can be said to represent the camera position.
                // NOTE: In OpenGL 1, a ModelView matrix is used, which is a combination of a model and
                // view matrix. In OpenGL 2, we can keep track of these matrices separately if we choose.
                Matrix.setLookAtM(mViewMatrix, 0, eyeX, eyeY, eyeZ, lookX, lookY, lookZ, upX, upY, upZ);

                var vertexShader =
                    "uniform mat4 u_MVPMatrix;      \n"		// A constant representing the combined model/view/projection matrix.

                  + "attribute vec4 a_Position;     \n"		// Per-vertex position information we will pass in.
                  + "attribute vec4 a_Color;        \n"		// Per-vertex color information we will pass in.			  

                  + "varying vec4 v_Color;          \n"		// This will be passed into the fragment shader.

                  + "void main()                    \n"		// The entry point for our vertex shader.
                  + "{                              \n"
                  + "   v_Color = a_Color;          \n"		// Pass the color through to the fragment shader. 
                    // It will be interpolated across the triangle.
                  + "   gl_Position = u_MVPMatrix   \n" 	// gl_Position is a special variable used to store the final position.
                  + "               * a_Position;   \n"     // Multiply the vertex by the matrix to get the final point in 			                                            			 
                  + "}                              \n";    // normalized screen coordinates.

                var fragmentShader =
                    "precision mediump float;       \n"		// Set the default precision to medium. We don't need as high of a 
                    // precision in the fragment shader.				
                  + "varying vec4 v_Color;          \n"		// This is the color from the vertex shader interpolated across the 
                    // triangle per fragment.			  
                  + "void main()                    \n"		// The entry point for our fragment shader.
                  + "{                              \n"
                  + "   gl_FragColor = v_Color;     \n"		// Pass the color directly through the pipeline.		  
                  + "}                              \n";

                // Load in the vertex shader.
                int vertexShaderHandle = gl.glCreateShader(gl.GL_VERTEX_SHADER);

                if (vertexShaderHandle != 0)
                {
                    // Pass in the shader source.
                    gl.glShaderSource(vertexShaderHandle, vertexShader);

                    // Compile the shader.
                    gl.glCompileShader(vertexShaderHandle);

                    // Get the compilation status.
                    var compileStatus = new int[1];
                    gl.glGetShaderiv(vertexShaderHandle, gl.GL_COMPILE_STATUS, compileStatus, 0);

                    // If the compilation failed, delete the shader.
                    if (compileStatus[0] == 0)
                    {
                        gl.glDeleteShader(vertexShaderHandle);
                        vertexShaderHandle = 0;
                    }
                }

                if (vertexShaderHandle == 0)
                {
                    throw null;
                    //throw new RuntimeException("Error creating vertex shader.");
                }

                // Load in the fragment shader shader.
                int fragmentShaderHandle = gl.glCreateShader(gl.GL_FRAGMENT_SHADER);

                if (fragmentShaderHandle != 0)
                {
                    // Pass in the shader source.
                    gl.glShaderSource(fragmentShaderHandle, fragmentShader);

                    // Compile the shader.
                    gl.glCompileShader(fragmentShaderHandle);

                    // Get the compilation status.
                    var compileStatus = new int[1];
                    gl.glGetShaderiv(fragmentShaderHandle, gl.GL_COMPILE_STATUS, compileStatus, 0);

                    // If the compilation failed, delete the shader.
                    if (compileStatus[0] == 0)
                    {
                        gl.glDeleteShader(fragmentShaderHandle);
                        fragmentShaderHandle = 0;
                    }
                }

                if (fragmentShaderHandle == 0)
                {
                    throw null;
                    //throw new RuntimeException("Error creating fragment shader.");
                }

                // Create a program object and store the handle to it.
                int programHandle = gl.glCreateProgram();

                if (programHandle != 0)
                {
                    // Bind the vertex shader to the program.
                    gl.glAttachShader(programHandle, vertexShaderHandle);

                    // Bind the fragment shader to the program.
                    gl.glAttachShader(programHandle, fragmentShaderHandle);

                    // Bind attributes
                    gl.glBindAttribLocation(programHandle, 0, "a_Position");
                    gl.glBindAttribLocation(programHandle, 1, "a_Color");

                    // Link the two shaders together into a program.
                    gl.glLinkProgram(programHandle);

                    // Get the link status.
                    var linkStatus = new int[1];
                    gl.glGetProgramiv(programHandle, gl.GL_LINK_STATUS, linkStatus, 0);

                    // If the link failed, delete the program.
                    if (linkStatus[0] == 0)
                    {
                        gl.glDeleteProgram(programHandle);
                        programHandle = 0;
                    }
                }

                if (programHandle == 0)
                {
                    throw null;
                    //throw new RuntimeException("Error creating program.");
                }

                // Set program handles. These will later be used to pass in values to the program.
                mMVPMatrixHandle = gl.glGetUniformLocation(programHandle, "u_MVPMatrix");
                mPositionHandle = gl.glGetAttribLocation(programHandle, "a_Position");
                mColorHandle = gl.glGetAttribLocation(programHandle, "a_Color");

                // Tell OpenGL to use this program when rendering.
                gl.glUseProgram(programHandle);
            }

            public void onSurfaceChanged(GL10 glUnused, int width, int height)
            {
                // Set the OpenGL viewport to the same size as the surface.
                gl.glViewport(0, 0, width, height);

                // Create a new perspective projection matrix. The height will stay the same
                // while the width will vary as per aspect ratio.
                float ratio = (float)width / height;
                float left = -ratio;
                float right = ratio;
                float bottom = -1.0f;
                float top = 1.0f;
                float near = 1.0f;
                float far = 10.0f;

                Matrix.frustumM(mProjectionMatrix, 0, left, right, bottom, top, near, far);
            }

            public void onDrawFrame(GL10 glUnused)
            {
                gl.glClear(gl.GL_DEPTH_BUFFER_BIT | gl.GL_COLOR_BUFFER_BIT);

                // Do a complete rotation every 10 seconds.
                long time = SystemClock.uptimeMillis() % 10000L;
                float angleInDegrees = (360.0f / 10000.0f) * ((int)time);

                // Draw the triangle facing straight on.
                Matrix.setIdentityM(mModelMatrix, 0);
                Matrix.rotateM(mModelMatrix, 0, angleInDegrees, 0.0f, 0.0f, 1.0f);
                drawTriangle(mTriangle1Vertices);

                // Draw one translated a bit down and rotated to be flat on the ground.
                Matrix.setIdentityM(mModelMatrix, 0);
                Matrix.translateM(mModelMatrix, 0, 0.0f, -1.0f, 0.0f);
                Matrix.rotateM(mModelMatrix, 0, 90.0f, 1.0f, 0.0f, 0.0f);
                Matrix.rotateM(mModelMatrix, 0, angleInDegrees, 0.0f, 0.0f, 1.0f);
                drawTriangle(mTriangle2Vertices);

                // Draw one translated a bit to the right and rotated to be facing to the left.
                Matrix.setIdentityM(mModelMatrix, 0);
                Matrix.translateM(mModelMatrix, 0, 1.0f, 0.0f, 0.0f);
                Matrix.rotateM(mModelMatrix, 0, 90.0f, 0.0f, 1.0f, 0.0f);
                Matrix.rotateM(mModelMatrix, 0, angleInDegrees, 0.0f, 0.0f, 1.0f);
                drawTriangle(mTriangle3Vertices);
            }

            /**
             * Draws a triangle from the given vertex data.
             * 
             * @param aTriangleBuffer The buffer containing the vertex data.
             */
            private void drawTriangle(FloatBuffer aTriangleBuffer)
            {
                // Pass in the position information
                aTriangleBuffer.position(mPositionOffset);
                gl.glVertexAttribPointer(mPositionHandle, mPositionDataSize, gl.GL_FLOAT, false,
                        mStrideBytes, aTriangleBuffer);

                gl.glEnableVertexAttribArray(mPositionHandle);

                // Pass in the color information
                aTriangleBuffer.position(mColorOffset);
                gl.glVertexAttribPointer(mColorHandle, mColorDataSize, gl.GL_FLOAT, false,
                        mStrideBytes, aTriangleBuffer);

                gl.glEnableVertexAttribArray(mColorHandle);

                // This multiplies the view matrix by the model matrix, and stores the result in the MVP matrix
                // (which currently contains model * view).
                Matrix.multiplyMM(mMVPMatrix, 0, mViewMatrix, 0, mModelMatrix, 0);

                // This multiplies the modelview matrix by the projection matrix, and stores the result in the MVP matrix
                // (which now contains model * view * projection).
                Matrix.multiplyMM(mMVPMatrix, 0, mProjectionMatrix, 0, mMVPMatrix, 0);

                gl.glUniformMatrix4fv(mMVPMatrixHandle, 1, false, mMVPMatrix, 0);
                gl.glDrawArrays(gl.GL_TRIANGLES, 0, 3);
            }



        }
    }



}
