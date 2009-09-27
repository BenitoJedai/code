using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Drawing
{
	[Script(Implements = typeof(global::System.Drawing.Size))]
	internal class __Size
	{
		public __Size(int width, int height)
		{
			this.Width = width;
			this.Height = height;
		}

		public int Width { get; set; }
		public int Height { get; set; }
	}
}
