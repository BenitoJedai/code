using ScriptCoreLib;

using java.lang;
using java.applet;
using java.awt;
using java.awt.@event;
using javax.common.runtime;
using java.awt.image;
using System;
using FlashPlasmaEngine;

namespace FlashPlasmaApplet.Java
{
	[Script]
	public partial class FlashPlasmaApplet : Applet
	{
		public const int DefaultWidth = 600;
		public const int DefaultHeight = 600;

		public override void init()
		{
			base.resize(DefaultWidth, DefaultHeight);

			Plasma.generatePlasma(DefaultWidth, DefaultHeight);
			var pix = Plasma.shiftPlasma(0);

			this.buffer = new MemoryImageSource(DefaultWidth, DefaultHeight, pix, 0, DefaultWidth);

			buffer.setAnimated(true);
			buffer.setFullBufferUpdates(true);


			new Thread(new SinePlasmaTimer { that = this }).start();


			base.resize(DefaultWidth, DefaultHeight);


			this.img = this.createImage(buffer);
		}


		public bool __stop;
		public void stop()
		{
			__stop = true;
		}


		Image img;


		public override void paint(global::java.awt.Graphics g)
		{
			if (g == null)
				return;

			g.drawImage(this.img, 0, 0, null);
		}

		public MemoryImageSource buffer;

		public int shift;


	}

	[Script]
	public class SinePlasmaTimer : Runnable
	{
		public FlashPlasmaApplet that;


		#region Runnable Members

		public void run()
		{
			while (!that.__stop)
			{
				that.shift++;
				Plasma.shiftPlasma(that.shift);

				that.buffer.newPixels();
				that.paint(that.getGraphics());

				Thread.yield();
			}
		}


		#endregion
	}



}
