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
using java.lang;
using java.nio;
using javax.microedition.khronos.egl;
using javax.microedition.khronos.opengles;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;

namespace AndroidOpenGLESLesson1Activity.Activities
{
    using opengl = GLES20;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using __gl = __WebGLRenderingContext;

    public class AndroidOpenGLESLesson1Activity : Activity
    {
        // port from http://www.learnopengles.com/android-lesson-one-getting-started/
        // see also: "Y:\opensource\github\Learn-OpenGLES-Tutorials\android\AndroidOpenGLESLessons\src\com\learnopengles\android\lesson1\LessonOneActivity.java"

        // http://developer.android.com/guide/developing/device.html#setting-up


        // running it on device:
        // attach device to usb

        /** Hold a reference to our GLSurfaceView */
        private GLSurfaceView mGLSurfaceView;

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);
            this.ToFullscreen();

            mGLSurfaceView = new GLSurfaceView(this);


            // Request an OpenGL ES 2.0 compatible context.
            mGLSurfaceView.setEGLContextClientVersion(2);

            // Set the renderer to our demo renderer, defined below.
            mGLSurfaceView.setRenderer(new LessonOneRenderer());


            setContentView(mGLSurfaceView);

            this.ShowToast("http://my.jsc-solutions.net");

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
            private WebGLUniformLocation mMVPMatrixHandle;

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

            __WebGLRenderingContext __gl = new __WebGLRenderingContext();
            ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext gl;

            public LessonOneRenderer()
            {
                this.gl = (ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext)(object)__gl;


                #region Define points for equilateral triangles.

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
                #endregion

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

            public void onSurfaceCreated(GL10 glUnused, javax.microedition.khronos.egl.EGLConfig config)
            {
                // Set the background clear color to gray.
                gl.clearColor(0.5f, 0.5f, 0.5f, 0.5f);

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



                // Create a program object and store the handle to it.
          

                var programHandle = gl.createProgram(
                    new Shaders.TriangleVertexShader(),
                    new Shaders.TriangleFragmentShader()
                );

                gl.bindAttribLocation(programHandle, 0, "a_Position");
                gl.bindAttribLocation(programHandle, 1, "a_Color");

                gl.linkProgram(programHandle);

                // Set program handles. These will later be used to pass in values to the program.
                mMVPMatrixHandle = gl.getUniformLocation(programHandle, "u_MVPMatrix");
                mPositionHandle = gl.getAttribLocation(programHandle, "a_Position");
                mColorHandle = gl.getAttribLocation(programHandle, "a_Color");

                // Tell OpenGL to use this program when rendering.
                gl.useProgram(programHandle);
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
                gl.clear(gl.DEPTH_BUFFER_BIT | gl.COLOR_BUFFER_BIT);

                // Do a complete rotation every 10 seconds.
                long time = SystemClock.uptimeMillis() % 10000L;
                float angleInDegrees = (360.0f / 10000.0f) * ((int)time);

                #region drawTriangle
                Action<FloatBuffer> drawTriangle =
                    (FloatBuffer aTriangleBuffer) =>
                    {
                        // Pass in the position information
                        aTriangleBuffer.position(mPositionOffset);
                        opengl.glVertexAttribPointer(mPositionHandle, mPositionDataSize, (int)gl.FLOAT, false,
                                mStrideBytes, aTriangleBuffer);

                        gl.enableVertexAttribArray((uint)mPositionHandle);

                        // Pass in the color information
                        aTriangleBuffer.position(mColorOffset);
                        opengl.glVertexAttribPointer(mColorHandle, mColorDataSize, (int)gl.FLOAT, false,
                                mStrideBytes, aTriangleBuffer);

                        gl.enableVertexAttribArray((uint)mColorHandle);

                        // This multiplies the view matrix by the model matrix, and stores the result in the MVP matrix
                        // (which currently contains model * view).
                        Matrix.multiplyMM(mMVPMatrix, 0, mViewMatrix, 0, mModelMatrix, 0);

                        // This multiplies the modelview matrix by the projection matrix, and stores the result in the MVP matrix
                        // (which now contains model * view * projection).
                        Matrix.multiplyMM(mMVPMatrix, 0, mProjectionMatrix, 0, mMVPMatrix, 0);

                        gl.uniformMatrix4fv(mMVPMatrixHandle, false, mMVPMatrix);
                        gl.drawArrays(gl.TRIANGLES, 0, 3);
                    };
                #endregion

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



        }
    }



}
