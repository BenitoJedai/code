using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using System.IO;

namespace ReflectionExample2
{
	public static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static void Main()
		{

			var t = typeof(Color);

			var cs1 = new StringBuilder();
			var cs2 = new StringBuilder();

			// this could work in java too if we were to implement it :)
			foreach (var p in t.GetProperties())
			{
				if (p.PropertyType.Equals(t))
				{
					Console.WriteLine(p.Name);

					var c = (Color)p.GetValue(null, null);

					cs1.AppendLine(p.Name + " = FromArgb(" + c.R + ", " + c.G + ", " + c.B + ");");
					cs2.AppendLine("static public Color " + p.Name + " { get; set; }");
				}
			}
			File.WriteAllText("Color.cs", cs1.ToString() + cs2.ToString());
		}
	}
}
