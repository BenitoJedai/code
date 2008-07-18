using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Drawing;
using System.Drawing.Imaging;

namespace RayCaster4.ActionScript
{
    /// <summary>
    /// This class defines the extension methods for this project
    /// </summary>
    [Script]
    internal static class Extensions
    {
        [Script(OptimizedCode = "return (uint)i;")]
        public static uint ToUInt32(this int i)
        {
            return (uint)i;
        }

        [Script(OptimizedCode = "return (int)i;")]
        public static int ToInt32(this uint i)
        {
            return (int)i;
        }

        [Script(OptimizedCode = "return (int)i;")]
        public static int ToInt32(this ulong i)
        {
            return (int)i;
        }

        public static void setPixel(this Bitmap b, int x, int y, uint c)
        {
            ulong a = (c | 0xff000000);

            b.SetPixel(x, y, Color.FromArgb(a.ToInt32()));
        }

        public static BitmapData @lock(this Bitmap b)
        {
            return b.LockBits(
                new Rectangle(0, 0, b.Width, b.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.DontCare
                );
        }



        public static int Floor(this int e)
        {
            return e;
        }

        //[Script(OptimizedCode = "return int(e);")]
        //[Script(IsDebugCode = true)]
        public static int Floor(this double e)
        {
            return (int)e;
        }

    }
}
