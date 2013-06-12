using android.app;
using android.content;
using android.graphics;
using android.hardware;
using android.view;
using java.io;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
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
        public static File InternalTakePicture()
        {
            var SAVE_PATH = android.os.Environment.getExternalStoragePublicDirectory(
          android.os.Environment.DIRECTORY_PICTURES) + "/";
            var f = new File(SAVE_PATH + "shot.jpg");


            var camera = android.hardware.Camera.open(0);


            var p = camera.getParameters();


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
            for (int i = 0; i < focusModes.size(); i++)
            {
                var focusMode = (string)focusModes.get(i);

                System.Console.WriteLine(new { focusMode });
            }

            System.Console.WriteLine("before setFocusMode ");
            p.setFocusMode(android.hardware.Camera.Parameters.FOCUS_MODE_INFINITY);


            //            E/SecCamera(   84): ERR(int android::fimc_poll(pollfd*)):No data in 10 secs..
            //I/ShotSingle(   84): CAMERA_MSG_COMPRESSED_IMAGE

            camera.setParameters(p);

            // http://stackoverflow.com/questions/9744790/android-possible-to-camera-capture-without-a-preview
            var b = new EventWaitHandle(false, EventResetMode.ManualReset);
            System.Console.WriteLine("before startPreview ");

            try
            {
                // #5 java.lang.RuntimeException: Can't create handler inside thread that has not called Looper.prepare()

                (ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext as Activity).runOnUiThread(
                    new f
                    {
                        y = delegate
                        {
                            try
                            {

                                // here, the unused surface view and holder
                                var dummy = new SurfaceView(ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext);

                                camera.setPreviewDisplay(dummy.getHolder());
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


            }
            catch
            {
                throw;
            }
            b.WaitOne();

            camera.@lock();
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



                            File directory = new File(SAVE_PATH);
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
                var f = foo.InternalTakePicture();
                System.Console.WriteLine("after takePicture");

                // Send it back to the caller.
                y(f.ToString());
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("error takePicture " + new { ex.Message, ex.StackTrace });

            }
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


}
