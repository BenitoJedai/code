using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            var shift = 0;


            // javascript wont support uint at this time
            // javascript needs power of 2 ?

            var DefaultWidth = Math.Max(128, this.Width);
            var DefaultHeight = Math.Max(128, this.Height);

            int[] bytes = new int[DefaultWidth * DefaultHeight];

            var bitmap = new Bitmap(DefaultWidth, DefaultHeight);
            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            this.ClientSizeChanged +=
                 delegate
                 {
                     bitmap.Dispose();

                     DefaultWidth = Math.Max(128, this.Width);
                     DefaultHeight = Math.Max(128, this.Height);

                     bytes = new int[DefaultWidth * DefaultHeight];

                     bitmap = new Bitmap(DefaultWidth, DefaultHeight);
                     rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                 };

            #region Paint
            this.Paint +=
                (_s, _e) =>
                {
                    _e.Graphics.DrawImage(
                        bitmap,
                        rect
                    );
                };
            #endregion


            bool Busy = false;
            Action Refresh = null;

            Refresh =
                delegate
                {
                    if (Busy)
                        return;

                    Busy = true;

                    Task.Factory.StartNew(
                        new { bytes, DefaultWidth, DefaultHeight, shift },
                        state =>
                        {
                            var watch = new Stopwatch();
                            watch.Start();


                            var buffer = MandelbrotProvider.DrawMandelbrotSet(state.shift, state.bytes, state.DefaultWidth, state.DefaultHeight);


                            watch.Stop();

                            return new { watch.ElapsedMilliseconds, buffer, Thread.CurrentThread.ManagedThreadId };
                        },
                        cancellationToken: default(CancellationToken),
                        creationOptions: TaskCreationOptions.LongRunning,
                        scheduler: TaskScheduler.Default
                    ).ContinueWith(
                        task =>
                        {
                            Busy = false;

                            // resized?
                            if (bytes.Length != task.Result.buffer.Length)
                            {
                                // retry
                                Refresh();
                                return;
                            }

                            var watch2 = new Stopwatch();
                            watch2.Start();

                            var data = bitmap.LockBits(
                                rect,
                                System.Drawing.Imaging.ImageLockMode.WriteOnly,
                                System.Drawing.Imaging.PixelFormat.Format32bppArgb
                            );

                            Marshal.Copy(task.Result.buffer, 0, data.Scan0, task.Result.buffer.Length);

                            //for (int i = 0; i < buffer.Length; i++)
                            //{
                            //    Marshal.WriteInt32(
                            //        data.Scan0,
                            //        i * 4,
                            //        unchecked((int)((uint)buffer[i] | 0xff000000))
                            //    );
                            //}



                            bitmap.UnlockBits(data);

                            watch2.Stop();


                            // fullscreen 315ms!
                            this.checkBox1.Text = " [" + task.Result.ManagedThreadId + "] " + task.Result.ElapsedMilliseconds + "ms " + DefaultWidth + "x" + DefaultHeight + " [" + Thread.CurrentThread.ManagedThreadId + "] " + watch2.ElapsedMilliseconds + "ms";
                            this.Invalidate();
                        },

                        // GUI ?
                        scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                    );



                };

            Refresh();

            this.ClientSizeChanged +=
             delegate
             {
                 Refresh();
             };

            timer1.Tick +=
                delegate
                {
                    shift += 1;


                    Refresh();


                };

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.timer1.Enabled = checkBox1.Checked;

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



                            bitmap[index] = unchecked((int)((uint)value | 0xff000000));
                            //bitmap[index] = value;
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
        public static int[] DrawMandelbrotSet(int shift, int[] bitmap, int DefaultWidth, int DefaultHeight)
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
