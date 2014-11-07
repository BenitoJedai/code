using android.content;
using android.graphics;
using android.net.wifi;
using android.view;
//using com.drew.imaging;
//using com.drew.metadata;
using java.io;
using java.net;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace com.abstractatech.dcimgalleryapp
{
    using System.ComponentModel;
    using System.Threading.Tasks;
    using ystring = Action<string>;


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
        //BUILD FAILED
        //C:\util\android-sdk-windows\tools\ant\build.xml:1139: The following error occurred while executing this line:
        //C:\util\android-sdk-windows\tools\ant\build.xml:1151: X:\jsc.internal.svn\keystore\asus\my-release-key.keystore (The system cannot find the path specified)


        public static File InternalTakePicture(int num = 0)
        {
            // do we have a special test for this?
            // X:\jsc.svn\examples\javascript\android\AndroidEnvironmentWebActivity\AndroidEnvironmentWebActivity\ApplicationWebService.cs

            var DIRECTORY_DCIM = global::android.os.Environment.DIRECTORY_DCIM;


            var path = global::android.os.Environment.getExternalStoragePublicDirectory(DIRECTORY_DCIM).getAbsolutePath();
            path += "/Camera";


            //var SAVE_PATH = android.os.Environment.getExternalStoragePublicDirectory(
            //    android.os.Environment.DIRECTORY_PICTURES
            //) + "/";

            var n = DateTime.Now;

            var filepath = path + "/shot" + n.Ticks + ".jpg";

            var f = new File(filepath);

            System.Console.WriteLine("InternalTakePicture " + new { filepath });

            //I/System.Console(31472): enter TakePicture
            //W/CameraService(  128): CameraService::connect X (pid 31472) rejected (existing client).
            //I/System.Console(31472): error takePicture { Message = Fail to connect to camera service, StackTrace = java.lang.RuntimeException: Fail to connect to camera service
            //I/System.Console(31472):        at android.hardware.Camera.native_setup(Native Method)
            //I/System.Console(31472):        at android.hardware.Camera.<init>(Camera.java:340)
            //I/System.Console(31472):        at android.hardware.Camera.open(Camera.java:302)
            var camera = global::android.hardware.Camera.open(num);

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

            var min = default(global::android.hardware.Camera.Size);

            for (int i = 0; i < s.size(); i++)
            {
                var size = (global::android.hardware.Camera.Size)s.get(i);

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
            var NextFocus = global::android.hardware.Camera.Parameters.FOCUS_MODE_FIXED;

            for (int i = 0; i < focusModes.size(); i++)
            {
                var focusMode = (string)focusModes.get(i);

                if (focusMode == global::android.hardware.Camera.Parameters.FOCUS_MODE_INFINITY)
                    NextFocus = global::android.hardware.Camera.Parameters.FOCUS_MODE_INFINITY;

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

            //I/System.Console(18439): enter TakePicture
            //I/System.Console(18439): InternalTakePicture { filepath = /storage/emulated/0/DCIM/Camera/shot635099960653670000.jpg }
            //I/AwesomePlayer(  131): setDataSource_l(URL suppressed)
            //D/dalvikvm(18439): GC_CONCURRENT freed 705K, 8% free 8765K/9500K, paused 4ms+3ms, total 27ms
            //I/AwesomePlayer(  131): setDataSource_l(URL suppressed)
            //I/CameraClient(  131): Opening camera 0
            //I/CameraHAL(  131): camera_device open
            //I/System.Console(18439): { width = 2592, height = 1944 }
            //I/System.Console(18439): { width = 2592, height = 1728 }
            //I/System.Console(18439): { width = 2592, height = 1458 }
            //I/System.Console(18439): { width = 2048, height = 1536 }
            //I/System.Console(18439): { width = 1600, height = 1200 }
            //I/System.Console(18439): { width = 1280, height = 1024 }
            //I/System.Console(18439): { width = 1152, height = 864 }
            //I/System.Console(18439): { width = 1280, height = 960 }
            //I/System.Console(18439): { width = 640, height = 480 }
            //I/System.Console(18439): { width = 320, height = 240 }
            //I/System.Console(18439): before setPictureSize
            //I/System.Console(18439): { focusMode = continuous-video }
            //I/System.Console(18439): { focusMode = auto }
            //I/System.Console(18439): { focusMode = macro }
            //I/System.Console(18439): { focusMode = infinity }
            //I/System.Console(18439): { focusMode = infinity }
            //I/System.Console(18439): { focusMode = continuous-picture }
            //I/System.Console(18439): before setFocusMode { NextFocus = infinity }

            //I/System.Console(18439): before startPreview
            //I/System.Console(18439): before getHolder { CurrentContext = com.abstractatech.dcimgalleryapp.ApplicationWebServiceActivity@4165c688 }
            //I/System.Console(18439): wait for startPreview...
            //I/System.Console(18439): before yield_surfaceCreated
            //I/System.Console(18439): before addContentView


            Action done = delegate { };

            try
            {
                // #5 java.lang.RuntimeException: Can't create handler inside thread that has not called Looper.prepare()

                (ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext as global::android.app.Activity).With(
                aa =>
                {
                    aa.runOnUiThread(
                       new f
                       {


                           y = delegate
                           {
                               // the activity must be visible?

                               try
                               {
                                   // D/Camera  ( 2464): app passed NULL surface

                                   System.Console.WriteLine("before getHolder " + new { ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext });

                                   //  the nexus 7 and droid x both don't support the passing of a dummy surfaceview to a camera object. Your response that all camera things must created in the activity is false. I was able to instantiate a camera within a thread by passing it a view just fine. 

                                   // here, the unused surface view and holder
                                   var SurfaceView = new SurfaceView(aa);

                                   // missing for android 2.2
                                   //dummy.setScaleX(0f);
                                   //dummy.setScaleY(0f);

                                   // https://code.google.com/p/android/issues/detail?id=28238
                                   // http://androidforums.com/application-development/21044-surfacecreated-never-called.html
                                   // https://groups.google.com/forum/#!topic/android-developers/JLVnKdsAKeg
                                   // http://grokbase.com/t/gg/android-developers/11bvbr8jp8/surfaceholder-callback-surfacecreated-not-being-triggered-when-surface-is-re-created
                                   // https://code.google.com/p/android/issues/detail?id=28238
                                   var h = SurfaceView.getHolder();

                                   // http://developer.android.com/reference/android/view/SurfaceHolder.html#SURFACE_TYPE_PUSH_BUFFERS
                                   var SURFACE_TYPE_PUSH_BUFFERS = 0x00000003;
                                   h.setType(SURFACE_TYPE_PUSH_BUFFERS);

                                   System.Console.WriteLine("before yield_surfaceCreated");

                                   #region yield_surfaceCreated
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
                                                   System.Console.WriteLine("error at at yield_surfaceCreated!");
                                                   throw;
                                               }
                                           }
                                       }
                                   );
                                   #endregion


                                   //h.setType(SurfaceHolder.SURFACE_TYPE_PUSH_BUFFERS);

                                   System.Console.WriteLine("before addContentView");

                                   // http://developer.android.com/reference/android/app/Activity.html
                                   aa.addContentView(
                                       SurfaceView,

                                       new global::android.widget.LinearLayout.LayoutParams(
                                     global::android.widget.LinearLayout.LayoutParams.WRAP_CONTENT,
                                     global::android.widget.LinearLayout.LayoutParams.WRAP_CONTENT
                                     )
                                   );

                                   //SurfaceView.getParent().r
                                   //aa.getChangingConfigurations
                                   done = delegate
                                   {
                                       aa.runOnUiThread(
                                          new f
                                          {
                                              y = delegate
                                              {
                                                  // https://groups.google.com/forum/?fromgroups#!topic/android-developers/liph4z9LnFA
                                                  // http://stackoverflow.com/questions/3080577/removing-a-view-from-an-activity

                                                  // how to Orphanize??
                                                  //SurfaceView.setVisibility(View.GONE);
                                                  System.Console.WriteLine("time to remove SurfaceView");

                                                  //[javac] Compiling 589 source files to V:\bin\classes
                                                  //[javac] V:\src\com\abstractatech\dcimgalleryapp\foo___c__DisplayClass13___c__DisplayClass15___c__DisplayClass17.java:90: error: ';' expected
                                                  //[javac]     private static ViewGroup _<InternalTakePicture>b__b_Isinst_0017(Object _0017)
                                                  //[javac]                               ^
                                                  //[javac] V:\src\com\abstractatech\dcimgalleryapp\foo___c__DisplayClass13___c__DisplayClass15___c__DisplayClass17.java:90: error: invalid method declaration; return type required
                                                  //[javac]     private static ViewGroup _<InternalTakePicture>b__b_Isinst_0017(Object _0017)
                                                  //[javac]                                                    ^
                                                  //[javac] 2 errors

                                                  SurfaceView.Orphanize();



                                              }
                                          }
                                      );

                                   };



                                   System.Console.WriteLine("callback to yield_surfaceCreated?");
                               }
                               catch
                               {
                                   System.Console.WriteLine("InternalTakePicture failed!");
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
            System.Console.WriteLine("wait for startPreview... ");
            //error takePicture { Message = Fail to connect to camera service, StackTrace = java.lang.RuntimeException: Fail to connect to camera service
            //       at android.hardware.Camera.native_setup(Native Method)
            //       at android.hardware.Camera.<init>(Camera.java:340)
            //       at android.hardware.Camera.open(Camera.java:302)

            b.WaitOne();
            System.Console.WriteLine("wait for startPreview... done");

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
    public class ApplicationWebService
    {
        public void TakePicture(string e, Action<string> y)
        {
            System.Console.WriteLine("enter TakePicture ");

            for (int cameraid = 0; cameraid < global::android.hardware.Camera.getNumberOfCameras(); cameraid++)
            {

                try
                {

                    var f = foo.InternalTakePicture(cameraid);
                    System.Console.WriteLine("after takePicture");

                    // Send it back to the caller.
                    y(f.ToString());

                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("error takePicture " + new { cameraid, ex.Message, ex.StackTrace });

                }
            }


        }



        public int skip = 0;
        public int take = 4;

        public Task File_list(
            // jsc, can we have events yet, thanks
            ystring yfile
            )
        {
            // this will crash for empty nexus

            var skip = this.skip;
            var take = this.take;


            var DIRECTORY_DCIM = global::android.os.Environment.DIRECTORY_DCIM;

            var path = global::android.os.Environment.getExternalStoragePublicDirectory(DIRECTORY_DCIM).getAbsolutePath();
            path += "/Camera";

            var f = new File(path);

            //Caused by: java.lang.Runtim
            //eException: Implement IComparable for java.lang.Long vs java.lang.Long
            //       at ScriptCoreLibJava.BCLImplementation.System.Collections.__Comparer.Compare(__Comparer.java:136)

            var a = f.listFiles().OrderByDescending(k => (double)k.lastModified()).ToArray();

            //foreach (var item in a)
            //{
            //    if (new File(path + "/" + item).isDirectory())
            //        ydirectory(path + "/" + item);
            //}


            foreach (var item in a)
            {
                if (skip > 0)
                {
                    skip--;
                }
                else
                {
                    if (take > 0)
                    {
                        take--;


                        yfile(path + "/" + item.getName());

                    }
                    else
                    {
                        break;
                    }
                }
            }

            return "".AsResult();
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

        public void GetEXIF(string path, ystring yield)
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

                        file.print(yield);

                    }
        }



        private static byte[] InternalReadBytes(string filepath, bool thumb = true)
        {
            var mImageData = (sbyte[])(object)System.IO.File.ReadAllBytes(filepath);

            if (thumb)
            {
                // X:\jsc.svn\examples\javascript\android\EXIFThumbnail\EXIFThumbnail\ApplicationWebService.cs

                //                [javac] V:\src\com\abstractatech\dcimgalleryapp\ApplicationWebService.java:263: error: unreported exception ImageProcessingException; must be caught or declared to be thrown
                //[javac]             metadata1 = ImageMetadataReader.readMetadata(new File(filepath));

                try
                {
#if xmetadata
  //<package id="AndroidMetadataExtractor" version="1.0.0.0" targetFramework="net40" />
                    var m = ImageMetadataReader.readMetadata(new File(filepath));

                    // http://stackoverflow.com/questions/10166373/metadata-extraction-java

                    var t = typeof(com.drew.metadata.exif.ExifThumbnailDirectory).ToClass();
                    if (m.containsDirectory(t))
                    {
                        var x = (com.drew.metadata.exif.ExifThumbnailDirectory)m.getDirectory(t);

                        System.Console.WriteLine(
                           filepath
                        );

                        mImageData = x.getThumbnailData();

                    }
#endif

                }
                catch
                {
                    // skip
                }


                if (mImageData == null)
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

            }

            return (byte[])(object)mImageData;
        }









#if false
        public void DownloadSDK(WebServiceHandler h)
        {
            var HostUri = new
            {
                Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":"),
                Port = int.Parse(h.Context.Request.Headers["Host"].SkipUntilIfAny(":"))
            };


            //#if DEBUG
            //            if (InternalMulticast == null)
            //                InternalMulticast = new WithClickOnceLANLauncher.ApplicationWebServiceMulticast
            //                {
            //                    Host = HostUri.Host,
            //                    Port = HostUri.Port,

            //                };
            //#else
            //            if (InternalMulticast == null)
            //                InternalMulticast = new AndroidApplicationWebServiceMulticast
            //                {
            //                    Host = HostUri.Host,
            //                    Port = HostUri.Port,

            //                };
            //#endif

            DownloadSDKFunction.DownloadSDK(h);

        }
#endif

        //#if DEBUG
        //        static WithClickOnceLANLauncher.ApplicationWebServiceMulticast InternalMulticast;
        //#else
        //        static AndroidApplicationWebServiceMulticast InternalMulticast;

        //#endif

    }




    [Obsolete("every jsc application is now adevertising")]
    [DesignerCategory("code")]
    class AndroidApplicationWebServiceMulticast : System.ComponentModel.Component
    {
        WifiManager wifi;
        WifiManager.MulticastLock multicastLock;

        public event Action<string> AtData;

        public AndroidApplicationWebServiceMulticast()
        {
            AtData += AndroidApplicationWebServiceMulticast_AtData;

            new Thread(
                delegate()
                {
                    // http://stackoverflow.com/questions/12610415/multicast-receiver-malfunction
                    // http://answers.unity3d.com/questions/250732/android-build-is-not-receiving-udp-broadcasts.html

                    // Acquire multicast lock
                    wifi = (WifiManager)
                        ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext.getSystemService(Context.WIFI_SERVICE);
                    multicastLock = wifi.createMulticastLock("multicastLock");
                    //multicastLock.setReferenceCounted(true);
                    multicastLock.acquire();

                    System.Console.WriteLine("LANBroadcastListener ready...");
                    try
                    {
                        byte[] b = new byte[0x100];

                        // https://code.google.com/p/android/issues/detail?id=40003

                        MulticastSocket socket = new MulticastSocket(40404); // must bind receive side
                        socket.setBroadcast(true);
                        socket.setReuseAddress(true);
                        socket.setTimeToLive(30);
                        socket.setReceiveBufferSize(0x100);

                        socket.joinGroup(InetAddress.getByName("239.1.2.3"));
                        System.Console.WriteLine("LANBroadcastListener joinGroup...");
                        while (true)
                        {
                            DatagramPacket dgram = new DatagramPacket((sbyte[])(object)b, b.Length);
                            socket.receive(dgram); // blocks until a datagram is received

                            var bytes = new System.IO.MemoryStream((byte[])(object)dgram.getData(), 0, dgram.getLength());


                            var listen = Encoding.UTF8.GetString(bytes.ToArray());



                            //dgram.setLength(b.Length); // must reset length field!s

                            if (AtData != null)
                                AtData(listen);

                        }
                    }
                    catch
                    {
                        System.Console.WriteLine("client error");
                    }
                }
            )
            {

                Name = "client"
            }.Start();


        }

        void AndroidApplicationWebServiceMulticast_AtData(string listen)
        {
            System.Console.WriteLine(

               new { server = new { listen } }
               );

            try
            {
                var xml = XElement.Parse(listen);

                if (xml.Value.StartsWith("Where are you?"))
                {
                    this.Send(
                        "Visit me at " + this.Host + ":" + this.Port
                    );

                }
            }
            catch
            {

            }


        }

        int c;
        void Send(string data)
        {
            /// http://www.daniweb.com/software-development/java/threads/424998/udp-client-server-in-java

            c++;

            //var n = c + " hello world";
            var n =
                new XElement("string",
                    new XAttribute("c", "" + c),
                    data
                ).ToString();

            new Thread(
                delegate()
                {
                    try
                    {
                        DatagramSocket socket = new DatagramSocket(); //construct a datagram socket and binds it to the available port and the localhos
                        byte[] b = Encoding.UTF8.GetBytes(n.ToString());    //creates a variable b of type byte
                        DatagramPacket dgram;
                        dgram = new DatagramPacket((sbyte[])(object)b, b.Length, InetAddress.getByName("239.1.2.3"), 40404);//sends the packet details, length of the packet,destination address and the port number as parameters to the DatagramPacket  

                        socket.send(dgram); //send the datagram packet from this port
                    }
                    catch
                    {
                        System.Console.WriteLine("server error");
                    }
                }
            )
            {

                Name = "server"
            }.Start();
        }

        public int Port { get; set; }
        public string Host { get; set; }

    }






    public static class X
    {
        public static void Orphanize(this View SurfaceView)
        {
            (SurfaceView.getParent() as ViewGroup).With(
                vg =>
                {
                    vg.removeView(SurfaceView);
                }
            );
        }

        public static void print(this File file, ystring yield)
        {
            // https://code.google.com/p/metadata-extractor/wiki/GettingStarted

            var w = new StringBuilder();

            w.AppendLine("metadata: ");

            try
            {
#if xmetadata
  //<package id="AndroidMetadataExtractor" version="1.0.0.0" targetFramework="net40" />

                // http://drewnoakes.com/code/exif/
                Metadata m = ImageMetadataReader.readMetadata(file);


                //            [javac] V:\src\DCIMCameraAppWithThumbnails\ApplicationWebService.java:191: unreported exception com.drew.imaging.ImageProcessingException; must be caught or declared to be thrown
                //[javac]                     metadata4 = ImageMetadataReader.readMetadata(file3);
                //[javac]                                                                 ^


                // Error	12	'com.drew.metadata.Metadata.getDirectories()' is not supported by the language	X:\jsc.svn\examples\javascript\android\com.abstractatech.dcimgalleryapp\com.abstractatech.dcimgalleryapp\ApplicationWebService.cs	1000	25	com.abstractatech.dcimgalleryapp
                // Error	13	'com.drew.metadata.Directory.getErrors()' is not supported by the language	X:\jsc.svn\examples\javascript\android\com.abstractatech.dcimgalleryapp\com.abstractatech.dcimgalleryapp\ApplicationWebService.cs	1019	38	com.abstractatech.dcimgalleryapp

                var i = m.getDirectories().iterator();

                while (i.hasNext())
                {
                    var directory = (Directory)i.next();

                    var tags = directory.getTags().toArray();

                    foreach (Tag tag in tags)
                    {

                        w.AppendLine(new { tag }.ToString());
                    }


                    if (directory.hasErrors())
                    {

                        var ierror = directory.getErrors().iterator();

                        while (ierror.hasNext())
                        {
                            var error = (string)ierror.next();

                            w.AppendLine(new { error }.ToString());
                        }
                    }
                }
#endif

                w.AppendLine("end of metadata");

            }
            catch (Exception ex)
            {
                w.AppendLine("error " + new { ex.Message, ex.StackTrace });
            }
            yield(w.ToString());

        }
    }




    class XAutoFocus : global::android.hardware.Camera.AutoFocusCallback
    {
        public Action<bool, global::android.hardware.Camera> yield;

        public void onAutoFocus(bool arg0, global::android.hardware.Camera arg1)
        {
            yield(arg0, arg1);
        }
    }

    class XCameraPictureCallback : global::android.hardware.Camera.PictureCallback
    {
        public Action<sbyte[], global::android.hardware.Camera> yield;

        public void onPictureTaken(sbyte[] arg0, global::android.hardware.Camera arg1)
        {
            yield(arg0, arg1);
        }
    }

    class XCameraPreviewCallback : global::android.hardware.Camera.PreviewCallback
    {
        public Action<sbyte[], global::android.hardware.Camera> yield;

        public void onPreviewFrame(sbyte[] arg0, global::android.hardware.Camera arg1)
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

    class XSurfaceHolder_Callback : SurfaceHolder_Callback
    {
        public Action yield_surfaceCreated;

        public void surfaceChanged(SurfaceHolder arg0, int arg1, int arg2, int arg3)
        {
            System.Console.WriteLine("surfaceChanged");
        }

        public void surfaceCreated(SurfaceHolder value)
        {
            System.Console.WriteLine("surfaceCreated");
            yield_surfaceCreated();
        }

        public void surfaceDestroyed(SurfaceHolder value)
        {
            System.Console.WriteLine("surfaceDestroyed");
        }
    }

}
