using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.applet;
using java.awt;

namespace WebApplication.Client.Java
{
	[Script]
	public class ApplicationApplet : Applet
	{
		public const int DefaultWidth = 320;
		public const int DefaultHeight = 200;

		public override void init()
		{
			//this.InitializeComponents();

			base.resize(DefaultWidth, DefaultHeight);
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
			g.drawString("hello world, this is the sample applet", 16, 64);
		}
	}
}
