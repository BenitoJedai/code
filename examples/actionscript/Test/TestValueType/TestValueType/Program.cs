using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ScriptCoreLib;
using System.Drawing;

[assembly: Obfuscation(Feature = "script")]

namespace TestValueType
{
	[Script(Implements = typeof(global::System.Drawing.Size))]
	internal class __Size
	{
		public __Size() : this(0, 0)
		{
			// default ctor for value type
		}

		public __Size(int width, int height)
		{
			this.Width = width;
			this.Height = height;
		}

		public int Width { get; set; }
		public int Height { get; set; }
	}

	class B
	{
		public Size Size { get; set; }
	}

	class Program : B
	{
		public B treeView1;

		void Invoke()
		{
			var w = this.Size.Width;
			var h = this.Size.Height;

			var s = new Size(w, h - 48);

			this.treeView1.Size = s;
		}

		static void Main(string[] args)
		{
			
		}
	}
}
