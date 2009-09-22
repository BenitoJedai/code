using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media
{
	[Script(Implements = typeof(global::System.Windows.Media.MatrixTransform))]
	internal class __MatrixTransform : __Transform
	{
		public double m11;
		public double m12;
		public double m21;
		public double m22;
		public double offsetX;
		public double offsetY;

		public __MatrixTransform(double m11, double m12, double m21, double m22, double offsetX, double offsetY)
		{
			this.m11 = m11;
			this.m12 = m12;
			this.m21 = m21;
			this.m22 = m22;
			this.offsetX = offsetX;
			this.offsetY = offsetY;
		}
	}
}
