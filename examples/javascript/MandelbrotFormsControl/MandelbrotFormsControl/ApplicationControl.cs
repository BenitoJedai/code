using Mandelbrot;
using MandelbrotFormsControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MandelbrotFormsControl
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(
                bitmap, 
                new Rectangle(0, 0, bitmap.Width, bitmap.Height)
            );

        }
        Bitmap bitmap;

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            bitmap = new Bitmap(
                MandelbrotProvider.DefaultWidth,
                MandelbrotProvider.DefaultHeight
            );

            var shift = 0;

            Action Refresh =
                delegate
                {
                    var buffer = Mandelbrot.MandelbrotProvider.DrawMandelbrotSet(shift);

                    var data = bitmap.LockBits(
                        new Rectangle(0, 0, bitmap.Width, bitmap.Height),
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

            var t = new Timer();

            t.Interval = 1;
            t.Tick +=
                delegate
                {
                    shift += 1;
                    Refresh();
                };

            t.Start();
        }

    }
}
