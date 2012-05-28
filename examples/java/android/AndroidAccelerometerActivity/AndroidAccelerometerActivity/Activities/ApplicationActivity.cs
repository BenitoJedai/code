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
using AndroidAccelerometerActivity.Library;
using ScriptCoreLib;
using ScriptCoreLib.Android;

namespace AndroidAccelerometerActivity.Activities
{
    [Description("inspired by http://webhole.net/2011/08/20/android-sdk-accelerometer-example-tutorial/")]
    public class AndroidAccelerometerActivity : Activity, SensorEventListener
    {

        public SensorManager sensorManager;

        TextView xCoor; // declare X axis object
        TextView yCoor; // declare Y axis object
        TextView zCoor; // declare Z axis object

        // running it on device:
        // attach device to usb
      
        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://www.dreamincode.net/forums/topic/130521-android-part-iii-dynamic-layouts/

            base.onCreate(savedInstanceState);

            setContentView(R.layout.main);

            xCoor = (TextView)findViewById(R.id.xcoor); // create X axis object
            yCoor = (TextView)findViewById(R.id.ycoor); // create Y axis object
            zCoor = (TextView)findViewById(R.id.zcoor); // create Z axis object

            sensorManager = (SensorManager)getSystemService(SENSOR_SERVICE);
            // add listener. The listener will be HelloAndroid (this) class
            sensorManager.registerListener(this,
                    sensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER),
                    SensorManager.SENSOR_DELAY_NORMAL);

            /*	More sensor speeds (taken from api docs)
                SENSOR_DELAY_FASTEST get sensor data as fast as possible
                SENSOR_DELAY_GAME	rate suitable for games
                SENSOR_DELAY_NORMAL	rate (default) suitable for screen orientation changes
            */


            this.ShowToast("http://jsc-solutions.net");
        }

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

                xCoor.setText("X: " + ((object)x).ToString());
                yCoor.setText("Y: " + ((object)y).ToString());
                zCoor.setText("Z: " + ((object)z).ToString());
            }
        }
    }


}
