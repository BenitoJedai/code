
using android.app;
using android.content;
using android.database;
using android.database.sqlite;
using android.provider;
using android.util;
using android.view;
using android.webkit;
using android.widget;
using AndroidOpenStreetMapViewActivity.Library;
using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Android;

using org.osmdroid.views;
using org.osmdroid.api;
using org.osmdroid.tileprovider.tilesource;
using org.osmdroid.util;

namespace AndroidOpenStreetMapViewActivity.Activities
{
    public class AndroidOpenStreetMapViewActivity : Activity
    {
        // inspired by 
        // http://android-er.blogspot.com/2012/05/openstreetmapview-openstreetmap-tools.html
        // http://android-er.blogspot.com/2012/05/prepare-java-build-path-to-osmdroid-and.html
        // http://android-er.blogspot.com/2012/05/simple-example-use-osmdroid-and-slf4j.html
        // those jars will need to end up at /libs folder!

        // C:\util\android-sdk-windows\tools\android.bat create project --package AndroidOpenStreetMapViewActivity.Activities --activity AndroidOpenStreetMapViewActivity  --target 2  --path y:\jsc.svn\examples\java\android\AndroidOpenStreetMapViewActivity\AndroidOpenStreetMapViewActivity\staging\apk\


        // running it in emulator:
        // start C:\util\android-sdk-windows\tools\android.bat avd
        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r  "y:\jsc.svn\examples\java\android\AndroidOpenStreetMapViewActivity\AndroidOpenStreetMapViewActivity\staging\bin\AndroidOpenStreetMapViewActivity-debug.apk"

        // note: rebuild could auto reinstall

        // "C:\util\android-sdk-windows\platform-tools\adb.exe" uninstall   AndroidOpenStreetMapViewActivity.Activities

        // running it on device:
        // attach device to usb
        //Z:\jsc.svn\examples\java\android\HelloAndroid>C:\util\android-sdk-windows\platform-tools\adb.exe devices
        //List of devices attached
        //3330A17632C000EC        device 

        private MapView myOpenMapView;
        private MapController myMapController;

        org.slf4j.Logger hack;

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            //requestWindowFeature(Window.FEATURE_NO_TITLE);
            //getWindow().setFlags(WindowManager_LayoutParams.FLAG_FULLSCREEN, WindowManager_LayoutParams.FLAG_FULLSCREEN);

            this.ShowToast("jsc-solutions.net\nWait while map loads...");

            setContentView(R.layout.main);

            myOpenMapView = (MapView)findViewById(R.id.openmapview);

            // http://code.google.com/p/osmdroid/source/browse/trunk/osmdroid-android/src/main/java/org/osmdroid/views/MapView.java
            // http://stackoverflow.com/questions/157119/c-sharp-can-i-override-with-derived-types

            myMapController = (MapController)((IMapView)myOpenMapView).getController();

            myOpenMapView.setTileSource(TileSourceFactory.MAPNIK);

            myOpenMapView.setBuiltInZoomControls(true);
            myOpenMapView.setMultiTouchControls(true);
            myMapController.setZoom(16);
            myMapController.setCenter(new GeoPoint(51496994, -134733));
        }

    }
}
