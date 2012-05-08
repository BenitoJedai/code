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
using AndroidOpenGLESLesson5Activity.Library;
using java.lang;
using java.nio;
using javax.microedition.khronos.egl;
using javax.microedition.khronos.opengles;
using ScriptCoreLib;
using System.ComponentModel;
using ScriptCoreLib.Android;

namespace AndroidOpenGLESLesson5Activity.Activities
{
    using opengl = GLES20;
    using gl = __WebGLRenderingContext;

    public class AndroidOpenGLESLesson5Activity : Activity
    {
        // port from http://www.learnopengles.com/android-lesson-five-an-introduction-to-blending/
        // Y:\opensource\github\Learn-OpenGLES-Tutorials\android\AndroidOpenGLESLessons\src\com\learnopengles\android\lesson4


        // C:\util\android-sdk-windows\tools\android.bat create project --package AndroidOpenGLESLesson5Activity.Activities --activity AndroidOpenGLESLesson5Activity  --target 2  --path y:\jsc.svn\examples\java\android\AndroidOpenGLESLesson5Activity\AndroidOpenGLESLesson5Activity\staging

        // http://developer.android.com/guide/developing/device.html#setting-up
        // running it in emulator:
        // C:\util\android-sdk-windows\tools\android.bat avd

        // note: rebuild could auto reinstall

        // running it on device:
        // attach device to usb
        // C:\util\android-sdk-windows\platform-tools\adb.exe devices
        //List of devices attached
        //3330A17632C000EC        device 

        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r "y:\jsc.svn\examples\java\android\AndroidOpenGLESLesson5Activity\AndroidOpenGLESLesson5Activity\staging\bin\AndroidOpenGLESLesson5Activity-debug.apk"

        // screenshot: home+back
        // at "F:\ScreenCapture\SC20120504-153450.png"

        // http://android-ui-utils.googlecode.com/hg/asset-studio/dist/icons-launcher.html#foreground.type=image&foreground.space.trim=0&foreground.space.pad=-0.1&crop=1&backgroundShape=none&backColor=ff0000%2C100&foreColor=000000%2C0


        /** Hold a reference to our GLSurfaceView */
        private GLSurfaceView mGLSurfaceView;

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            mGLSurfaceView = new GLSurfaceView(this);


            // Request an OpenGL ES 2.0 compatible context.
            mGLSurfaceView.setEGLContextClientVersion(2);

            // Set the renderer to our demo renderer, defined below.
            mGLSurfaceView.setRenderer(new LessonFiveRenderer(this));

            setContentView(mGLSurfaceView);

            this.ShowToast("http://jsc-solutions.net");
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


        class LessonFiveGLSurfaceView : GLSurfaceView
        {
            public LessonFiveRenderer mRenderer;

            public LessonFiveGLSurfaceView(Context context) : base(context)
            {
                nop();
            }

            private void nop()
            {
            }


            class Handler : Runnable
            {
                public LessonFiveGLSurfaceView __this;

                public void run()
                {
                    __this.mRenderer.switchMode();
                }
            }

            public override bool onTouchEvent(MotionEvent e)
            {
                if (e != null)
                {
                    if (e.getAction() == MotionEvent.ACTION_DOWN)
                    {
                        if (mRenderer != null)
                        {
                            // Ensure we call switchMode() on the OpenGL thread.
                            // queueEvent() is a method of GLSurfaceView that will do this for us.
                            queueEvent(new Handler { __this = this });

                            return true;
                        }
                    }
                }

                return base.onTouchEvent(e);
            }

            // Hides superclass method.
            public void setRenderer(LessonFiveRenderer renderer)
            {
                mRenderer = renderer;
                base.setRenderer(renderer);
            }
        }

        class LessonFiveRenderer : GLSurfaceView.Renderer
        {
            __WebGLRenderingContext gl = new __WebGLRenderingContext();




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

            /** Store our model data in a float buffer. */
            private FloatBuffer mCubePositions;
            private FloatBuffer mCubeColors;

            /** This will be used to pass in the transformation matrix. */
            private __WebGLUniformLocation mMVPMatrixHandle;

            /** This will be used to pass in model position information. */
            private int mPositionHandle;

            /** This will be used to pass in model color information. */
            private int mColorHandle;

            /** How many bytes per float. */
            private int mBytesPerFloat = 4;

            /** Size of the position data in elements. */
            private int mPositionDataSize = 3;

            /** Size of the color data in elements. */
            private int mColorDataSize = 4;

            /** This is a handle to our cube shading program. */
            private __WebGLProgram mProgramHandle;

            /** This will be used to switch between blending mode and regular mode. */
            private bool mBlending = true;

            /**
             * Initialize the model data.
             */
            public LessonFiveRenderer(Context activityContext)
            {
                mActivityContext = activityContext;

                // Define points for a cube.
                // X, Y, Z
                float[] p1p = { -1.0f, 1.0f, 1.0f };
                float[] p2p = { 1.0f, 1.0f, 1.0f };
                float[] p3p = { -1.0f, -1.0f, 1.0f };
                float[] p4p = { 1.0f, -1.0f, 1.0f };
                float[] p5p = { -1.0f, 1.0f, -1.0f };
                float[] p6p = { 1.0f, 1.0f, -1.0f };
                float[] p7p = { -1.0f, -1.0f, -1.0f };
                float[] p8p = { 1.0f, -1.0f, -1.0f };

                float[] cubePositionData = ShapeBuilder.generateCubeData(p1p, p2p, p3p, p4p, p5p, p6p, p7p, p8p, p1p.Length);

                // Points of the cube: color information
                // R, G, B, A
                float[] p1c = { 1.0f, 0.0f, 0.0f, 1.0f };		// red			
                float[] p2c = { 1.0f, 0.0f, 1.0f, 1.0f };		// magenta
                float[] p3c = { 0.0f, 0.0f, 0.0f, 1.0f };		// black
                float[] p4c = { 0.0f, 0.0f, 1.0f, 1.0f };		// blue
                float[] p5c = { 1.0f, 1.0f, 0.0f, 1.0f };		// yellow
                float[] p6c = { 1.0f, 1.0f, 1.0f, 1.0f };		// white
                float[] p7c = { 0.0f, 1.0f, 0.0f, 1.0f };		// green
                float[] p8c = { 0.0f, 1.0f, 1.0f, 1.0f };		// cyan

                float[] cubeColorData = ShapeBuilder.generateCubeData(p1c, p2c, p3c, p4c, p5c, p6c, p7c, p8c, p1c.Length);

                // Initialize the buffers.
                mCubePositions = ByteBuffer.allocateDirect(cubePositionData.Length * mBytesPerFloat)
                .order(ByteOrder.nativeOrder()).asFloatBuffer();
                mCubePositions.put(cubePositionData).position(0);

                mCubeColors = ByteBuffer.allocateDirect(cubeColorData.Length * mBytesPerFloat)
                .order(ByteOrder.nativeOrder()).asFloatBuffer();
                mCubeColors.put(cubeColorData).position(0);
            }

            

            public void switchMode()
            {
                mBlending = !mBlending;

                if (mBlending)
                {
                    // No culling of back faces
                    GLES20.glDisable(GLES20.GL_CULL_FACE);

                    // No depth testing
                    GLES20.glDisable(GLES20.GL_DEPTH_TEST);

                    // Enable blending
                    GLES20.glEnable(GLES20.GL_BLEND);
                    GLES20.glBlendFunc(GLES20.GL_ONE, GLES20.GL_ONE);
                }
                else
                {
                    // Cull back faces
                    GLES20.glEnable(GLES20.GL_CULL_FACE);

                    // Enable depth testing
                    GLES20.glEnable(GLES20.GL_DEPTH_TEST);

                    // Disable blending
                    GLES20.glDisable(GLES20.GL_BLEND);
                }
            }

            public void onSurfaceCreated(GL10 glUnused, EGLConfig config)
            {
                // Set the background clear color to black.
                gl.clearColor(0.0f, 0.0f, 0.0f, 0.0f);

                // No culling of back faces
                GLES20.glDisable(GLES20.GL_CULL_FACE);

                // No depth testing
                GLES20.glDisable(GLES20.GL_DEPTH_TEST);

                // Enable blending
                GLES20.glEnable(GLES20.GL_BLEND);
                GLES20.glBlendFunc(GLES20.GL_ONE, GLES20.GL_ONE);
                //		GLES20.glBlendEquation(GLES20.GL_FUNC_ADD);

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
                    new Shaders.colorVertexShader(),
                    new Shaders.colorFragmentShader(),
                     "a_Position", "a_Color" 
                );
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
                if (mBlending)
                {
                    gl.clear(GLES20.GL_COLOR_BUFFER_BIT);
                }
                else
                {
                    gl.clear(GLES20.GL_COLOR_BUFFER_BIT | GLES20.GL_DEPTH_BUFFER_BIT);
                }

                // Do a complete rotation every 10 seconds.
                long time = SystemClock.uptimeMillis() % 10000L;
                float angleInDegrees = (360.0f / 10000.0f) * ((int)time);

                // Set our program
                gl.useProgram(mProgramHandle);

                // Set program handles for cube drawing.
                mMVPMatrixHandle = gl.getUniformLocation(mProgramHandle, "u_MVPMatrix");
                mPositionHandle = gl.getAttribLocation(mProgramHandle, "a_Position");
                mColorHandle = gl.getAttribLocation(mProgramHandle, "a_Color");

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

                // This multiplies the view matrix by the model matrix, and stores the result in the MVP matrix
                // (which currently contains model * view).
                Matrix.multiplyMM(mMVPMatrix, 0, mViewMatrix, 0, mModelMatrix, 0);

                // This multiplies the modelview matrix by the projection matrix, and stores the result in the MVP matrix
                // (which now contains model * view * projection).
                Matrix.multiplyMM(mMVPMatrix, 0, mProjectionMatrix, 0, mMVPMatrix, 0);

                // Pass in the combined matrix.
                gl.uniformMatrix4fv(mMVPMatrixHandle, 1, false, mMVPMatrix, 0);

                // Draw the cube.
                GLES20.glDrawArrays(GLES20.GL_TRIANGLES, 0, 36);
            }



        }

    }

}
