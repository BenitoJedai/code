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

		Brush InternalFill;

		public override void InternalSetFill(Brush s)
		{
			InternalFill = s;

			InternalUpdate();
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


			if (_Width < 1)
				if (_Height < 1)
					return;

			g.clear();

			var stroke = this.Stroke as SolidColorBrush;
			var fill = this.InternalFill as SolidColorBrush;
			var draw = false;

			if (stroke != null)
			{
				__Color color = stroke.Color;

				g.lineStyle(1, color, 1);

				draw = true;
			}

			if (fill != null)
			{
				uint color = (__Color)fill.Color;

				if (color != 0x00ffffffu)
				{

					color &= 0xffffff;

					g.beginFill(color);
					draw = true;
				}
			}

			if (draw)
			{
				try
				{
					g.drawRect(0, 0, _Width, _Height);
					g.endFill();
				}
				catch (Exception ex)
				{
					throw new Exception(
						new { _Width, _Height }.ToString()
					);
				}
			}

		}

	}
}
