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
using AndroidOpenGLESLesson4Activity.Library;
using java.lang;
using java.nio;
using javax.microedition.khronos.egl;
using javax.microedition.khronos.opengles;
using ScriptCoreLib;

namespace AndroidOpenGLESLesson4Activity.Activities
{
    //using WebGLRenderingContext = GLES20; 
    using gl = GLES20;

    [Script]
    public class AndroidOpenGLESLesson4Activity : Activity
    {
        // port from http://www.learnopengles.com/android-lesson-four-introducing-basic-texturing/
        // Y:\opensource\github\Learn-OpenGLES-Tutorials\android\AndroidOpenGLESLessons\src\com\learnopengles\android\lesson4


        // C:\util\android-sdk-windows\tools\android.bat create project --package AndroidOpenGLESLesson4Activity.Activities --activity AndroidOpenGLESLesson4Activity  --target 2  --path y:\jsc.svn\examples\java\android\AndroidOpenGLESLesson4Activity\AndroidOpenGLESLesson4Activity\staging

        // http://developer.android.com/guide/developing/device.html#setting-up
        // running it in emulator:
        // C:\util\android-sdk-windows\tools\android.bat avd

        // note: rebuild could auto reinstall

        // running it on device:
        // attach device to usb
        // C:\util\android-sdk-windows\platform-tools\adb.exe devices
        //List of devices attached
        //3330A17632C000EC        device 

        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r "y:\jsc.svn\examples\java\android\AndroidOpenGLESLesson4Activity\AndroidOpenGLESLesson4Activity\staging\bin\AndroidOpenGLESLesson4Activity-debug.apk"

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
                mGLSurfaceView.setRenderer(new LessonFourRenderer(this));
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
        class LessonFourRenderer : GLSurfaceView.Renderer
        {

            private Context mActivityContext;

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
            private FloatBuffer mCubeTextureCoordinates;

            /** This will be used to pass in the transformation matrix. */
            private int mMVPMatrixHandle;

            /** This will be used to pass in the modelview matrix. */
            private int mMVMatrixHandle;

            /** This will be used to pass in the light position. */
            private int mLightPosHandle;

            /** This will be used to pass in the texture. */
            private int mTextureUniformHandle;

            /** This will be used to pass in model position information. */
            private int mPositionHandle;

            /** This will be used to pass in model color information. */
            private int mColorHandle;

            /** This will be used to pass in model normal information. */
            private int mNormalHandle;

            /** This will be used to pass in model texture coordinate information. */
            private int mTextureCoordinateHandle;

            /** How many bytes per float. */
            private int mBytesPerFloat = 4;

            /** Size of the position data in elements. */
            private int mPositionDataSize = 3;

            /** Size of the color data in elements. */
            private int mColorDataSize = 4;

            /** Size of the normal data in elements. */
            private int mNormalDataSize = 3;

            /** Size of the texture coordinate data in elements. */
            private int mTextureCoordinateDataSize = 2;

            /** Used to hold a light centered on the origin in model space. We need a 4th coordinate so we can get translations to work when
             *  we multiply this by our transformation matrices. */
            private float[] mLightPosInModelSpace = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };

            /** Used to hold the current position of the light in world space (after transformation via model matrix). */
            private float[] mLightPosInWorldSpace = new float[4];

            /** Used to hold the transformed position of the light in eye space (after transformation via modelview matrix) */
            private float[] mLightPosInEyeSpace = new float[4];

            /** This is a handle to our cube shading program. */
            private int mProgramHandle;

            /** This is a handle to our light point program. */
            private int mPointProgramHandle;

            /** This is a handle to our texture data. */
            private int mTextureDataHandle;

            /**
             * Initialize the model data.
             */
            public LessonFourRenderer(Context activityContext)
            {
                mActivityContext = activityContext;

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

                // S, T (or X, Y)
                // Texture coordinate data.
                // Because images have a Y axis pointing downward (values increase as you move down the image) while
                // OpenGL has a Y axis pointing upward, we adjust for that here by flipping the Y axis.
                // What's more is that the texture coordinates are the same for every face.
                float[] cubeTextureCoordinateData =
		        {												
				        // Front face
				        0.0f, 0.0f, 				
				        0.0f, 1.0f,
				        1.0f, 0.0f,
				        0.0f, 1.0f,
				        1.0f, 1.0f,
				        1.0f, 0.0f,				
				
				        // Right face 
				        0.0f, 0.0f, 				
				        0.0f, 1.0f,
				        1.0f, 0.0f,
				        0.0f, 1.0f,
				        1.0f, 1.0f,
				        1.0f, 0.0f,	
				
				        // Back face 
				        0.0f, 0.0f, 				
				        0.0f, 1.0f,
				        1.0f, 0.0f,
				        0.0f, 1.0f,
				        1.0f, 1.0f,
				        1.0f, 0.0f,	
				
				        // Left face 
				        0.0f, 0.0f, 				
				        0.0f, 1.0f,
				        1.0f, 0.0f,
				        0.0f, 1.0f,
				        1.0f, 1.0f,
				        1.0f, 0.0f,	
				
				        // Top face 
				        0.0f, 0.0f, 				
				        0.0f, 1.0f,
				        1.0f, 0.0f,
				        0.0f, 1.0f,
				        1.0f, 1.0f,
				        1.0f, 0.0f,	
				
				        // Bottom face 
				        0.0f, 0.0f, 				
				        0.0f, 1.0f,
				        1.0f, 0.0f,
				        0.0f, 1.0f,
				        1.0f, 1.0f,
				        1.0f, 0.0f
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

                mCubeTextureCoordinates = ByteBuffer.allocateDirect(cubeTextureCoordinateData.Length * mBytesPerFloat)
                .order(ByteOrder.nativeOrder()).asFloatBuffer();
                mCubeTextureCoordinates.put(cubeTextureCoordinateData).position(0);
            }

            protected String getVertexShader()
            {
                return @"

uniform mat4 u_MVPMatrix;		// A constant representing the combined model/view/projection matrix.      		       
uniform mat4 u_MVMatrix;		// A constant representing the combined model/view matrix.       		
		  			
attribute vec4 a_Position;		// Per-vertex position information we will pass in.   				
attribute vec4 a_Color;			// Per-vertex color information we will pass in. 				
attribute vec3 a_Normal;		// Per-vertex normal information we will pass in.      
attribute vec2 a_TexCoordinate; // Per-vertex texture coordinate information we will pass in. 		
		  
varying vec3 v_Position;		// This will be passed into the fragment shader.       		
varying vec4 v_Color;			// This will be passed into the fragment shader.          		
varying vec3 v_Normal;			// This will be passed into the fragment shader.  
varying vec2 v_TexCoordinate;   // This will be passed into the fragment shader.    		
		  
// The entry point for our vertex shader.  
void main()                                                 	
{                                                         
	// Transform the vertex into eye space. 	
	v_Position = vec3(u_MVMatrix * a_Position);            
		
	// Pass through the color.
	v_Color = a_Color;
	
	// Pass through the texture coordinate.
	v_TexCoordinate = a_TexCoordinate;                                      
	
	// Transform the normal's orientation into eye space.
    v_Normal = vec3(u_MVMatrix * vec4(a_Normal, 0.0));
          
	// gl_Position is a special variable used to store the final position.
	// Multiply the vertex by the matrix to get the final point in normalized screen coordinates.
	gl_Position = u_MVPMatrix * a_Position;                       		  
}                                                          

";
            }

            protected String getFragmentShader()
            {
                return @"

precision mediump float;       	// Set the default precision to medium. We don't need as high of a 
								// precision in the fragment shader.
uniform vec3 u_LightPos;       	// The position of the light in eye space.
uniform sampler2D u_Texture;    // The input texture.
  
varying vec3 v_Position;		// Interpolated position for this fragment.
varying vec4 v_Color;          	// This is the color from the vertex shader interpolated across the 
  								// triangle per fragment.
varying vec3 v_Normal;         	// Interpolated normal for this fragment.
varying vec2 v_TexCoordinate;   // Interpolated texture coordinate per fragment.
  
// The entry point for our fragment shader.
void main()                    		
{                              
	// Will be used for attenuation.
    float distance = length(u_LightPos - v_Position);                  
	
	// Get a lighting direction vector from the light to the vertex.
    vec3 lightVector = normalize(u_LightPos - v_Position);              	

	// Calculate the dot product of the light vector and vertex normal. If the normal and light vector are
	// pointing in the same direction then it will get max illumination.
    float diffuse = max(dot(v_Normal, lightVector), 0.0);               	  		  													  

	// Add attenuation. 
    diffuse = diffuse * (1.0 / (1.0 + (0.10 * distance)));
    
    // Add ambient lighting
    diffuse = diffuse + 0.3;  

	// Multiply the color by the diffuse illumination level and texture value to get final output color.
    gl_FragColor = (v_Color * diffuse * texture2D(u_Texture, v_TexCoordinate));                                  		
  }                                                                     	



";
            }

            public void onSurfaceCreated(GL10 glUnused, EGLConfig config)
            {
                // Set the background clear color to black.
                GLES20.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);

                // Use culling to remove back faces.
                GLES20.glEnable(GLES20.GL_CULL_FACE);

                // Enable depth testing
                GLES20.glEnable(GLES20.GL_DEPTH_TEST);

                // Enable texture mapping
                GLES20.glEnable(GLES20.GL_TEXTURE_2D);

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

                var vertexShader = getVertexShader();
                var fragmentShader = getFragmentShader();

                int vertexShaderHandle = ShaderHelper.compileShader(GLES20.GL_VERTEX_SHADER, vertexShader);
                int fragmentShaderHandle = ShaderHelper.compileShader(GLES20.GL_FRAGMENT_SHADER, fragmentShader);

                mProgramHandle = ShaderHelper.createAndLinkProgram(vertexShaderHandle, fragmentShaderHandle,
                        new String[] { "a_Position", "a_Color", "a_Normal", "a_TexCoordinate" });

                // Define a simple shader program for our point.
                string pointVertexShader = @"

uniform mat4 u_MVPMatrix;      		
attribute vec4 a_Position;     		

void main()                    
{                              
	gl_Position = u_MVPMatrix * a_Position;   
    gl_PointSize = 5.0;         
}                              

";
                string pointFragmentShader = @"

precision mediump float;
       					          
void main()                    
{                              
	gl_FragColor = vec4(1.0, 1.0, 1.0, 1.0);             
}                              

";

                int pointVertexShaderHandle = ShaderHelper.compileShader(GLES20.GL_VERTEX_SHADER, pointVertexShader);
                int pointFragmentShaderHandle = ShaderHelper.compileShader(GLES20.GL_FRAGMENT_SHADER, pointFragmentShader);
                mPointProgramHandle = ShaderHelper.createAndLinkProgram(pointVertexShaderHandle, pointFragmentShaderHandle,
                        new String[] { "a_Position" });

                // Load the texture
                mTextureDataHandle = TextureHelper.loadTexture(mActivityContext, R.drawable.bumpy_bricks_public_domain);
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
                GLES20.glUseProgram(mProgramHandle);

                // Set program handles for cube drawing.
                mMVPMatrixHandle = GLES20.glGetUniformLocation(mProgramHandle, "u_MVPMatrix");
                mMVMatrixHandle = GLES20.glGetUniformLocation(mProgramHandle, "u_MVMatrix");
                mLightPosHandle = GLES20.glGetUniformLocation(mProgramHandle, "u_LightPos");
                mTextureUniformHandle = GLES20.glGetUniformLocation(mProgramHandle, "u_Texture");
                mPositionHandle = GLES20.glGetAttribLocation(mProgramHandle, "a_Position");
                mColorHandle = GLES20.glGetAttribLocation(mProgramHandle, "a_Color");
                mNormalHandle = GLES20.glGetAttribLocation(mProgramHandle, "a_Normal");
                mTextureCoordinateHandle = GLES20.glGetAttribLocation(mProgramHandle, "a_TexCoordinate");

                // Set the active texture unit to texture unit 0.
                GLES20.glActiveTexture(GLES20.GL_TEXTURE0);

                // Bind the texture to this unit.
                GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, mTextureDataHandle);

                // Tell the texture uniform sampler to use this texture in the shader by binding to texture unit 0.
                GLES20.glUniform1i(mTextureUniformHandle, 0);

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

                // Pass in the texture coordinate information
                mCubeTextureCoordinates.position(0);
                GLES20.glVertexAttribPointer(mTextureCoordinateHandle, mTextureCoordinateDataSize, GLES20.GL_FLOAT, false,
                        0, mCubeTextureCoordinates);

                GLES20.glEnableVertexAttribArray(mTextureCoordinateHandle);

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
        }

    }

}
