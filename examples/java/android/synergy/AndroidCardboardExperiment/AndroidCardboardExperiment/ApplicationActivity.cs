using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using com.google.vrtoolkit.cardboard;
using javax.microedition.khronos.egl;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.Android.Manifest;
using android.opengl;
using java.nio;

namespace AndroidCardboardExperiment.Activities
{
	[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "16")]
	[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
	//[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
	public class ApplicationActivity :
		 com.google.vrtoolkit.cardboard.CardboardActivity, com.google.vrtoolkit.cardboard.CardboardView.StereoRenderer
	{
		// https://github.com/pollux-/GoogleCardboardPhotoSphere-VR-

		//		D/HeadMountedDisplayManager( 3942): Cardboard screen parameters file not found: java.io.FileNotFoundException: /storage/emulated/0/Cardboard/phone_params: open failed: ENOENT(No such file or directory)
		//D/HeadMountedDisplayManager( 3942): Cardboard device parameters file not found: java.io.FileNotFoundException: /storage/emulated/0/Cardboard/current_device_params: open failed: ENOENT(No such file or directory)
		//D/HeadMountedDisplayManager( 3942): Bundled Cardboard device parameters not found: java.io.FileNotFoundException: Cardboard/current_device_params
		//I/art( 3942): Rejecting re-init on previously-failed class java.lang.Class<com.google.vrtoolkit.cardboard.proto.CardboardDevice$DeviceParams>

		// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150413
		// X:\jsc.svn\core\ScriptCoreLib\JavaScript\WebVR\VRDevice.cs

		private static float Z_NEAR = 0.1f;
		private static float Z_FAR = 100.0f;

		private static float CAMERA_Z = 0.01f;
		private static float TIME_DELTA = 0.3f;

		private static float YAW_LIMIT = 0.12f;
		private static float PITCH_LIMIT = 0.12f;

		private static int COORDS_PER_VERTEX = 3;

		// We keep the light always position just above the user.
		private static float[] LIGHT_POS_IN_WORLD_SPACE = new float[] { 0.0f, 2.0f, 0.0f, 1.0f };

		private float[] lightPosInEyeSpace = new float[4];

		private FloatBuffer floorVertices;
		private FloatBuffer floorColors;
		private FloatBuffer floorNormals;

		private FloatBuffer cubeVertices;
		private FloatBuffer cubeColors;
		private FloatBuffer cubeFoundColors;
		private FloatBuffer cubeNormals;

		private int cubeProgram;
		private int floorProgram;

		private int cubePositionParam;
		private int cubeNormalParam;
		private int cubeColorParam;
		private int cubeModelParam;
		private int cubeModelViewParam;
		private int cubeModelViewProjectionParam;
		private int cubeLightPosParam;

		private int floorPositionParam;
		private int floorNormalParam;
		private int floorColorParam;
		private int floorModelParam;
		private int floorModelViewParam;
		private int floorModelViewProjectionParam;
		private int floorLightPosParam;

		private float[] modelCube;
		private float[] camera;
		private float[] view;
		private float[] headView;
		private float[] modelViewProjection;
		private float[] modelView;
		private float[] modelFloor;

		private int score = 0;
		private float objectDistance = 12f;
		private float floorDepth = 20f;

		private Vibrator vibrator;
		//private CardboardOverlayView overlayView;

		public void onDrawEye(Eye eye)
		{
			GLES20.glClear(GLES20.GL_COLOR_BUFFER_BIT | GLES20.GL_DEPTH_BUFFER_BIT);

			checkGLError("colorParam");

			// Apply the eye transformation to the camera.
			Matrix.multiplyMM(view, 0, eye.getEyeView(), 0, camera, 0);

			// Set the position of the light
			Matrix.multiplyMV(lightPosInEyeSpace, 0, view, 0, LIGHT_POS_IN_WORLD_SPACE, 0);

			// Build the ModelView and ModelViewProjection matrices
			// for calculating cube position and light.
			float[] perspective = eye.getPerspective(Z_NEAR, Z_FAR);
			Matrix.multiplyMM(modelView, 0, view, 0, modelCube, 0);
			Matrix.multiplyMM(modelViewProjection, 0, perspective, 0, modelView, 0);
			drawCube();

			// Set modelView for the floor, so we draw floor in the correct location
			Matrix.multiplyMM(modelView, 0, view, 0, modelFloor, 0);
			Matrix.multiplyMM(modelViewProjection, 0, perspective, 0,
			  modelView, 0);
			drawFloor();
		}

		public void drawFloor()
		{
			GLES20.glUseProgram(floorProgram);

			// Set ModelView, MVP, position, normals, and color.
			GLES20.glUniform3fv(floorLightPosParam, 1, lightPosInEyeSpace, 0);
			GLES20.glUniformMatrix4fv(floorModelParam, 1, false, modelFloor, 0);
			GLES20.glUniformMatrix4fv(floorModelViewParam, 1, false, modelView, 0);
			GLES20.glUniformMatrix4fv(floorModelViewProjectionParam, 1, false,
				modelViewProjection, 0);
			GLES20.glVertexAttribPointer(floorPositionParam, COORDS_PER_VERTEX, GLES20.GL_FLOAT,
				false, 0, floorVertices);
			GLES20.glVertexAttribPointer(floorNormalParam, 3, GLES20.GL_FLOAT, false, 0,
				floorNormals);
			GLES20.glVertexAttribPointer(floorColorParam, 4, GLES20.GL_FLOAT, false, 0, floorColors);

			GLES20.glDrawArrays(GLES20.GL_TRIANGLES, 0, 6);

			checkGLError("drawing floor");
		}

		public void onFinishFrame(Viewport value)
		{
		}

		public void onNewFrame(HeadTransform headTransform)
		{
			// Build the Model part of the ModelView matrix.
			Matrix.rotateM(modelCube, 0, TIME_DELTA, 0.5f, 0.5f, 1.0f);

			// Build the camera matrix and apply it to the ModelView.
			Matrix.setLookAtM(camera, 0, 0.0f, 0.0f, CAMERA_Z, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f);

			headTransform.getHeadView(headView, 0);

			checkGLError("onReadyToDraw");
		}

		public void onRendererShutdown()
		{
		}

		public void onSurfaceChanged(int arg0, int arg1)
		{
		}

		public void onSurfaceCreated(javax.microedition.khronos.egl.EGLConfig value)
		{
			Console.WriteLine("onSurfaceCreated");

			GLES20.glClearColor(0.1f, 0.1f, 0.1f, 0.5f); // Dark background so text shows up well.

			ByteBuffer bbVertices = ByteBuffer.allocateDirect(WorldLayoutData.CUBE_COORDS.Length * 4);
			bbVertices.order(ByteOrder.nativeOrder());
			cubeVertices = bbVertices.asFloatBuffer();
			cubeVertices.put(WorldLayoutData.CUBE_COORDS);
			cubeVertices.position(0);

			ByteBuffer bbColors = ByteBuffer.allocateDirect(WorldLayoutData.CUBE_COLORS.Length * 4);
			bbColors.order(ByteOrder.nativeOrder());
			cubeColors = bbColors.asFloatBuffer();
			cubeColors.put(WorldLayoutData.CUBE_COLORS);
			cubeColors.position(0);

			ByteBuffer bbFoundColors = ByteBuffer.allocateDirect(
				WorldLayoutData.CUBE_FOUND_COLORS.Length * 4);
			bbFoundColors.order(ByteOrder.nativeOrder());
			cubeFoundColors = bbFoundColors.asFloatBuffer();
			cubeFoundColors.put(WorldLayoutData.CUBE_FOUND_COLORS);
			cubeFoundColors.position(0);

			ByteBuffer bbNormals = ByteBuffer.allocateDirect(WorldLayoutData.CUBE_NORMALS.Length * 4);
			bbNormals.order(ByteOrder.nativeOrder());
			cubeNormals = bbNormals.asFloatBuffer();
			cubeNormals.put(WorldLayoutData.CUBE_NORMALS);
			cubeNormals.position(0);

			// make a floor
			ByteBuffer bbFloorVertices = ByteBuffer.allocateDirect(WorldLayoutData.FLOOR_COORDS.Length * 4);
			bbFloorVertices.order(ByteOrder.nativeOrder());
			floorVertices = bbFloorVertices.asFloatBuffer();
			floorVertices.put(WorldLayoutData.FLOOR_COORDS);
			floorVertices.position(0);

			ByteBuffer bbFloorNormals = ByteBuffer.allocateDirect(WorldLayoutData.FLOOR_NORMALS.Length * 4);
			bbFloorNormals.order(ByteOrder.nativeOrder());
			floorNormals = bbFloorNormals.asFloatBuffer();
			floorNormals.put(WorldLayoutData.FLOOR_NORMALS);
			floorNormals.position(0);

			ByteBuffer bbFloorColors = ByteBuffer.allocateDirect(WorldLayoutData.FLOOR_COLORS.Length * 4);
			bbFloorColors.order(ByteOrder.nativeOrder());
			floorColors = bbFloorColors.asFloatBuffer();
			floorColors.put(WorldLayoutData.FLOOR_COLORS);
			floorColors.position(0);



			Func<int, string, int> loadGLShader = (int type, string code) =>
		   {
			   int shader = GLES20.glCreateShader(type);
			   GLES20.glShaderSource(shader, code);
			   GLES20.glCompileShader(shader);

			   // Get the compilation status.
			   int[] compileStatus = new int[1];
			   GLES20.glGetShaderiv(shader, GLES20.GL_COMPILE_STATUS, compileStatus, 0);

			   // If the compilation failed, delete the shader.
			   if (compileStatus[0] == 0)
			   {
				   Console.WriteLine("Error compiling shader: " + GLES20.glGetShaderInfoLog(shader));
				   GLES20.glDeleteShader(shader);
				   shader = 0;
			   }

			   if (shader == 0)
			   {
				   throw new Exception("Error creating shader.");
			   }

			   return shader;
		   };


			int vertexShader = loadGLShader(GLES20.GL_VERTEX_SHADER, new Shaders.light_vertexVertexShader().ToString());
			int gridShader = loadGLShader(GLES20.GL_FRAGMENT_SHADER, new Shaders.grid_fragmentFragmentShader().ToString());
			int passthroughShader = loadGLShader(GLES20.GL_FRAGMENT_SHADER, new Shaders.passthrough_fragmentFragmentShader().ToString());

			cubeProgram = GLES20.glCreateProgram();
			GLES20.glAttachShader(cubeProgram, vertexShader);
			GLES20.glAttachShader(cubeProgram, passthroughShader);
			GLES20.glLinkProgram(cubeProgram);
			GLES20.glUseProgram(cubeProgram);

			checkGLError("Cube program");

			cubePositionParam = GLES20.glGetAttribLocation(cubeProgram, "a_Position");
			cubeNormalParam = GLES20.glGetAttribLocation(cubeProgram, "a_Normal");
			cubeColorParam = GLES20.glGetAttribLocation(cubeProgram, "a_Color");

			cubeModelParam = GLES20.glGetUniformLocation(cubeProgram, "u_Model");
			cubeModelViewParam = GLES20.glGetUniformLocation(cubeProgram, "u_MVMatrix");
			cubeModelViewProjectionParam = GLES20.glGetUniformLocation(cubeProgram, "u_MVP");
			cubeLightPosParam = GLES20.glGetUniformLocation(cubeProgram, "u_LightPos");

			GLES20.glEnableVertexAttribArray(cubePositionParam);
			GLES20.glEnableVertexAttribArray(cubeNormalParam);
			GLES20.glEnableVertexAttribArray(cubeColorParam);

			checkGLError("Cube program params");

			floorProgram = GLES20.glCreateProgram();
			GLES20.glAttachShader(floorProgram, vertexShader);
			GLES20.glAttachShader(floorProgram, gridShader);
			GLES20.glLinkProgram(floorProgram);
			GLES20.glUseProgram(floorProgram);

			checkGLError("Floor program");

			floorModelParam = GLES20.glGetUniformLocation(floorProgram, "u_Model");
			floorModelViewParam = GLES20.glGetUniformLocation(floorProgram, "u_MVMatrix");
			floorModelViewProjectionParam = GLES20.glGetUniformLocation(floorProgram, "u_MVP");
			floorLightPosParam = GLES20.glGetUniformLocation(floorProgram, "u_LightPos");

			floorPositionParam = GLES20.glGetAttribLocation(floorProgram, "a_Position");
			floorNormalParam = GLES20.glGetAttribLocation(floorProgram, "a_Normal");
			floorColorParam = GLES20.glGetAttribLocation(floorProgram, "a_Color");

			GLES20.glEnableVertexAttribArray(floorPositionParam);
			GLES20.glEnableVertexAttribArray(floorNormalParam);
			GLES20.glEnableVertexAttribArray(floorColorParam);

			checkGLError("Floor program params");

			GLES20.glEnable(GLES20.GL_DEPTH_TEST);

			// Object first appears directly in front of user.
			Matrix.setIdentityM(modelCube, 0);
			Matrix.translateM(modelCube, 0, 0, 0, -objectDistance);

			Matrix.setIdentityM(modelFloor, 0);
			Matrix.translateM(modelFloor, 0, 0, -floorDepth, 0); // Floor appears below user.

			checkGLError("onSurfaceCreated");
		}

		protected override void onCreate(Bundle savedInstanceState)
		{
			Console.WriteLine("onCreate");

			base.onCreate(savedInstanceState);

			//this.setRequestedOrientation(LAN
			// [javac]
			//		W:\src\AndroidCardboardExperiment\Activities\ApplicationActivity.java:23: error: FullscreenMode is not public in com.google.vrtoolkit.cardboard; cannot be accessed from outside package
			//[javac] import com.google.vrtoolkit.cardboard.FullscreenMode;
			//		[javac]                                      ^
			//{ com.google.vrtoolkit.cardboard.FullscreenMode ref0; }

			var sv = new ScrollView(this);
			var ll = new LinearLayout(this);
			//ll.setOrientation(LinearLayout.VERTICAL);
			sv.addView(ll);


			this.setContentView(sv);

			var cardboardView = new CardboardView(this).AttachTo(ll);

			cardboardView.setRenderer(this);
			setCardboardView(cardboardView);
		}

		private static void checkGLError(String label)
		{
			int error = GLES20.glGetError();
			if (error != GLES20.GL_NO_ERROR)
			{
				throw new Exception(label + ": glError " + error);
			}
		}

		public void drawCube()
		{
			GLES20.glUseProgram(cubeProgram);

			GLES20.glUniform3fv(cubeLightPosParam, 1, lightPosInEyeSpace, 0);

			// Set the Model in the shader, used to calculate lighting
			GLES20.glUniformMatrix4fv(cubeModelParam, 1, false, modelCube, 0);

			// Set the ModelView in the shader, used to calculate lighting
			GLES20.glUniformMatrix4fv(cubeModelViewParam, 1, false, modelView, 0);

			// Set the position of the cube
			GLES20.glVertexAttribPointer(cubePositionParam, COORDS_PER_VERTEX, GLES20.GL_FLOAT,
				false, 0, cubeVertices);

			// Set the ModelViewProjection matrix in the shader.
			GLES20.glUniformMatrix4fv(cubeModelViewProjectionParam, 1, false, modelViewProjection, 0);

			// Set the normal positions of the cube, again for shading
			GLES20.glVertexAttribPointer(cubeNormalParam, 3, GLES20.GL_FLOAT, false, 0, cubeNormals);



			var cc = cubeFoundColors;
			if (isLookingAtObject()) cc = cubeColors;

			GLES20.glVertexAttribPointer(cubeColorParam, 4, GLES20.GL_FLOAT, false, 0,
				cc);

			GLES20.glDrawArrays(GLES20.GL_TRIANGLES, 0, 36);
			checkGLError("Drawing cube");
		}

		// script: error JSC1000: Java : Opcode not implemented: bge.un.s at AndroidCardboardExperiment.Activities.ApplicationActivity.isLookingAtObject
		private bool isLookingAtObject()
		{
			float[] initVec = { 0, 0, 0, 1.0f };
			float[] objPositionVec = new float[4];

			// Convert object space to camera space. Use the headView from onNewFrame.
			Matrix.multiplyMM(modelView, 0, headView, 0, modelCube, 0);
			Matrix.multiplyMV(objPositionVec, 0, modelView, 0, initVec, 0);

			float pitch = (float)Math.Atan2(objPositionVec[1], -objPositionVec[2]);
			float yaw = (float)Math.Atan2(objPositionVec[0], -objPositionVec[2]);

			if (Math.Abs(pitch) < PITCH_LIMIT)
				if (Math.Abs(yaw) < YAW_LIMIT)
					return true;
			return false;
		}
	}


}


//I/dalvikvm(10527): Could not find method android.view.View.setSystemUiVisibility, referenced from method com.google.vrtoolkit.cardboard.FullscreenMode.setFullscreenMode
//W/dalvikvm(10527): VFY: unable to resolve virtual method 4054: Landroid/view/View;.setSystemUiVisibility(I)V
//D/dalvikvm(10527): VFY: replacing opcode 0x6e at 0x000c
//D/dalvikvm(10527): VFY: dead code 0x000f-000f in Lcom/google/vrtoolkit/cardboard/FullscreenMode;.setFullscreenMode()V
//I/dalvikvm(10527): Failed resolving Lcom/google/vrtoolkit/cardboard/FullscreenMode$1; interface 742 'Landroid/view/View$OnSystemUiVisibilityChangeListener;'
//W/dalvikvm(10527): Link of class 'Lcom/google/vrtoolkit/cardboard/FullscreenMode$1;' failed
//E/dalvikvm(10527): Could not find class 'com.google.vrtoolkit.cardboard.FullscreenMode$1', referenced from method com.google.vrtoolkit.cardboard.FullscreenMode.startFullscreenMode
//W/dalvikvm(10527): VFY: unable to resolve new-instance 801 (Lcom/google/vrtoolkit/cardboard/FullscreenMode$1;) in Lcom/google/vrtoolkit/cardboard/FullscreenMode;
//D/dalvikvm(10527): VFY: replacing opcode 0x22 at 0x0020
//D/dalvikvm(10527): VFY: dead code 0x0022-0027 in Lcom/google/vrtoolkit/cardboard/FullscreenMode;.startFullscreenMode()V


//E/AndroidRuntime(10273): java.lang.NoClassDefFoundError: com.google.vrtoolkit.cardboard.FullscreenMode$1
//E/AndroidRuntime(10273):        at com.google.vrtoolkit.cardboard.FullscreenMode.startFullscreenMode(FullscreenMode.java:34)
//E/AndroidRuntime(10273):        at com.google.vrtoolkit.cardboard.CardboardActivity.onCreate(CardboardActivity.java:182)
//E/AndroidRuntime(10273):        at AndroidCardboardExperiment.Activities.ApplicationActivity.onCreate(ApplicationActivity.java:346)
//E/AndroidRuntime(10273):        at android.app.Instrumentation.callActivityOnCreate(Instrumentation.java:1047)
//E/AndroidRuntime(10273):        at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:1615)
//E/AndroidRuntime(10273):        at android.app.ActivityThread.handleLaunchActivity(ActivityThread.java:1667)
//E/AndroidRuntime(10273):        at android.app.ActivityThread.access$1500(ActivityThread.java:117)
//E/AndroidRuntime(10273):        at android.app.ActivityThread$H.handleMessage(ActivityThread.java:935)
//E/AndroidRuntime(10273):        at android.os.Handler.dispatchMessage(Handler.java:99)
//E/AndroidRuntime(10273):        at android.os.Looper.loop(Looper.java:123)
//E/AndroidRuntime(10273):        at android.app.ActivityThread.main(ActivityThread.java:3687)
//E/AndroidRuntime(10273):        at java.lang.reflect.Method.invokeNative(Native Method)
//E/AndroidRuntime(10273):        at java.lang.reflect.Method.invoke(Method.java:507)
//E/AndroidRuntime(10273):        at com.android.internal.os.ZygoteInit$MethodAndArgsCaller.run(ZygoteInit.java:842)
//E/AndroidRuntime(10273):        at com.android.internal.os.ZygoteInit.main(ZygoteInit.java:600)
//E/AndroidRuntime(10273):        at dalvik.system.NativeStart.main(Native Method)
//W/ActivityManager(  129):   Force finishing activity AndroidCardboardExperiment.Activities/.ApplicationActivity