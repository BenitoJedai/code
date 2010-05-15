using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.awt;
using java.applet;
using System.Windows.Forms;

namespace ScriptCoreLib.Java.Extensions
{
	public static class WindowsFormsExtensions
	{
		public static void EnableVisualStyles(this Applet a)
		{
			// a dirty redirect.
			Application.EnableVisualStyles();
		}

		public static void ReplaceContentWith(this Applet a, UserControl u)
		{
			var s = u.Size;

			a.setSize(s.Width, s.Height);

			u.AttachTo(a);
		}

		public static System.Windows.Forms.Control AttachTo(this System.Windows.Forms.Control e, Applet parent)
		{
			parent.setLayout(null);

			ScriptCoreLibJava.Windows.Forms.Extensions.AttachTo(e, (Container)parent);

			return e;
		}
	}
}
