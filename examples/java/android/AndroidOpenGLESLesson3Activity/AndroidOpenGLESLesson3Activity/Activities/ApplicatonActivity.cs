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
using AndroidOpenGLESLesson3Activity.Library;
using java.lang;
using java.nio;
using javax.microedition.khronos.egl;
using javax.microedition.khronos.opengles;
using ScriptCoreLib;

namespace AndroidOpenGLESLesson3Activity.Activities
{
    //using WebGLRenderingContext = GLES20; 
    using gl = GLES20;

    [Script]
    public class AndroidOpenGLESLesson3Activity : Activity
    {
        // port from http://www.learnopengles.com/android-lesson-three-moving-to-per-fragment-lighting/
        // Y:\opensource\github\Learn-OpenGLES-Tutorials\android\AndroidOpenGLESLessons\src\com\learnopengles\android\lesson2


        // C:\util\android-sdk-windows\tools\android.bat create project --package AndroidOpenGLESLesson3Activity.Activities --activity AndroidOpenGLESLesson3Activity  --target 2  --path y:\jsc.svn\examples\java\android\AndroidOpenGLESLesson3Activity\AndroidOpenGLESLesson3Activity\staging

        // http://developer.android.com/guide/developing/device.html#setting-up
        // running it in emulator:
        // C:\util\android-sdk-windows\tools\android.bat avd

        // note: rebuild could auto reinstall

        // running it on device:
        // attach device to usb
        // C:\util\android-sdk-windows\platform-tools\adb.exe devices
        //List of devices attached
        //3330A17632C000EC        device 

        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r "y:\jsc.svn\examples\java\android\AndroidOpenGLESLesson3Activity\AndroidOpenGLESLesson3Activity\staging\bin\AndroidOpenGLESLesson3Activity-debug.apk"

        // screenshot: home+back
        // at "F:\ScreenCapture\SC20120504-153450.png"

        // http://android-ui-utils.googlecode.com/hg/asset-studio/dist/icons-launcher.html#foreground.type=image&foreground.space.trim=0&foreground.space.pad=-0.1&crop=1&backgroundShape=none&backColor=ff0000%2C100&foreColor=000000%2C0


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
                mGLSurfaceView.setRenderer(new LessonThreeRenderer());
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
        class LessonThreeRenderer : GLSurfaceView.Renderer
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
            public LessonThreeRenderer()
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

                  + "attribute vec4 a_Position;     \n"		// Per-vertex position information we will pass in.
                  + "attribute vec4 a_Color;        \n"		// Per-vertex color information we will pass in.
                  + "attribute vec3 a_Normal;       \n"		// Per-vertex normal information we will pass in.

                  + "varying vec3 v_Position;       \n"		// This will be passed into the fragment shader.
                  + "varying vec4 v_Color;          \n"		// This will be passed into the fragment shader.
                  + "varying vec3 v_Normal;         \n"		// This will be passed into the fragment shader.

                // The entry point for our vertex shader.  
                  + "void main()                                                \n"
                  + "{                                                          \n"
                            // Transform the vertex into eye space.
                  + "   v_Position = vec3(u_MVMatrix * a_Position);             \n"
                            // Pass through the color.
                  + "   v_Color = a_Color;                                      \n"
                            // Transform the normal's orientation into eye space.
                  + "   v_Normal = vec3(u_MVMatrix * vec4(a_Normal, 0.0));      \n"
                            // gl_Position is a special variable used to store the final position.
                            // Multiply the vertex by the matrix to get the final point in normalized screen coordinates.
                  + "   gl_Position = u_MVPMatrix * a_Position;                 \n"
                  + "}                                                          \n";     

                return vertexShader;
            }

            protected string getFragmentShader()
            {
                var fragmentShader =
	                "precision mediump float;       \n"		// Set the default precision to medium. We don't need as high of a 
													        // precision in the fragment shader.
		          + "uniform vec3 u_LightPos;       \n"	    // The position of the light in eye space.
		  
		          + "varying vec3 v_Position;		\n"		// Interpolated position for this fragment.
		          + "varying vec4 v_Color;          \n"		// This is the color from the vertex shader interpolated across the 
		  											        // triangle per fragment.
		          + "varying vec3 v_Normal;         \n"		// Interpolated normal for this fragment.
		  
		        // The entry point for our fragment shader.
		          + "void main()                    \n"		
		          + "{                              \n"
		        // Will be used for attenuation.
		          + "   float distance = length(u_LightPos - v_Position);                  \n"
		        // Get a lighting direction vector from the light to the vertex.
		          + "   vec3 lightVector = normalize(u_LightPos - v_Position);             \n" 	
		        // Calculate the dot product of the light vector and vertex normal. If the normal and light vector are
		        // pointing in the same direction then it will get max illumination.
		          + "   float diffuse = max(dot(v_Normal, lightVector), 0.1);              \n" 	  		  													  
		        // Add attenuation. 
		          + "   diffuse = diffuse * (1.0 / (1.0 + (0.25 * distance * distance)));  \n"
		        // Multiply the color by the diffuse illumination level to get final output color.
		          + "   gl_FragColor = v_Color * diffuse;                                  \n"
                  + "}                                                                     \n";	

                return fragmentShader;
            }

            public void onSurfaceCreated(GL10 glUnused, EGLConfig config)
            {
                // Set the background clear color to black.
                gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);

                // Use culling to remove back faces.
                gl.glEnable(gl.GL_CULL_FACE);

                // Enable depth testing
                gl.glEnable(gl.GL_DEPTH_TEST);

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

                int vertexShaderHandle = compileShader(gl.GL_VERTEX_SHADER, vertexShader);
                int fragmentShaderHandle = compileShader(gl.GL_FRAGMENT_SHADER, fragmentShader);

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

                int pointVertexShaderHandle = compileShader(gl.GL_VERTEX_SHADER, pointVertexShader);
                int pointFragmentShaderHandle = compileShader(gl.GL_FRAGMENT_SHADER, pointFragmentShader);
                mPointProgramHandle = createAndLinkProgram(pointVertexShaderHandle, pointFragmentShaderHandle,
                        new String[] { "a_Position" });
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
                gl.glClear(gl.GL_COLOR_BUFFER_BIT | gl.GL_DEPTH_BUFFER_BIT);

                // Do a complete rotation every 10 seconds.
                long time = SystemClock.uptimeMillis() % 10000L;
                float angleInDegrees = (360.0f / 10000.0f) * ((int)time);

                // Set our per-vertex lighting program.
                gl.glUseProgram(mPerVertexProgramHandle);

                // Set program handles for cube drawing.
                mMVPMatrixHandle = gl.glGetUniformLocation(mPerVertexProgramHandle, "u_MVPMatrix");
                mMVMatrixHandle = gl.glGetUniformLocation(mPerVertexProgramHandle, "u_MVMatrix");
                mLightPosHandle = gl.glGetUniformLocation(mPerVertexProgramHandle, "u_LightPos");
                mPositionHandle = gl.glGetAttribLocation(mPerVertexProgramHandle, "a_Position");
                mColorHandle = gl.glGetAttribLocation(mPerVertexProgramHandle, "a_Color");
                mNormalHandle = gl.glGetAttribLocation(mPerVertexProgramHandle, "a_Normal");

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
                gl.glUseProgram(mPointProgramHandle);
                drawLight();
            }

            /**
             * Draws a cube.
             */
            private void drawCube()
            {
                // Pass in the position information
                mCubePositions.position(0);
                gl.glVertexAttribPointer(mPositionHandle, mPositionDataSize, gl.GL_FLOAT, false,
                        0, mCubePositions);

                gl.glEnableVertexAttribArray(mPositionHandle);

                // Pass in the color information
                mCubeColors.position(0);
                gl.glVertexAttribPointer(mColorHandle, mColorDataSize, gl.GL_FLOAT, false,
                        0, mCubeColors);

                gl.glEnableVertexAttribArray(mColorHandle);

                // Pass in the normal information
                mCubeNormals.position(0);
                gl.glVertexAttribPointer(mNormalHandle, mNormalDataSize, gl.GL_FLOAT, false,
                        0, mCubeNormals);

                gl.glEnableVertexAttribArray(mNormalHandle);

                // This multiplies the view matrix by the model matrix, and stores the result in the MVP matrix
                // (which currently contains model * view).
                Matrix.multiplyMM(mMVPMatrix, 0, mViewMatrix, 0, mModelMatrix, 0);

                // Pass in the modelview matrix.
                gl.glUniformMatrix4fv(mMVMatrixHandle, 1, false, mMVPMatrix, 0);

                // This multiplies the modelview matrix by the projection matrix, and stores the result in the MVP matrix
                // (which now contains model * view * projection).
                Matrix.multiplyMM(mMVPMatrix, 0, mProjectionMatrix, 0, mMVPMatrix, 0);

                // Pass in the combined matrix.
                gl.glUniformMatrix4fv(mMVPMatrixHandle, 1, false, mMVPMatrix, 0);

                // Pass in the light position in eye space.        
                gl.glUniform3f(mLightPosHandle, mLightPosInEyeSpace[0], mLightPosInEyeSpace[1], mLightPosInEyeSpace[2]);

                // Draw the cube.
                gl.glDrawArrays(gl.GL_TRIANGLES, 0, 36);
            }

            /**
             * Draws a point representing the position of the light.
             */
            private void drawLight()
            {
                int pointMVPMatrixHandle = gl.glGetUniformLocation(mPointProgramHandle, "u_MVPMatrix");
                int pointPositionHandle = gl.glGetAttribLocation(mPointProgramHandle, "a_Position");

                // Pass in the position.
                gl.glVertexAttrib3f(pointPositionHandle, mLightPosInModelSpace[0], mLightPosInModelSpace[1], mLightPosInModelSpace[2]);

                // Since we are not using a buffer object, disable vertex arrays for this attribute.
                gl.glDisableVertexAttribArray(pointPositionHandle);

                // Pass in the transformation matrix.
                Matrix.multiplyMM(mMVPMatrix, 0, mViewMatrix, 0, mLightModelMatrix, 0);
                Matrix.multiplyMM(mMVPMatrix, 0, mProjectionMatrix, 0, mMVPMatrix, 0);
                gl.glUniformMatrix4fv(pointMVPMatrixHandle, 1, false, mMVPMatrix, 0);

                // Draw the point.
                gl.glDrawArrays(gl.GL_POINTS, 0, 1);
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
                int shaderHandle = gl.glCreateShader(shaderType);

                if (shaderHandle != 0)
                {
                    // Pass in the shader source.
                    gl.glShaderSource(shaderHandle, shaderSource);

                    // Compile the shader.
                    gl.glCompileShader(shaderHandle);

                    // Get the compilation status.
                    var compileStatus = new int[1];
                    gl.glGetShaderiv(shaderHandle, gl.GL_COMPILE_STATUS, compileStatus, 0);

                    // If the compilation failed, delete the shader.
                    if (compileStatus[0] == 0)
                    {
                        //Log.e(TAG, "Error compiling shader: " + gl.glGetShaderInfoLog(shaderHandle));
                        gl.glDeleteShader(shaderHandle);
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
                int programHandle = gl.glCreateProgram();

                if (programHandle != 0)
                {
                    // Bind the vertex shader to the program.
                    gl.glAttachShader(programHandle, vertexShaderHandle);

                    // Bind the fragment shader to the program.
                    gl.glAttachShader(programHandle, fragmentShaderHandle);

                    // Bind attributes
                    if (attributes != null)
                    {
                        int size = attributes.Length;
                        for (int i = 0; i < size; i++)
                        {
                            gl.glBindAttribLocation(programHandle, i, attributes[i]);
                        }
                    }

                    // Link the two shaders together into a program.
                    gl.glLinkProgram(programHandle);

                    // Get the link status.
                    var linkStatus = new int[1];
                    gl.glGetProgramiv(programHandle, gl.GL_LINK_STATUS, linkStatus, 0);

                    // If the link failed, delete the program.
                    if (linkStatus[0] == 0)
                    {
                        //Log.e(TAG, "Error compiling program: " + gl.glGetProgramInfoLog(programHandle));
                        gl.glDeleteProgram(programHandle);
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
