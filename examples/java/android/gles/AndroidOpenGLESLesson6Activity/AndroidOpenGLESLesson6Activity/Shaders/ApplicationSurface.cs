
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
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using java.io;


namespace AndroidOpenGLESLesson6Activity.Shaders
{
    using gl = WebGLRenderingContext;
    using opengl = GLES20;

    class ApplicationSurface
    {
        public ApplicationSurface(RenderingContextView v, Button button_set_min_filter, Button button_set_mag_filter, Activity OwnerActivity)
        {
            v.onsurface +=
             gl =>
             {
                 //var __gl = (ScriptCoreLib.Android.__WebGLRenderingContext)(object)gl;

                 #region fields
                 /**
                     * Store the model matrix. This matrix is used to move models from object space (where each model can be thought
                     * of being located at the center of the universe) to world space.
                     */
                 float[] mModelMatrix = new float[16];

                 /**
                  * Store the view matrix. This can be thought of as our camera. This matrix transforms world space to eye space;
                  * it positions things relative to our eye.
                  */
                 float[] mViewMatrix = new float[16];

                 /** Store the projection matrix. This is used to project the scene onto a 2D viewport. */
                 float[] mProjectionMatrix = new float[16];

                 /** Allocate storage for the final combined matrix. This will be passed into the shader program. */
                 float[] mMVPMatrix = new float[16];

                 /** Store the accumulated rotation. */
                 float[] mAccumulatedRotation = new float[16];

                 /** Store the current rotation. */
                 float[] mCurrentRotation = new float[16];

                 /** A temporary matrix. */
                 float[] mTemporaryMatrix = new float[16];

                 /** 
                  * Stores a copy of the model matrix specifically for the light position.
                  */
                 float[] mLightModelMatrix = new float[16];

                 /** Store our model data in a float buffer. */
                 FloatBuffer mCubePositions;
                 FloatBuffer mCubeNormals;
                 FloatBuffer mCubeTextureCoordinates;
                 FloatBuffer mCubeTextureCoordinatesForPlane;

                 /** This will be used to pass in the transformation matrix. */
                 ScriptCoreLib.JavaScript.WebGL.WebGLUniformLocation mMVPMatrixHandle;

                 /** This will be used to pass in the modelview matrix. */
                 ScriptCoreLib.JavaScript.WebGL.WebGLUniformLocation mMVMatrixHandle;

                 /** This will be used to pass in the light position. */
                 ScriptCoreLib.JavaScript.WebGL.WebGLUniformLocation mLightPosHandle;

                 /** This will be used to pass in the texture. */
                 ScriptCoreLib.JavaScript.WebGL.WebGLUniformLocation mTextureUniformHandle;

                 /** This will be used to pass in model position information. */
                 int mPositionHandle;

                 /** This will be used to pass in model normal information. */
                 int mNormalHandle;

                 /** This will be used to pass in model texture coordinate information. */
                 int mTextureCoordinateHandle;

                 /** How many bytes per float. */
                 int mBytesPerFloat = 4;

                 /** Size of the position data in elements. */
                 int mPositionDataSize = 3;

                 /** Size of the normal data in elements. */
                 int mNormalDataSize = 3;

                 /** Size of the texture coordinate data in elements. */
                 int mTextureCoordinateDataSize = 2;

                 /** Used to hold a light centered on the origin in model space. We need a 4th coordinate so we can get translations to work when
                  *  we multiply this by our transformation matrices. */
                 float[] mLightPosInModelSpace = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };

                 /** Used to hold the current position of the light in world space (after transformation via model matrix). */
                 float[] mLightPosInWorldSpace = new float[4];

                 /** Used to hold the transformed position of the light in eye space (after transformation via modelview matrix) */
                 float[] mLightPosInEyeSpace = new float[4];

                 /** This is a handle to our cube shading program. */
                 ScriptCoreLib.JavaScript.WebGL.WebGLProgram mProgramHandle;

                 /** This is a handle to our light point program. */
                 ScriptCoreLib.JavaScript.WebGL.WebGLProgram mPointProgramHandle;

                 /** These are handles to our texture data. */
                 ScriptCoreLib.JavaScript.WebGL.WebGLTexture mBrickDataHandle;
                 ScriptCoreLib.JavaScript.WebGL.WebGLTexture mGrassDataHandle;

                 #endregion

                 #region ontouchmove
                 // These still work without volatile, but refreshes are not guaranteed to happen.					
                 /* volatile */
                 float mDeltaX = 0;
                 /* volatile */
                 float mDeltaY = 0;

                 v.ontouchmove +=
                     (x, y) =>
                     {
                         mDeltaX += x;
                         mDeltaY += y;
                     };
                 #endregion

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

                 #region  Initialize the buffers.
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
                 #endregion


                 // Set the background clear color to black.
                 gl.clearColor(0.0f, 0.0f, 0.0f, 0.0f);

                 // Use culling to remove back faces.
                 gl.enable(gl.CULL_FACE);

                 // Enable depth testing
                 gl.enable(gl.DEPTH_TEST);

                 // Enable texture mapping
                 gl.enable(gl.TEXTURE_2D);

                 #region setLookAtM
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
                 #endregion


                 #region mProgramHandle
                 mProgramHandle = gl.createProgram(
                  new Shaders.per_pixel_tex_and_lightVertexShader(),
                  new Shaders.per_pixel_tex_and_lightFragmentShader()
                 );

                 gl.bindAttribLocation(mProgramHandle, 0, "a_Position");
                 gl.bindAttribLocation(mProgramHandle, 1, "a_Color");
                 gl.bindAttribLocation(mProgramHandle, 2, "a_TexCoordinate");

                 gl.linkProgram(mProgramHandle);
                 #endregion

                 // Define a simple shader program for our point.

                 #region mPointProgramHandle
                 mPointProgramHandle = gl.createProgram(
                     new Shaders.pointVertexShader(),
                     new Shaders.pointFragmentShader()
                 );

                 gl.bindAttribLocation(mPointProgramHandle, 0, "a_Position");

                 gl.linkProgram(mPointProgramHandle);
                 #endregion


                 #region loadTexture
                 Func<android.graphics.Bitmap, ScriptCoreLib.JavaScript.WebGL.WebGLTexture> loadTexture = (bitmap) =>
                 {
                     var textureHandle = gl.createTexture();

                     // Bind to the texture in OpenGL
                     gl.bindTexture(gl.TEXTURE_2D, textureHandle);

                     // Set filtering
                     gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.NEAREST);
                     gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.NEAREST);

                     // Load the bitmap into the bound texture.
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
                         value = OwnerActivity.getResources().getAssets().open(spath);
                     }
                     catch
                     {

                     }
                     return value;

                 };
                 #endregion


                 // cant we use knownAssets yet?
                 var stone_wall_public_domain = android.graphics.BitmapFactory.decodeStream(
                     openFileFromAssets("assets/AndroidOpenGLESLesson6Activity/stone_wall_public_domain.png")
                 );


                 var noisy_grass_public_domain = android.graphics.BitmapFactory.decodeStream(
                     openFileFromAssets("assets/AndroidOpenGLESLesson6Activity/noisy_grass_public_domain.png")
                 );

                 // Load the texture
                 mBrickDataHandle = loadTexture(
                     stone_wall_public_domain
                 );

                 gl.generateMipmap(gl.TEXTURE_2D);


                 mGrassDataHandle = loadTexture(
                   noisy_grass_public_domain
                 );

                 gl.generateMipmap(gl.TEXTURE_2D);



                 // Initialize the accumulated rotation matrix
                 Matrix.setIdentityM(mAccumulatedRotation, 0);

                 #region onresize
                 v.onresize +=
                     (width, height) =>
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
                         float far = 1000.0f;

                         Matrix.frustumM(mProjectionMatrix, 0, left, right, bottom, top, near, far);
                     };
                 #endregion


                 #region TEXTURE_MIN_FILTER
                 button_set_min_filter.AtClick(
                     delegate
                     {
                         var builder = new AlertDialog.Builder(OwnerActivity);


                         builder.setTitle("Set TEXTURE_MIN_FILTER!");
                         builder.setItems(
                             new[] {
                                    "NEAREST",
                                    "LINEAR",
                                    "NEAREST_MIPMAP_NEAREST",
                                    "NEAREST_MIPMAP_LINEAR",
                                    "LINEAR_MIPMAP_NEAREST",
                                    "LINEAR_MIPMAP_LINEAR",
                                },
                             item =>
                             {

                                 v.queueEvent(
                                     delegate
                                     {
                                         int filter;

                                         if (item == 0)
                                         {
                                             filter = (int)gl.NEAREST;
                                         }
                                         else if (item == 1)
                                         {
                                             filter = (int)gl.LINEAR;
                                         }
                                         else if (item == 2)
                                         {
                                             filter = (int)gl.NEAREST_MIPMAP_NEAREST;
                                         }
                                         else if (item == 3)
                                         {
                                             filter = (int)gl.NEAREST_MIPMAP_LINEAR;
                                         }
                                         else if (item == 4)
                                         {
                                             filter = (int)gl.LINEAR_MIPMAP_NEAREST;
                                         }
                                         else // if (item == 5)
                                         {
                                             filter = (int)gl.LINEAR_MIPMAP_LINEAR;
                                         }

                                         if (mBrickDataHandle != null)
                                             if (mGrassDataHandle != null)
                                             {
                                                 gl.bindTexture(gl.TEXTURE_2D, mBrickDataHandle);
                                                 gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, filter);
                                                 gl.bindTexture(gl.TEXTURE_2D, mGrassDataHandle);
                                                 gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, filter);

                                             }
                                     }
                                 );
                             }
                         );


                         var dialog = builder.create();

                         dialog.setOwnerActivity(OwnerActivity);
                         dialog.show();


                     }
                     );
                 #endregion

                 #region TEXTURE_MAG_FILTER
                 button_set_mag_filter.AtClick(
                     delegate
                     {
                         var builder = new AlertDialog.Builder(OwnerActivity);

                         builder.setTitle("Set TEXTURE_MAG_FILTER");
                         builder.setItems(
                             new[]{
                    	            "GL_NEAREST",
                                    "GL_LINEAR" 	
                                },
                             item =>
                             {


                                 v.queueEvent(
                                     delegate
                                     {
                                         int filter;

                                         if (item == 0)
                                         {
                                             filter = (int)gl.NEAREST;
                                         }
                                         else // if (item == 1)
                                         {
                                             filter = (int)gl.LINEAR;
                                         }

                                         if (mBrickDataHandle != null)
                                             if (mGrassDataHandle != null)
                                             {
                                                 gl.bindTexture(gl.TEXTURE_2D, mBrickDataHandle);
                                                 gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, filter);
                                                 gl.bindTexture(gl.TEXTURE_2D, mGrassDataHandle);
                                                 gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, filter);
                                             }

                                     }
                                 );
                             }
                         );

                         var dialog = builder.create();

                         dialog.setOwnerActivity(OwnerActivity);
                         dialog.show();
                     }
                 );
                 #endregion




                 #region onframe
                 v.onframe +=
                     delegate
                     {
                         gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                         // Do a complete rotation every 10 seconds.
                         long time = SystemClock.uptimeMillis() % 10000L;
                         long slowTime = SystemClock.uptimeMillis() % 100000L;
                         float angleInDegrees = (360.0f / 10000.0f) * ((int)time);
                         float slowAngleInDegrees = (360.0f / 100000.0f) * ((int)slowTime);

                         var program = mProgramHandle;
                         // Set our per-vertex lighting program.
                         gl.useProgram(program);

                         var uniforms = program.Uniforms(gl);

                         // Set program handles for cube drawing.
                         mMVPMatrixHandle = gl.getUniformLocation(program, "u_MVPMatrix");
                         mMVMatrixHandle = gl.getUniformLocation(program, "u_MVMatrix");
                         mLightPosHandle = gl.getUniformLocation(program, "u_LightPos");
                         mTextureUniformHandle = gl.getUniformLocation(program, "u_Texture");

                         mPositionHandle = gl.getAttribLocation(program, "a_Position");
                         mNormalHandle = gl.getAttribLocation(program, "a_Normal");
                         mTextureCoordinateHandle = gl.getAttribLocation(program, "a_TexCoordinate");

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
                         gl.activeTexture(gl.TEXTURE0);

                         // Bind the texture to this unit.
                         gl.bindTexture(gl.TEXTURE_2D, mBrickDataHandle);

                         // Tell the texture uniform sampler to use this texture in the shader by binding to texture unit 0.
                         gl.uniform1i(mTextureUniformHandle, 0);

                         // Pass in the texture coordinate information
                         mCubeTextureCoordinates.position(0);

                         opengl.glVertexAttribPointer(mTextureCoordinateHandle, mTextureCoordinateDataSize, (int)gl.FLOAT, false,
                                 0, mCubeTextureCoordinates);

                         #region drawCube
                         Action drawCube =
                             delegate
                             {
                                 // Pass in the position information
                                 mCubePositions.position(0);
                                 opengl.glVertexAttribPointer(mPositionHandle, mPositionDataSize, (int)gl.FLOAT, false,
                                         0, mCubePositions);

                                 gl.enableVertexAttribArray((uint)mPositionHandle);

                                 // Pass in the normal information
                                 mCubeNormals.position(0);
                                 opengl.glVertexAttribPointer(mNormalHandle, mNormalDataSize, (int)gl.FLOAT, false,
                                         0, mCubeNormals);

                                 gl.enableVertexAttribArray((uint)mNormalHandle);

                                 // This multiplies the view matrix by the model matrix, and stores the result in the MVP matrix
                                 // (which currently contains model * view).
                                 Matrix.multiplyMM(mMVPMatrix, 0, mViewMatrix, 0, mModelMatrix, 0);

                                 // Pass in the modelview matrix.
                                 gl.uniformMatrix4fv(mMVMatrixHandle, false, mMVPMatrix);

                                 // This multiplies the modelview matrix by the projection matrix, and stores the result in the MVP matrix
                                 // (which now contains model * view * projection).        
                                 Matrix.multiplyMM(mTemporaryMatrix, 0, mProjectionMatrix, 0, mMVPMatrix, 0);
                                 java.lang.System.arraycopy(mTemporaryMatrix, 0, mMVPMatrix, 0, 16);

                                 // Pass in the combined matrix.
                                 gl.uniformMatrix4fv(mMVPMatrixHandle, false, mMVPMatrix);

                                 // Pass in the light position in eye space.        
                                 gl.uniform3f(mLightPosHandle, mLightPosInEyeSpace[0], mLightPosInEyeSpace[1], mLightPosInEyeSpace[2]);

                                 // Draw the cube.k
                                 gl.drawArrays(gl.TRIANGLES, 0, 36);
                             };
                         #endregion

                         drawCube();

                         // Draw a plane
                         Matrix.setIdentityM(mModelMatrix, 0);
                         Matrix.translateM(mModelMatrix, 0, 0.0f, -2.0f, -5.0f);
                         Matrix.scaleM(mModelMatrix, 0, 25.0f, 1.0f, 25.0f);
                         Matrix.rotateM(mModelMatrix, 0, slowAngleInDegrees, 0.0f, 1.0f, 0.0f);

                         // Set the active texture unit to texture unit 0.
                         gl.activeTexture(gl.TEXTURE0);

                         // Bind the texture to this unit.
                         gl.bindTexture(gl.TEXTURE_2D, mGrassDataHandle);

                         // Tell the texture uniform sampler to use this texture in the shader by binding to texture unit 0.
                         gl.uniform1i(mTextureUniformHandle, 0);

                         // Pass in the texture coordinate information
                         mCubeTextureCoordinatesForPlane.position(0);
                         opengl.glVertexAttribPointer(mTextureCoordinateHandle, mTextureCoordinateDataSize, (int)gl.FLOAT, false,
                                 0, mCubeTextureCoordinatesForPlane);

                         gl.enableVertexAttribArray((uint)mTextureCoordinateHandle);

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
                                 Matrix.multiplyMM(mTemporaryMatrix, 0, mProjectionMatrix, 0, mMVPMatrix, 0);
                                 java.lang.System.arraycopy(mTemporaryMatrix, 0, mMVPMatrix, 0, 16);

                                 gl.uniformMatrix4fv(pointMVPMatrixHandle, false, mMVPMatrix);

                                 // Draw the point.
                                 gl.drawArrays(gl.POINTS, 0, 1);
                             };
                         #endregion

                         // Draw a point to indicate the light.
                         gl.useProgram(mPointProgramHandle);
                         drawLight();
                     };
                 #endregion

             };

        }
    }
}
