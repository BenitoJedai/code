using ScriptCoreLib;

using java.lang;
using java.applet;
using java.awt;
using java.awt.@event;
using javax.common.runtime;
using java.awt.image;

namespace SinePlasma.source.java
{
	[Script]
	public class SinePlasmaTimer : Runnable
	{
		public SinePlasma that;


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


	[Script]
	public partial class SinePlasma : Applet
	{
		// http://rsb.info.nih.gov/plasma2/
		// http://forums.sun.com/thread.jspa?threadID=367212&messageID=1557275&forumID=5

		public MemoryImageSource buffer;

		public int shift;


		public override void init()
		{
			Plasma.generatePlasma(Settings.DefaultWidth, Settings.DefaultHeight);
			var pix = Plasma.shiftPlasma(0);

			this.buffer = new MemoryImageSource(Settings.DefaultWidth, Settings.DefaultHeight, pix, 0, Settings.DefaultWidth);

			buffer.setAnimated(true);
			buffer.setFullBufferUpdates(true);


			new Thread(new SinePlasmaTimer { that = this }).start();


			base.resize(Settings.DefaultWidth, Settings.DefaultHeight);


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

	}
}
