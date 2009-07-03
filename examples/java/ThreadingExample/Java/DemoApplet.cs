using ScriptCoreLib;

using java.lang;
using java.applet;
using java.awt;
using java.awt.@event;
using javax.common.runtime;
using ThreadingExample.Java.Businesslayer;

namespace ThreadingExample.Java
{
	[Script]
	public partial class ThreadingExample : Applet
	{
		public override void init()
		{
			this.InitializeComponents();

			base.resize(Settings.DefaultWidth, Settings.DefaultHeight);
		}

		static Color GetBlue(double b)
		{
			int u = (int)(0xff * b);

			return new Color(u);
		}

		public override void paint(global::java.awt.Graphics g)
		{
			// old school gradient :)

			var h = this.getHeight();
			var w = this.getWidth();

			for (int i = 0; i < h; i++)
			{

				g.setColor(GetBlue(1 - (double)i / (double)h));
				g.drawLine(0, i, w, i);
			}

			g.setColor(new Color(0xffffff));
			g.drawString("This example will show how to write multithreaded applet.", 16, 64);
		}

		readonly LongComputation MyComputation = new LongComputation();

		public void Button1_Clicked()
		{
			this.Button1.Enabled = false;
			this.Button2.Enabled = true;

			this.Button1.setLabel("start @ " + MyComputation.Current.Value);
			this.Button2.setLabel("Stop computing");

			MyComputation.Start();
		}

		public void Button2_Clicked()
		{
			this.Button1.Enabled = true;
			this.Button2.Enabled = false;

			MyComputation.Stop();

			this.Button1.setLabel("Start computing");
			this.Button2.setLabel("stop @ " + MyComputation.Current.Value);
		}

		
		public void Button3_MouseEnter()
		{
			this.Button1.Enabled = false;
			this.Button2.Enabled = false;

			this.MyComputation.Start();

			this.Button1.setLabel("start @ " + MyComputation.Current.Value);
			this.Button2.setLabel("Stop computing");

		}


		public void Button3_MouseExit()
		{
			this.Button1.Enabled = true;
			this.Button2.Enabled = false;

			this.MyComputation.Stop();


			this.Button1.setLabel("Start computing");
			this.Button2.setLabel("stop @ " + MyComputation.Current.Value);
		}
	}
}
