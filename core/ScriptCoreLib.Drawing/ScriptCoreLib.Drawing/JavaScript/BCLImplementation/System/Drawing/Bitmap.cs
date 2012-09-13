using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing
{
    [Script(Implements = typeof(global::System.Drawing.Bitmap))]
    internal class __Bitmap : __Image
    {
        public __Bitmap(int width, int height)
        {

        }

        public BitmapData LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format)
        {
            return default(BitmapData);
        }

        public void UnlockBits(BitmapData bitmapdata)
        {

        }
    }
}
