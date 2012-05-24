using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.location;
using android.os;
using android.provider;
using android.webkit;
using android.widget;
using AndroidLocationActivity.Library;
using ScriptCoreLib;
using ScriptCoreLib.Android;

namespace AndroidLocationActivity.Activities
{
    public class AndroidLocationActivity : Activity
    {
        // inspired by http://android-er.blogspot.com/2012/05/obtaining-user-location.html

        // C:\util\android-sdk-windows\tools\android.bat create project --package AndroidLocationActivity.Activities --activity AndroidLocationActivity  --target 2  --path y:\jsc.svn\examples\java\android\AndroidLocationActivity\AndroidLocationActivity\staging\
        // JSC should not explicity import all interfaces like Callback if not being defined 
        // see also: 
        // http://stackoverflow.com/questions/4055634/simple-java-question
        // http://developer.android.com/guide/developing/building/building-cmdline.html
        // http://developer.android.com/guide/developing/device.html#setting-up

        // running it in emulator:
        // start C:\util\android-sdk-windows\tools\android.bat avd
        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r  "y:\jsc.svn\examples\java\android\AndroidLocationActivity\AndroidLocationActivity\staging\bin\AndroidLocationActivity-debug.apk"

        // note: rebuild could auto reinstall

        // running it on device:
        // attach device to usb
        //Z:\jsc.svn\examples\java\android\HelloAndroid>C:\util\android-sdk-windows\platform-tools\adb.exe devices
        //List of devices attached
        //3330A17632C000EC        device 


        String PROVIDER = LocationManager.GPS_PROVIDER;
        //string PROVIDER = LocationManager.NETWORK_PROVIDER;

        LocationManager locationManager;
        double myLatitude, myLongitude;

        TextView textLatitude, textLongitude;

        LocationListener myLocationListener;

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);
            setContentView(R.layout.main);
            textLatitude = (TextView)findViewById(R.id.Latitude);
            textLongitude = (TextView)findViewById(R.id.Longitude);

            locationManager = (LocationManager)getSystemService(Context.LOCATION_SERVICE);
            Location lastLocation = locationManager.getLastKnownLocation(PROVIDER);
            if (lastLocation != null)
            {
                updateLoc(lastLocation);
            }

            this.myLocationListener = new MyLocationListener
            {
                __this = this
            };

            this.ShowToast("http://jsc-solutions.net");
        }

        private void updateLoc(Location loc)
        {
            // concat object object
            // double tostring

            textLatitude.setText("Latitude: " + ((object)loc.getLatitude()).ToString());
            textLongitude.setText("Longitude: " + ((object)loc.getLongitude()).ToString());
        }

        protected override void onResume()
        {
            // TODO Auto-generated method stub
            base.onResume();
            locationManager.requestLocationUpdates(PROVIDER, 0, 0, myLocationListener);
        }

        protected override void onPause()
        {
            // TODO Auto-generated method stub
            base.onPause();
            locationManager.removeUpdates(myLocationListener);
        }

        #region MyLocationListener
        class MyLocationListener : LocationListener
        {
            public AndroidLocationActivity __this;

            public void onLocationChanged(Location location)
            {
                __this.updateLoc(location);

            }

            public void onProviderDisabled(String provider)
            {
                // TODO Auto-generated method stub

            }

            public void onProviderEnabled(String provider)
            {
                // TODO Auto-generated method stub

            }

            public void onStatusChanged(String provider, int status, Bundle extras)
            {
                // TODO Auto-generated method stub

            }
        }
        #endregion



    }
}
