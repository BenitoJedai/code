using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media
{
	[Script(Implements = typeof(global::System.Windows.Media.RectangleGeometry))]
	internal class __RectangleGeometry : __Geometry
	{
		public Rect Rect { get; set; }
	}
}
