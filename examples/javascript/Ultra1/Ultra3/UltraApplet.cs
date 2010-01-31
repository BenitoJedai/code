using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.applet;
using java.awt;
using ScriptCoreLibJava.Extensions;
using ScriptCoreLibJava.Windows.Forms;
using java.lang.reflect;

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
					this.RaiseToJavaScript(this.Content.textBox1.Text, "");
				};

			this.AtToJava +=
				(e, y) =>
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

		public delegate void Action1(string e, string y);

		public event Action1 AtToJava;

		public void RaiseToJava(string e, string y)
		{
			if (AtToJava != null)
				AtToJava(e,y);
		}

		public event Action1 AtToJavaScript;

		public void RaiseToJavaScript(string e, string y)
		{
			if (AtToJavaScript != null)
				AtToJavaScript(e,y);
		}

		public void __add_AtToJavaScript(string callback)
		{
			this.AtToJavaScript +=
				(x, y) =>
				{
					this.StatusText = "sent!";

					Invoke(this, callback, new[] { x, y });
				};
		}

		public static object Invoke(Applet a, string method, object[] args)
		{
			var w = new StringBuilder();

			w.Append(method);
			w.Append("(");
			for (int i = 0; i < args.Length; i++)
			{
				// we are only supporting strings at the moment!

				if (i > 0)
					w.Append(", ");

				w.Append("'");

				var t = ((string)args[i]).Replace("'", @"\'");

				w.Append(t);

				w.Append("'");
			}
			w.Append(")");

			return EvaluateJavaScript(a, w.ToString());
		}

		public static object EvaluateJavaScript(Applet that, string js)
		{
			object r = null;
			Method getWindow = null;
			Method eval = null;

			try
			{
				// 2047
				// http://www.rgagnon.com/javadetails/java-0172.html

				var c = global::java.lang.Class.forName("netscape.javascript.JSObject");

				foreach (Method m in c.getMethods())
				{
					if (m.getName() == "getWindow") getWindow = m;
					if (m.getName() == "eval") eval = m;
				}

				var getWindow_Arguments = new object[] { that };

				var jsWindow = getWindow.invoke(c, getWindow_Arguments);

				var eval_Arguments = new object[] { js };

				r = eval.invoke(jsWindow, eval_Arguments);

				//base.getAppletContext().showDocument(new URL("javascript:" + js));
			}
			catch (global::csharp.ThrowableException e)
			{
				throw new global::csharp.RuntimeException("Script is not supported. " + e.Message);
			}


			return r;
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
