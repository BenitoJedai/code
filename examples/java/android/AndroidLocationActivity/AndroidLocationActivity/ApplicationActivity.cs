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
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;

namespace AndroidLocationActivity.Activities
{

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "21")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class AndroidLocationActivity : Activity
    {
        // inspired by http://android-er.blogspot.com/2012/05/obtaining-user-location.html

        // C:\util\android-sdk-windows\tools\android.bat create project --package AndroidLocationActivity.Activities --activity AndroidLocationActivity  --target 2  --path y:\jsc.svn\examples\java\android\AndroidLocationActivity\AndroidLocationActivity\staging\
        // see also: 
        // http://stackoverflow.com/questions/4055634/simple-java-question
        // http://developer.android.com/guide/developing/building/building-cmdline.html
        // http://developer.android.com/guide/developing/device.html#setting-up




        string PROVIDER = LocationManager.GPS_PROVIDER;
        //string PROVIDER = LocationManager.prov;
        //string PROVIDER = LocationManager.NETWORK_PROVIDER;

        LocationManager locationManager;
        double myLatitude, myLongitude;

        TextView textLatitude, textLongitude;

        LocationListener myLocationListener;

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);
            var ll = new LinearLayout(this);
            ll.setOrientation(LinearLayout.VERTICAL);

            textLatitude = new TextView(this).AttachTo(ll);
            textLongitude = new TextView(this).AttachTo(ll);

            textLatitude.setText("?");
 
            setContentView(ll);

            locationManager = (LocationManager)this.getSystemService(Context.LOCATION_SERVICE);
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
