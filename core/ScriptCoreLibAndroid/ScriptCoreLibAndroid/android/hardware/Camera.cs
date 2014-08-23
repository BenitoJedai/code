using android.view;
using ScriptCoreLib;


namespace android.hardware
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/hardware/Camera.java


    [Script(IsNative = true)]
    public partial class Camera
    {
        // include/ui/Camera.h

        // X:\jsc.svn\examples\java\android\AndroidCameraActivity\AndroidCameraActivity\ApplicationActivity.cs
        // X:\jsc.svn\examples\javascript\android\com.abstractatech.dcimgalleryapp\com.abstractatech.dcimgalleryapp\ApplicationWebService.cs

        // tested by ?


        public void release()
        {
        }

        //public native static int getNumberOfCameras();
        public static int getNumberOfCameras()
        {
            return default(int);
        }

        public static Camera open(int cameraId)
        {
            // can we get camera feeds to our webgl example?
            return default(Camera);

        }

        public interface ErrorCallback
        {
            void onError(int arg0, global::android.hardware.Camera arg1);

        }

        public void setErrorCallback(ErrorCallback cb)
        {
        }

        public interface PreviewCallback
        {
            void onPreviewFrame(sbyte[] arg0, global::android.hardware.Camera arg1);

        }

        public void setOneShotPreviewCallback(PreviewCallback cb)
        { }


        public interface PictureCallback
        {
            void onPictureTaken(sbyte[] arg0, global::android.hardware.Camera arg1);

        }

        public interface ShutterCallback
        { }


        public void takePicture(ShutterCallback shutter, PictureCallback raw,
            PictureCallback jpeg)
        {
            // will jsc inline such oneliners?
            takePicture(shutter, raw, null, jpeg);
        }

        public void takePicture(ShutterCallback shutter, PictureCallback raw,
            PictureCallback postview, PictureCallback jpeg)
        {
        }


        public interface AutoFocusCallback
        {
            void onAutoFocus(bool arg0, global::android.hardware.Camera arg1);
        }

        public class Parameters
        {
            // Parameter keys to communicate with the camera driver.
            public static readonly string FOCUS_MODE_FIXED = "fixed";
            public static readonly string FOCUS_MODE_INFINITY = "infinity";

            public void setRotation(int rotation)
            {
            }

            public java.util.List<Size> getSupportedPictureSizes()
            {
                return default(java.util.List<Size>);
            }

            public void setPictureSize(int width, int height)
            {
            }

            // is jsc java natives generating generic type info?
            public java.util.List<string> getSupportedFocusModes()
            {
                return default(java.util.List<string>);
            }

            public void setFocusMode(string value)
            {
            }
        }

        public class Size
        {
            public int width;
            public int height;
        }

        public Parameters getParameters()
        {
            return default(Parameters);
        }

        public void setParameters(Parameters @params)
        {
        }

        public void setPreviewDisplay(SurfaceHolder holder)
        {
        }

        //public native final void startPreview();
        public void startPreview()
        {
        }
    }
}
