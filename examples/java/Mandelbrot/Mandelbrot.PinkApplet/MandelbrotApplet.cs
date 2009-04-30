using ScriptCoreLib;

using java.lang;
using java.applet;
using java.awt;
using java.awt.@event;
using javax.common.runtime;
using java.awt.image;
using ScriptCoreLibJava.Threading;
using Mandelbrot.Core;

namespace Mandelbrot.PinkApplet
{
	[Script]
	public partial class MandelbrotApplet : Applet
	{
		public override void init()
		{
			base.resize(MandelbrotProvider.DefaultWidth, MandelbrotProvider.DefaultHeight);

		

			this.buffer = new MemoryImageSource(MandelbrotProvider.DefaultWidth, MandelbrotProvider.DefaultHeight, MandelbrotProvider.DrawMandelbrotSet(0), 0, MandelbrotProvider.DefaultWidth);

			buffer.setAnimated(true);
			buffer.setFullBufferUpdates(true);

			this.img = this.createImage(buffer);

			CurrentAnimator = 0.AtDelay(new Animator { Target = this });
		}


		public void stop()
		{
			CurrentAnimator.Thread.Abort();
		}

		public override void paint(global::java.awt.Graphics g)
		{
			if (g == null)
				return;

			g.drawImage(this.img, 0, 0, null);
		}

		Image img;
		public MemoryImageSource buffer;
		public ThreadedActionInvoker CurrentAnimator;

		[Script]
		public class Animator : ThreadedAction
		{
			public MandelbrotApplet Target;

			public override void Invoke()
			{
				var shift = 0;

				while (true)
				{
					var a = MandelbrotProvider.DrawMandelbrotSet(shift);
					for (int i = 0; i < a.Length; i++)
					{
						a[i] = (int)(0xFF000000u | (uint)a[i]);
					}

					Target.buffer.newPixels();
					Target.paint(Target.getGraphics());

					shift++;
					Thread.yield();
				}
			}
		}

		public static void main(string[] args)
		{
			//Console.WriteLine("Hello World");
		}
	}
}
