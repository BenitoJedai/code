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
}
