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

        // -compile:
        //[javac] Compiling 705 source files to V:\bin\classes
        //[javac] V:\src\com\abstractatech\dcimgalleryapp\foo.java:223: error: method setErrorCallback in class Camera cannot be applied to given types;
        //[javac]         camera25.setErrorCallback(callback28);
        //[javac]                 ^
        //[javac]   required: ErrorCallback
        //[javac]   found: XErrorCallback
        //[javac]   reason: actual argument XErrorCallback cannot be converted to ErrorCallback by method invocation conversion
        //[javac] V:\src\com\abstractatech\dcimgalleryapp\foo.java:240: error: method setOneShotPreviewCallback in class Camera cannot be applied to given types;
        //[javac]         camera29.setOneShotPreviewCallback(callback30);
        //[javac]                 ^
        //[javac]   required: PreviewCallback
        //[javac]   found: XCameraPreviewCallback
        //[javac]   reason: actual argument XCameraPreviewCallback cannot be converted to PreviewCallback by method invocation conversion
        //[javac] V:\src\com\abstractatech\dcimgalleryapp\foo.java:258: error: no suitable method found for takePicture(<null>,<null>,XCameraPictureCallback)
        //[javac]         camera31.takePicture(null, null, callback32);
        //[javac]                 ^
        //[javac]     method Camera.takePicture(ShutterCallback,PictureCallback,PictureCallback,PictureCallback) is not applicable
        //[javac]       (actual and formal argument lists differ in length)
        //[javac]     method Camera.takePicture(ShutterCallback,PictureCallback,PictureCallback) is not applicable
        //[javac]       (actual argument XCameraPictureCallback cannot be converted to PictureCallback by method invocation conversion)

        [Script(IsNative = true)]
        public interface ErrorCallback
        {
            void onError(int arg0, global::android.hardware.Camera arg1);

        }

        public void setErrorCallback(ErrorCallback cb)
        {
        }

        [Script(IsNative = true)]
        public interface PreviewCallback
        {
            void onPreviewFrame(sbyte[] arg0, global::android.hardware.Camera arg1);

        }

        public void setOneShotPreviewCallback(PreviewCallback cb)
        { }


        [Script(IsNative = true)]
        public interface PictureCallback
        {
            void onPictureTaken(sbyte[] arg0, global::android.hardware.Camera arg1);

        }

        [Script(IsNative = true)]
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


        [Script(IsNative = true)]
        public interface AutoFocusCallback
        {
            void onAutoFocus(bool arg0, global::android.hardware.Camera arg1);
        }

        [Script(IsNative = true)]
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


            // X:\jsc.svn\examples\java\android\AndroidCameraActivity\AndroidCameraActivity\ApplicationActivity.cs
            public void setPictureFormat(int f)
            { }

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

        [Script(IsNative = true)]
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

        public void stopPreview()
        {
        }
    }
}
