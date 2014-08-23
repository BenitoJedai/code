using java.io;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.graphics
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/graphics/java/android/graphics/Bitmap.java
    // http://developer.android.com/reference/android/graphics/Bitmap.html
    [Script(IsNative = true)]
    public class Bitmap
    {
        // X:\jsc.svn\examples\javascript\android\com.abstractatech.dcimgalleryapp\com.abstractatech.dcimgalleryapp\ApplicationWebService.cs


        public bool compress(CompressFormat format, int quality, OutputStream stream)
        {
            return default(bool);
        }

        public int getWidth()
        {
            return default(int);
        }
        public int getHeight()
        {
            return default(int);
        }
        public static Bitmap createScaledBitmap(Bitmap src, int dstWidth, int dstHeight,
        bool filter)
        {
            return default(Bitmap);
        }

        // http://developer.android.com/reference/android/graphics/Bitmap.CompressFormat.html
        [Script(IsNative = true)]
        public class CompressFormat
        {
            // java enums are interesting..

            public static CompressFormat JPEG;
            public static CompressFormat PNG;

        }

    }
}
