using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.opengl;
using android.os;
using android.util;
using android.view;
using android.widget;
using AndroidGLAccelerometerSpiralActivity.Shaders;
using java.lang;
using java.nio;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;


namespace AndroidGLAccelerometerSpiralActivity.Activities
{
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using android.hardware;
    using android.content.pm;
    //using opengl = GLES20;


    public class ApplicationActivity : Activity
    {
        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;


        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE);
            getWindow().requestFeature(Window.FEATURE_ACTION_BAR_OVERLAY);
            this.ToFullscreen();

            var v = new MyView(this);

            v.onsurface =
                gl =>
                {
                    //var __gl = (ScriptCoreLib.Android.__WebGLRenderingContext)(object)gl;



                    Log.wtf("AndroidGLAccelerometerSpiralActivity", "onsurface");

                    var buffer = gl.createBuffer();

                    gl.bindBuffer(gl.ARRAY_BUFFER, buffer);

                    gl.bufferData(gl.ARRAY_BUFFER,
                          new Float32Array(
                              -1.0f, -1.0f, 1.0f,
                              -1.0f, -1.0f, 1.0f,
                              1.0f, -1.0f, 1.0f,
                              1.0f, -1.0f, 1.0f), gl.STATIC_DRAW);


                    // Create Program


                    var currentProgram = gl.createProgram();

                    var vs = gl.createShader(new SpiralVertexShader());
                    var fs = gl.createShader(new SpiralFragmentShader());

                    gl.attachShader(currentProgram, vs);
                    gl.attachShader(currentProgram, fs);

                    gl.deleteShader(vs);
                    gl.deleteShader(fs);

                    gl.linkProgram(currentProgram);

                    var parameters_time = 0f;
                    var parameters_screenWidth = 0;
                    var parameters_screenHeight = 0;
                    var parameters_aspectX = 0.0f;
                    var parameters_aspectY = 1.0f;

                    #region onresize
                    v.onresize =
                        (width, height) =>
                        {
                            Log.wtf("AndroidGLAccelerometerSpiralActivity", "onresize");

                            parameters_screenWidth = width;
                            parameters_screenHeight = height;

                            parameters_aspectX = (float)width / (float)height;
                            parameters_aspectY = 1.0f;

                            gl.viewport(0, 0, width, height);
                        };
                    #endregion

                    var speed = 200f;
                    var xx = 0.5f;
                    var yy = 0.5f;

                    onaccelerometer +=
                        (x, y, z) =>
                        {
                            speed = 10 + 200 * x / 10f;

                            var ay = y;
                            if (y < 0)
                                ay = -y;

                            yy = (10f - ay) / 10f;

                            if (yy < 0)
                                y = 0;

                            if (yy > 10)
                                yy = 10;

                            var ax = x;
                            if (x < 0)
                                ax = -x;

                            xx = (10f - ax) / 10f;

                            if (xx < 0)
                                x = 0;

                            if (xx > 10)
                                xx = 10;
                        };

                    #region onframe
                    var framecount = 0;
                    v.onframe =
                        delegate
                        {
                            var time = parameters_time / 1000f;

                            //if (framecount == 0)
                            //Log.wtf("AndroidGLAccelerometerSpiralActivity", "onframe " + ((object)yy).ToString());

                            parameters_time += speed;

                            gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                            // Load program into GPU

                            gl.useProgram(currentProgram);

                            // Get var locations

                            var vertex_position = gl.getAttribLocation(currentProgram, "position");

                            // Set values to program variables

                            //Log.wtf("AndroidGLAccelerometerSpiralActivity", "onframe " + ((object)yy).ToString());
                            gl.uniform1f(gl.getUniformLocation(currentProgram, "ucolor_1"), xx);
                            gl.uniform1f(gl.getUniformLocation(currentProgram, "ucolor_2"), yy);

                            gl.uniform1f(gl.getUniformLocation(currentProgram, "time"), time);
                            gl.uniform2f(gl.getUniformLocation(currentProgram, "resolution"), parameters_screenWidth, parameters_screenHeight);
                            gl.uniform2f(gl.getUniformLocation(currentProgram, "aspect"), parameters_aspectX, parameters_aspectY);



                            // Render geometry

                            gl.bindBuffer(gl.ARRAY_BUFFER, buffer);
                            //opengl.glVertexAttribPointer(vertex_position, 2, (int)gl.FLOAT, false, 0, 0);
                            gl.vertexAttribPointer((uint)vertex_position, 2, gl.FLOAT, false, 0, 0);
                            gl.enableVertexAttribArray((uint)vertex_position);
                            gl.drawArrays(gl.TRIANGLES, 0, 6);
                            gl.disableVertexAttribArray((uint)vertex_position);

                            framecount++;
                        };
                    #endregion

                    Log.wtf("AndroidGLAccelerometerSpiralActivity", "onsurface done");

                };


            view = v;
            this.setContentView(v);
            this.TryHideActionbar();

            this.ShowToast("http://my.jsc-solutions.net");
        }

        public View view;

        #region MyView
        class MyView : GLSurfaceView, GLSurfaceView.Renderer
        {
            WebGLRenderingContext gl;

            public Action<WebGLRenderingContext> onsurface;
            public Action onframe;
            public Action<int, int> onresize;

            public MyView(Context c)
                : base(c)
            {
                // Create an OpenGL ES 2.0 context.
                setEGLContextClientVersion(2);

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
        }
        #endregion

        #region HideLater
        private const int HIDE_DELAY_MILLIS = 5000;

        class HideLater : View.OnSystemUiVisibilityChangeListener, Runnable
        {
            public ApplicationActivity that;

            public void run()
            {
                that.getWindow().getDecorView().setSystemUiVisibility(
                                   View.SYSTEM_UI_FLAG_HIDE_NAVIGATION | View.SYSTEM_UI_FLAG_LOW_PROFILE);
            }

            public void onSystemUiVisibilityChange(int value)
            {
                that.view.postDelayed(
                    this, HIDE_DELAY_MILLIS
                );
            }
        }


        private void TryHideActionbar()
        {
            try
            {
                //Log.wtf("AndroidGLAccelerometerSpiralActivity", "TryHideActionbar");
                var h = new HideLater { that = this };
                this.view.setOnSystemUiVisibilityChangeListener(
                   h
                    );

                h.onSystemUiVisibilityChange(0);
                //Log.wtf("AndroidGLAccelerometerSpiralActivity", "TryHideActionbar done");
            }
            catch
            {
                Log.wtf("AndroidGLAccelerometerSpiralActivity", "TryHideActionbar error");

                //throw;
            }
        }
        #endregion


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

        event Action<float, float, float> onaccelerometer
        {
            remove
            {
            }

            add
            {
                SensorManager sensorManager;


                sensorManager = (SensorManager)getSystemService(SENSOR_SERVICE);
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


    }


}
