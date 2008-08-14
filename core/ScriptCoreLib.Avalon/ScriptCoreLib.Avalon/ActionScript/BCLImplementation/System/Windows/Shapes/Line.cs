using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media;
using System.Windows.Media;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Shapes
{

	[Script(Implements = typeof(global::System.Windows.Shapes.Line))]
	internal class __Line : __Shape
	{
		double _X1;
		public double X1 { get { return _X1; } set { _X1 = value; InternalUpdate(); } }
		double _Y1;
		public double Y1 { get { return _Y1; } set { _Y1 = value; InternalUpdate(); } }
		double _X2;
		public double X2 { get { return _X2; } set { _X2 = value; InternalUpdate(); } }
		double _Y2;
		public double Y2 { get { return _Y2; } set { _Y2 = value; InternalUpdate(); } }

		void InternalUpdate()
		{
			var g = this.InternalSprite.graphics;

			g.clear();

			var stroke = this.Stroke as SolidColorBrush;

			if (stroke != null)
			{
				__Color color = stroke.Color;

				g.lineStyle(1, color, 1);
				g.moveTo(X1, Y1);
				g.lineTo(X2, Y2);
			}

		}

	}
}
