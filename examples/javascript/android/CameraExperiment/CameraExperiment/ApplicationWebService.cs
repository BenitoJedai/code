using android.app;
using android.content;
using android.graphics;
using android.hardware;
using android.view;
using java.io;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace CameraExperiment
{
    class f : java.lang.Runnable
    {
        public Action y;

        public void run()
        {
            y();
        }
    }

    public static class foo
    {
        public static File InternalTakePicture(int num = 0)
        {
            var DIRECTORY_DCIM = global::android.os.Environment.DIRECTORY_DCIM;


            var path = global::android.os.Environment.getExternalStoragePublicDirectory(DIRECTORY_DCIM).getAbsolutePath();
            path += "/Camera";


            //var SAVE_PATH = android.os.Environment.getExternalStoragePublicDirectory(
            //    android.os.Environment.DIRECTORY_PICTURES
            //) + "/";

            var n = DateTime.Now;

            var f = new File(path + "/shot" + n.Ticks + ".jpg");

            //I/System.Console(31472): enter TakePicture
            //W/CameraService(  128): CameraService::connect X (pid 31472) rejected (existing client).
            //I/System.Console(31472): error takePicture { Message = Fail to connect to camera service, StackTrace = java.lang.RuntimeException: Fail to connect to camera service
            //I/System.Console(31472):        at android.hardware.Camera.native_setup(Native Method)
            //I/System.Console(31472):        at android.hardware.Camera.<init>(Camera.java:340)
            //I/System.Console(31472):        at android.hardware.Camera.open(Camera.java:302)
            var camera = android.hardware.Camera.open(num);

            //            W/CameraService(  128): CameraService::connect X (pid 2499) rejected (existing client).
            //D/dalvikvm( 2499): GC_CONCURRENT freed 873K, 12% free 7525K/8544K, paused 4ms+4ms, total 59ms
            //D/dalvikvm( 2499): WAIT_FOR_CONCURRENT_GC blocked 14ms
            //I/System.Console( 2499): error takePicture { Message = Fail to connect to camera service, StackTrace = java.lang.RuntimeException: Fail to connect to camera service
            //I/System.Console( 2499):        at android.hardware.Camera.native_setup(Native Method)
            //I/System.Console( 2499):        at android.hardware.Camera.<init>(Camera.java:340)
            //I/System.Console( 2499):        at android.hardware.Camera.open(Camera.java:302)
            //I/System.Console( 2499):        at CameraExperiment.foo.InternalTakePicture(foo.java:65)

            var p = camera.getParameters();

            p.setRotation(0);

            //camera.stopFaceDetection();

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

            System.Console.WriteLine("before setPictureSize ");
            p.setPictureSize(min.width, min.height);

            //E/CameraHardwareSec(   84): android::status_t android::CameraHardwareSec::setSceneModeParameter(const android::CameraParameters&): unmatched focus_mode(continuous-picture)
            //E/CameraHardwareSec(   84): virtual android::status_t android::CameraHardwareSec::setParameters(const android::CameraParameters&): Failed to setting scene mode


            var focusModes = p.getSupportedFocusModes();
            var NextFocus = android.hardware.Camera.Parameters.FOCUS_MODE_FIXED;

            for (int i = 0; i < focusModes.size(); i++)
            {
                var focusMode = (string)focusModes.get(i);

                if (focusMode == android.hardware.Camera.Parameters.FOCUS_MODE_INFINITY)
                    NextFocus = android.hardware.Camera.Parameters.FOCUS_MODE_INFINITY;

                System.Console.WriteLine(new { focusMode });
            }

            //            I/System.Console(31232): before setPictureSize
            //I/System.Console(31232): { focusMode = fixed }
            //I/System.Console(31232): before setFocusMode
            //E/NvOmxCameraSettingsParser(  128): Failed substring capabilities check, unsupported parameter: 'infinity', original: fixed
            //E/NvOmxCameraSettingsParser(  128): extractChanges: Invalid parameter!
            //E/NvOmxCamera(  128): setParameters: Invalid parameters
            //I/System.Console(31232): error takePicture { Message = setParameters failed, StackTrace = java.lang.RuntimeException: setParameters failed

            // { focusMode = auto }
            // { focusMode = infinity }
            // { focusMode = macro }
            // before setFocusMode
            //9): android::status_t android::CameraHardwareSec::setSceneModeParameter(const android::CameraParameters&): unmatched focus_mode(fixed)
            //9): virtual android::status_t android::CameraHardwareSec::setParameters(const android::CameraParameters&): Failed to setting scene mode
            // error takePicture { Message = setParameters failed, StackTrace = java.lang.RuntimeException: setParameters failed
            //        at android.hardware.Camera.native_setParameters(Native Method)
            //        at android.hardware.Camera.setParameters(Camera.java:950)
            //        at CameraExperiment.foo.InternalTakePicture(foo.java:105)

            //            E/SecCamera(   84): ERR(int android::fimc_v4l2_s_ctrl(int, unsigned int, unsigned int)):VIDIOC_S_CTRL(id = 0x800005b (91), value = 0) failed ret = -1
            //E/SecCamera(   84): ERR(int android::SecCamera::setFaceDetect(int)):Fail on V4L2_CID_CAMERA_FACE_DETECTION
            //E/SecCamera(   84): ERR(int android::fimc_v4l2_s_ctrl(int, unsigned int, unsigned int)):VIDIOC_S_CTRL(id = 0x8000063 (99), value = 6) failed ret = -1
            //E/SecCamera(   84): ERR(int android::SecCamera::setFocusMode(int)):Fail on V4L2_CID_CAMERA_FOCUS_MODE
            //E/CameraHardwareSec(   84): android::status_t android::CameraHardwareSec::setSceneModeParameter(const android::CameraParameters&): mSecCamera->setFocusMode(6) fail
            //E/CameraHardwareSec(   84): virtual android::status_t android::CameraHardwareSec::setParameters(const android::CameraParameters&): Failed to setting scene mode
            //E/SecCamera(   84): ERR(int android::fimc_v4l2_s_ctrl(int, unsigned int, unsigned int)):VIDIOC_S_CTRL(id = 0x800006c (108), value = 1) failed ret = -1
            //E/SecCamera(   84): ERR(int android::SecCamera::setBatchReflection()):Fail on V4L2_CID_CAMERA_BATCH_REFLECTION
            //E/CameraHardwareSec(   84): ERR(virtual android::status_t android::CameraHardwareSec::setParameters(const android::CameraParameters&)):Fail on mSecCamera->setBatchCmd


            System.Console.WriteLine("before setFocusMode " + new { NextFocus });
            //p.setFocusMode(android.hardware.Camera.Parameters.FOCUS_MODE_INFINITY);
            p.setFocusMode(NextFocus);


            //            E/SecCamera(   84): ERR(int android::fimc_poll(pollfd*)):No data in 10 secs..
            //I/ShotSingle(   84): CAMERA_MSG_COMPRESSED_IMAGE

            camera.setParameters(p);

            // http://stackoverflow.com/questions/9744790/android-possible-to-camera-capture-without-a-preview
            var b = new EventWaitHandle(false, EventResetMode.ManualReset);
            System.Console.WriteLine("before startPreview ");


            Action done = delegate { };

            try
            {
                // #5 java.lang.RuntimeException: Can't create handler inside thread that has not called Looper.prepare()

                (ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext as Activity).With(
                aa =>
                {
                    aa.runOnUiThread(
                       new f
                       {
                           y = delegate
                           {
                               try
                               {
                                   // D/Camera  ( 2464): app passed NULL surface

                                   System.Console.WriteLine("before getHolder ");

                                   //  the nexus 7 and droid x both don't support the passing of a dummy surfaceview to a camera object. Your response that all camera things must created in the activity is false. I was able to instantiate a camera within a thread by passing it a view just fine. 

                                   // here, the unused surface view and holder
                                   var dummy = new SurfaceView(ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext);

                                   // missing for android 2.2
                                   //dummy.setScaleX(0f);
                                   //dummy.setScaleY(0f);

                                   var h = dummy.getHolder();

                                   // http://developer.android.com/reference/android/view/SurfaceHolder.html#SURFACE_TYPE_PUSH_BUFFERS
                                   var SURFACE_TYPE_PUSH_BUFFERS = 0x00000003;
                                   h.setType(SURFACE_TYPE_PUSH_BUFFERS);

                                   h.addCallback(
                                       new XSurfaceHolder_Callback
                                       {
                                           yield_surfaceCreated = delegate
                                           {
                                               System.Console.WriteLine("at yield_surfaceCreated ");

                                               try
                                               {

                                                   camera.setPreviewDisplay(h);
                                                   camera.startPreview();

                                                   System.Console.WriteLine("after startPreview ");

                                                   b.Set();
                                               }
                                               catch
                                               {
                                                   throw;
                                               }
                                           }
                                       }
                                   );

                                   //h.setType(SurfaceHolder.SURFACE_TYPE_PUSH_BUFFERS);


                                   aa.addContentView(dummy, new android.widget.LinearLayout.LayoutParams(
                                     android.widget.LinearLayout.LayoutParams.WRAP_CONTENT,
                                     android.widget.LinearLayout.LayoutParams.WRAP_CONTENT
                                     )
                                   );

                                   done = delegate
                                   {
                                       aa.runOnUiThread(
                                          new f
                                          {
                                              y = delegate
                                              {
                                                  // https://groups.google.com/forum/?fromgroups#!topic/android-developers/liph4z9LnFA

                                                  // how to Orphanize??
                                                  dummy.setVisibility(View.GONE);

                                              }
                                          }
                                      );

                                   };



                               }
                               catch
                               {
                                   throw;
                               }

                           }
                       }
                        );
                }
                );


            }
            catch
            {
                throw;
            }
            b.WaitOne();

            //camera.@lock();
            var a = new EventWaitHandle(false, EventResetMode.ManualReset);
            //var b = new EventWaitHandle(false, EventResetMode.ManualReset);

            //            E/SecCamera(   84): ERR(int android::fimc_v4l2_s_ctrl(int, unsigned int, unsigned int)):VIDIOC_S_CTRL(id = 0x800005d (93), value = 1) failed ret = -1
            //E/SecCamera(   84): ERR(int android::SecCamera::setAutofocus()):Fail on V4L2_CID_CAMERA_SET_AUTO_FOCUS
            //E/CameraHardwareSec(   84): ERR(int android::CameraHardwareSec::autoFocusThread()):Fail on mSecCamera->setAutofocus()

            //System.Console.WriteLine("before autoFocus " + new { f });

            ////

            //camera.autoFocus(
            //    new XAutoFocus
            //    {
            //        yield = delegate
            //        {
            //            System.Console.WriteLine("at autoFocus " + new { f });

            // https://github.com/mozilla-b2g/android-device-crespo/blob/master/libcamera/SecCamera.cpp



            //            E/SecCamera(   84): ERR(int android::fimc_poll(pollfd*)):No data in 10 secs..
            //I/ShotSingle(   84): CAMERA_MSG_COMPRESSED_IMAGE
            //D/dalvikvm( 6608): GC_FOR_MALLOC freed 607K, 58% free 2856K/6727K, external 2013K/2108K, paused 18ms
            //I/dalvikvm-heap( 6608): Grow heap (frag case) to 7.847MB for 614416-byte allocation
            //D/dalvikvm( 6608): GC_FOR_MALLOC freed 46K, 54% free 3410K/7367K, external 2013K/2108K, paused 13ms
            //I/System.Console( 6608): enter XCameraPictureCallback { Length = 0 }
            //I/System.Console( 6608): exit XCameraPictureCallback

            //for (int i = 0; i < 11; i++)
            //{
            //    System.Console.WriteLine("warming up camera machine... " + i);
            //    Thread.Sleep(1000);

            //}




            // http://stackoverflow.com/questions/15279911/using-camera-without-preview-or-surface-in-android
            // http://handycodeworks.com/?p=19
            // you are required to call startPreview() first before calling takePicture()
            System.Console.WriteLine("before takePicture " + new { f });

            camera.setErrorCallback(
                new XErrorCallback
                {
                    yield = (err, c) =>
                    {
                        System.Console.WriteLine(new { err });
                    }

                }
            );

            // preview ready?

            var at_setPreviewCallback = new EventWaitHandle(false, EventResetMode.ManualReset);

            System.Console.WriteLine("before setPreviewCallback ");
            // is this of any use?
            camera.setOneShotPreviewCallback(
                new XCameraPreviewCallback
                {
                    yield = delegate
                    {
                        at_setPreviewCallback.Set();
                    }
                }
            );

            at_setPreviewCallback.WaitOne();
            System.Console.WriteLine("after setPreviewCallback ");
            Thread.Sleep(150);


            camera.takePicture(
                null, null,
                new XCameraPictureCallback
                {
                    yield = (data, c) =>
                    {
                        System.Console.WriteLine("enter XCameraPictureCallback " + new { data.Length });

                        if (data.Length > 0)
                        {
                            var bmp = BitmapFactory.decodeByteArray(data, 0, data.Length);



                            File directory = new File(path);
                            directory.mkdirs();

                            ByteArrayOutputStream bytes = new ByteArrayOutputStream();
                            bmp.compress(Bitmap.CompressFormat.JPEG, 100, bytes);


                            try
                            {
                                f.createNewFile();

                                FileOutputStream fo = new FileOutputStream(f);
                                fo.write(bytes.toByteArray());
                            }
                            catch
                            {
                                throw;
                            }
                        }
                        System.Console.WriteLine("exit XCameraPictureCallback");

                        camera.release();

                        done();

                        //[javac] V:\src\CameraExperiment\ApplicationWebService___c__DisplayClass2.java:54: cannot find symbol
                        //[javac] symbol  : method Set()
                        //[javac] location: class ScriptCoreLibJava.BCLImplementation.System.Threading.__AutoResetEvent
                        //[javac]         this.a.Set();
                        //[javac]               ^

                        a.Set();
                    }
                }
            );




            //            I/System.Console( 6264): before takePicture { f = /mnt/sdcard/Pictures/shot.jpg }
            //I/System.Console( 6264): { width = 2560, height = 1920 }
            //I/System.Console( 6264): { width = 2560, height = 1536 }
            //I/System.Console( 6264): { width = 2048, height = 1536 }
            //I/System.Console( 6264): { width = 2048, height = 1232 }
            //I/System.Console( 6264): { width = 1600, height = 1200 }
            //I/System.Console( 6264): { width = 1600, height = 960 }
            //I/System.Console( 6264): { width = 800, height = 480 }
            //I/System.Console( 6264): { width = 640, height = 480 }
            //I/ShotSingle(   84): ShotSingle::takePicture start
            //I/ShotSingle(   84): ShotSingle::takePicture end
            //I/System.Console( 6264): after takePicture
            //        }
            //    }
            //);



            System.Console.WriteLine("will wait for takePicture to complete ... " + new { f });
            a.WaitOne();
            return f;
        }

    }
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        public void TakePicture(string e, Action<string> y)
        {
            System.Console.WriteLine("enter TakePicture ");
            try
            {

                for (int i = 0; i < android.hardware.Camera.getNumberOfCameras(); i++)
                {
                    var f = foo.InternalTakePicture(i);
                    System.Console.WriteLine("after takePicture");

                    // Send it back to the caller.
                    y(f.ToString());

                }

            }
            catch (Exception ex)
            {
                System.Console.WriteLine("error takePicture " + new { ex.Message, ex.StackTrace });

            }
        }

        const string thumb = "/thumb";
        const string io = "/io";

        // refactor this into separate partial class file
        public void Handler(WebServiceHandler h)
        {


            var path = h.Context.Request.Path;

            var is_io = path.StartsWith(io);
            var is_thumb = path.StartsWith(thumb);

            if (is_io || is_thumb)
            {


                var filepath = path.SkipUntilIfAny(io);

                if (is_thumb)
                {
                    filepath = path.SkipUntilIfAny(thumb);
                }

                // is this still a problem?
                filepath = filepath.Replace("%20", " ");

                var file = new File(filepath);


                if (file.exists())
                    if (file.isFile())
                        if (path.EndsWith(".jpg"))
                        {
                            var bytes = InternalReadBytes(filepath, is_thumb);

                            h.Context.Response.ContentType = "image/jpg";

                            // http://www.webscalingblog.com/performance/caching-http-headers-cache-control-max-age.html
                            h.Context.Response.AddHeader("Cache-Control", "max-age=2592000");

                            // send all the bytes

                            h.Context.Response.OutputStream.Write(bytes, 0, bytes.Length);



                            h.CompleteRequest();
                            return;
                        }

                h.Context.Response.ContentType = "text/html";
                h.Context.Response.Write("what ya lookin for?");
                h.Context.Response.Write(new XElement("pre", filepath).ToString());
                h.CompleteRequest();
                return;
            }
        }

        public void GetEXIF(string path, Action<string> yield)
        {

            var is_io = path.StartsWith(io);
            var is_thumb = path.StartsWith(thumb);

            var filepath = path.SkipUntilIfAny(io);

            if (is_thumb)
            {
                filepath = path.SkipUntilIfAny(thumb);
            }

            // is this still a problem?
            filepath = filepath.Replace("%20", " ");

            var file = new File(filepath);


            if (file.exists())
                if (file.isFile())
                    if (path.EndsWith(".jpg"))
                    {

                        //file.print(yield);

                    }
        }



        private static byte[] InternalReadBytes(string filepath, bool thumb = true)
        {
            var mImageData = (sbyte[])(object)System.IO.File.ReadAllBytes(filepath);

            if (thumb)
            {
                // http://stackoverflow.com/questions/2577221/android-how-to-create-runtime-thumbnail
                int THUMBNAIL_HEIGHT = 96;

                //int THUMBNAIL_WIDTH = 66;

                var imageBitmap = BitmapFactory.decodeByteArray(mImageData, 0, mImageData.Length);
                float width = imageBitmap.getWidth();
                float height = imageBitmap.getHeight();
                float ratio = width / height;
                imageBitmap = Bitmap.createScaledBitmap(imageBitmap, (int)(THUMBNAIL_HEIGHT * ratio), THUMBNAIL_HEIGHT, false);

                //int padding = (THUMBNAIL_WIDTH - imageBitmap.getWidth()) / 2;
                //imageView.setPadding(padding, 0, padding, 0);
                //imageView.setImageBitmap(imageBitmap);



                ByteArrayOutputStream baos = new ByteArrayOutputStream();
                // http://developer.android.com/reference/android/graphics/Bitmap.html
                imageBitmap.compress(Bitmap.CompressFormat.PNG, 0, baos);
                mImageData = baos.toByteArray();

            }

            return (byte[])(object)mImageData;
        }
    }

    class XAutoFocus : android.hardware.Camera.AutoFocusCallback
    {
        public Action<bool, android.hardware.Camera> yield;

        public void onAutoFocus(bool arg0, android.hardware.Camera arg1)
        {
            yield(arg0, arg1);
        }
    }

    class XCameraPictureCallback : android.hardware.Camera.PictureCallback
    {
        public Action<sbyte[], android.hardware.Camera> yield;

        public void onPictureTaken(sbyte[] arg0, android.hardware.Camera arg1)
        {
            yield(arg0, arg1);
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

    class XErrorCallback : android.hardware.Camera.ErrorCallback
    {
        public Action<int, android.hardware.Camera> yield;



        public void onError(int arg0, android.hardware.Camera arg1)
        {
            yield(arg0, arg1);
        }
    }

    class XSurfaceHolder_Callback : SurfaceHolder_Callback
    {
        public Action yield_surfaceCreated;

        public void surfaceChanged(SurfaceHolder arg0, int arg1, int arg2, int arg3)
        {
        }

        public void surfaceCreated(SurfaceHolder value)
        {
            yield_surfaceCreated();
        }

        public void surfaceDestroyed(SurfaceHolder value)
        {
        }
    }
}
