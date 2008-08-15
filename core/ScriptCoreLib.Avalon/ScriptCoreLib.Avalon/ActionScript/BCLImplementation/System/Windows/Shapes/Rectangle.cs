using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media;
using System.Windows.Media;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Shapes
{

	[Script(Implements = typeof(global::System.Windows.Shapes.Rectangle))]
	internal class __Rectangle : __Shape
	{
		double _Width;
		double _Height;

		public __Rectangle()
		{
			_Width = 0;
			_Height = 0;
		}

		public override void InternalSetWidth(double value)
		{
			_Width = value;

			InternalUpdate();
		}

		public override void InternalSetHeight(double value)
		{
			_Height = value;

			InternalUpdate();
		}

		void InternalUpdate()
		{
			var g = this.InternalSprite.graphics;

			g.clear();

			var stroke = this.Stroke as SolidColorBrush;
			var fill = this.Fill as SolidColorBrush;

			if (stroke != null)
			{
				__Color color = stroke.Color;

				g.lineStyle(1, color, 1);
			
			}

			if (fill != null)
			{
				__Color color = fill.Color;

				g.beginFill(color);
			}

			g.drawRect(0, 0, _Width, _Height);
		}

	}
}
