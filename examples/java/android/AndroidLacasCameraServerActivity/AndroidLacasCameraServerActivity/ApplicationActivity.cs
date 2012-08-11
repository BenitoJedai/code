using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using android.content;
using android.graphics;
using java.util;
using java.net;

namespace AndroidLacasCameraServerActivity.Activities
{
    public class ApplicationActivity : com.lacas.testsocket.TestSocketActivity
    {
        // see also: http://code.google.com/p/android-simple-httpserver-camerapicture/

        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;

        /*
         
E/AndroidRuntime(14423): java.lang.RuntimeException: Fail to connect to camera service
E/AndroidRuntime(14423):        at android.hardware.Camera.native_setup(Native Method)
E/AndroidRuntime(14423):        at android.hardware.Camera.<init>(Camera.java:306)
E/AndroidRuntime(14423):        at android.hardware.Camera.open(Camera.java:268)
E/AndroidRuntime(14423):        at com.lacas.testsocket.helper.CameraHelper$Preview.surfaceCreated(CameraHelper.java:54)
E/AndroidRuntime(14423):        at android.view.SurfaceView.updateWindow(SurfaceView.java:543)
E/AndroidRuntime(14423):        at android.view.SurfaceView.access$000(SurfaceView.java:81)
E/AndroidRuntime(14423):        at android.view.SurfaceView$3.onPreDraw(SurfaceView.java:169)
E/AndroidRuntime(14423):        at android.view.ViewTreeObserver.dispatchOnPreDraw(ViewTreeObserver.java:671)
E/AndroidRuntime(14423):        at android.view.ViewRootImpl.performTraversals(ViewRootImpl.java:1818)
E/AndroidRuntime(14423):        at android.view.ViewRootImpl.doTraversal(ViewRootImpl.java:998)
E/AndroidRuntime(14423):        at android.view.ViewRootImpl$TraversalRunnable.run(ViewRootImpl.java:4212)
E/AndroidRuntime(14423):        at android.view.Choreographer$CallbackRecord.run(Choreographer.java:725)
E/AndroidRuntime(14423):        at android.view.Choreographer.doCallbacks(Choreographer.java:555)
E/AndroidRuntime(14423):        at android.view.Choreographer.doFrame(Choreographer.java:525)
E/AndroidRuntime(14423):        at android.view.Choreographer$FrameDisplayEventReceiver.run(Choreographer.java:711)
E/AndroidRuntime(14423):        at android.os.Handler.handleCallback(Handler.java:615)
E/AndroidRuntime(14423):        at android.os.Handler.dispatchMessage(Handler.java:92)
E/AndroidRuntime(14423):        at android.os.Looper.loop(Looper.java:137)
E/AndroidRuntime(14423):        at android.app.ActivityThread.main(ActivityThread.java:4745)
E/AndroidRuntime(14423):        at java.lang.reflect.Method.invokeNative(Native Method)
E/AndroidRuntime(14423):        at java.lang.reflect.Method.invoke(Method.java:511)
E/AndroidRuntime(14423):        at com.android.internal.os.ZygoteInit$MethodAndArgsCaller.run(ZygoteInit.java:786)
E/AndroidRuntime(14423):        at com.android.internal.os.ZygoteInit.main(ZygoteInit.java:553)
E/AndroidRuntime(14423):        at dalvik.system.NativeStart.main(Native Method)
         * 
         * 
         * */

        public static string getLocalIpAddress()
        {
            var value = "";

            try
            {
                for (Enumeration en = NetworkInterface.getNetworkInterfaces(); en.hasMoreElements(); )
                {
                    NetworkInterface intf = (NetworkInterface)en.nextElement();
                    for (Enumeration enumIpAddr = intf.getInetAddresses(); enumIpAddr.hasMoreElements(); )
                    {
                        InetAddress inetAddress = (InetAddress)enumIpAddr.nextElement();

                        //Log.wtf("getLocalIpAddress", inetAddress.getHostAddress().ToString());

                        var v6 = inetAddress is Inet6Address;

                        if (v6)
                        {
                        }
                        else if (!inetAddress.isLoopbackAddress())
                        {
                            if (value == "")
                                value = inetAddress.getHostAddress().ToString();
                        }
                    }
                }
            }
            catch
            {
            }

            if (value == "")
            {
                // no wifi
                value = "127.0.0.1";
            }

            return value;
        }

        public class DrawOnTop : View
        {
            public DrawOnTop(Context context)
                : base(context)
            {
            }

            public string text;

            protected override void onDraw(Canvas canvas)
            {

                Paint paint = new Paint();

                paint.setStyle(Paint.Style.STROKE);
                paint.setColor(Color.RED);
                paint.setTextSize(30);

                canvas.drawText(text, 10, 60, paint);

                base.onDraw(canvas);
            }
        }

        public override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var mDraw = new DrawOnTop(this)
               {
                   text = getLocalIpAddress() + ":1112"
               };

            addContentView(mDraw, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WRAP_CONTENT, ViewGroup.LayoutParams.WRAP_CONTENT));
        }


    }


}
