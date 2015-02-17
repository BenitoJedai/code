using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;
using android.content;
using android.hardware;
using java.io;

namespace AndroidCameraActivity.Activities
{
    public class ApplicationActivity : Activity
    {
        //        [javac]
        //        Compiling 614 source files to W:\bin\classes
        //[javac] W:\src\AndroidCameraActivity\Activities\ApplicationActivity_Preview.java:40: error: bad operand types for binary operator '>'
        // [javac]             if (((this.oncamera > null)))
        // [javac]                                 ^


        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;

        public class Preview : SurfaceView, SurfaceHolder_Callback
        {
            public Action<Camera> oncamera;

            public Camera camera;

            SurfaceHolder mHolder;

            public Preview(Context context)
                : base(context)
            {
                mHolder = getHolder();
                mHolder.addCallback(this);

                // http://developer.android.com/reference/android/view/SurfaceHolder.html#SURFACE_TYPE_PUSH_BUFFERS
                mHolder.setType(3);
            }

            public void surfaceCreated(SurfaceHolder holder)
            {
                // http://developer.android.com/reference/android/hardware/Camera.html#open(int)
                camera = Camera.open(0);
                try
                {
                    camera.setPreviewDisplay(holder);

                    if (oncamera != null)
                        oncamera(camera);
                }
                catch
                {
                }
            }

            public void surfaceDestroyed(SurfaceHolder holder)
            {
                camera.stopPreview();
                camera.release();
                camera = null;
            }

            public void surfaceChanged(SurfaceHolder holder, int format, int w, int h)
            {
                Camera.Parameters parameters = camera.getParameters();
                //parameters.setPreviewSize(640, 480);
                parameters.setPictureFormat(android.graphics.PixelFormat.JPEG);
                camera.setParameters(parameters);
                camera.startPreview();
            }
        }

        class takePicture_handler : Camera.PictureCallback
        {
            public Action<sbyte[]> handler;

            public void onPictureTaken(sbyte[] data, Camera camera)
            {
                // jsc, if byte[] is only primitive name it data
                // jsc, if camera is only object name it such

                handler(data);
            }
        }

        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            //ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);


            var b = new Button(this);
            ll.addView(b);

            var p = new Preview(this);

            b.WithText("take a picture");

            p.oncamera =
                camera =>
                    b.AtClick(
                        v =>
                        {
                            camera.takePicture(null, null,
                                new takePicture_handler
                                {
                                    handler =
                                        data =>
                                        {
                                            b.WithText("at click");
                                            try
                                            {
                                                // http://stackoverflow.com/questions/11874273/android-nexus-7-jelly-bean-startpreview-takepicture-calling-getcamerastereomode

                                                //E/NvOmxCamera(  126): OMX_ERRORTYPE android::NvOmxCamera::getCameraStereoMode(NvxComponent*, NvOmxCameraUserStereoMode&): Error: invalid NVX mode 0.
                                                //E/NvOmxCamera(  126): OMX_ERRORTYPE android::NvOmxCamera::getCameraStereoModeAndCaptureInfo(NvxComponent*, NvOmxCameraUserStereoMode&, NVX_STEREOCAPTUREINFO&): getCameraStereoMode failed with 0x00000000
                                                //D/NvOsDebugPrintf(  126): NvMMLiteJPEGEncSetAttribute: Incorrect value 0 for stereo capture type
                                                //E/NvOmxCameraSettings(  126): OMX_ERRORTYPE android::programStereoInfo(OMX_HANDLETYPE, const NVX_STEREOCAPTUREINFO&, android::NvxWrappers*): pNvxWrappers->OMX_SetConfigIL failed with 0x80001005
                                                //D/NvOsDebugPrintf(  126): Tryproc: INBuffer-Values of Width and Height 1280 960
                                                //D/dalvikvm(29535): GC_FOR_ALLOC freed 6686K, 52% free 7716K/15943K, paused 25ms, total 27ms


                                                var SAVE_PATH = android.os.Environment.getExternalStoragePublicDirectory(
                                                    android.os.Environment.DIRECTORY_PICTURES
                                                    );


                                                SAVE_PATH.mkdirs();

                                                var bmp = android.graphics.BitmapFactory.decodeByteArray(data, 0, data.Length);

                                                ByteArrayOutputStream bytes = new ByteArrayOutputStream();
                                                bmp.compress(android.graphics.Bitmap.CompressFormat.JPEG, 100, bytes);

                                                File f = new File(SAVE_PATH.ToString() + "/hello2.jpg");



                                                f.createNewFile();

                                                FileOutputStream fo = new FileOutputStream(f);
                                                fo.write(bytes.toByteArray());

                                                Intent intent = new Intent();
                                                intent.setAction(android.content.Intent.ACTION_VIEW);

                                                var imgUri = android.net.Uri.fromFile(f);

                                                intent.setDataAndType(imgUri, "image/*");

                                                b.WithText("done!");

                                                startActivity(intent);

                                            }
                                            // Error	1	The type caught or thrown must be derived from System.Exception	y:\jsc.svn\examples\java\android\AndroidCameraActivity\AndroidCameraActivity\ApplicationActivity.cs	154	52	AndroidCameraActivity
                                            catch (Exception ex)
                                            {
                                                b.WithText("saving.. error! " + ex.Message);

                                                //throw;
                                            }


                                        }
                                }
                            );

                        }
                    );


            this.setContentView(p);
            this.addContentView(sv, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WRAP_CONTENT, ViewGroup.LayoutParams.WRAP_CONTENT));
        }


    }


}

namespace ScriptCoreLib
{
    [Script(
   HasNoPrototype = true,
  Implements = typeof(global::System.Exception),
  ImplementationType = typeof(java.lang.Throwable))]
    internal class __Exception
    {
        public __Exception() { }
        public __Exception(string e) { }
        public string Message
        {
            [Script(ExternalTarget = "getMessage")]
            get
            { return default(string); }
        }
    }
}