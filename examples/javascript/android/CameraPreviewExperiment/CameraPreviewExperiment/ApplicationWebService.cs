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
using System.Collections;
using System.Collections.Generic;
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
            #region log
            var st = new Stopwatch();
            st.Start();
            Action<string> log =
                x =>
                {
                    var z = (st.Elapsed + " " + x);

                    Console.WriteLine(z);
                    y(z);
                };
            #endregion

            log("getting ready...");

            var DIRECTORY_DCIM = global::android.os.Environment.DIRECTORY_DCIM;

            var path = global::android.os.Environment.getExternalStoragePublicDirectory(DIRECTORY_DCIM).getAbsolutePath();
            path += "/Camera";

            //var n = DateTime.Now;
            //var f = new java.io.File(path + "/shot" + n.Ticks + ".jpg");

            var camera = default(android.hardware.Camera);
            var surface = default(SurfaceView);

            try
            {
                // PreviewCallbackWithBuffer { cc = 0, Length = 1048576 }
                // W/CameraService(   84): CameraService::connect X (pid 2117) rejected (existing client).

                log("android.hardware.Camera.open... " + new { index });

                camera = android.hardware.Camera.open(index);

                log("android.hardware.Camera.open... done ");

                var PreviewFormat = ImageFormat.UNKNOWN;

                #region setParameters
                var p = camera.getParameters();

                // The size of the buffer must match the values described above.
                // Gets the supported preview formats. NV21 is always supported.

                // http://developer.android.com/reference/android/hardware/Camera.Parameters.html#getSupportedPreviewFormats()

                #region SupportedPictureFormat

                foreach (int SupportedPictureFormat in p.getSupportedPictureFormats().AsEnumerable())
                {
                    Console.WriteLine(new { SupportedPictureFormat });
                }

                //p.getSupportedPictureFormats().With(
                //     pformats =>
                //     {
                //         for (int i = 0; i < pformats.size(); i++)
                //         {
                //             var SupportedPictureFormat = (int)pformats.get(i);

                //             Console.WriteLine(new { SupportedPictureFormat });
                //         }
                //     }
                // );
                #endregion



                #region SupportedPreviewFormat

                foreach (int SupportedPreviewFormat in p.getSupportedPreviewFormats().AsEnumerable())
                {

                    if (SupportedPreviewFormat == ImageFormat.NV21)
                        PreviewFormat = SupportedPreviewFormat;
                    else if (PreviewFormat == ImageFormat.UNKNOWN)
                        PreviewFormat = SupportedPreviewFormat;

                    log("" + new { SupportedPreviewFormat });

                    Console.WriteLine(new { SupportedPreviewFormat });
                }
                //p.getSupportedPreviewFormats().With(
                //    pformats =>
                //    {
                //        for (int i = 0; i < pformats.size(); i++)
                //        {
                //            var SupportedPreviewFormat = (int)pformats.get(i);


                //            if (SupportedPreviewFormat == ImageFormat.NV21)
                //                PreviewFormat = SupportedPreviewFormat;
                //            else if (PreviewFormat == ImageFormat.UNKNOWN)
                //                PreviewFormat = SupportedPreviewFormat;

                //            log("" + new { SupportedPreviewFormat });
                //        }
                //    }
                //);
                #endregion


                //p.setPictureFormat(ImageFormat.YV12);

                p.setPreviewFormat(PreviewFormat);


                //D/DOMX    (  127): ERROR: failed check:(eError == OMX_ErrorNone) || (eError == OMX_ErrorNoMore) - returning error: 0x80001005 - Error returned from OMX API in ducati
                //E/CameraHAL(  127): Error while configuring rotation 0x80001005
                // http://questiontrack.com/galaxy-nexus-specificaly-camera-startpreview-failed-993603.html
                // http://stackoverflow.com/questions/16839869/orientation-error-causing-crash

                ////p.setRotation(0);

                #region getSupportedPreviewSizes
                //var s = p.getSupportedPreviewSizes();

                var min = default(android.hardware.Camera.Size);

                //for (int i = 0; i < s.size(); i++)
                foreach (android.hardware.Camera.Size size in p.getSupportedPreviewSizes().AsEnumerable())
                {
                    //var size = (android.hardware.Camera.Size)s.get(i);

                    //                I/System.Console( 6058): before takePicture { f = /mnt/sdcard/Pictures/shot.jpg }
                    //I/System.Console( 6058): { size = android.hardware.Camera$Size@4fde180 }

                    var SupportedPreviewSize = new { size.width, size.height };
                    log("" + new { SupportedPreviewSize });

                    if (min == null)
                        min = size;
                    else if (min.width > size.width)
                        min = size;

                }


                #endregion
                p.setPreviewSize(min.width, min.height);

                //For formats besides YV12, the size of the buffer is determined by multiplying the
                // preview image width, height, and bytes per pixel. The width and height can be read 
                // from getPreviewSize(). Bytes per pixel can be computed from getBitsPerPixel(int) / 8, 
                // using the image format from getPreviewFormat().

                //p.setPictureSize(min.width, min.height);


                // I/System.Console( 2860): { width = 640, height = 480, bytesperpixel = 0, buffersize = 0 }
                // the number of bits per pixel of the given format or -1 if the format doesn't exist or is not supported.
                //var bytesperpixel = (ImageFormat.getBitsPerPixel(ImageFormat.NV21) / 8);

                // http://stackoverflow.com/questions/13703596/mediacodec-and-camera-colorspaces-dont-match
                // https://code.google.com/p/android/issues/detail?id=37655

                var bitsperpixel = (ImageFormat.getBitsPerPixel(PreviewFormat));
                var buffersize = min.width * min.height / 8 * bitsperpixel;

                // 12
                // http://www.fourcc.org/yuv.php

                //var buffersize = 460800;

                log("" + new { min.width, min.height, bitsperpixel, buffersize });


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
                #endregion


                // preview layout size: 736/1216
                buffersize = 1843200;

                // E/Camera-JNI( 3148): Manually set buffer was too small! Expected 460800 bytes, but got 307200!
                camera.addCallbackBuffer(new sbyte[buffersize]);
                camera.addCallbackBuffer(new sbyte[buffersize]);
                camera.addCallbackBuffer(new sbyte[buffersize]);
                camera.addCallbackBuffer(new sbyte[buffersize]);
                camera.addCallbackBuffer(new sbyte[buffersize]);

                var a = new EventWaitHandle(false, EventResetMode.ManualReset);

                // Task.ContinueWith
                // await

                (ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext as Activity).StartNew(
                    aa =>
                    {
                        log("at runOnUiThread...");


                        // solve issue with callback not being called: release the camera and try again. It usually works.
                        //To solve issue with rotation 0x80001005: restart app / service

                        // http://stackoverflow.com/questions/13546788/camera-takepicture-not-working-on-my-jb-gb-froyo-phones
                        aa.setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE);


                        #region setErrorCallback
                        camera.setErrorCallback(
                           new XErrorCallback
                           {
                               yield = (err, c) =>
                               {
                                   log("" + new { err });
                               }

                           }
                       );
                        #endregion

                        surface = new SurfaceView(ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext);

                        var holder = surface.getHolder();

                        // http://developer.android.com/reference/android/view/SurfaceHolder.html#SURFACE_TYPE_PUSH_BUFFERS
                        var SURFACE_TYPE_PUSH_BUFFERS = 0x00000003;
                        holder.setType(SURFACE_TYPE_PUSH_BUFFERS);




                        log("setPreviewCallbackWithBuffer");

                        var cc = 0;

                        //E/CameraHardwareSec(   84): int android::CameraHardwareSec::previewThread(): mSkipFrame(3) > 0
                        //E/CameraHardwareSec(   84): int android::CameraHardwareSec::previewThread(): mSkipFrame(2) > 0
                        //E/CameraHardwareSec(   84): int android::CameraHardwareSec::previewThread(): mSkipFrame(1) > 0

                        // http://stackoverflow.com/questions/16878042/camera-not-working-in-google-nexus-tablet

                        #region camera.PreviewCallbackWithBuffer
                        camera.PreviewCallbackWithBuffer(
                            (rawdata, c) =>
                            {
                                if (surface == null)
                                {
                                    // W/CameraHardwareSec(   84): virtual android::status_t android::CameraHardwareSec::cancelPicture() : not supported, just returning NO_ERROR
                                    return;
                                }

                                //                                                                   10080.0ms PreviewCallbackWithBuffer enter { cc = 58, Length = 460800 }
                                //10119.0ms PreviewCallbackWithBuffer compressToJpeg done { cc = 58, Elapsed = 39.0ms }
                                //10174.0ms PreviewCallbackWithBuffer ToBase64String done { cc = 58, Elapsed = 94.0ms }

                                var xcc = cc;

                                log("PreviewCallbackWithBuffer enter " + new { xcc, rawdata.Length });

                                // failed to flush { Length = 14619 }
                                //new Thread(
                                //    delegate()
                                {
                                    if (surface == null)
                                    {
                                        // W/CameraHardwareSec(   84): virtual android::status_t android::CameraHardwareSec::cancelPicture() : not supported, just returning NO_ERROR
                                        return;
                                    }

                                    var cst = new Stopwatch();
                                    cst.Start();

                                    // http://stackoverflow.com/questions/3426614/android-converting-from-nv21-preview-format-on-nexus-one-to-jpeg
                                    // http://developer.android.com/reference/android/graphics/YuvImage.html

                                    //Caused by: java.lang.IllegalArgumentException: only support ImageFormat.NV21 and ImageFormat.YUY2 for now
                                    //at android.graphics.YuvImage.<init>(YuvImage.java:82)


                                    // https://code.google.com/p/android/issues/detail?id=823
                                    // https://code.google.com/p/android-source-browsing/source/browse/graphics/java/android/graphics/YuvImage.java?repo=platform--frameworks--base&name=android-cts-4.1_r1
                                    var yuv = new YuvImage(
                                        rawdata,
                                        PreviewFormat,
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

                                    if (surface == null)
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

                                        aa.StartNew(
                                                delegate
                                                {
                                                    if (surface != null)
                                                    {
                                                        surface.setVisibility(View.GONE);
                                                        surface = null;

                                                    }

                                                    a.Set();
                                                }
                                        );
                                    }
                                }
                                //).Start();

                                cc++;


                            }
                        );
                        #endregion


                        #region holder.surfaceChanged
                        holder.surfaceChanged(
                            delegate
                            {
                                log("surfaceChanged?");
                            }
                        );
                        #endregion

                        #region holder.surfaceCreated
                        holder.surfaceCreated(
                            delegate
                            {
                                log("surfaceCreated!");

                                // http://stackoverflow.com/questions/12098298/android-camera-app-passed-null-surface
                                // http://stackoverflow.com/questions/16945524/app-passed-null-surface-while-taking-a-picture-without-a-surfaceview

                                //  app passed NULL surface
                                log("before setPreviewDisplay, delay");

                                Thread.Sleep(400);

                                // https://code.google.com/p/zxing/source/browse/trunk/android/src/com/google/zxing/client/android/camera/CameraManager.java
                                // http://stackoverflow.com/questions/16945524/app-passed-null-surface-while-taking-a-picture-without-a-surfaceview

                                // http://stackoverflow.com/questions/4852740/surfaceview-getholder-not-returning-surfaceholder

                                log("before setPreviewDisplay " + new { holder });
                                // inside surface changed?

                                //if (surface == 0)
                                //{
                                //    LOGE("app passed NULL surface");
                                //    return NO_INIT;
                                //}

                                // https://android.googlesource.com/platform/frameworks/native/+/a6938bab1f6fa76ae98ebbe44f4e534e05fa0993/libs/ui/Camera.cpp
                                camera.setTryPreviewDisplay(holder);
                                log("after setPreviewDisplay");


                                log("startPreview, delay");
                                Thread.Sleep(200);
                                log("startPreview");

                                camera.startPreview();
                                log("after startPreview");
                            }
                        );
                        #endregion


                        #region addContentView
                        //surface.setBackgroundColor(Color.argb(0x0F, 255, 0, 0));

                        var pp = new android.widget.LinearLayout.LayoutParams(
                            android.widget.LinearLayout.LayoutParams.FILL_PARENT,
                            android.widget.LinearLayout.LayoutParams.FILL_PARENT
                            );
                        //pp.setMargins(64, 64, 64, 64);

                        aa.addContentView(surface, pp);
                        #endregion





                    }
                );

                a.WaitOne();

                log("PreviewCallbackWithBuffer done");

            }
            catch (Exception ex)
            {
                log("error: " + new { ex.Message, ex.StackTrace });

                throw new Exception("", ex);
            }
            finally
            {
                log("finally");
                // using
                if (camera != null)
                {
                    camera.stopPreview();
                    camera.release();
                    camera = null;
                }


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

        public Action yield_surfaceCreated;
        public Action yield_surfaceChanged;

        public void surfaceChanged(SurfaceHolder arg0, int arg1, int arg2, int arg3)
        {
            if (yield_surfaceChanged != null)
                yield_surfaceChanged();
        }

        public void surfaceCreated(SurfaceHolder value)
        {
            if (yield_surfaceCreated != null)
                yield_surfaceCreated();
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

    class XErrorCallback : global::android.hardware.Camera.ErrorCallback
    {
        public Action<int, global::android.hardware.Camera> yield;



        public void onError(int arg0, global::android.hardware.Camera arg1)
        {
            yield(arg0, arg1);
        }
    }

    public static class X
    {
        public static void setTryPreviewDisplay(this android.hardware.Camera c, SurfaceHolder i)
        {
            try
            {
                c.setPreviewDisplay(i);
            }
            catch
            {
                throw;
            }
        }

        public static void PreviewCallbackWithBuffer(this android.hardware.Camera camera, Action<sbyte[], android.hardware.Camera> yield)
        {
            camera.setPreviewCallbackWithBuffer(
                      new XCameraPreviewCallback
                      {
                          yield = yield
                      }
            );

        }





        public static void surfaceChanged(this SurfaceHolder h, Action yield_surfaceChanged)
        {
            h.addCallback(
                new XSurfaceHolder_Callback
                {
                    yield_surfaceCreated = yield_surfaceChanged
                }
            );
        }

        public static void surfaceCreated(this SurfaceHolder h, Action yield_surfaceCreated)
        {
            h.addCallback(
                new XSurfaceHolder_Callback
                {
                    yield_surfaceCreated = yield_surfaceCreated
                }
            );
        }

        public static void StartNew(this Activity e, Action<Activity> yield)
        {
            e.runOnUiThread(
                        new f
                        {
                            y = () => yield(e)
                        }

            );

        }

        public static IEnumerable<object> AsEnumerable(this java.util.List source)
        {
            return Enumerable.Range(0, source.size()).Select(k => source.get(k));

        }
    }
}
