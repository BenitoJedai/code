
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
using AndroidNeHeLesson01Activity.Library;
using java.lang;
using java.nio;
using javax.microedition.khronos.egl;
using javax.microedition.khronos.opengles;
using ScriptCoreLib;
using ScriptCoreLib.Android;

namespace AndroidNeHeLesson01Activity.Activities
{
    using opengl = GLES20;
    using gl = __WebGLRenderingContext;

    public class AndroidNeHeLesson01Activity : Activity
    {
        // Y:\opensource\googlecode\nehe\lessons\android\lesson01\src\net\gamedev\nehe\android\lesson01
        // "Y:\jsc.svn\examples\javascript\WebGLLesson01\WebGLLesson01.sln"

        // C:\util\android-sdk-windows\tools\android.bat create project --package AndroidNeHeLesson01Activity.Activities --activity AndroidNeHeLesson01Activity  --target 2  --path y:\jsc.svn\examples\java\android\AndroidNeHeLesson01Activity\AndroidNeHeLesson01Activity\staging

        // http://developer.android.com/guide/developing/device.html#setting-up
        // running it in emulator:
        // C:\util\android-sdk-windows\tools\android.bat avd

        // note: rebuild could auto reinstall

        // running it on device:
        // attach device to usb
        // C:\util\android-sdk-windows\platform-tools\adb.exe devices
        //List of devices attached
        //3330A17632C000EC        device 

        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r "y:\jsc.svn\examples\java\android\AndroidNeHeLesson01Activity\AndroidNeHeLesson01Activity\staging\bin\AndroidNeHeLesson01Activity-debug.apk"

        // screenshot: home+back
        // at "F:\ScreenCapture\SC20120504-153450.png"

        // http://android-ui-utils.googlecode.com/hg/asset-studio/dist/icons-launcher.html#foreground.type=image&foreground.space.trim=0&foreground.space.pad=-0.1&crop=1&backgroundShape=none&backColor=ff0000%2C100&foreColor=000000%2C0


        /** Hold a reference to our GLSurfaceView */

        public GLSurfaceView glSurfaceView;


        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);



            glSurfaceView = new GLSurfaceView(this);

            // http://www.learnopengles.com/android-emulator-now-supports-native-opengl-es2-0/


            //we definitely want a GLES 2 context
            glSurfaceView.setEGLContextClientVersion(2);
            //and create our own renderer
            glSurfaceView.setRenderer(new Lesson01Renderer(this));

            setContentView(glSurfaceView);


            this.ShowToast("http://jsc-solutions.net");
        }


        #region pause

        protected override void onResume()
        {
            // The activity must call the GL surface view's onResume() on activity onResume().
            base.onResume();
            glSurfaceView.onResume();
        }


        protected override void onPause()
        {
            // The activity must call the GL surface view's onPause() on activity onPause().
            base.onPause();
            glSurfaceView.onPause();
        }

        #endregion




    }


    public class Lesson01Renderer : GLSurfaceView.Renderer
    {


        // needed for resource loading
        private Context context = null;

    
        private int positionAttribLocation = 0;

        private int vertexVBO = 0;

        private int mFrameCount = 0;

        private long mStartTimeNS;

        private FloatBuffer vertices;

        public Lesson01Renderer(Context context)
        {
            this.context = context;
            mStartTimeNS = java.lang.System.nanoTime();
        }

        public void onDrawFrame(GL10 arg0)
        {
            //measure performance
            ++mFrameCount;
            if (mFrameCount % 50 == 0)
            {
                long now = java.lang.System.nanoTime();
                double msPerFrame = (now - mStartTimeNS) / 1e6 / mFrameCount;
                //Log.i("NeHe", "ms per frame: " + msPerFrame +
                //       " (fps: " + (1000 / msPerFrame) + ")");
                mFrameCount = 0;
                mStartTimeNS = now;
            }

            //draw
            gl.clearColor(0.0f, 0.0f, 0.0f, 0.0f);
            gl.clear(opengl.GL_COLOR_BUFFER_BIT);

            opengl.glBindBuffer(opengl.GL_ARRAY_BUFFER, vertexVBO);
            gl.vertexAttribPointer(0, 3, opengl.GL_FLOAT, false, 0, vertices);
            gl.enableVertexAttribArray(0);

            gl.drawArrays(opengl.GL_TRIANGLES, 0, 3);
        }


      

        // all setup and data loading goes here
        public void onSurfaceCreated(GL10 arg0, EGLConfig arg1)
        {
            var shaderProgram = gl.createProgram();


            var vs = gl.createShader( new Shaders.GeometryVertexShader() );
            var fs = gl.createShader(  new Shaders.GeometryVertexShader() );


            gl.attachShader(shaderProgram, vs);
            gl.attachShader(shaderProgram, fs);


            gl.linkProgram(shaderProgram);

            
            gl.useProgram(shaderProgram);
            positionAttribLocation = gl.getAttribLocation(shaderProgram, "position");

            // setup geometry
            float[] verticesData = 
            { 
                0.0f, 0.5f, 0.0f, 
                -0.5f, -0.5f, 0.0f, 
                0.5f,  -0.5f, 0.0f 
            };

            vertices = ByteBuffer
                    .allocateDirect(verticesData.Length * 4)
                    .order(ByteOrder.nativeOrder()).asFloatBuffer();
            vertices.put(verticesData).position(0);
        }

        // resizing or reorienting the screen has happened,
        // adjust projection matrices
        public void onSurfaceChanged(GL10 arg0, int arg1, int arg2)
        {

        }



        __WebGLRenderingContext gl = new __WebGLRenderingContext();


  
     

    


    }


}
