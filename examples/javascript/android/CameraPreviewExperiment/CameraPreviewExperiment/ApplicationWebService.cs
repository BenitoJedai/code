using android.app;
using android.content.pm;
using android.graphics;
using android.view;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml.Linq;

namespace CameraPreviewExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService
    {

        //Caused by: java.lang.RuntimeException: android-81d176f57160fb0c
        //       at ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__Socket.get_RemoteEndPoint(__Socket.java:91)
        //       at CameraPreviewExperiment.Activities.ApplicationWebServiceActivity___c__DisplayClass22._CreateServer_b__1d(ApplicationWebServiceActivity___c__DisplayClass22.java:153)
        //       ... 19 more
        //Caused by: java.net.UnknownHostException: android-81d176f57160fb0c
        //       at java.net.InetAddress.lookupHostByName(InetAddress.java:497)
        //       at java.net.InetAddress.getAllByNameImpl(InetAddress.java:294)
        //       at java.net.InetAddress.getByName(InetAddress.java:325)
        //       at ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__Socket.get_RemoteEndPoint(__Socket.java:87)
        //       ... 20 more

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            var i = int.Parse(e);
            // JSC shold use chrome omnisearch to tell about running network apps

            // http://stackoverflow.com/questions/14815103/android-streaming-the-camera-as-mjpeg
            // http://stackoverflow.com/questions/2456802/android-camera-preview
            // https://github.com/commonsguy/cw-advandroid/blob/master/Camera/Preview/src/com/commonsware/android/camera/PreviewDemo.java

            // reactivate camera and give me frames
            //foo.Invoke(y);
            foo.Invoke(i, y, 60);

            // Send it back to the caller.
            //y(e);
        }

        public void getNumberOfCameras(Action<string> yield)
        {
            // http://stackoverflow.com/questions/10679261/accessing-front-and-back-camera-in-android-at-the-same-time
            // http://stackoverflow.com/questions/12382322/is-it-possible-to-use-front-and-back-camera-at-same-time-in-android

            var i = android.hardware.Camera.getNumberOfCameras();

            yield("" + i);
        }

        public void WebMethod2Long(string e, Action<string> y)
        {
            //            I/DEBUG   (17693): *** *** *** *** *** *** *** *** *** *** *** *** *** *** *** ***
            //I/DEBUG   (17693): Build fingerprint: 'samsung/GT-I9000/GT-I9000:2.3.3/GINGERBREAD/XWJVN:user/release-keys'
            //I/DEBUG   (17693): pid: 18667, tid: 18690  >>> /system/bin/mediaserver <<<
            //I/DEBUG   (17693): signal 11 (SIGSEGV), code 1 (SEGV_MAPERR), fault addr 00000000
            //I/DEBUG   (17693):  r0 000628b8  r1 00000000  r2 726a2622  r3 726a2622

            var i = int.Parse(e);

            y("WebMethod2Long entering " + new { i });

            // throttle the client
            Thread.Sleep(11);

            y("WebMethod2Long entered");

            // JSC shold use chrome omnisearch to tell about running network apps

            // http://stackoverflow.com/questions/14815103/android-streaming-the-camera-as-mjpeg
            // http://stackoverflow.com/questions/2456802/android-camera-preview
            // https://github.com/commonsguy/cw-advandroid/blob/master/Camera/Preview/src/com/commonsware/android/camera/PreviewDemo.java

            // reactivate camera and give me frames
            foo.Invoke(i, y, 60);

            // Send it back to the caller.
            //y(e);

            y("WebMethod2Long exit");
        }
    }

    #region WebMethod2
    public sealed partial class ApplicationWebService
    {
        public /* will not be part of web service itself */ void Handler(WebServiceHandler h)
        {
            var Accepts = h.Context.Request.Headers["Accept"];

            //if (h.Context.Request.Path == "/xml")

            if (Accepts != null)
                if (Accepts.Contains("text/event-stream"))
                {
                    h.Context.Response.ContentType = "text/event-stream";


                    // A potentially dangerous Request.QueryString value was detected from the client (e="<client value="15.12...").
                    //var _e_xml = h.Context.Request.RawUrl
                    //    .SkipUntilLastOrEmpty("?")
                    //    .SkipUntilOrEmpty("e=")
                    //    .TakeUntilIfAny("&");

                    //  method: System.String get_RawUrl()
                    //var _e_xml_decoded = HttpUtility.UrlDecode(_e_xml);

                    //var _e = XElement.Parse(_e_xml_decoded);

                    var e = h.Context.Request.QueryString["e"];

                    this.WebMethod2Long(
                        e,
                        y =>
                        {
                            Console.WriteLine("*");
                            Console.WriteLine("*");
                            Console.WriteLine("event!");


                            var _y = y.ToString()
                                .Replace("\n", "\\n")
                                .Replace("\r", "\\r");


                            h.Context.Response.Write("event: y\n");
                            h.Context.Response.Write("data: " + _y + "\n\n");
                            h.Context.Response.Flush();
                        }
                    );

                    h.CompleteRequest();
                    return;
                }
        }
    }

    public static class __ApplicationWebService_WebMethod2
    {
        public static void async_WebMethod2(this ApplicationWebService x, string e, Action<string> y)
        {
            var q = new StringBuilder();

            //q.Append("/event-stream");
            q.Append("?");

            q.Append("e=" + e);

            var s = new EventSource(q.ToString());

            s["y"] = a =>
            {


                var data = a.data.ToString()
                    .Replace("\\r", "\r")
                    .Replace("\\n", "\n");

                //Console.WriteLine(new { data });

                //var _y = XElement.Parse(data);

                y(data);
            };

            s.onerror +=
                delegate
                {
                    s.close();
                };
        }

    }
    #endregion


    static class foo
    {
        // can we send a zip file?
        public static void Invoke(int index, Action<string> y, int frames = 4)
        {
            var st = new Stopwatch();
            st.Start();
            Action<string> log =
                x =>
                {
                    var z = (st.Elapsed + " " + x);

                    Console.WriteLine(z);
                    y(z);
                };

            log("getting ready...");

            var DIRECTORY_DCIM = global::android.os.Environment.DIRECTORY_DCIM;

            var path = global::android.os.Environment.getExternalStoragePublicDirectory(DIRECTORY_DCIM).getAbsolutePath();
            path += "/Camera";

            //var n = DateTime.Now;
            //var f = new java.io.File(path + "/shot" + n.Ticks + ".jpg");

            var camera = default(android.hardware.Camera);
            var dummy = default(SurfaceView);

            try
            {
                // PreviewCallbackWithBuffer { cc = 0, Length = 1048576 }
                // W/CameraService(   84): CameraService::connect X (pid 2117) rejected (existing client).

                Console.WriteLine("*");
                Console.WriteLine("*");
                Console.WriteLine("android.hardware.Camera.open");

                log("android.hardware.Camera.open...");

                camera = android.hardware.Camera.open(index);

                Console.WriteLine("*");
                Console.WriteLine("*");
                Console.WriteLine("after android.hardware.Camera.open");

                //I/SecCameraHardwareInterface(   84): SecCameraHardwareInterface created: pid=84, cameraId=0
                //I/SecCamera(   84): Name of input channel[0] is CE147
                //I/SecCamera(   84): Name of input channel[0] is CE147
                //I/SecCamera(   84): Name of input channel[0] is CE147
                //I/ShotCommon(   84): ShotCommon created: pid=84
                //I/ShotCommon(   84): Preview width(640), height(480)
                //I/ShotCommon(   84): Preview color format yuv420sp
                //I/ShotCommon(   84): Picture width(2560), height(1920)
                //I/ShotCommon(   84): Picture color format jpeg
                //I/ShotCommon(   84): mUseOverlay = 1
                //I/ShotSingle(   84): ShotSingle created: pid=84


                var p = camera.getParameters();

                // The size of the buffer must match the values described above.
                // Gets the supported preview formats. NV21 is always supported.

                // http://developer.android.com/reference/android/hardware/Camera.Parameters.html#getSupportedPreviewFormats()
                p.getSupportedPreviewFormats().With(
                    pformats =>
                    {
                        for (int i = 0; i < pformats.size(); i++)
                        {
                            var pformat = (int)pformats.get(i);

                            Console.WriteLine(new { pformat });
                        }
                    }
                );


                p.setPictureFormat(ImageFormat.NV21);
                p.setPreviewFormat(ImageFormat.NV21);

                //D/DOMX    (  127): ERROR: failed check:(eError == OMX_ErrorNone) || (eError == OMX_ErrorNoMore) - returning error: 0x80001005 - Error returned from OMX API in ducati
                //E/CameraHAL(  127): Error while configuring rotation 0x80001005
                // http://questiontrack.com/galaxy-nexus-specificaly-camera-startpreview-failed-993603.html
                // http://stackoverflow.com/questions/16839869/orientation-error-causing-crash

                ////p.setRotation(0);

                #region setPictureSize
                var s = p.getSupportedPictureSizes();

                var min = default(android.hardware.Camera.Size);

                for (int i = 0; i < s.size(); i++)
                {
                    var size = (android.hardware.Camera.Size)s.get(i);

                    //                I/System.Console( 6058): before takePicture { f = /mnt/sdcard/Pictures/shot.jpg }
                    //I/System.Console( 6058): { size = android.hardware.Camera$Size@4fde180 }

                    System.Console.WriteLine(new { size.width, size.height });

                    if (min == null)
                        min = size;
                    else if (min.width > size.width)
                        min = size;


                }


                #endregion

                //For formats besides YV12, the size of the buffer is determined by multiplying the
                // preview image width, height, and bytes per pixel. The width and height can be read 
                // from getPreviewSize(). Bytes per pixel can be computed from getBitsPerPixel(int) / 8, 
                // using the image format from getPreviewFormat().

                p.setPictureSize(min.width, min.height);
                p.setPreviewSize(min.width, min.height);

                // { buffersize = -307200 }

                // I/System.Console( 2860): { width = 640, height = 480, bytesperpixel = 0, buffersize = 0 }
                // the number of bits per pixel of the given format or -1 if the format doesn't exist or is not supported.
                var bytesperpixel = (ImageFormat.getBitsPerPixel(ImageFormat.NV21) / 8);
                //var buffersize = min.width * min.height * bytesperpixel;
                var buffersize = 460800;

                Console.WriteLine("*");
                Console.WriteLine("*");
                Console.WriteLine("" + new { min.width, min.height, bytesperpixel, buffersize });


                // E/Camera-JNI( 3148): Manually set buffer was too small! Expected 460800 bytes, but got 307200!
                camera.addCallbackBuffer(new sbyte[buffersize]);
                camera.addCallbackBuffer(new sbyte[buffersize]);
                camera.addCallbackBuffer(new sbyte[buffersize]);
                camera.addCallbackBuffer(new sbyte[buffersize]);


                #region setFocusMode
                var focusModes = p.getSupportedFocusModes();
                var NextFocus = android.hardware.Camera.Parameters.FOCUS_MODE_FIXED;

                for (int i = 0; i < focusModes.size(); i++)
                {
                    var focusMode = (string)focusModes.get(i);

                    if (focusMode == android.hardware.Camera.Parameters.FOCUS_MODE_INFINITY)
                        NextFocus = android.hardware.Camera.Parameters.FOCUS_MODE_INFINITY;

                    System.Console.WriteLine(new { focusMode });
                }

                p.setFocusMode(NextFocus);
                #endregion

                camera.setParameters(p);

                //I/System.Console(29651): { width = 2560, height = 1920 }
                //I/System.Console(29651): { width = 2560, height = 1536 }
                //I/System.Console(29651): { width = 2048, height = 1536 }
                //I/System.Console(29651): { width = 2048, height = 1232 }
                //I/System.Console(29651): { width = 1600, height = 1200 }
                //I/System.Console(29651): { width = 1600, height = 960 }
                //I/System.Console(29651): { width = 800, height = 480 }
                //I/System.Console(29651): { width = 640, height = 480 }
                //I/System.Console(29651): { focusMode = auto }
                //I/System.Console(29651): { focusMode = infinity }
                //I/System.Console(29651): { focusMode = macro }
                //I/System.Console(29651): before runOnUiThread
                //I/System.Console(29651): at runOnUiThread
                //I/System.Console(29651): before addCallback
                //I/System.Console(29651): before setPreviewDisplay
                //D/Camera  (29651): app passed NULL surface

                var a = new EventWaitHandle(false, EventResetMode.ManualReset);

                log("before runOnUiThread...");

                System.Console.WriteLine("before runOnUiThread");
                (ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext as Activity).With(
                    aa =>
                    {

                        aa.runOnUiThread(
                           new f
                           {
                               y = delegate
                               {
                                   log("at runOnUiThread...");
                                   System.Console.WriteLine("at runOnUiThread");

                                   // solve issue with callback not being called: release the camera and try again. It usually works.
                                   //To solve issue with rotation 0x80001005: restart app / service

                                   // http://stackoverflow.com/questions/13546788/camera-takepicture-not-working-on-my-jb-gb-froyo-phones
                                   aa.setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE);



                                   //D/Camera  (28463): app passed NULL surface
                                   //I/System.Console(28463): after setPreviewDisplay
                                   //W/SurfaceTexture( 1439): [CameraPreviewExperiment.Activities/CameraPreviewExperiment.Activities.ApplicationWebServiceActivity] cancelBuffer: SurfaceTexture has been abandoned!
                                   //D/memalloc(28463): /dev/pmem: Unmapping buffer base:0x55afb000 size:15032320 offset:13496320
                                   //D/memalloc(28463): /dev/pmem: Unmapping buffer base:0x5969b000 size:16568320 offset:15032320
                                   //D/memalloc(28463): /dev/pmem: Unmapping buffer base:0x56951000 size:12042240 offset:10506240
                                   //I/Adreno200-EGLSUB(28463): <ConfigWindowMatch:2078>: Format RGBA_8888.
                                   //I/System.Console(28463): *
                                   //I/System.Console(28463): *
                                   //I/System.Console(28463): yield_surfaceChanged
                                   //I/System.Console(28463): *
                                   //I/System.Console(28463): *
                                   //I/System.Console(28463): event!
                                   //D/dalvikvm(28463): GC_FOR_ALLOC freed 6K, 25% free 6544K/8675K, paused 34ms
                                   //I/dalvikvm-heap(28463): Grow heap (frag case) to 6.925MB for 460816-byte allocation
                                   //D/dalvikvm(28463): GC_FOR_ALLOC freed 0K, 24% free 6994K/9187K, paused 33ms
                                   //I/System.Console(28463): *
                                   //I/System.Console(28463): *
                                   //I/System.Console(28463): event!
                                   //D/CameraService( 1442): startPreview (pid 28463)
                                   //D/CameraService( 1442): OMADM DCMO CAMERA flag = 1
                                   //D/QualcommCameraHardware( 1442): previewEnabled()  E
                                   //D/QualcommCameraHardware( 1442): previewEnabled() X, mCameraRunning=0 mPreviewStartInProgress=0
                                   //I/mm-camera( 1442): int android::set_preview_window(camera_device*, preview_stream_ops*): E window = 0x0
                                   //I/QualcommCameraHardware( 1442): setPreviewWindow: E, window 0x0, mPreviewWindow 0x0
                                   //I/mm-camera( 1442): int android::set_preview_window(camera_device*, preview_stream_ops*): X


                                   dummy = new SurfaceView(ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext);

                                   //dummy.setWidth(96);
                                   //dummy.setHeight(96);

                                   var h = dummy.getHolder();

                                   // http://developer.android.com/reference/android/view/SurfaceHolder.html#SURFACE_TYPE_PUSH_BUFFERS
                                   var SURFACE_TYPE_PUSH_BUFFERS = 0x00000003;
                                   h.setType(SURFACE_TYPE_PUSH_BUFFERS);




                                   System.Console.WriteLine("before addCallback");
                                   camera.addCallbackBuffer(new sbyte[0x100000]);
                                   h.addCallback(
                                       new XSurfaceHolder_Callback
                                       {
                                           yield_surfaceChanged =
                                               delegate
                                               {
                                                   Console.WriteLine("*");
                                                   Console.WriteLine("*");
                                                   Console.WriteLine("yield_surfaceChanged");
                                                   log("at yield_surfaceChanged...");

                                                   camera.addCallbackBuffer(new sbyte[buffersize]);

                                                   var cc = 0;


                                                   //E/CameraHardwareSec(   84): int android::CameraHardwareSec::previewThread(): mSkipFrame(3) > 0
                                                   //E/CameraHardwareSec(   84): int android::CameraHardwareSec::previewThread(): mSkipFrame(2) > 0
                                                   //E/CameraHardwareSec(   84): int android::CameraHardwareSec::previewThread(): mSkipFrame(1) > 0
                                                   #region setPreviewCallbackWithBuffer
                                                   camera.setPreviewCallbackWithBuffer(
                                                       new XCameraPreviewCallback
                                                       {
                                                           yield =
                                                               (dataNV21, c) =>
                                                               {
                                                                   if (dummy == null)
                                                                   {
                                                                       // W/CameraHardwareSec(   84): virtual android::status_t android::CameraHardwareSec::cancelPicture() : not supported, just returning NO_ERROR
                                                                       return;
                                                                   }

                                                                   //                                                                   10080.0ms PreviewCallbackWithBuffer enter { cc = 58, Length = 460800 }
                                                                   //10119.0ms PreviewCallbackWithBuffer compressToJpeg done { cc = 58, Elapsed = 39.0ms }
                                                                   //10174.0ms PreviewCallbackWithBuffer ToBase64String done { cc = 58, Elapsed = 94.0ms }

                                                                   var xcc = cc;

                                                                   log("PreviewCallbackWithBuffer enter " + new { xcc, dataNV21.Length });

                                                                   // failed to flush { Length = 14619 }
                                                                   //new Thread(
                                                                   //    delegate()
                                                                   {
                                                                       if (dummy == null)
                                                                       {
                                                                           // W/CameraHardwareSec(   84): virtual android::status_t android::CameraHardwareSec::cancelPicture() : not supported, just returning NO_ERROR
                                                                           return;
                                                                       }

                                                                       var cst = new Stopwatch();
                                                                       cst.Start();

                                                                       // http://stackoverflow.com/questions/3426614/android-converting-from-nv21-preview-format-on-nexus-one-to-jpeg
                                                                       // http://developer.android.com/reference/android/graphics/YuvImage.html

                                                                       var yuv = new YuvImage(
                                                                           dataNV21,
                                                                           ImageFormat.NV21,
                                                                           min.width,
                                                                           min.height,
                                                                           null
                                                                        );

                                                                       var m = new java.io.ByteArrayOutputStream();

                                                                       yuv.compressToJpeg(
                                                                           new Rect(0, 0, min.width, min.height),
                                                                           20,
                                                                           m);

                                                                       var data = (byte[])(object)m.toByteArray();

                                                                       log("PreviewCallbackWithBuffer compressToJpeg done " + new { xcc, cst.Elapsed });


                                                                       Console.WriteLine("compressToJpeg "
                                                                            + new { data.Length }
                                                                        );

                                                                       var src = "data:image/jpg;base64," +
                                                                             Convert.ToBase64String(
                                                                                 data
                                                                             );

                                                                       log("PreviewCallbackWithBuffer ToBase64String done " + new { xcc, cst.Elapsed });

                                                                       y(src);


                                                                       //PreviewCallbackWithBuffer { cc = 0, Length = 1048576 }

                                                                       if (dummy == null)
                                                                       {
                                                                           // W/CameraHardwareSec(   84): virtual android::status_t android::CameraHardwareSec::cancelPicture() : not supported, just returning NO_ERROR
                                                                           return;
                                                                       }

                                                                       //camera.addCallbackBuffer();
                                                                       camera.addCallbackBuffer(new sbyte[buffersize]);

                                                                       log("PreviewCallbackWithBuffer exit " + new { xcc, cst.Elapsed });


                                                                       if (xcc == frames)
                                                                       {

                                                                           //dummy.setVisibility(View.GONE);
                                                                           //dummy = null;

                                                                           // Caused by: android.view.ViewRoot$CalledFromWrongThreadException: Only the original thread that created a view hierarchy can touch its views.

                                                                           aa.runOnUiThread(
                                                                              new f
                                                                              {
                                                                                  y = delegate
                                                                                  {
                                                                                      if (dummy != null)
                                                                                      {
                                                                                          dummy.setVisibility(View.GONE);
                                                                                          dummy = null;

                                                                                      }

                                                                                      a.Set();
                                                                                  }
                                                                              }
                                                                          );
                                                                       }
                                                                   }
                                                                   //).Start();

                                                                   cc++;


                                                               }
                                                       }
                                                   );
                                                   #endregion


                                                   log("startPreview");
                                                   camera.startPreview();
                                               }
                                       }
                                   );


                                   var pp = new android.widget.LinearLayout.LayoutParams(
                                     android.widget.LinearLayout.LayoutParams.FILL_PARENT,
                                     android.widget.LinearLayout.LayoutParams.FILL_PARENT
                                     );

                                   pp.setMargins(64, 64, 64, 64);

                                   dummy.setBackgroundColor(Color.argb(0x0F, 255, 0, 0));

                                   aa.addContentView(dummy, pp);

                                   Console.WriteLine("before setPreviewDisplay");
                                   // https://code.google.com/p/zxing/source/browse/trunk/android/src/com/google/zxing/client/android/camera/CameraManager.java
                                   // http://stackoverflow.com/questions/16945524/app-passed-null-surface-while-taking-a-picture-without-a-surfaceview

                                   try
                                   {
                                       camera.setPreviewDisplay(h);
                                   }
                                   catch
                                   {
                                       throw;
                                   }
                                   Console.WriteLine("after setPreviewDisplay");

                               }
                           }
                        );

                    }
                );

                a.WaitOne();
                Console.WriteLine("done!");

                log("PreviewCallbackWithBuffer done");

            }
            finally
            {
                Console.WriteLine("*");
                Console.WriteLine("*");
                Console.WriteLine("finally");
                // using
                if (camera != null)
                {
                    camera.stopPreview();
                    camera.release();
                    camera = null;
                }


            }
        }

        public static void Invoke(Action<string> y, int frames = 4)
        {
            try
            {

                //for (int i = 0; i < android.hardware.Camera.getNumberOfCameras(); i++)
                for (int i = 0; i < 1; i++)
                {
                    Invoke(0, y, frames);
                }
            }
            catch
            {
                throw;
            }
        }
    }

    class f : java.lang.Runnable
    {
        public Action y;

        public void run()
        {
            y();
        }
    }

    class XSurfaceHolder_Callback : SurfaceHolder_Callback
    {
        // https://github.com/commonsguy/cw-advandroid/blob/master/Camera/Preview/src/com/commonsware/android/camera/PreviewDemo.java

        public Action yield_surfaceChanged;

        public void surfaceChanged(SurfaceHolder arg0, int arg1, int arg2, int arg3)
        {
        }

        public void surfaceCreated(SurfaceHolder value)
        {
            yield_surfaceChanged();
        }

        public void surfaceDestroyed(SurfaceHolder value)
        {
        }
    }


    class XCameraPreviewCallback : android.hardware.Camera.PreviewCallback
    {
        public Action<sbyte[], android.hardware.Camera> yield;

        public void onPreviewFrame(sbyte[] arg0, android.hardware.Camera arg1)
        {
            yield(arg0, arg1);
        }
    }

}
