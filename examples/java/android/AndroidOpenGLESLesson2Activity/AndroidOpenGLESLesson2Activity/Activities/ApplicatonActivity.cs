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
using AndroidOpenGLESLesson2Activity.Library;
using java.lang;
using java.nio;
using javax.microedition.khronos.egl;
using javax.microedition.khronos.opengles;
using ScriptCoreLib;

namespace AndroidOpenGLESLesson2Activity.Activities
{
    using gl = GLES20;

    [Script]
    public class AndroidOpenGLESLesson2Activity : Activity
    {
        // port from http://www.learnopengles.com/android-lesson-two-ambient-and-diffuse-lighting/
        // Y:\opensource\github\Learn-OpenGLES-Tutorials\android\AndroidOpenGLESLessons\src\com\learnopengles\android\lesson2


        // C:\util\android-sdk-windows\tools\android.bat create project --package AndroidOpenGLESLesson2Activity.Activities --activity AndroidOpenGLESLesson2Activity  --target 2  --path y:\jsc.svn\examples\java\android\AndroidOpenGLESLesson2Activity\AndroidOpenGLESLesson2Activity\staging

        // http://developer.android.com/guide/developing/device.html#setting-up
        // Caution: OpenGL ES 2.0 is currently not supported by the Android Emulator. You must have a physical test device running Android 2.2 (API Level 8) or higher in order to run and test the example code in this tutorial.

        // note: rebuild could auto reinstall

        // running it on device:
        // attach device to usb
        // C:\util\android-sdk-windows\platform-tools\adb.exe devices
        //List of devices attached
        //3330A17632C000EC        device 

        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r "y:\jsc.svn\examples\java\android\AndroidOpenGLESLesson2Activity\AndroidOpenGLESLesson2Activity\staging\bin\AndroidOpenGLESLesson2Activity-debug.apk"

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
                mGLSurfaceView.setRenderer(new LessonTwoRenderer());
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
        class LessonTwoRenderer : GLSurfaceView.Renderer
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

            /** 
             * Stores a copy of the model matrix specifically for the light position.
             */
            private float[] mLightModelMatrix = new float[16];

            /** Store our model data in a float buffer. */
            private FloatBuffer mCubePositions;
            private FloatBuffer mCubeColors;
            private FloatBuffer mCubeNormals;

            /** This will be used to pass in the transformation matrix. */
            private int mMVPMatrixHandle;

            /** This will be used to pass in the modelview matrix. */
            private int mMVMatrixHandle;

            /** This will be used to pass in the light position. */
            private int mLightPosHandle;

            /** This will be used to pass in model position information. */
            private int mPositionHandle;

            /** This will be used to pass in model color information. */
            private int mColorHandle;

            /** This will be used to pass in model normal information. */
            private int mNormalHandle;

            /** How many bytes per float. */
            private int mBytesPerFloat = 4;

            /** Size of the position data in elements. */
            private int mPositionDataSize = 3;

            /** Size of the color data in elements. */
            private int mColorDataSize = 4;

            /** Size of the normal data in elements. */
            private int mNormalDataSize = 3;

            /** Used to hold a light centered on the origin in model space. We need a 4th coordinate so we can get translations to work when
             *  we multiply this by our transformation matrices. */
            private float[] mLightPosInModelSpace = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };

            /** Used to hold the current position of the light in world space (after transformation via model matrix). */
            private float[] mLightPosInWorldSpace = new float[4];

            /** Used to hold the transformed position of the light in eye space (after transformation via modelview matrix) */
            private float[] mLightPosInEyeSpace = new float[4];

            /** This is a handle to our per-vertex cube shading program. */
            private int mPerVertexProgramHandle;

            /** This is a handle to our light point program. */
            private int mPointProgramHandle;

            /**
             * Initialize the model data.
             */
            public LessonTwoRenderer()
            {
                // Define points for a cube.		

                // X, Y, Z
                float[] cubePositionData =
		{
				// In OpenGL counter-clockwise winding is default. This means that when we look at a triangle, 
				// if the points are counter-clockwise we are looking at the "front". If not we are looking at
				// the back. OpenGL has an optimization where all back-facing triangles are culled, since they
				// usually represent the backside of an object and aren't visible anyways.
				
				// Front face
				-1.0f, 1.0f, 1.0f,				
				-1.0f, -1.0f, 1.0f,
				1.0f, 1.0f, 1.0f, 
				-1.0f, -1.0f, 1.0f, 				
				1.0f, -1.0f, 1.0f,
				1.0f, 1.0f, 1.0f,
				
				// Right face
				1.0f, 1.0f, 1.0f,				
				1.0f, -1.0f, 1.0f,
				1.0f, 1.0f, -1.0f,
				1.0f, -1.0f, 1.0f,				
				1.0f, -1.0f, -1.0f,
				1.0f, 1.0f, -1.0f,
				
				// Back face
				1.0f, 1.0f, -1.0f,				
				1.0f, -1.0f, -1.0f,
				-1.0f, 1.0f, -1.0f,
				1.0f, -1.0f, -1.0f,				
				-1.0f, -1.0f, -1.0f,
				-1.0f, 1.0f, -1.0f,
				
				// Left face
				-1.0f, 1.0f, -1.0f,				
				-1.0f, -1.0f, -1.0f,
				-1.0f, 1.0f, 1.0f, 
				-1.0f, -1.0f, -1.0f,				
				-1.0f, -1.0f, 1.0f, 
				-1.0f, 1.0f, 1.0f, 
				
				// Top face
				-1.0f, 1.0f, -1.0f,				
				-1.0f, 1.0f, 1.0f, 
				1.0f, 1.0f, -1.0f, 
				-1.0f, 1.0f, 1.0f, 				
				1.0f, 1.0f, 1.0f, 
				1.0f, 1.0f, -1.0f,
				
				// Bottom face
				1.0f, -1.0f, -1.0f,				
				1.0f, -1.0f, 1.0f, 
				-1.0f, -1.0f, -1.0f,
				1.0f, -1.0f, 1.0f, 				
				-1.0f, -1.0f, 1.0f,
				-1.0f, -1.0f, -1.0f,
		};

                // R, G, B, A
                float[] cubeColorData =
		{				
				// Front face (red)
				1.0f, 0.0f, 0.0f, 1.0f,				
				1.0f, 0.0f, 0.0f, 1.0f,
				1.0f, 0.0f, 0.0f, 1.0f,
				1.0f, 0.0f, 0.0f, 1.0f,				
				1.0f, 0.0f, 0.0f, 1.0f,
				1.0f, 0.0f, 0.0f, 1.0f,
				
				// Right face (green)
				0.0f, 1.0f, 0.0f, 1.0f,				
				0.0f, 1.0f, 0.0f, 1.0f,
				0.0f, 1.0f, 0.0f, 1.0f,
				0.0f, 1.0f, 0.0f, 1.0f,				
				0.0f, 1.0f, 0.0f, 1.0f,
				0.0f, 1.0f, 0.0f, 1.0f,
				
				// Back face (blue)
				0.0f, 0.0f, 1.0f, 1.0f,				
				0.0f, 0.0f, 1.0f, 1.0f,
				0.0f, 0.0f, 1.0f, 1.0f,
				0.0f, 0.0f, 1.0f, 1.0f,				
				0.0f, 0.0f, 1.0f, 1.0f,
				0.0f, 0.0f, 1.0f, 1.0f,
				
				// Left face (yellow)
				1.0f, 1.0f, 0.0f, 1.0f,				
				1.0f, 1.0f, 0.0f, 1.0f,
				1.0f, 1.0f, 0.0f, 1.0f,
				1.0f, 1.0f, 0.0f, 1.0f,				
				1.0f, 1.0f, 0.0f, 1.0f,
				1.0f, 1.0f, 0.0f, 1.0f,
				
				// Top face (cyan)
				0.0f, 1.0f, 1.0f, 1.0f,				
				0.0f, 1.0f, 1.0f, 1.0f,
				0.0f, 1.0f, 1.0f, 1.0f,
				0.0f, 1.0f, 1.0f, 1.0f,				
				0.0f, 1.0f, 1.0f, 1.0f,
				0.0f, 1.0f, 1.0f, 1.0f,
				
				// Bottom face (magenta)
				1.0f, 0.0f, 1.0f, 1.0f,				
				1.0f, 0.0f, 1.0f, 1.0f,
				1.0f, 0.0f, 1.0f, 1.0f,
				1.0f, 0.0f, 1.0f, 1.0f,				
				1.0f, 0.0f, 1.0f, 1.0f,
				1.0f, 0.0f, 1.0f, 1.0f
		};

                // X, Y, Z
                // The normal is used in light calculations and is a vector which points
                // orthogonal to the plane of the surface. For a cube model, the normals
                // should be orthogonal to the points of each face.
                float[] cubeNormalData =
		{												
				// Front face
				0.0f, 0.0f, 1.0f,				
				0.0f, 0.0f, 1.0f,
				0.0f, 0.0f, 1.0f,
				0.0f, 0.0f, 1.0f,				
				0.0f, 0.0f, 1.0f,
				0.0f, 0.0f, 1.0f,
				
				// Right face 
				1.0f, 0.0f, 0.0f,				
				1.0f, 0.0f, 0.0f,
				1.0f, 0.0f, 0.0f,
				1.0f, 0.0f, 0.0f,				
				1.0f, 0.0f, 0.0f,
				1.0f, 0.0f, 0.0f,
				
				// Back face 
				0.0f, 0.0f, -1.0f,				
				0.0f, 0.0f, -1.0f,
				0.0f, 0.0f, -1.0f,
				0.0f, 0.0f, -1.0f,				
				0.0f, 0.0f, -1.0f,
				0.0f, 0.0f, -1.0f,
				
				// Left face 
				-1.0f, 0.0f, 0.0f,				
				-1.0f, 0.0f, 0.0f,
				-1.0f, 0.0f, 0.0f,
				-1.0f, 0.0f, 0.0f,				
				-1.0f, 0.0f, 0.0f,
				-1.0f, 0.0f, 0.0f,
				
				// Top face 
				0.0f, 1.0f, 0.0f,			
				0.0f, 1.0f, 0.0f,
				0.0f, 1.0f, 0.0f,
				0.0f, 1.0f, 0.0f,				
				0.0f, 1.0f, 0.0f,
				0.0f, 1.0f, 0.0f,
				
				// Bottom face 
				0.0f, -1.0f, 0.0f,			
				0.0f, -1.0f, 0.0f,
				0.0f, -1.0f, 0.0f,
				0.0f, -1.0f, 0.0f,				
				0.0f, -1.0f, 0.0f,
				0.0f, -1.0f, 0.0f
		};

                // Initialize the buffers.
                mCubePositions = ByteBuffer.allocateDirect(cubePositionData.Length * mBytesPerFloat)
                .order(ByteOrder.nativeOrder()).asFloatBuffer();
                mCubePositions.put(cubePositionData).position(0);

                mCubeColors = ByteBuffer.allocateDirect(cubeColorData.Length * mBytesPerFloat)
                .order(ByteOrder.nativeOrder()).asFloatBuffer();
                mCubeColors.put(cubeColorData).position(0);

                mCubeNormals = ByteBuffer.allocateDirect(cubeNormalData.Length * mBytesPerFloat)
                .order(ByteOrder.nativeOrder()).asFloatBuffer();
                mCubeNormals.put(cubeNormalData).position(0);
            }

            protected String getVertexShader()
            {
                // TODO: Explain why we normalize the vectors, explain some of the vector math behind it all. Explain what is eye space.
                var vertexShader =
                    "uniform mat4 u_MVPMatrix;      \n"		// A constant representing the combined model/view/projection matrix.
                  + "uniform mat4 u_MVMatrix;       \n"		// A constant representing the combined model/view matrix.	
                  + "uniform vec3 u_LightPos;       \n"	    // The position of the light in eye space.

                  + "attribute vec4 a_Position;     \n"		// Per-vertex position information we will pass in.
                  + "attribute vec4 a_Color;        \n"		// Per-vertex color information we will pass in.
                  + "attribute vec3 a_Normal;       \n"		// Per-vertex normal information we will pass in.

                  + "varying vec4 v_Color;          \n"		// This will be passed into the fragment shader.

                  + "void main()                    \n" 	// The entry point for our vertex shader.
                  + "{                              \n"
                    // Transform the vertex into eye space.
                  + "   vec3 modelViewVertex = vec3(u_MVMatrix * a_Position);              \n"
                    // Transform the normal's orientation into eye space.
                  + "   vec3 modelViewNormal = vec3(u_MVMatrix * vec4(a_Normal, 0.0));     \n"
                    // Will be used for attenuation.
                  + "   float distance = length(u_LightPos - modelViewVertex);             \n"
                    // Get a lighting direction vector from the light to the vertex.
                  + "   vec3 lightVector = normalize(u_LightPos - modelViewVertex);        \n"
                    // Calculate the dot product of the light vector and vertex normal. If the normal and light vector are
                    // pointing in the same direction then it will get max illumination.
                  + "   float diffuse = max(dot(modelViewNormal, lightVector), 0.1);       \n"
                    // Attenuate the light based on distance.
                  + "   diffuse = diffuse * (1.0 / (1.0 + (0.25 * distance * distance)));  \n"
                    // Multiply the color by the illumination level. It will be interpolated across the triangle.
                  + "   v_Color = a_Color * diffuse;                                       \n"
                    // gl_Position is a special variable used to store the final position.
                    // Multiply the vertex by the matrix to get the final point in normalized screen coordinates.		
                  + "   gl_Position = u_MVPMatrix * a_Position;                            \n"
                  + "}                                                                     \n";

                return vertexShader;
            }

            protected string getFragmentShader()
            {
                var fragmentShader =
                    "precision mediump float;       \n"		// Set the default precision to medium. We don't need as high of a 
                    // precision in the fragment shader.				
                  + "varying vec4 v_Color;          \n"		// This is the color from the vertex shader interpolated across the 
                    // triangle per fragment.			  
                  + "void main()                    \n"		// The entry point for our fragment shader.
                  + "{                              \n"
                  + "   gl_FragColor = v_Color;     \n"		// Pass the color directly through the pipeline.		  
                  + "}                              \n";

                return fragmentShader;
            }

            public void onSurfaceCreated(GL10 glUnused, EGLConfig config)
            {
                // Set the background clear color to black.
                GLES20.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);

                // Use culling to remove back faces.
                GLES20.glEnable(GLES20.GL_CULL_FACE);

                // Enable depth testing
                GLES20.glEnable(GLES20.GL_DEPTH_TEST);

                // Position the eye in front of the origin.
                float eyeX = 0.0f;
                float eyeY = 0.0f;
                float eyeZ = -0.5f;

                // We are looking toward the distance
                float lookX = 0.0f;
                float lookY = 0.0f;
                float lookZ = -5.0f;

                // Set our up vector. This is where our head would be pointing were we holding the camera.
                float upX = 0.0f;
                float upY = 1.0f;
                float upZ = 0.0f;

                // Set the view matrix. This matrix can be said to represent the camera position.
                // NOTE: In OpenGL 1, a ModelView matrix is used, which is a combination of a model and
                // view matrix. In OpenGL 2, we can keep track of these matrices separately if we choose.
                Matrix.setLookAtM(mViewMatrix, 0, eyeX, eyeY, eyeZ, lookX, lookY, lookZ, upX, upY, upZ);

                string vertexShader = getVertexShader();
                string fragmentShader = getFragmentShader();

                int vertexShaderHandle = compileShader(GLES20.GL_VERTEX_SHADER, vertexShader);
                int fragmentShaderHandle = compileShader(GLES20.GL_FRAGMENT_SHADER, fragmentShader);

                mPerVertexProgramHandle = createAndLinkProgram(vertexShaderHandle, fragmentShaderHandle,
                        new String[] { "a_Position", "a_Color", "a_Normal" });

                // Define a simple shader program for our point.
                var pointVertexShader =
                    "uniform mat4 u_MVPMatrix;      \n"
                  + "attribute vec4 a_Position;     \n"
                  + "void main()                    \n"
                  + "{                              \n"
                  + "   gl_Position = u_MVPMatrix   \n"
                  + "               * a_Position;   \n"
                  + "   gl_PointSize = 5.0;         \n"
                  + "}                              \n";

                var pointFragmentShader =
                    "precision mediump float;       \n"
                  + "void main()                    \n"
                  + "{                              \n"
                  + "   gl_FragColor = vec4(1.0,    \n"
                  + "   1.0, 1.0, 1.0);             \n"
                  + "}                              \n";

                int pointVertexShaderHandle = compileShader(GLES20.GL_VERTEX_SHADER, pointVertexShader);
                int pointFragmentShaderHandle = compileShader(GLES20.GL_FRAGMENT_SHADER, pointFragmentShader);
                mPointProgramHandle = createAndLinkProgram(pointVertexShaderHandle, pointFragmentShaderHandle,
                        new String[] { "a_Position" });
            }

            public void onSurfaceChanged(GL10 glUnused, int width, int height)
            {
                // Set the OpenGL viewport to the same size as the surface.
                GLES20.glViewport(0, 0, width, height);

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
                GLES20.glClear(GLES20.GL_COLOR_BUFFER_BIT | GLES20.GL_DEPTH_BUFFER_BIT);

                // Do a complete rotation every 10 seconds.
                long time = SystemClock.uptimeMillis() % 10000L;
                float angleInDegrees = (360.0f / 10000.0f) * ((int)time);

                // Set our per-vertex lighting program.
                GLES20.glUseProgram(mPerVertexProgramHandle);

                // Set program handles for cube drawing.
                mMVPMatrixHandle = GLES20.glGetUniformLocation(mPerVertexProgramHandle, "u_MVPMatrix");
                mMVMatrixHandle = GLES20.glGetUniformLocation(mPerVertexProgramHandle, "u_MVMatrix");
                mLightPosHandle = GLES20.glGetUniformLocation(mPerVertexProgramHandle, "u_LightPos");
                mPositionHandle = GLES20.glGetAttribLocation(mPerVertexProgramHandle, "a_Position");
                mColorHandle = GLES20.glGetAttribLocation(mPerVertexProgramHandle, "a_Color");
                mNormalHandle = GLES20.glGetAttribLocation(mPerVertexProgramHandle, "a_Normal");

                // Calculate position of the light. Rotate and then push into the distance.
                Matrix.setIdentityM(mLightModelMatrix, 0);
                Matrix.translateM(mLightModelMatrix, 0, 0.0f, 0.0f, -5.0f);
                Matrix.rotateM(mLightModelMatrix, 0, angleInDegrees, 0.0f, 1.0f, 0.0f);
                Matrix.translateM(mLightModelMatrix, 0, 0.0f, 0.0f, 2.0f);

                Matrix.multiplyMV(mLightPosInWorldSpace, 0, mLightModelMatrix, 0, mLightPosInModelSpace, 0);
                Matrix.multiplyMV(mLightPosInEyeSpace, 0, mViewMatrix, 0, mLightPosInWorldSpace, 0);

                // Draw some cubes.        
                Matrix.setIdentityM(mModelMatrix, 0);
                Matrix.translateM(mModelMatrix, 0, 4.0f, 0.0f, -7.0f);
                Matrix.rotateM(mModelMatrix, 0, angleInDegrees, 1.0f, 0.0f, 0.0f);
                drawCube();

                Matrix.setIdentityM(mModelMatrix, 0);
                Matrix.translateM(mModelMatrix, 0, -4.0f, 0.0f, -7.0f);
                Matrix.rotateM(mModelMatrix, 0, angleInDegrees, 0.0f, 1.0f, 0.0f);
                drawCube();

                Matrix.setIdentityM(mModelMatrix, 0);
                Matrix.translateM(mModelMatrix, 0, 0.0f, 4.0f, -7.0f);
                Matrix.rotateM(mModelMatrix, 0, angleInDegrees, 0.0f, 0.0f, 1.0f);
                drawCube();

                Matrix.setIdentityM(mModelMatrix, 0);
                Matrix.translateM(mModelMatrix, 0, 0.0f, -4.0f, -7.0f);
                drawCube();

                Matrix.setIdentityM(mModelMatrix, 0);
                Matrix.translateM(mModelMatrix, 0, 0.0f, 0.0f, -5.0f);
                Matrix.rotateM(mModelMatrix, 0, angleInDegrees, 1.0f, 1.0f, 0.0f);
                drawCube();

                // Draw a point to indicate the light.
                GLES20.glUseProgram(mPointProgramHandle);
                drawLight();
            }

            /**
             * Draws a cube.
             */
            private void drawCube()
            {
                // Pass in the position information
                mCubePositions.position(0);
                GLES20.glVertexAttribPointer(mPositionHandle, mPositionDataSize, GLES20.GL_FLOAT, false,
                        0, mCubePositions);

                GLES20.glEnableVertexAttribArray(mPositionHandle);

                // Pass in the color information
                mCubeColors.position(0);
                GLES20.glVertexAttribPointer(mColorHandle, mColorDataSize, GLES20.GL_FLOAT, false,
                        0, mCubeColors);

                GLES20.glEnableVertexAttribArray(mColorHandle);

                // Pass in the normal information
                mCubeNormals.position(0);
                GLES20.glVertexAttribPointer(mNormalHandle, mNormalDataSize, GLES20.GL_FLOAT, false,
                        0, mCubeNormals);

                GLES20.glEnableVertexAttribArray(mNormalHandle);

                // This multiplies the view matrix by the model matrix, and stores the result in the MVP matrix
                // (which currently contains model * view).
                Matrix.multiplyMM(mMVPMatrix, 0, mViewMatrix, 0, mModelMatrix, 0);

                // Pass in the modelview matrix.
                GLES20.glUniformMatrix4fv(mMVMatrixHandle, 1, false, mMVPMatrix, 0);

                // This multiplies the modelview matrix by the projection matrix, and stores the result in the MVP matrix
                // (which now contains model * view * projection).
                Matrix.multiplyMM(mMVPMatrix, 0, mProjectionMatrix, 0, mMVPMatrix, 0);

                // Pass in the combined matrix.
                GLES20.glUniformMatrix4fv(mMVPMatrixHandle, 1, false, mMVPMatrix, 0);

                // Pass in the light position in eye space.        
                GLES20.glUniform3f(mLightPosHandle, mLightPosInEyeSpace[0], mLightPosInEyeSpace[1], mLightPosInEyeSpace[2]);

                // Draw the cube.
                GLES20.glDrawArrays(GLES20.GL_TRIANGLES, 0, 36);
            }

            /**
             * Draws a point representing the position of the light.
             */
            private void drawLight()
            {
                int pointMVPMatrixHandle = GLES20.glGetUniformLocation(mPointProgramHandle, "u_MVPMatrix");
                int pointPositionHandle = GLES20.glGetAttribLocation(mPointProgramHandle, "a_Position");

                // Pass in the position.
                GLES20.glVertexAttrib3f(pointPositionHandle, mLightPosInModelSpace[0], mLightPosInModelSpace[1], mLightPosInModelSpace[2]);

                // Since we are not using a buffer object, disable vertex arrays for this attribute.
                GLES20.glDisableVertexAttribArray(pointPositionHandle);

                // Pass in the transformation matrix.
                Matrix.multiplyMM(mMVPMatrix, 0, mViewMatrix, 0, mLightModelMatrix, 0);
                Matrix.multiplyMM(mMVPMatrix, 0, mProjectionMatrix, 0, mMVPMatrix, 0);
                GLES20.glUniformMatrix4fv(pointMVPMatrixHandle, 1, false, mMVPMatrix, 0);

                // Draw the point.
                GLES20.glDrawArrays(GLES20.GL_POINTS, 0, 1);
            }

            /** 
             * Helper function to compile a shader.
             * 
             * @param shaderType The shader type.
             * @param shaderSource The shader source code.
             * @return An OpenGL handle to the shader.
             */
            private int compileShader(int shaderType, string shaderSource)
            {
                int shaderHandle = GLES20.glCreateShader(shaderType);

                if (shaderHandle != 0)
                {
                    // Pass in the shader source.
                    GLES20.glShaderSource(shaderHandle, shaderSource);

                    // Compile the shader.
                    GLES20.glCompileShader(shaderHandle);

                    // Get the compilation status.
                    var compileStatus = new int[1];
                    GLES20.glGetShaderiv(shaderHandle, GLES20.GL_COMPILE_STATUS, compileStatus, 0);

                    // If the compilation failed, delete the shader.
                    if (compileStatus[0] == 0)
                    {
                        //Log.e(TAG, "Error compiling shader: " + GLES20.glGetShaderInfoLog(shaderHandle));
                        GLES20.glDeleteShader(shaderHandle);
                        shaderHandle = 0;
                    }
                }

                if (shaderHandle == 0)
                {
                    throw null;
                    //throw new RuntimeException("Error creating shader.");
                }

                return shaderHandle;
            }

            /**
             * Helper function to compile and link a program.
             * 
             * @param vertexShaderHandle An OpenGL handle to an already-compiled vertex shader.
             * @param fragmentShaderHandle An OpenGL handle to an already-compiled fragment shader.
             * @param attributes Attributes that need to be bound to the program.
             * @return An OpenGL handle to the program.
             */
            private int createAndLinkProgram(int vertexShaderHandle, int fragmentShaderHandle, string[] attributes)
            {
                int programHandle = GLES20.glCreateProgram();

                if (programHandle != 0)
                {
                    // Bind the vertex shader to the program.
                    GLES20.glAttachShader(programHandle, vertexShaderHandle);

                    // Bind the fragment shader to the program.
                    GLES20.glAttachShader(programHandle, fragmentShaderHandle);

                    // Bind attributes
                    if (attributes != null)
                    {
                        int size = attributes.Length;
                        for (int i = 0; i < size; i++)
                        {
                            GLES20.glBindAttribLocation(programHandle, i, attributes[i]);
                        }
                    }

                    // Link the two shaders together into a program.
                    GLES20.glLinkProgram(programHandle);

                    // Get the link status.
                    var linkStatus = new int[1];
                    GLES20.glGetProgramiv(programHandle, GLES20.GL_LINK_STATUS, linkStatus, 0);

                    // If the link failed, delete the program.
                    if (linkStatus[0] == 0)
                    {
                        //Log.e(TAG, "Error compiling program: " + GLES20.glGetProgramInfoLog(programHandle));
                        GLES20.glDeleteProgram(programHandle);
                        programHandle = 0;
                    }
                }

                if (programHandle == 0)
                {
                    throw null;
                    //throw new RuntimeException("Error creating program.");
                }

                return programHandle;
            }

        }
    }



}
