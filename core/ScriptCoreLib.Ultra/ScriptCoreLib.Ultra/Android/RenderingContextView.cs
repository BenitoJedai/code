extern alias globalandroid;

using globalandroid::android.app;
using globalandroid::android.content;
using globalandroid::android.hardware;
using globalandroid::android.opengl;
using globalandroid::android.view;
using globalandroid::javax.microedition.khronos.opengles;


using ScriptCoreLib.JavaScript.WebGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android
{
    // this will require augmented ScriptCoreLibAndroid build
    // what about cleanroom build? such types might not be available yet!
    public class RenderingContextView : GLSurfaceView, GLSurfaceView.Renderer, ISurface
    {
        // Error	7	'ScriptCoreLib.Android.RenderingContextView' does not implement interface member 
        // 'android.opengl.GLSurfaceView.Renderer.onSurfaceCreated(javax.microedition.khronos.opengles.GL10, android.opengl.EGLConfig)'	X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Android\RenderingContextView.cs	21	18	ScriptCoreLib.Ultra

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322

        WebGLRenderingContext gl;

        public event Action<WebGLRenderingContext> onsurface;
        public event Action onframe;
        public event Action<int, int> onresize;

        Context c;

        public RenderingContextView(Context c)
            : base(c)
        {
            this.c = c;

            // Create an OpenGL ES 2.0 context.
            setEGLContextClientVersion(2);

            var DEBUG_CHECK_GL_ERROR = 1;
            var DEBUG_LOG_GL_CALLS = 2;

            setDebugFlags(DEBUG_CHECK_GL_ERROR | DEBUG_LOG_GL_CALLS);

            // set the mRenderer member
            setRenderer(this);
        }



        public void onDrawFrame(GL10 value)
        {
            if (onframe != null)
                onframe();
        }

        public void onSurfaceChanged(GL10 arg0, int arg1, int arg2)
        {
            if (onresize != null)
                onresize(arg1, arg2);
        }

        public void onSurfaceCreated(GL10 arg0, globalandroid::javax.microedition.khronos.egl.EGLConfig arg1)
        {
            gl = new WebGLRenderingContext();
            if (onsurface != null)
                onsurface(gl);
        }

        #region onaccelerometer
        class MySensorEventListener : SensorEventListener
        {
            public Action<float, float, float> onaccelerometer;

            public void onAccuracyChanged(Sensor sensor, int accuracy)
            {

            }
            public void onSensorChanged(SensorEvent e)
            {
                var TYPE_ACCELEROMETER = 1;

                // check sensor type
                if (e.sensor.getType() == TYPE_ACCELEROMETER)
                {

                    // assign directions
                    float x = e.values[0];
                    float y = e.values[1];
                    float z = e.values[2];

                    if (onaccelerometer != null)
                        onaccelerometer(x, y, z);
                }
            }
        }

        public event Action<float, float, float> onaccelerometer
        {
            remove
            {
            }

            add
            {
                SensorManager sensorManager;

                // http://developer.android.com/reference/android/app/Activity.html

                var SENSOR_SERVICE = "sensor";
                var SENSOR_DELAY_GAME = 1;
                var TYPE_ACCELEROMETER = 1;

                sensorManager = (SensorManager)c.getSystemService(SENSOR_SERVICE);
                // add listener. The listener will be HelloAndroid (this) class
                sensorManager.registerListener(
                    new MySensorEventListener { onaccelerometer = value }
                    ,
                        sensorManager.getDefaultSensor(TYPE_ACCELEROMETER),
                        SENSOR_DELAY_GAME);

                /*	More sensor speeds (taken from api docs)
                    SENSOR_DELAY_FASTEST get sensor data as fast as possible
                    SENSOR_DELAY_GAME	rate suitable for games
                    SENSOR_DELAY_NORMAL	rate (default) suitable for screen orientation changes
                */
            }
        }
        #endregion

        public event Action<float, float> ontouchmove;

        // Offsets for touch events	 
        private float mPreviousX;
        private float mPreviousY;

        public float mDensity;

        public override bool onTouchEvent(MotionEvent e)
        {
            if (e != null)
            {
                float x = e.getX();
                float y = e.getY();

                var MotionEvent_ACTION_MOVE = 7;
                if (e.getAction() == MotionEvent_ACTION_MOVE)
                {

                    float deltaX = (x - mPreviousX) / mDensity / 2f;
                    float deltaY = (y - mPreviousY) / mDensity / 2f;

                    if (ontouchmove != null)
                        ontouchmove(deltaX, deltaY);
                    //mRenderer.mDeltaX += deltaX;
                    //mRenderer.mDeltaY += deltaY;
                }

                mPreviousX = x;
                mPreviousY = y;

                return true;
            }

            return base.onTouchEvent(e);
        }
    }

}
