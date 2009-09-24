using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media
{
	[Script(Implements = typeof(global::System.Windows.Media.ScaleTransform))]
	internal class __ScaleTransform : __Transform
	{
		public double ScaleX { get; set; }
		public double ScaleY { get; set; }
	}
}
