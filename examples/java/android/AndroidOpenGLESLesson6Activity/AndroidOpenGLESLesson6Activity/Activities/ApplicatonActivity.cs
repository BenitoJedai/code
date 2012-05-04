
using android.app;
using android.content;
using android.content.pm;
using android.opengl;
using android.os;
using android.provider;
using android.view;
using android.webkit;
using android.widget;
using AndroidOpenGLESLesson6Activity.Library;
using java.lang;
using java.nio;
using javax.microedition.khronos.egl;
using javax.microedition.khronos.opengles;
using ScriptCoreLib;

namespace AndroidOpenGLESLesson6Activity.Activities
{
    using System.ComponentModel;
    using android.util;
    //using WebGLRenderingContext = GLES20; 
    using gl = GLES20;

    [Script]
    public class AndroidOpenGLESLesson6Activity : Activity
    {
        // port from http://www.learnopengles.com/android-lesson-six-an-introduction-to-texture-filtering/
        // Y:\opensource\github\Learn-OpenGLES-Tutorials\android\AndroidOpenGLESLessons\src\com\learnopengles\android\lesson6


        // C:\util\android-sdk-windows\tools\android.bat create project --package AndroidOpenGLESLesson6Activity.Activities --activity AndroidOpenGLESLesson6Activity  --target 2  --path y:\jsc.svn\examples\java\android\AndroidOpenGLESLesson6Activity\AndroidOpenGLESLesson6Activity\staging

        // http://developer.android.com/guide/developing/device.html#setting-up
        // running it in emulator:
        // C:\util\android-sdk-windows\tools\android.bat avd

        // note: rebuild could auto reinstall

        // running it on device:
        // attach device to usb
        // C:\util\android-sdk-windows\platform-tools\adb.exe devices
        //List of devices attached
        //3330A17632C000EC        device 

        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r "y:\jsc.svn\examples\java\android\AndroidOpenGLESLesson6Activity\AndroidOpenGLESLesson6Activity\staging\bin\AndroidOpenGLESLesson6Activity-debug.apk"

        // screenshot: home+back
        // at "F:\ScreenCapture\SC20120504-153450.png"

        // http://android-ui-utils.googlecode.com/hg/asset-studio/dist/icons-launcher.html#foreground.type=image&foreground.space.trim=0&foreground.space.pad=-0.1&crop=1&backgroundShape=none&backColor=ff0000%2C100&foreColor=000000%2C0


        /** Hold a reference to our GLSurfaceView */

        public LessonSixGLSurfaceView mGLSurfaceView;
        public LessonSixRenderer mRenderer;

        public static int MIN_DIALOG = 1;
        public static int MAG_DIALOG = 2;

        public int mMinSetting = -1;
        public int mMagSetting = -1;


        public static string MIN_SETTING = "min_setting";
        public static string MAG_SETTING = "mag_setting";

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);


            setContentView(R.layout.main);

            mGLSurfaceView = (LessonSixGLSurfaceView)findViewById(R.id.gl_surface_view);

            // Check if the system supports OpenGL ES 2.0.
            ActivityManager activityManager = (ActivityManager)getSystemService(Context.ACTIVITY_SERVICE);
            ConfigurationInfo configurationInfo = activityManager.getDeviceConfigurationInfo();
            bool supportsEs2 = configurationInfo.reqGlEsVersion >= 0x20000;

            if (supportsEs2)
            {
                // Request an OpenGL ES 2.0 compatible context.
                mGLSurfaceView.setEGLContextClientVersion(2);

                DisplayMetrics displayMetrics = new DisplayMetrics();
                getWindowManager().getDefaultDisplay().getMetrics(displayMetrics);

                // Set the renderer to our demo renderer, defined below.
                mRenderer = new LessonSixRenderer(this);
                mGLSurfaceView.setRenderer(mRenderer, displayMetrics.density);
            }
            else
            {
                // This is where you could create an OpenGL ES 1.x compatible
                // renderer if you wanted to support both ES 1 and ES 2.
                return;
            }

            findViewById(R.id.button_set_min_filter).setOnClickListener(
                new button_set_min_filter_onclick { __this = this } 
            );

            findViewById(R.id.button_set_mag_filter).setOnClickListener(
               new button_set_mag_filter_onclick { __this = this }
           );
        

            // Restore previous settings
            if (savedInstanceState != null)
            {
                mMinSetting = savedInstanceState.getInt(MIN_SETTING, -1);
                mMagSetting = savedInstanceState.getInt(MAG_SETTING, -1);

                if (mMinSetting != -1) { setMinSetting(mMinSetting); }
                if (mMagSetting != -1) { setMagSetting(mMagSetting); }
            }

        }

        [Script]
        class button_set_min_filter_onclick : android.view.View.OnClickListener
        {
            public AndroidOpenGLESLesson6Activity __this;

            public void onClick(View v) 
            {
                __this.showDialog(MIN_DIALOG);		
            }
        }

        [Script]
        class button_set_mag_filter_onclick : android.view.View.OnClickListener
        {
            public AndroidOpenGLESLesson6Activity __this;

            public void onClick(View v)
            {
                __this.showDialog(MAG_DIALOG);
            }
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





        protected override void onSaveInstanceState(Bundle outState)
        {
            outState.putInt(MIN_SETTING, mMinSetting);
            outState.putInt(MAG_SETTING, mMagSetting);
        }

        [Script]
        class setMinSettingHandler : Runnable
        {
            public AndroidOpenGLESLesson6Activity __this;
            public int item;

            public void run()
            {
                int filter;

                if (item == 0)
                {
                    filter = GLES20.GL_NEAREST;
                }
                else if (item == 1)
                {
                    filter = GLES20.GL_LINEAR;
                }
                else if (item == 2)
                {
                    filter = GLES20.GL_NEAREST_MIPMAP_NEAREST;
                }
                else if (item == 3)
                {
                    filter = GLES20.GL_NEAREST_MIPMAP_LINEAR;
                }
                else if (item == 4)
                {
                    filter = GLES20.GL_LINEAR_MIPMAP_NEAREST;
                }
                else // if (item == 5)
                {
                    filter = GLES20.GL_LINEAR_MIPMAP_LINEAR;
                }

                __this.mRenderer.setMinFilter(filter);
            }
        }

        private void setMinSetting(int item)
        {
            mMinSetting = item;

            mGLSurfaceView.queueEvent(new setMinSettingHandler { __this = this, item = item }

            );
        }



        [Script]
        class setMagSettingHandler : Runnable
        {
            public AndroidOpenGLESLesson6Activity __this;
            public int item;

            public void run()
            {
                int filter;

                if (item == 0)
                {
                    filter = GLES20.GL_NEAREST;
                }
                else // if (item == 1)
                {
                    filter = GLES20.GL_LINEAR;
                }

                __this.mRenderer.setMagFilter(filter);
            }
        }

        private void setMagSetting(int item)
        {
            mMagSetting = item;

            mGLSurfaceView.queueEvent(new setMagSettingHandler { __this = this, item = item });
        }

        [Script]
        class lesson_six_min_filter_types_onclick : DialogInterface_OnClickListener
        {
            public AndroidOpenGLESLesson6Activity __this;

            public void onClick( DialogInterface dialog,  int item) 
            {
                __this.setMinSetting(item);
            }
        }

        [Script]
        class lesson_six_mag_filter_types_onclick : DialogInterface_OnClickListener
        {
            public AndroidOpenGLESLesson6Activity __this;

            public void onClick(DialogInterface dialog, int item)
            {
                __this.setMagSetting(item); 
            }
        }


        protected override Dialog onCreateDialog(int id)
        {
            Dialog dialog = null;

            if (id == MIN_DIALOG)
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                builder.setTitle((CharSequence)(object)"Set GL_TEXTURE_MIN_FILTER");

                var lesson_six_min_filter_types = new[] {
                    "GL_NEAREST",
    	            "GL_LINEAR",
    	            "GL_NEAREST_MIPMAP_NEAREST",
    	            "GL_NEAREST_MIPMAP_LINEAR",
    	            "GL_LINEAR_MIPMAP_NEAREST",
    	            "GL_LINEAR_MIPMAP_LINEAR",
                };

                builder.setItems(
                    (CharSequence[])(object)lesson_six_min_filter_types,
                    new lesson_six_min_filter_types_onclick { __this = this }
                );
                

                dialog = builder.create();

            }
            else if (id == MAG_DIALOG)
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                builder.setTitle((CharSequence)(object)"Set GL_TEXTURE_MAG_FILTER");

                var lesson_six_mag_filter_types = new string[]{
                    	        "GL_NEAREST",
                        "GL_LINEAR" 	
                };


                builder.setItems(
                    (CharSequence[])(object)lesson_six_mag_filter_types,
                    new lesson_six_mag_filter_types_onclick { __this = this }
                );
                

              

                dialog = builder.create();
            }
            else
            {
                dialog = null;
            }

            return dialog;
        }



        

    }


    [Script]
    public class LessonSixRenderer : GLSurfaceView.Renderer
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

        /** Store the accumulated rotation. */
        private float[] mAccumulatedRotation = new float[16];

        /** Store the current rotation. */
        private float[] mCurrentRotation = new float[16];

        /** A temporary matrix. */
        private float[] mTemporaryMatrix = new float[16];

        /** 
         * Stores a copy of the model matrix specifically for the light position.
         */
        private float[] mLightModelMatrix = new float[16];

        /** Store our model data in a float buffer. */
        private FloatBuffer mCubePositions;
        private FloatBuffer mCubeNormals;
        private FloatBuffer mCubeTextureCoordinates;
        private FloatBuffer mCubeTextureCoordinatesForPlane;

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

        /** This will be used to pass in model normal information. */
        private int mNormalHandle;

        /** This will be used to pass in model texture coordinate information. */
        private int mTextureCoordinateHandle;

        /** How many bytes per float. */
        private int mBytesPerFloat = 4;

        /** Size of the position data in elements. */
        private int mPositionDataSize = 3;

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

        /** These are handles to our texture data. */
        private int mBrickDataHandle;
        private int mGrassDataHandle;

        /** Temporary place to save the min and mag filter, in case the activity was restarted. */
        private int mQueuedMinFilter;
        private int mQueuedMagFilter;

        // These still work without volatile, but refreshes are not guaranteed to happen.					
        public /* volatile */ float mDeltaX;
        public /* volatile */ float mDeltaY;

        /**
         * Initialize the model data.
         */
        public LessonSixRenderer(Context activityContext)
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

            // S, T (or X, Y)
            // Texture coordinate data.
            // Because images have a Y axis pointing downward (values increase as you move down the image) while
            // OpenGL has a Y axis pointing upward, we adjust for that here by flipping the Y axis.
            // What's more is that the texture coordinates are the same for every face.
            float[] cubeTextureCoordinateDataForPlane =
		{												
				// Front face
				0.0f, 0.0f, 				
				0.0f, 25.0f,
				25.0f, 0.0f,
				0.0f, 25.0f,
				25.0f, 25.0f,
				25.0f, 0.0f,				
				
				// Right face 
				0.0f, 0.0f, 				
				0.0f, 25.0f,
				25.0f, 0.0f,
				0.0f, 25.0f,
				25.0f, 25.0f,
				25.0f, 0.0f,	
				
				// Back face 
				0.0f, 0.0f, 				
				0.0f, 25.0f,
				25.0f, 0.0f,
				0.0f, 25.0f,
				25.0f, 25.0f,
				25.0f, 0.0f,	
				
				// Left face 
				0.0f, 0.0f, 				
				0.0f, 25.0f,
				25.0f, 0.0f,
				0.0f, 25.0f,
				25.0f, 25.0f,
				25.0f, 0.0f,	
				
				// Top face 
				0.0f, 0.0f, 				
				0.0f, 25.0f,
				25.0f, 0.0f,
				0.0f, 25.0f,
				25.0f, 25.0f,
				25.0f, 0.0f,	
				
				// Bottom face 
				0.0f, 0.0f, 				
				0.0f, 25.0f,
				25.0f, 0.0f,
				0.0f, 25.0f,
				25.0f, 25.0f,
				25.0f, 0.0f
		};

            // Initialize the buffers.
            mCubePositions = ByteBuffer.allocateDirect(cubePositionData.Length * mBytesPerFloat)
            .order(ByteOrder.nativeOrder()).asFloatBuffer();
            mCubePositions.put(cubePositionData).position(0);

            mCubeNormals = ByteBuffer.allocateDirect(cubeNormalData.Length * mBytesPerFloat)
            .order(ByteOrder.nativeOrder()).asFloatBuffer();
            mCubeNormals.put(cubeNormalData).position(0);

            mCubeTextureCoordinates = ByteBuffer.allocateDirect(cubeTextureCoordinateData.Length * mBytesPerFloat)
            .order(ByteOrder.nativeOrder()).asFloatBuffer();
            mCubeTextureCoordinates.put(cubeTextureCoordinateData).position(0);

            mCubeTextureCoordinatesForPlane = ByteBuffer.allocateDirect(cubeTextureCoordinateDataForPlane.Length * mBytesPerFloat)
            .order(ByteOrder.nativeOrder()).asFloatBuffer();
            mCubeTextureCoordinatesForPlane.put(cubeTextureCoordinateDataForPlane).position(0);
        }


        protected string getVertexShader()
        {
            return @"
uniform mat4 u_MVPMatrix;		// A constant representing the combined model/view/projection matrix.      		       
uniform mat4 u_MVMatrix;		// A constant representing the combined model/view matrix.       		
		  			
attribute vec4 a_Position;		// Per-vertex position information we will pass in.   							
attribute vec3 a_Normal;		// Per-vertex normal information we will pass in.      
attribute vec2 a_TexCoordinate; // Per-vertex texture coordinate information we will pass in. 		
		  
varying vec3 v_Position;		// This will be passed into the fragment shader.       		          		
varying vec3 v_Normal;			// This will be passed into the fragment shader.  
varying vec2 v_TexCoordinate;   // This will be passed into the fragment shader.    		
		  
// The entry point for our vertex shader.  
void main()                                                 	
{                                                         
	// Transform the vertex into eye space. 	
	v_Position = vec3(u_MVMatrix * a_Position);            		
	
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

        protected string getFragmentShader()
        {
            return @"

precision mediump float;       	// Set the default precision to medium. We don't need as high of a 
								// precision in the fragment shader.
uniform vec3 u_LightPos;       	// The position of the light in eye space.
uniform sampler2D u_Texture;    // The input texture.
  
varying vec3 v_Position;		// Interpolated position for this fragment.
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
    diffuse = diffuse * (1.0 / (1.0 + (0.25 * distance)));
    
    // Add ambient lighting
    diffuse = diffuse + 0.7;  

	// Multiply the color by the diffuse illumination level and texture value to get final output color.
    gl_FragColor = (diffuse * texture2D(u_Texture, v_TexCoordinate));                                  		
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

            string vertexShader = getVertexShader();
            string fragmentShader = getFragmentShader();

            int vertexShaderHandle = ShaderHelper.compileShader(GLES20.GL_VERTEX_SHADER, vertexShader);
            int fragmentShaderHandle = ShaderHelper.compileShader(GLES20.GL_FRAGMENT_SHADER, fragmentShader);

            mProgramHandle = ShaderHelper.createAndLinkProgram(vertexShaderHandle, fragmentShaderHandle,
                    new string[] { "a_Position", "a_Normal", "a_TexCoordinate" });

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
                    new string[] { "a_Position" });

            // Load the texture
            mBrickDataHandle = TextureHelper.loadTexture(mActivityContext, R.drawable.stone_wall_public_domain);
            GLES20.glGenerateMipmap(GLES20.GL_TEXTURE_2D);

            mGrassDataHandle = TextureHelper.loadTexture(mActivityContext, R.drawable.noisy_grass_public_domain);
            GLES20.glGenerateMipmap(GLES20.GL_TEXTURE_2D);

            if (mQueuedMinFilter != 0)
            {
                setMinFilter(mQueuedMinFilter);
            }

            if (mQueuedMagFilter != 0)
            {
                setMagFilter(mQueuedMagFilter);
            }

            // Initialize the accumulated rotation matrix
            Matrix.setIdentityM(mAccumulatedRotation, 0);
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
            float far = 1000.0f;

            Matrix.frustumM(mProjectionMatrix, 0, left, right, bottom, top, near, far);
        }

        public void onDrawFrame(GL10 glUnused)
        {
            GLES20.glClear(GLES20.GL_COLOR_BUFFER_BIT | GLES20.GL_DEPTH_BUFFER_BIT);

            // Do a complete rotation every 10 seconds.
            long time = SystemClock.uptimeMillis() % 10000L;
            long slowTime = SystemClock.uptimeMillis() % 100000L;
            float angleInDegrees = (360.0f / 10000.0f) * ((int)time);
            float slowAngleInDegrees = (360.0f / 100000.0f) * ((int)slowTime);

            // Set our per-vertex lighting program.
            GLES20.glUseProgram(mProgramHandle);

            // Set program handles for cube drawing.
            mMVPMatrixHandle = GLES20.glGetUniformLocation(mProgramHandle, "u_MVPMatrix");
            mMVMatrixHandle = GLES20.glGetUniformLocation(mProgramHandle, "u_MVMatrix");
            mLightPosHandle = GLES20.glGetUniformLocation(mProgramHandle, "u_LightPos");
            mTextureUniformHandle = GLES20.glGetUniformLocation(mProgramHandle, "u_Texture");
            mPositionHandle = GLES20.glGetAttribLocation(mProgramHandle, "a_Position");
            mNormalHandle = GLES20.glGetAttribLocation(mProgramHandle, "a_Normal");
            mTextureCoordinateHandle = GLES20.glGetAttribLocation(mProgramHandle, "a_TexCoordinate");

            // Calculate position of the light. Rotate and then push into the distance.
            Matrix.setIdentityM(mLightModelMatrix, 0);
            Matrix.translateM(mLightModelMatrix, 0, 0.0f, 0.0f, -2.0f);
            Matrix.rotateM(mLightModelMatrix, 0, angleInDegrees, 0.0f, 1.0f, 0.0f);
            Matrix.translateM(mLightModelMatrix, 0, 0.0f, 0.0f, 3.5f);

            Matrix.multiplyMV(mLightPosInWorldSpace, 0, mLightModelMatrix, 0, mLightPosInModelSpace, 0);
            Matrix.multiplyMV(mLightPosInEyeSpace, 0, mViewMatrix, 0, mLightPosInWorldSpace, 0);

            // Draw a cube.
            // Translate the cube into the screen.
            Matrix.setIdentityM(mModelMatrix, 0);
            Matrix.translateM(mModelMatrix, 0, 0.0f, 0.8f, -3.5f);

            // Set a matrix that contains the current rotation.
            Matrix.setIdentityM(mCurrentRotation, 0);
            Matrix.rotateM(mCurrentRotation, 0, mDeltaX, 0.0f, 1.0f, 0.0f);
            Matrix.rotateM(mCurrentRotation, 0, mDeltaY, 1.0f, 0.0f, 0.0f);
            mDeltaX = 0.0f;
            mDeltaY = 0.0f;

            // Multiply the current rotation by the accumulated rotation, and then set the accumulated rotation to the result.
            Matrix.multiplyMM(mTemporaryMatrix, 0, mCurrentRotation, 0, mAccumulatedRotation, 0);
            java.lang.System.arraycopy(mTemporaryMatrix, 0, mAccumulatedRotation, 0, 16);

            // Rotate the cube taking the overall rotation into account.     	
            Matrix.multiplyMM(mTemporaryMatrix, 0, mModelMatrix, 0, mAccumulatedRotation, 0);
            java.lang.System.arraycopy(mTemporaryMatrix, 0, mModelMatrix, 0, 16);

            // Set the active texture unit to texture unit 0.
            GLES20.glActiveTexture(GLES20.GL_TEXTURE0);

            // Bind the texture to this unit.
            GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, mBrickDataHandle);

            // Tell the texture uniform sampler to use this texture in the shader by binding to texture unit 0.
            GLES20.glUniform1i(mTextureUniformHandle, 0);

            // Pass in the texture coordinate information
            mCubeTextureCoordinates.position(0);
            GLES20.glVertexAttribPointer(mTextureCoordinateHandle, mTextureCoordinateDataSize, GLES20.GL_FLOAT, false,
                    0, mCubeTextureCoordinates);

            drawCube();

            // Draw a plane
            Matrix.setIdentityM(mModelMatrix, 0);
            Matrix.translateM(mModelMatrix, 0, 0.0f, -2.0f, -5.0f);
            Matrix.scaleM(mModelMatrix, 0, 25.0f, 1.0f, 25.0f);
            Matrix.rotateM(mModelMatrix, 0, slowAngleInDegrees, 0.0f, 1.0f, 0.0f);

            // Set the active texture unit to texture unit 0.
            GLES20.glActiveTexture(GLES20.GL_TEXTURE0);

            // Bind the texture to this unit.
            GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, mGrassDataHandle);

            // Tell the texture uniform sampler to use this texture in the shader by binding to texture unit 0.
            GLES20.glUniform1i(mTextureUniformHandle, 0);

            // Pass in the texture coordinate information
            mCubeTextureCoordinatesForPlane.position(0);
            GLES20.glVertexAttribPointer(mTextureCoordinateHandle, mTextureCoordinateDataSize, GLES20.GL_FLOAT, false,
                    0, mCubeTextureCoordinatesForPlane);

            GLES20.glEnableVertexAttribArray(mTextureCoordinateHandle);

            drawCube();

            // Draw a point to indicate the light.
            GLES20.glUseProgram(mPointProgramHandle);
            drawLight();
        }

        public void setMinFilter(int filter)
        {
            if (mBrickDataHandle != 0)
                if (mGrassDataHandle != 0)
                {
                    GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, mBrickDataHandle);
                    GLES20.glTexParameteri(GLES20.GL_TEXTURE_2D, GLES20.GL_TEXTURE_MIN_FILTER, filter);
                    GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, mGrassDataHandle);
                    GLES20.glTexParameteri(GLES20.GL_TEXTURE_2D, GLES20.GL_TEXTURE_MIN_FILTER, filter);

                    return;
                }

            mQueuedMinFilter = filter;
        }

        public void setMagFilter(int filter)
        {
            if (mBrickDataHandle != 0)
                if (mGrassDataHandle != 0)
                {
                    GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, mBrickDataHandle);
                    GLES20.glTexParameteri(GLES20.GL_TEXTURE_2D, GLES20.GL_TEXTURE_MAG_FILTER, filter);
                    GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, mGrassDataHandle);
                    GLES20.glTexParameteri(GLES20.GL_TEXTURE_2D, GLES20.GL_TEXTURE_MAG_FILTER, filter);

                    return;
                }

            mQueuedMagFilter = filter;
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
            Matrix.multiplyMM(mTemporaryMatrix, 0, mProjectionMatrix, 0, mMVPMatrix, 0);
            java.lang.System.arraycopy(mTemporaryMatrix, 0, mMVPMatrix, 0, 16);

            // Pass in the combined matrix.
            GLES20.glUniformMatrix4fv(mMVPMatrixHandle, 1, false, mMVPMatrix, 0);

            // Pass in the light position in eye space.        
            GLES20.glUniform3f(mLightPosHandle, mLightPosInEyeSpace[0], mLightPosInEyeSpace[1], mLightPosInEyeSpace[2]);

            // Draw the cube.k
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
            Matrix.multiplyMM(mTemporaryMatrix, 0, mProjectionMatrix, 0, mMVPMatrix, 0);
            java.lang.System.arraycopy(mTemporaryMatrix, 0, mMVPMatrix, 0, 16);
            GLES20.glUniformMatrix4fv(pointMVPMatrixHandle, 1, false, mMVPMatrix, 0);

            // Draw the point.
            GLES20.glDrawArrays(GLES20.GL_POINTS, 0, 1);
        }



    }


    [Script]
    public class LessonSixGLSurfaceView : GLSurfaceView
    {
        private LessonSixRenderer mRenderer;

        // Offsets for touch events	 
        private float mPreviousX;
        private float mPreviousY;

        private float mDensity;

        public LessonSixGLSurfaceView(Context context)
            : base(context)
        {
        }

        public LessonSixGLSurfaceView(Context context, AttributeSet attrs)
            : base(context, attrs)
        {
        }

        public override bool onTouchEvent(MotionEvent e)
        {
            if (e != null)
            {
                float x = e.getX();
                float y = e.getY();

                if (e.getAction() == MotionEvent.ACTION_MOVE)
                {
                    if (mRenderer != null)
                    {
                        float deltaX = (x - mPreviousX) / mDensity / 2f;
                        float deltaY = (y - mPreviousY) / mDensity / 2f;

                        mRenderer.mDeltaX += deltaX;
                        mRenderer.mDeltaY += deltaY;
                    }
                }

                mPreviousX = x;
                mPreviousY = y;

                return true;
            }

            return base.onTouchEvent(e);
        }

        // Hides superclass method.
        public void setRenderer(LessonSixRenderer renderer, float density)
        {
            mRenderer = renderer;
            mDensity = density;
            base.setRenderer(renderer);
        }
    }


}
