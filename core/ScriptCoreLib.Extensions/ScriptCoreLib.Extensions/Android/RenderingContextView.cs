﻿using android.app;
using android.content;
using android.hardware;
using android.opengl;
using android.view;
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

            setDebugFlags(DEBUG_CHECK_GL_ERROR | DEBUG_LOG_GL_CALLS);

            // set the mRenderer member
            setRenderer(this);
        }



        public void onDrawFrame(javax.microedition.khronos.opengles.GL10 value)
        {
            if (onframe != null)
                onframe();
        }

        public void onSurfaceChanged(javax.microedition.khronos.opengles.GL10 arg0, int arg1, int arg2)
        {
            if (onresize != null)
                onresize(arg1, arg2);
        }

        public void onSurfaceCreated(javax.microedition.khronos.opengles.GL10 arg0, javax.microedition.khronos.egl.EGLConfig arg1)
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

                // check sensor type
                if (e.sensor.getType() == Sensor.TYPE_ACCELEROMETER)
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


                sensorManager = (SensorManager)c.getSystemService(Activity.SENSOR_SERVICE);
                // add listener. The listener will be HelloAndroid (this) class
                sensorManager.registerListener(
                    new MySensorEventListener { onaccelerometer = value }
                    ,
                        sensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER),
                        SensorManager.SENSOR_DELAY_GAME);

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

                if (e.getAction() == MotionEvent.ACTION_MOVE)
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
