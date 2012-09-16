using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PlasmaFormsControl.Library
{
    public partial class PlasmaControl : UserControl
    {
        public PlasmaControl()
        {
            this.InitializeComponent();

        }



        private void MandelbrotComponent_Load(object sender, EventArgs e)
        {

            var DefaultWidth = 128;
            var DefaultHeight = 128;

            Plasma.generatePlasma(DefaultWidth, DefaultHeight);

            var shift = 0;

            var bitmap = new Bitmap(
              DefaultWidth,
              DefaultHeight
            );

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);


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
                    var buffer = Plasma.shiftPlasma(shift);

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

                    shift++;

                    bitmap.UnlockBits(data);
                    this.Invalidate();
                };

            Refresh();

            timer1.Tick +=
                delegate
                {
                    if (Refresh == null)
                        return;

                    shift += 1;
                    Refresh();
                };

        }
    }

    public static class Plasma
    {
        static int width;
        static int height;

        static uint[] palette;
        static uint[] plasma;
        public static uint[] newPlasma;




        public static uint[] generatePlasma(int width, int height)
        {
            Plasma.width = width;
            Plasma.height = height;



            palette = new uint[256];
            plasma = new uint[width * height];
            newPlasma = new uint[width * height];

            for (var x = 0; x < 256; x++)
            {
                var b = (int)((int)(128.0 + 128 * Math.Sin(Math.PI * x / 16.0)) & 0xff);
                var g = (int)((int)(128.0 + 128 * Math.Sin(Math.PI * x / 128.0)) & 0xff);
                var r = 0;

                uint color = (uint)(r << 16 | g << 8 | b);

                //color |= 0xff000000u;

                palette[x] = color;
            }

            int index = 0;

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    uint color = (uint)((
                        128.0 + (128.0 * Math.Sin(x / 16.0)) +
                        128.0 + (128.0 * Math.Sin(y / 8.0)) +
                        128.0 + (128.0 * Math.Sin((x + y) / 16.0)) +
                        128.0 + (128.0 * Math.Sin(Math.Sqrt(x * x + y * y) / 8.0))
                    ) / 4);


                    //color |= 0xff000000u;

                    plasma[index++] = (uint)(color & 0xffffff);
                }
            }

            return plasma;
        }

        public static uint[] shiftPlasma(int shift)
        {
            var index = 0;


            for (var x = 0; x < width; x++)
            {

                for (var y = 0; y < height; y++)
                {
                    newPlasma[index] = palette[(plasma[index] + (uint)shift) % 256];
                    index++;
                }
            }

            return newPlasma;
        }
    }

}
