using java.applet;
using java.awt;
using java.awt.image;
using java.lang;
using Mandelbrot;
using System;

namespace MandelbrotApplet
{
    public sealed class ApplicationApplet : Applet
    {
        public override void init()
        {
            base.resize(MandelbrotProvider.DefaultWidth, MandelbrotProvider.DefaultHeight);

            //Console
            System.Console.WriteLine("init");

            this.buffer = new MemoryImageSource(MandelbrotProvider.DefaultWidth, MandelbrotProvider.DefaultHeight, MandelbrotProvider.DrawMandelbrotSet(0), 0, MandelbrotProvider.DefaultWidth);

            buffer.setAnimated(true);
            buffer.setFullBufferUpdates(true);

            var shift = 0;

            Action onframe = delegate
            {

                var a = MandelbrotProvider.DrawMandelbrotSet(shift);
                for (int i = 0; i < a.Length; i++)
                {
                    a[i] = (int)(0xFF000000u | (uint)a[i]);
                }

                this.buffer.newPixels();
                this.paint(this.getGraphics());

                shift++;
            };

            onframe();

            this.img = this.createImage(buffer);


            var t = new System.Threading.Thread(
                delegate()
                {
                    System.Console.WriteLine("Thread start");

                    while (true)
                    {
                        onframe();

                        Thread.yield();
                    }
                }
            )
            {
                IsBackground = true
            };

            t.Start();

        }

        System.Threading.Thread t;

        public void stop()
        {
            t.Abort();
        }

        public override void paint(global::java.awt.Graphics g)
        {
            if (g == null)
                return;

            g.drawImage(this.img, 0, 0, null);
        }

        Image img;
        public MemoryImageSource buffer;



    }
}
