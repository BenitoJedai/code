using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media
{
	[Script(Implements = typeof(global::System.Windows.Media.TranslateTransform))]
	internal class __TranslateTransform : __Transform
	{
		public double X { get; set; }
		public double Y { get; set; }
		
		public __TranslateTransform(double offsetX, double offsetY)
		{
			this.X = offsetX;
			this.Y = offsetY;
		}
	}
}
