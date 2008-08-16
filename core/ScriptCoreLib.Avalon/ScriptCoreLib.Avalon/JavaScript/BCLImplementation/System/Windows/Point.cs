using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows
{
	[Script(Implements = typeof(global::System.Windows.Point))]
	internal class __Point
	{
		public double X { get; set; }
		public double Y { get; set; }
	}
}
