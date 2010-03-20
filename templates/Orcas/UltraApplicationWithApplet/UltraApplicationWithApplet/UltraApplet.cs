using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.applet;
using java.awt;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Remoting.Extensions;
using System.Diagnostics;

namespace UltraApplicationWithApplet
{


	public sealed partial class UltraApplet : Applet
	{
		public const int DefaultWidth = 400;
		public const int DefaultHeight = 200;

		public override void init()
		{
			Console.WriteLine(".init");


			base.resize(DefaultWidth, DefaultHeight);
			// creating the java applet

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
		}
	}

	public static class UltraAppletIntegration
	{
		public static void CreateApplet(this Application a)
		{
			var x = new IHTMLButton("create UltraApplet");

			x.AttachToDocument();

			x.onclick +=
				delegate
				{
					var o = new UltraApplet();

					o.AttachAppletToDocument();

					o.BuildPage(o.ToHTMLElement().ToProxy());
				};
		}
	}

}
