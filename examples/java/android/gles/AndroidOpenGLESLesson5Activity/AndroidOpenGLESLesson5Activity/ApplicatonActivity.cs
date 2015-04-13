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
using System.ComponentModel;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;

namespace AndroidOpenGLESLesson5Activity.Activities
{
	using opengl = android.opengl.GLES20;
	using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
	using __gl = ScriptCoreLib.Android.WebGL.__WebGLRenderingContext;
	using f = System.Single;
	using android.opengl;

	public class AndroidOpenGLESLesson5Activity : Activity
	{
		// port from http://www.learnopengles.com/android-lesson-five-an-introduction-to-blending/
		// Y:\opensource\github\Learn-OpenGLES-Tutorials\android\AndroidOpenGLESLessons\src\com\learnopengles\android\lesson4

		// http://android-ui-utils.googlecode.com/hg/asset-studio/dist/icons-launcher.html#foreground.type=image&foreground.space.trim=0&foreground.space.pad=-0.1&crop=1&backgroundShape=none&backColor=ff0000%2C100&foreColor=000000%2C0

		/** Hold a reference to our GLSurfaceView */
		private LessonFiveGLSurfaceView mGLSurfaceView;

		protected override void onCreate(global::android.os.Bundle savedInstanceState)
		{
			base.onCreate(savedInstanceState);

			//this.ToFullscreen();

			mGLSurfaceView = new LessonFiveGLSurfaceView(this);

			var r = new LessonFiveRenderer(this);

			mGLSurfaceView.ontouchdown +=
				delegate
				{
					r.switchMode();

				};

			// Request an OpenGL ES 2.0 compatible context.
			mGLSurfaceView.setEGLContextClientVersion(2);

			// Set the renderer to our demo renderer, defined below.
			mGLSurfaceView.setRenderer(r);

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


		class LessonFiveGLSurfaceView : GLSurfaceView
		{
			public LessonFiveGLSurfaceView(Context context)
				: base(context)
			{
				nop();
			}

			private void nop()
			{
			}

			#region ontouchdown
			public event Action ontouchdown;

			public override bool onTouchEvent(MotionEvent e)
			{
				if (e != null)
				{
					if (e.getAction() == MotionEvent.ACTION_DOWN)
					{

						// Ensure we call switchMode() on the OpenGL thread.
						// queueEvent() is a method of GLSurfaceView that will do this for us.

						// X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Android\Extensions\GLSurfaceViewExtensions.cs
						this.queueEvent(
							() =>
							{
								if (ontouchdown != null)
									ontouchdown();

							}
						);

						return true;
					}
				}

				return base.onTouchEvent(e);
			}
			#endregion



		}

		class LessonFiveRenderer : GLSurfaceView.Renderer
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

			/** Store our model data in a float buffer. */
			private FloatBuffer mCubePositions;
			private FloatBuffer mCubeColors;

			/** This will be used to pass in the transformation matrix. */
			private WebGLUniformLocation mMVPMatrixHandle;

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
			private WebGLProgram mProgramHandle;

			/** This will be used to switch between blending mode and regular mode. */
			private bool mBlending = true;

			__gl __gl = new __gl();
			ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext gl;



			//script: error JSC1000: Java : Opcode not implemented: stelem.r4 at AndroidOpenGLESLesson5Activity.Activities.AndroidOpenGLESLesson5Activity+LessonFiveRenderer+<>c.<.ctor>b__17_0

			public LessonFiveRenderer(Context activityContext)
			{
				this.gl = (ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext)(object)__gl;

				mActivityContext = activityContext;

				#region generateCubeData
				Func<f[], f[], f[], f[], f[], f[], f[], f[], int, f[]> generateCubeData =
					(f[] point1,
					 f[] point2,
					 f[] point3,
					 f[] point4,
					 f[] point5,
					 f[] point6,
					 f[] point7,
					 f[] point8,
					 int elementsPerPoint) =>
					{
						// Given a cube with the points defined as follows:
						// front left top, front right top, front left bottom, front right bottom,
						// back left top, back right top, back left bottom, back right bottom,		
						// return an array of 6 sides, 2 triangles per side, 3 vertices per triangle, and 4 floats per vertex.
						int FRONT = 0;
						int RIGHT = 1;
						int BACK = 2;
						int LEFT = 3;
						int TOP = 4;
						int BOTTOM = 5;

						int size = elementsPerPoint * 6 * 6;
						float[] cubeData = new float[size];

						for (int face = 0; face < 6; face++)
						{
							// Relative to the side, p1 = top left, p2 = top right, p3 = bottom left, p4 = bottom right
							float[] p1, p2, p3, p4;

							// Select the points for this face
							if (face == FRONT)
							{
								p1 = point1; p2 = point2; p3 = point3; p4 = point4;
							}
							else if (face == RIGHT)
							{
								p1 = point2; p2 = point6; p3 = point4; p4 = point8;
							}
							else if (face == BACK)
							{
								p1 = point6; p2 = point5; p3 = point8; p4 = point7;
							}
							else if (face == LEFT)
							{
								p1 = point5; p2 = point1; p3 = point7; p4 = point3;
							}
							else if (face == TOP)
							{
								p1 = point5; p2 = point6; p3 = point1; p4 = point2;
							}
							else // if (side == BOTTOM)
							{
								p1 = point8; p2 = point7; p3 = point4; p4 = point3;
							}

							// In OpenGL counter-clockwise winding is default. This means that when we look at a triangle, 
							// if the points are counter-clockwise we are looking at the "front". If not we are looking at
							// the back. OpenGL has an optimization where all back-facing triangles are culled, since they
							// usually represent the backside of an object and aren't visible anyways.

							// Build the triangles
							//  1---3,6
							//  | / |
							// 2,4--5
							int offset = face * elementsPerPoint * 6;

							for (int i = 0; i < elementsPerPoint; i++) { cubeData[offset++] = p1[i]; }
							for (int i = 0; i < elementsPerPoint; i++) { cubeData[offset++] = p3[i]; }
							for (int i = 0; i < elementsPerPoint; i++) { cubeData[offset++] = p2[i]; }
							for (int i = 0; i < elementsPerPoint; i++) { cubeData[offset++] = p3[i]; }
							for (int i = 0; i < elementsPerPoint; i++) { cubeData[offset++] = p4[i]; }
							for (int i = 0; i < elementsPerPoint; i++) { cubeData[offset++] = p2[i]; }
						}

						return cubeData;
					};
				#endregion


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

				float[] cubePositionData = generateCubeData(p1p, p2p, p3p, p4p, p5p, p6p, p7p, p8p, p1p.Length);

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

				float[] cubeColorData = generateCubeData(p1c, p2c, p3c, p4c, p5c, p6c, p7c, p8c, p1c.Length);

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
					gl.disable(gl.CULL_FACE);

					// No depth testing
					gl.disable(gl.DEPTH_TEST);

					// Enable blending
					gl.enable(gl.BLEND);
					gl.blendFunc(gl.ONE, gl.ONE);
				}
				else
				{
					// Cull back faces
					gl.enable(gl.CULL_FACE);

					// Enable depth testing
					gl.enable(gl.DEPTH_TEST);

					// Disable blending
					gl.disable(gl.BLEND);
				}
			}

			public void onSurfaceCreated(GL10 glUnused, javax.microedition.khronos.egl.EGLConfig config)
			{
				// Set the background clear color to black.
				gl.clearColor(0.0f, 0.0f, 0.0f, 0.0f);

				// No culling of back faces
				gl.disable(gl.CULL_FACE);

				// No depth testing
				gl.disable(gl.DEPTH_TEST);

				// Enable blending
				gl.enable(gl.BLEND);
				gl.blendFunc(gl.ONE, gl.ONE);
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



				mProgramHandle = gl.createProgram(
				  new Shaders.colorVertexShader(),
				  new Shaders.colorFragmentShader()
			  );

				gl.bindAttribLocation(mProgramHandle, 0, "a_Position");
				gl.bindAttribLocation(mProgramHandle, 1, "a_Color");

				gl.linkProgram(mProgramHandle);

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
					gl.clear(gl.COLOR_BUFFER_BIT);
				}
				else
				{
					gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);
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

				#region drawCube
				Action drawCube =
					delegate
					{
						// Pass in the position information
						mCubePositions.position(0);


						GLES20.glVertexAttribPointer(mPositionHandle, mPositionDataSize, (int)gl.FLOAT, false,
								0, mCubePositions);

						gl.enableVertexAttribArray((uint)mPositionHandle);

						// Pass in the color information
						mCubeColors.position(0);
						GLES20.glVertexAttribPointer(mColorHandle, mColorDataSize, (int)gl.FLOAT, false,
								0, mCubeColors);

						gl.enableVertexAttribArray((uint)mColorHandle);

						// This multiplies the view matrix by the model matrix, and stores the result in the MVP matrix
						// (which currently contains model * view).
						Matrix.multiplyMM(mMVPMatrix, 0, mViewMatrix, 0, mModelMatrix, 0);

						// This multiplies the modelview matrix by the projection matrix, and stores the result in the MVP matrix
						// (which now contains model * view * projection).
						Matrix.multiplyMM(mMVPMatrix, 0, mProjectionMatrix, 0, mMVPMatrix, 0);

						// Pass in the combined matrix.
						gl.uniformMatrix4fv(mMVPMatrixHandle, false, mMVPMatrix);

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
			}




		}

	}

}
