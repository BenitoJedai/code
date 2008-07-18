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
    public class PointDouble
    {
        public double X;
        public double Y;
    }

    /// <summary>
    /// This class defines the extension methods for this project
    /// </summary>
    [Script]
    internal static class Extensions
    {

        public static double DegreesToRadians(this int Degrees)
        {
            return (Math.PI * 2) * Degrees / 360;
        }

        public static int RadiansToDegrees(this double Arc)
        {
            return (360 * Arc / (Math.PI * 2)).ToInt32();
        }

        public static int ToInt32(this double e)
        {
            return Convert.ToInt32(e);
        }

        public static double GetDistance(this PointDouble p)
        {
            return Math.Sqrt(p.X * p.X + p.Y * p.Y);
        }

        public static double GetRotation(this PointDouble p)
        {
            var x = p.X;
            var y = p.Y;

            if (x == 0)
                if (y < 0)
                    return System.Math.PI / 2;
                else
                    return (System.Math.PI / 2) * 3;

            var a = System.Math.Atan(y / x);

            if (x < 0)
                a += System.Math.PI;
            else if (y < 0)
                a += System.Math.PI * 2;


            return a;
        }

        public static double Min(this double e, double x)
        {
            return Math.Min(e, x);
        }

        public static double Min(this double e)
        {
            return Math.Min(e, 1);
        }

        public static void DrawImage(this Graphics g, Image i, double x, double y, double cx, double cy)
        {
            g.DrawImage(
               i, (float)x, (float)y, (float)cx, (float)cy);
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
