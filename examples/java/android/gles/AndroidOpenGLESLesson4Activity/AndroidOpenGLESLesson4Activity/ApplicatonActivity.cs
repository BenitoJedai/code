using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.content.pm;
//using android.opengl;
using android.os;
using android.provider;
using android.view;
using android.webkit;
using android.widget;
using java.lang;
using java.nio;
//using javax.microedition.khronos.egl;
using javax.microedition.khronos.opengles;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;

namespace AndroidOpenGLESLesson4Activity.Activities
{
    using opengl = android.opengl.GLES20;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using __gl = ScriptCoreLib.Android.WebGL.__WebGLRenderingContext;
    using java.io;
    using android.opengl;




    public class AndroidOpenGLESLesson4Activity : Activity
    {
        // port from http://www.learnopengles.com/android-lesson-four-introducing-basic-texturing/
        // Y:\opensource\github\Learn-OpenGLES-Tutorials\android\AndroidOpenGLESLessons\src\com\learnopengles\android\lesson4

        // http://android-ui-utils.googlecode.com/hg/asset-studio/dist/icons-launcher.html#foreground.type=image&foreground.space.trim=0&foreground.space.pad=-0.1&crop=1&backgroundShape=none&backColor=ff0000%2C100&foreColor=000000%2C0

        private GLSurfaceView mGLSurfaceView;


        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);
            //this.ToFullscreen();

            mGLSurfaceView = new GLSurfaceView(this);


            // Request an OpenGL ES 2.0 compatible context.
            mGLSurfaceView.setEGLContextClientVersion(2);

            // Set the renderer to our demo renderer, defined below.
            mGLSurfaceView.setRenderer(new LessonFourRenderer(this));


            setContentView(mGLSurfaceView);

            //this.ShowToast("http://my.jsc-solutions.net !");
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
            private WebGLUniformLocation mMVPMatrixHandle;

            /** This will be used to pass in the modelview matrix. */
            private WebGLUniformLocation mMVMatrixHandle;

            /** This will be used to pass in the light position. */
            private WebGLUniformLocation mLightPosHandle;

            /** This will be used to pass in the texture. */
            private WebGLUniformLocation mTextureUniformHandle;

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
            private WebGLProgram mProgramHandle;

            /** This is a handle to our light point program. */
            private WebGLProgram mPointProgramHandle;

            /** This is a handle to our texture data. */
            private WebGLTexture mTextureDataHandle;

            __gl __gl = new __gl();
            ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext gl;


            public LessonFourRenderer(Context activityContext)
            {
                this.gl = (ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext)(object)__gl;


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
                #endregion

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

            public void onSurfaceCreated(GL10 glUnused, javax.microedition.khronos.egl.EGLConfig config)
            {
                // Set the background clear color to black.
                gl.clearColor(0.0f, 0.0f, 0.0f, 0.0f);

                // Use culling to remove back faces.
                gl.enable(gl.CULL_FACE);

                // Enable depth testing
                gl.enable(gl.DEPTH_TEST);

                // Enable texture mapping
                gl.enable(gl.TEXTURE_2D);

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


             


                mProgramHandle = gl.createProgram(
                      new Shaders.per_pixelVertexShader(),
                      new Shaders.per_pixelFragmentShader()
                  );

                gl.bindAttribLocation(mProgramHandle, 0, "a_Position");
                gl.bindAttribLocation(mProgramHandle, 1, "a_Color");
                gl.bindAttribLocation(mProgramHandle, 2, "a_Normal");
                gl.bindAttribLocation(mProgramHandle, 3, "a_TexCoordinate");

                gl.linkProgram(mProgramHandle);

                // Define a simple shader program for our point.

                mPointProgramHandle = gl.createProgram(
                    new Shaders.pointVertexShader(),
                    new Shaders.pointFragmentShader()
                );

                gl.bindAttribLocation(mPointProgramHandle, 0, "a_Position");

                gl.linkProgram(mPointProgramHandle);


                #region loadTexture
                Func<android.graphics.Bitmap, WebGLTexture> loadTexture = (bitmap) =>
               {
                   var textureHandle = gl.createTexture();

                   // Bind to the texture in OpenGL
                   gl.bindTexture(gl.TEXTURE_2D, textureHandle);

                   // Set filtering
                   gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.NEAREST);
                   gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.NEAREST);

                   // Load the bitmap into the bound texture.
                   //gl.texImage2D(
                   GLUtils.texImage2D((int)gl.TEXTURE_2D, 0, bitmap, 0);

                   // Recycle the bitmap, since its data has been loaded into OpenGL.
                   bitmap.recycle();


                   return textureHandle;
               };
                #endregion

                #region openFileFromAssets
                Func<string, InputStream> openFileFromAssets = (string spath) =>
                {
                    InputStream value = null;
                    try
                    {
                        value = this.mActivityContext.getResources().getAssets().open(spath);
                    }
                    catch
                    {

                    }
                    return value;

                };
                #endregion


                // Read in the resource
                var bumpy_bricks_public_domain = android.graphics.BitmapFactory.decodeStream(
                    openFileFromAssets("bumpy_bricks_public_domain.jpg")
                );

                // Load the texture
                mTextureDataHandle = loadTexture(
                    bumpy_bricks_public_domain
                );

                gl.generateMipmap(gl.TEXTURE_2D);

            }

            public void onSurfaceChanged(GL10 glUnused, int width, int height)
            {
                // Set the OpenGL viewport to the same size as the surface.
                gl.viewport(0, 0, width, height);

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
                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                // Do a complete rotation every 10 seconds.
                long time = SystemClock.uptimeMillis() % 10000L;
                float angleInDegrees = (360.0f / 10000.0f) * ((int)time);

                // Set our per-vertex lighting program.
                gl.useProgram(mProgramHandle);

                // Set program handles for cube drawing.
                mMVPMatrixHandle = gl.getUniformLocation(mProgramHandle, "u_MVPMatrix");
                mMVMatrixHandle = gl.getUniformLocation(mProgramHandle, "u_MVMatrix");
                mLightPosHandle = gl.getUniformLocation(mProgramHandle, "u_LightPos");
                mTextureUniformHandle = gl.getUniformLocation(mProgramHandle, "u_Texture");

                mPositionHandle = gl.getAttribLocation(mProgramHandle, "a_Position");
                mColorHandle = gl.getAttribLocation(mProgramHandle, "a_Color");
                mNormalHandle = gl.getAttribLocation(mProgramHandle, "a_Normal");
                mTextureCoordinateHandle = gl.getAttribLocation(mProgramHandle, "a_TexCoordinate");

                // Set the active texture unit to texture unit 0.
                gl.activeTexture(gl.TEXTURE0);

                // Bind the texture to this unit.
                gl.bindTexture(gl.TEXTURE_2D, mTextureDataHandle);

                // Tell the texture uniform sampler to use this texture in the shader by binding to texture unit 0.
                gl.uniform1i(mTextureUniformHandle, 0);

                // Calculate position of the light. Rotate and then push into the distance.
                Matrix.setIdentityM(mLightModelMatrix, 0);
                Matrix.translateM(mLightModelMatrix, 0, 0.0f, 0.0f, -5.0f);
                Matrix.rotateM(mLightModelMatrix, 0, angleInDegrees, 0.0f, 1.0f, 0.0f);
                Matrix.translateM(mLightModelMatrix, 0, 0.0f, 0.0f, 2.0f);

                Matrix.multiplyMV(mLightPosInWorldSpace, 0, mLightModelMatrix, 0, mLightPosInModelSpace, 0);
                Matrix.multiplyMV(mLightPosInEyeSpace, 0, mViewMatrix, 0, mLightPosInWorldSpace, 0);

                #region drawCube
                Action drawCube =
                    delegate
                    {
                        // Pass in the position information
                        mCubePositions.position(0);
                        opengl.glVertexAttribPointer(mPositionHandle, mPositionDataSize, (int)gl.FLOAT, false,
                                0, mCubePositions);

                        gl.enableVertexAttribArray((uint)mPositionHandle);

                        // Pass in the color information
                        mCubeColors.position(0);
                        opengl.glVertexAttribPointer(mColorHandle, mColorDataSize, (int)gl.FLOAT, false,
                                0, mCubeColors);

                        gl.enableVertexAttribArray((uint)mColorHandle);

                        // Pass in the normal information
                        mCubeNormals.position(0);
                        opengl.glVertexAttribPointer(mNormalHandle, mNormalDataSize, (int)gl.FLOAT, false,
                                0, mCubeNormals);

                        gl.enableVertexAttribArray((uint)mNormalHandle);

                        // Pass in the texture coordinate information
                        mCubeTextureCoordinates.position(0);
                        opengl.glVertexAttribPointer(mTextureCoordinateHandle, mTextureCoordinateDataSize, opengl.GL_FLOAT, false,
                                0, mCubeTextureCoordinates);

                        gl.enableVertexAttribArray((uint)mTextureCoordinateHandle);

                        // This multiplies the view matrix by the model matrix, and stores the result in the MVP matrix
                        // (which currently contains model * view).
                        Matrix.multiplyMM(mMVPMatrix, 0, mViewMatrix, 0, mModelMatrix, 0);

                        // Pass in the modelview matrix.
                        gl.uniformMatrix4fv(mMVMatrixHandle, false, mMVPMatrix);

                        // This multiplies the modelview matrix by the projection matrix, and stores the result in the MVP matrix
                        // (which now contains model * view * projection).
                        Matrix.multiplyMM(mMVPMatrix, 0, mProjectionMatrix, 0, mMVPMatrix, 0);

                        // Pass in the combined matrix.
                        gl.uniformMatrix4fv(mMVPMatrixHandle, false, mMVPMatrix);

                        // Pass in the light position in eye space.        
                        gl.uniform3f(mLightPosHandle, mLightPosInEyeSpace[0], mLightPosInEyeSpace[1], mLightPosInEyeSpace[2]);

                        // Draw the cube.
                        gl.drawArrays(gl.TRIANGLES, 0, 36);
                    };
                #endregion

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

                #region drawLight
                Action drawLight =
                    delegate
                    {
                        var pointMVPMatrixHandle = gl.getUniformLocation(mPointProgramHandle, "u_MVPMatrix");
                        var pointPositionHandle = gl.getAttribLocation(mPointProgramHandle, "a_Position");

                        // Pass in the position.
                        gl.vertexAttrib3f((uint)pointPositionHandle, mLightPosInModelSpace[0], mLightPosInModelSpace[1], mLightPosInModelSpace[2]);

                        // Since we are not using a buffer object, disable vertex arrays for this attribute.
                        gl.disableVertexAttribArray((uint)pointPositionHandle);

                        // Pass in the transformation matrix.
                        Matrix.multiplyMM(mMVPMatrix, 0, mViewMatrix, 0, mLightModelMatrix, 0);
                        Matrix.multiplyMM(mMVPMatrix, 0, mProjectionMatrix, 0, mMVPMatrix, 0);
                        gl.uniformMatrix4fv(pointMVPMatrixHandle,  false, mMVPMatrix);

                        // Draw the point.
                        gl.drawArrays(gl.POINTS, 0, 1);
                    };
                #endregion

                // Draw a point to indicate the light.
                gl.useProgram(mPointProgramHandle);
                drawLight();
            }



        }

    }

}
