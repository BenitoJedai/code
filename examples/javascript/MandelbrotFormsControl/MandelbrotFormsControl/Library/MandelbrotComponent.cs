using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MandelbrotFormsControl.Library
{
    public partial class MandelbrotComponent : UserControl
    {
        public MandelbrotComponent()
        {
            this.InitializeComponent();

        }



        private void MandelbrotComponent_Load(object sender, EventArgs e)
        {
            var bitmap = new Bitmap(
              MandelbrotProvider.DefaultWidth,
              MandelbrotProvider.DefaultHeight
          );
            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var shift = 0;

            this.Paint +=
                (_s, _e) =>
                {
                    _e.Graphics.DrawImage(
                        bitmap,
                        rect
                    );
                };

            Action Refresh =
                delegate
                {
                    var buffer = MandelbrotProvider.DrawMandelbrotSet(shift);

                    var data = bitmap.LockBits(
                        rect,
                        System.Drawing.Imaging.ImageLockMode.WriteOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb
                    );


                    for (int i = 0; i < buffer.Length; i++)
                    {
                        Marshal.WriteInt32(
                            data.Scan0,
                            i * 4,
                            unchecked((int)((uint)buffer[i] | 0xff000000))
                        );
                    }



                    bitmap.UnlockBits(data);
                    this.Invalidate();
                };

            Refresh();

            timer1.Tick +=
                delegate
                {
                    shift += 1;
                    Refresh();
                };

        }
    }

    public static class MandelbrotCore
    {
        // alcemy does not seem to work with type definitions yet?

        static int _max = 30;
        static int _escape = 20;

        public static double rmin = -.75;
        public static double rmax = -.46;
        public static double imin = -.65;
        public static double imax = -.50;

        public static int InitializeMandelbrotCore()
        {
            _max = 30;
            _escape = 20;


            rmin = -.75;
            rmax = -.46;
            imin = -.65;
            imax = -.50;




            return 0;
        }

        public static void DrawMandelbrotSet(int[] bitmap, double rmin, double rmax,
                            double imin, double imax, int width, int height)
        {
            // http://www.eggheadcafe.com/tutorials/aspnet/05748429-75a4-449a-9aab-82758cfb13df/animating-mandelbrot-frac.aspx


            double dr = (rmax - rmin) / (width - 1);
            double di = (imax - imin) / (height - 1);

            for (int x = 0; x < width; x++)
            {
                double cr = rmin + (x * dr);
                for (int y = 0; y < height; y++)
                {
                    double ci = imin + (y * di);
                    double zr = cr;
                    double zi = ci;
                    int count = 0;

                    while (count < _max)
                    {
                        double zr2 = zr * zr;
                        double zi2 = zi * zi;

                        if (zr2 + zi2 > _escape)
                        {
                            var index = (y * width) + x;
                            var value = ((int)Math.Pow(count + 1, 5) % 0xFFFFFF);
                            //bitmap[index] = (int)(value | 0xFF000000u);
                            bitmap[index] = value;
                            break;
                        }
                        zi = ci + (2.0 * zr * zi);
                        zr = cr + zr2 - zi2;
                        count++;
                    }

                    if (count == _max)
                    {
                        var index = (y * width) + x;
                        bitmap[index] = 0; // Black
                    }
                }
            }


        }
    }

    public static class MandelbrotProvider
    {
        // only these public functions shall be exported via alchemy

        // // alcemy does not seem to work with type definitions yet?

        //public const int DefaultWidth = 320;
        //public const int DefaultHeight = 200;

        public const int DefaultWidth = 128;
        public const int DefaultHeight = 128;
        // javascript wont support uint at this time
        // javascript needs power of 2 ?

        static int[] bitmap = new int[DefaultWidth * DefaultHeight];


        public static int InitializeMandelbrotProvider()
        {
            bitmap = new int[DefaultWidth * DefaultHeight];

            MandelbrotCore.InitializeMandelbrotCore();

            return 0;
        }

        public static int[] DrawMandelbrotSet(int shift)
        {
            var rmin = MandelbrotCore.rmin - .002 * (double)shift;
            var rmax = MandelbrotCore.rmax + .002 * (double)shift;
            var imin = MandelbrotCore.imin - .002 * (double)shift;
            var imax = MandelbrotCore.imax + .002 * (double)shift;

            MandelbrotCore.DrawMandelbrotSet(bitmap, rmin, rmax, imin, imax, DefaultWidth, DefaultHeight);

            return bitmap;
        }



    }
}
