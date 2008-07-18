using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace RayCaster4.ActionScript
{
    /// <summary>
    /// This class defines the extension methods for this project
    /// </summary>
    [Script]
    internal static class Extensions
    {
        public static double Min(this double e)
        {
            return Math.Min(e, 1);
        }

        public static void DrawLine(this Graphics g, Pen p, double x, double y, double cx, double cy)
        {
            g.DrawLine(p, (float)x, (float)y, (float)cx, (float)cy);
        }

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

            if (lock_cache.ContainsKey(b))
            {
                var d = lock_cache[b];

                Marshal.WriteInt32(d.Scan0, x * 4 + y * d.Stride, a.ToInt32());
            }
            else
                b.SetPixel(x, y, Color.FromArgb(a.ToInt32()));
        }

        static Dictionary<Bitmap, BitmapData> lock_cache = new Dictionary<Bitmap, BitmapData>();

        public static BitmapData @lock(this Bitmap b)
        {
            
            return lock_cache[b] = b.LockBits(
                new Rectangle(0, 0, b.Width, b.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb
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
