
using System;
using System.ComponentModel;
using android.app;
using android.content;
using android.content.pm;
using android.opengl;
using android.os;
using android.provider;
using android.util;
using android.view;
using android.webkit;
using android.widget;
using java.lang;
using java.nio;
using javax.microedition.khronos.egl;
using javax.microedition.khronos.opengles;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;

namespace AndroidOpenGLESLesson6Activity.Activities
{
    using gl__ = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using gl = __WebGLRenderingContext;
    using opengl = GLES20;

    #region R
    [Script(IsNative = true)]
    public static class R
    {
        [Script(IsNative = true)]
        public static class drawable
        {
            public static int stone_wall_public_domain;
            public static int noisy_grass_public_domain;
        }


        [Script(IsNative = true)]
        public static class layout
        {
            public static int main;
        }

        [Script(IsNative = true)]
        public static class id
        {
            public static int gl_surface_view;

            public static int button_set_min_filter;
            public static int button_set_mag_filter;

        }
    }
    #endregion

    public class AndroidOpenGLESLesson6Activity : Activity
    {
        // port from http://www.learnopengles.com/android-lesson-six-an-introduction-to-texture-filtering/
        // Y:\opensource\github\Learn-OpenGLES-Tutorials\android\AndroidOpenGLESLessons\src\com\learnopengles\android\lesson6


        // http://android-ui-utils.googlecode.com/hg/asset-studio/dist/icons-launcher.html#foreground.type=image&foreground.space.trim=0&foreground.space.pad=-0.1&crop=1&backgroundShape=none&backColor=ff0000%2C100&foreColor=000000%2C0

        /** Hold a reference to our GLSurfaceView */

        public LessonSixGLSurfaceView mGLSurfaceView;
        public LessonSixRenderer mRenderer;

        public static int MIN_DIALOG = 1;
        public static int MAG_DIALOG = 2;

        public int mMinSetting = -1;
        public int mMagSetting = -1;


        public const string MIN_SETTING = "min_setting";
        public const string MAG_SETTING = "mag_setting";

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            this.ToFullscreen();

            setContentView(R.layout.main);

            mGLSurfaceView = (LessonSixGLSurfaceView)findViewById(R.id.gl_surface_view);

            // http://www.learnopengles.com/android-emulator-now-supports-native-opengl-es2-0/


            // Request an OpenGL ES 2.0 compatible context.
            mGLSurfaceView.setEGLContextClientVersion(2);

            DisplayMetrics displayMetrics = new DisplayMetrics();
            getWindowManager().getDefaultDisplay().getMetrics(displayMetrics);

            // Set the renderer to our demo renderer, defined below.
            mRenderer = new LessonSixRenderer(this);
            mGLSurfaceView.setRenderer(mRenderer, displayMetrics.density);


            findViewById(R.id.button_set_min_filter).AtClick(
                delegate
                {
                    this.showDialog(MIN_DIALOG);
                }
            );

            findViewById(R.id.button_set_mag_filter).AtClick(
             delegate
             {
                 this.showDialog(MAG_DIALOG);
             }
         );


            this.ShowToast("http://my.jsc-solutions.net a");
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




     
        private void setMinSetting(int item)
        {
            mMinSetting = item;

            mGLSurfaceView.queueEvent(
                delegate
                {
                    int filter;

                    if (item == 0)
                    {
                        filter = (int)gl__.NEAREST;
                    }
                    else if (item == 1)
                    {
                        filter = (int)gl__.LINEAR;
                    }
                    else if (item == 2)
                    {
                        filter = (int)gl__.NEAREST_MIPMAP_NEAREST;
                    }
                    else if (item == 3)
                    {
                        filter = (int)gl__.NEAREST_MIPMAP_LINEAR;
                    }
                    else if (item == 4)
                    {
                        filter = (int)gl__.LINEAR_MIPMAP_NEAREST;
                    }
                    else // if (item == 5)
                    {
                        filter = (int)gl__.LINEAR_MIPMAP_LINEAR;
                    }

                    this.mRenderer.setMinFilter(filter);
                }
            );
        }



        

        private void setMagSetting(int item)
        {
            mMagSetting = item;

            mGLSurfaceView.queueEvent(
                delegate
                {
                    int filter;

                    if (item == 0)
                    {
                        filter = (int)gl__.NEAREST;
                    }
                    else // if (item == 1)
                    {
                        filter = (int)gl__.LINEAR;
                    }

                    this.mRenderer.setMagFilter(filter);
                }
            );
        }

        class lesson_six_min_filter_types_onclick : DialogInterface_OnClickListener
        {
            public AndroidOpenGLESLesson6Activity __this;

            public void onClick(DialogInterface dialog, int item)
            {
                __this.setMinSetting(item);
            }
        }

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
                builder.setTitle("Set GL_TEXTURE_MIN_FILTER");

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
                builder.setTitle("Set GL_TEXTURE_MAG_FILTER");

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
        private ScriptCoreLib.JavaScript.WebGL.WebGLUniformLocation mMVPMatrixHandle;

        /** This will be used to pass in the modelview matrix. */
        private ScriptCoreLib.JavaScript.WebGL.WebGLUniformLocation mMVMatrixHandle;

        /** This will be used to pass in the light position. */
        private ScriptCoreLib.JavaScript.WebGL.WebGLUniformLocation mLightPosHandle;

        /** This will be used to pass in the texture. */
        private ScriptCoreLib.JavaScript.WebGL.WebGLUniformLocation mTextureUniformHandle;

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
        private ScriptCoreLib.JavaScript.WebGL.WebGLProgram mProgramHandle;

        /** This is a handle to our light point program. */
        private ScriptCoreLib.JavaScript.WebGL.WebGLProgram mPointProgramHandle;

        /** These are handles to our texture data. */
        private ScriptCoreLib.JavaScript.WebGL.WebGLTexture mBrickDataHandle;
        private ScriptCoreLib.JavaScript.WebGL.WebGLTexture mGrassDataHandle;

        /** Temporary place to save the min and mag filter, in case the activity was restarted. */
        private int mQueuedMinFilter;
        private int mQueuedMagFilter;

        // These still work without volatile, but refreshes are not guaranteed to happen.					
        public /* volatile */ float mDeltaX;
        public /* volatile */ float mDeltaY;

        __WebGLRenderingContext gl = new __WebGLRenderingContext();
        ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext gl__ ;


        public LessonSixRenderer(Context activityContext)
        {
            this.gl__ = (ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext)(object)gl;

            mActivityContext = activityContext;

            #region Define points for a cube.

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
            #endregion

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



        public void onSurfaceCreated(GL10 glUnused, EGLConfig config)
        {
            // Set the background clear color to black.
            gl__.clearColor(0.0f, 0.0f, 0.0f, 0.0f);

            // Use culling to remove back faces.
            gl__.enable(gl__.CULL_FACE);

            // Enable depth testing
            gl__.enable(gl__.DEPTH_TEST);

            // Enable texture mapping
            gl__.enable(gl__.TEXTURE_2D);

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


            mProgramHandle = gl.createAndLinkProgram(
                new Shaders.per_pixel_tex_and_lightVertexShader(),
                new Shaders.per_pixel_tex_and_lightFragmentShader(),
                "a_Position", "a_Normal", "a_TexCoordinate"
            );

            // Define a simple shader program for our point.
            mPointProgramHandle = gl.createAndLinkProgram(
                new Shaders.pointVertexShader(),
                new Shaders.pointFragmentShader(),
                "a_Position"
            );


            #region loadTexture
            Func<android.graphics.Bitmap, ScriptCoreLib.JavaScript.WebGL.WebGLTexture> loadTexture = (bitmap) =>
            {
                var textureHandle = gl__.createTexture();

                // Bind to the texture in OpenGL
                gl__.bindTexture(gl__.TEXTURE_2D, textureHandle);

                // Set filtering
                gl__.texParameteri(gl__.TEXTURE_2D, gl__.TEXTURE_MIN_FILTER, (int)gl__.NEAREST);
                gl__.texParameteri(gl__.TEXTURE_2D, gl__.TEXTURE_MAG_FILTER, (int)gl__.NEAREST);

                // Load the bitmap into the bound texture.
                GLUtils.texImage2D((int)gl__.TEXTURE_2D, 0, bitmap, 0);

                // Recycle the bitmap, since its data has been loaded into OpenGL.
                bitmap.recycle();
               

                return textureHandle;
            };
            #endregion

            // Load the texture
            mBrickDataHandle = loadTexture(
                 android.graphics.BitmapFactory.decodeResource(
                     mActivityContext.getResources(),
                     R.drawable.stone_wall_public_domain,
                     new android.graphics.BitmapFactory.Options
                     {
                         inScaled = false // No pre-scaling
                     }
                 )
             );

            gl__.generateMipmap(gl__.TEXTURE_2D);


            mGrassDataHandle = loadTexture(
              android.graphics.BitmapFactory.decodeResource(
                  mActivityContext.getResources(),
                  R.drawable.noisy_grass_public_domain,
                  new android.graphics.BitmapFactory.Options
                  {
                      inScaled = false // No pre-scaling
                  }
              )
          );

            gl__.generateMipmap(gl__.TEXTURE_2D);

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
            gl__.viewport(0, 0, width, height);

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
            gl__.clear(gl__.COLOR_BUFFER_BIT | gl__.DEPTH_BUFFER_BIT);

            // Do a complete rotation every 10 seconds.
            long time = SystemClock.uptimeMillis() % 10000L;
            long slowTime = SystemClock.uptimeMillis() % 100000L;
            float angleInDegrees = (360.0f / 10000.0f) * ((int)time);
            float slowAngleInDegrees = (360.0f / 100000.0f) * ((int)slowTime);

            var program = mProgramHandle;
            // Set our per-vertex lighting program.
            gl__.useProgram(program);


            // Set program handles for cube drawing.
            mMVPMatrixHandle = gl__.getUniformLocation(program, "u_MVPMatrix");
            mMVMatrixHandle = gl__.getUniformLocation(program, "u_MVMatrix");
            mLightPosHandle = gl__.getUniformLocation(program, "u_LightPos");
            mTextureUniformHandle = gl__.getUniformLocation(program, "u_Texture");

            mPositionHandle = gl__.getAttribLocation(program, "a_Position");
            mNormalHandle = gl__.getAttribLocation(program, "a_Normal");
            mTextureCoordinateHandle = gl__.getAttribLocation(program, "a_TexCoordinate");

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
            mDeltaX = 0.1f;
            mDeltaY = 0.1f;

            // Multiply the current rotation by the accumulated rotation, and then set the accumulated rotation to the result.
            Matrix.multiplyMM(mTemporaryMatrix, 0, mCurrentRotation, 0, mAccumulatedRotation, 0);
            java.lang.System.arraycopy(mTemporaryMatrix, 0, mAccumulatedRotation, 0, 16);

            // Rotate the cube taking the overall rotation into account.     	
            Matrix.multiplyMM(mTemporaryMatrix, 0, mModelMatrix, 0, mAccumulatedRotation, 0);
            java.lang.System.arraycopy(mTemporaryMatrix, 0, mModelMatrix, 0, 16);

            // Set the active texture unit to texture unit 0.
            gl__.activeTexture(gl__.TEXTURE0);

            // Bind the texture to this unit.
            gl__.bindTexture(gl__.TEXTURE_2D, mBrickDataHandle);

            // Tell the texture uniform sampler to use this texture in the shader by binding to texture unit 0.
            gl__.uniform1i(mTextureUniformHandle, 0);

            // Pass in the texture coordinate information
            mCubeTextureCoordinates.position(0);

            opengl.glVertexAttribPointer(mTextureCoordinateHandle, mTextureCoordinateDataSize, (int)gl__.FLOAT, false,
                    0, mCubeTextureCoordinates);

            #region drawCube
            Action drawCube =
                delegate
                {
                    // Pass in the position information
                    mCubePositions.position(0);
                    opengl.glVertexAttribPointer(mPositionHandle, mPositionDataSize, (int)gl__.FLOAT, false,
                            0, mCubePositions);

                    gl__.enableVertexAttribArray((uint)mPositionHandle);

                    // Pass in the normal information
                    mCubeNormals.position(0);
                    opengl.glVertexAttribPointer(mNormalHandle, mNormalDataSize, (int)gl__.FLOAT, false,
                            0, mCubeNormals);

                    gl__.enableVertexAttribArray((uint)mNormalHandle);

                    // This multiplies the view matrix by the model matrix, and stores the result in the MVP matrix
                    // (which currently contains model * view).
                    Matrix.multiplyMM(mMVPMatrix, 0, mViewMatrix, 0, mModelMatrix, 0);

                    // Pass in the modelview matrix.
                    gl.uniformMatrix4fv(mMVMatrixHandle, 1, false, mMVPMatrix, 0);

                    // This multiplies the modelview matrix by the projection matrix, and stores the result in the MVP matrix
                    // (which now contains model * view * projection).        
                    Matrix.multiplyMM(mTemporaryMatrix, 0, mProjectionMatrix, 0, mMVPMatrix, 0);
                    java.lang.System.arraycopy(mTemporaryMatrix, 0, mMVPMatrix, 0, 16);

                    // Pass in the combined matrix.
                    gl.uniformMatrix4fv(mMVPMatrixHandle, 1, false, mMVPMatrix, 0);

                    // Pass in the light position in eye space.        
                    gl__.uniform3f(mLightPosHandle, mLightPosInEyeSpace[0], mLightPosInEyeSpace[1], mLightPosInEyeSpace[2]);

                    // Draw the cube.k
                    gl.drawArrays(gl__.TRIANGLES, 0, 36);
                };
            #endregion

            drawCube();

            // Draw a plane
            Matrix.setIdentityM(mModelMatrix, 0);
            Matrix.translateM(mModelMatrix, 0, 0.0f, -2.0f, -5.0f);
            Matrix.scaleM(mModelMatrix, 0, 25.0f, 1.0f, 25.0f);
            Matrix.rotateM(mModelMatrix, 0, slowAngleInDegrees, 0.0f, 1.0f, 0.0f);

            // Set the active texture unit to texture unit 0.
            gl__.activeTexture(gl__.TEXTURE0);

            // Bind the texture to this unit.
            gl__.bindTexture(gl__.TEXTURE_2D, mGrassDataHandle);

            // Tell the texture uniform sampler to use this texture in the shader by binding to texture unit 0.
            gl__.uniform1i(mTextureUniformHandle, 0);

            // Pass in the texture coordinate information
            mCubeTextureCoordinatesForPlane.position(0);
            opengl.glVertexAttribPointer(mTextureCoordinateHandle, mTextureCoordinateDataSize, (int)gl__.FLOAT, false,
                    0, mCubeTextureCoordinatesForPlane);

            gl__.enableVertexAttribArray((uint)mTextureCoordinateHandle);

            drawCube();


            #region drawLight
            Action drawLight =
                delegate
                {
                    var pointMVPMatrixHandle = gl__.getUniformLocation(mPointProgramHandle, "u_MVPMatrix");
                    var pointPositionHandle = gl__.getAttribLocation(mPointProgramHandle, "a_Position");

                    // Pass in the position.
                    gl__.vertexAttrib3f((uint)pointPositionHandle, mLightPosInModelSpace[0], mLightPosInModelSpace[1], mLightPosInModelSpace[2]);

                    // Since we are not using a buffer object, disable vertex arrays for this attribute.
                    gl__.disableVertexAttribArray((uint)pointPositionHandle);

                    // Pass in the transformation matrix.
                    Matrix.multiplyMM(mMVPMatrix, 0, mViewMatrix, 0, mLightModelMatrix, 0);
                    Matrix.multiplyMM(mTemporaryMatrix, 0, mProjectionMatrix, 0, mMVPMatrix, 0);
                    java.lang.System.arraycopy(mTemporaryMatrix, 0, mMVPMatrix, 0, 16);
                    gl.uniformMatrix4fv(pointMVPMatrixHandle, 1, false, mMVPMatrix, 0);

                    // Draw the point.
                    gl__.drawArrays(gl__.POINTS, 0, 1);
                };
            #endregion

            // Draw a point to indicate the light.
            gl__.useProgram(mPointProgramHandle);
            drawLight();
        }

        public void setMinFilter(int filter)
        {
            if (mBrickDataHandle != null)
                if (mGrassDataHandle != null)
                {
                    gl__.bindTexture(gl__.TEXTURE_2D, mBrickDataHandle);
                    gl__.texParameteri(gl__.TEXTURE_2D, gl__.TEXTURE_MIN_FILTER, filter);
                    gl__.bindTexture(gl__.TEXTURE_2D, mGrassDataHandle);
                    gl__.texParameteri(gl__.TEXTURE_2D, gl__.TEXTURE_MIN_FILTER, filter);

                    return;
                }

            mQueuedMinFilter = filter;
        }

        public void setMagFilter(int filter)
        {
            if (mBrickDataHandle != null)
                if (mGrassDataHandle != null)
                {
                    gl__.bindTexture(gl__.TEXTURE_2D, mBrickDataHandle);
                    gl__.texParameteri(gl__.TEXTURE_2D, gl__.TEXTURE_MAG_FILTER, filter);
                    gl__.bindTexture(gl__.TEXTURE_2D, mGrassDataHandle);
                    gl__.texParameteri(gl__.TEXTURE_2D, gl__.TEXTURE_MAG_FILTER, filter);

                    return;
                }

            mQueuedMagFilter = filter;
        }




    }


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


        public LessonSixGLSurfaceView(Context context, android.util.AttributeSet a)
            : base(context, a)
        {
       //     Caused by: java.lang.NoSuchMethodException: <init> [class android.content.Context, interface android.util.AttributeSet]
       //at java.lang.Class.getConstructorOrMethod(Class.java:460)
       //at java.lang.Class.getConstructor(Class.java:431)
       //at android.view.LayoutInflater.createView(LayoutInflater.java:561)
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
