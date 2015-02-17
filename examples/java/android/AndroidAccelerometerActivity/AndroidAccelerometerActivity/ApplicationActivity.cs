using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using android.app;
using android.hardware;
using android.provider;
using android.webkit;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using android.content.pm;

namespace AndroidAccelerometerActivity.Activities
{

    [Description("inspired by http://webhole.net/2011/08/20/android-sdk-accelerometer-example-tutorial/")]
    public class AndroidAccelerometerActivity : Activity
    {


        TextView xCoor; // declare X axis object
        TextView yCoor; // declare Y axis object
        TextView zCoor; // declare Z axis object

        // running it on device:
        // attach device to usb

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://www.dreamincode.net/forums/topic/130521-android-part-iii-dynamic-layouts/

            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            xCoor = new TextView(this).AttachTo(ll);
            yCoor = new TextView(this).AttachTo(ll);
            zCoor = new TextView(this).AttachTo(ll);
            setContentView(sv);

            this.onaccelerometer +=
                (x, y, z) =>
                {
                    xCoor.setText("X: " + ((object)x).ToString());
                    yCoor.setText("Y: " + ((object)y).ToString());
                    zCoor.setText("Z: " + ((object)z).ToString());
                };



            //setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE);

            //this.ShowToast("http://jsc-solutions.net");
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
                //[javac]
                //           Compiling 610 source files to W:\bin\classes
                //   [javac] W:\src\AndroidAccelerometerActivity\Activities\AndroidAccelerometerActivity_MySensorEventListener.java:43: error: bad operand types for binary operator '>'
                //[javac]             if (((this.onaccelerometer > null)))


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
