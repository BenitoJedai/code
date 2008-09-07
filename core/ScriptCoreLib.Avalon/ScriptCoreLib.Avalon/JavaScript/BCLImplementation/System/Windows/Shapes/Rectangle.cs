using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Shapes
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
			//var g = this.InternalSprite.graphics;

			//g.clear();

			var stroke = this.Stroke as SolidColorBrush;
			var fill = InternalFill as SolidColorBrush;

			if (stroke != null)
			{
				__Color color = stroke.Color;

				InternalSprite.style.borderColor = color;
			
			}

			if (fill != null)
			{
				__Color color = fill.Color;

				InternalSprite.style.backgroundColor = color;
			}

			InternalSprite.style.width = _Width + "px";
			InternalSprite.style.height = _Height + "px";
			//g.drawRect(0, 0, _Width, _Height);
		}

	}
}
