﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows
{
	[Script(Implements = typeof(global::System.Windows.Rect))]
	internal class __Rect
	{
		public double Height { get; set; }
		public double Width { get; set; }
		public double X { get; set; }

		public double Y { get; set; }

		public __Rect()
		{

		}
	}
}
