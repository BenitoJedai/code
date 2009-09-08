using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media
{
	[Script(Implements = typeof(global::System.Windows.Media.Matrix))]
	internal class __Matrix
	{
		public double M11 { get; set; }
		public double M12 { get; set; }
		public double M21 { get; set; }
		public double M22 { get; set; }
		public double OffsetX { get; set; }
		public double OffsetY { get; set; }
	}
}
