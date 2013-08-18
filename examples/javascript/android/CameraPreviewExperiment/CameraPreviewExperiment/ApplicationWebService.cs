using android.app;
using android.graphics;
using android.view;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace CameraPreviewExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // JSC shold use chrome omnisearch to tell about running network apps

            // http://stackoverflow.com/questions/14815103/android-streaming-the-camera-as-mjpeg
            // http://stackoverflow.com/questions/2456802/android-camera-preview
            // https://github.com/commonsguy/cw-advandroid/blob/master/Camera/Preview/src/com/commonsware/android/camera/PreviewDemo.java

            foo.Invoke(y);
            // Send it back to the caller.
            //y(e);
        }

    }

    static class foo
    {
        // can we send a zip file?
        public static void Invoke(int index, Action<string> y)
        {
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

                p.setRotation(0);

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

                System.Console.WriteLine("before runOnUiThread");
                (ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext as Activity).With(
                    aa =>
                    {

                        aa.runOnUiThread(
                           new f
                           {
                               y = delegate
                               {
                                   System.Console.WriteLine("at runOnUiThread");


                                   dummy = new SurfaceView(ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext);

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
                                                   camera.addCallbackBuffer(new sbyte[buffersize]);

                                                   var cc = 0;


                                                   //E/CameraHardwareSec(   84): int android::CameraHardwareSec::previewThread(): mSkipFrame(3) > 0
                                                   //E/CameraHardwareSec(   84): int android::CameraHardwareSec::previewThread(): mSkipFrame(2) > 0
                                                   //E/CameraHardwareSec(   84): int android::CameraHardwareSec::previewThread(): mSkipFrame(1) > 0
                                                   camera.setPreviewCallbackWithBuffer(
                                                       new XCameraPreviewCallback
                                                       {
                                                           yield =
                                                               (dataNV21, c) =>
                                                               {

                                                                   Console.WriteLine("*");
                                                                   Console.WriteLine("*");
                                                                   Console.WriteLine("PreviewCallbackWithBuffer "
                                                                       + new { cc, dataNV21.Length, dummy }
                                                                   );

                                                                   if (dummy == null)
                                                                   {
                                                                       // W/CameraHardwareSec(   84): virtual android::status_t android::CameraHardwareSec::cancelPicture() : not supported, just returning NO_ERROR
                                                                       return;
                                                                   }


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
                                                                       50,
                                                                       m);

                                                                   var data = (byte[])(object)m.toByteArray();

                                                                   Console.WriteLine("compressToJpeg "
                                                                        + new { data.Length }
                                                                    );

                                                                   var src = "data:image/jpg;base64," +
                                                                         Convert.ToBase64String(
                                                                             data
                                                                         );

                                                                   y(src);


                                                                   //PreviewCallbackWithBuffer { cc = 0, Length = 1048576 }
                                                                   cc++;


                                                                   //camera.addCallbackBuffer();
                                                                   camera.addCallbackBuffer(new sbyte[buffersize]);

                                                                   if (cc == 16)
                                                                   {
                                                                       camera.stopPreview();

                                                                       dummy.setVisibility(View.GONE);
                                                                       dummy = null;

                                                                       a.Set();
                                                                   }
                                                               }
                                                       }
                                                   );

                                                   camera.startPreview();
                                               }
                                       }
                                   );

                                   aa.addContentView(dummy, new android.widget.LinearLayout.LayoutParams(
                                     android.widget.LinearLayout.LayoutParams.WRAP_CONTENT,
                                     android.widget.LinearLayout.LayoutParams.WRAP_CONTENT
                                     )
                                   );

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
            }
            finally
            {
                Console.WriteLine("*");
                Console.WriteLine("*");
                Console.WriteLine("finally");
                // using
                if (camera != null)
                {
                    camera.release();
                    camera = null;
                }

                if (dummy != null)
                    dummy.setVisibility(View.GONE);
            }
        }

        public static void Invoke(Action<string> y)
        {
            try
            {

                //for (int i = 0; i < android.hardware.Camera.getNumberOfCameras(); i++)
                for (int i = 0; i < 1; i++)
                {
                    Invoke(i, y);
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
