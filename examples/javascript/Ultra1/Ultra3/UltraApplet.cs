using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.applet;
using java.awt;
using ScriptCoreLibJava.Extensions;
using ScriptCoreLibJava.Windows.Forms;

namespace Ultra3
{
	[ScriptApplicationEntryPoint(Width = DefaultWidth, Height = DefaultHeight)]
	public class UltraApplet : Applet
	{
		public const int DefaultWidth = 300;
		public const int DefaultHeight = 300;

		readonly UserControl1 Content = new UserControl1();

		public override void init()
		{
			base.resize(DefaultWidth, DefaultHeight);
			this.setLayout(null);


			this.Content.AttachTo(this);

			this.Content.label2.Text = ">>";
			this.Content.button1.Click +=
				delegate
				{
					this.RaiseToJavaScript(this.Content.textBox1.Text);
				};

			this.AtToJava +=
				e =>
				{
					this.StatusText = ">> " + e;
				};
		}

		public string StatusText
		{
			get
			{
				return this.Content.label2.Text;
			}
			set
			{
				this.Content.label2.Text = value;
			}
		}

		public delegate void Action1(string e);

		public event Action1 AtToJava;

		public void RaiseToJava(string e)
		{
			if (AtToJava != null)
				AtToJava(e);
		}

		public event Action1 AtToJavaScript;

		public void RaiseToJavaScript(string e)
		{
			if (AtToJavaScript != null)
				AtToJavaScript(e);
		}

		static Color GetBlue(double b)
		{
			int u = (int)(0xff * b);

			return new Color(u, u, u);
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


			base.paint(g);
		}
		
	}


}
