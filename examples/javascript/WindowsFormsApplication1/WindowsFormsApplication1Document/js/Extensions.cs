using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Windows.Forms;

namespace WindowsFormsApplication1Document.js
{
	[Script]
	static class Extensions
	{
		public static int Random(this int i)
		{
			return new Random().Next(i);
		}

		public static void ShowAt(this Form f, int x, int y)
		{
			f.Location = new System.Drawing.Point(x, y);
			f.Show();
		}
	}
}
