using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Shapes
{
	[Script(Implements = typeof(global::System.Windows.Shapes.Path))]
	internal class __Path : __Shape
	{


		public Geometry Data
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				var g = InternalSprite.graphics;

				g.clear();

				var AsSolidColorBrush = this.Fill as SolidColorBrush;
				var AsRectangleGeometry = value as RectangleGeometry;

				if (AsRectangleGeometry != null)
					if (AsSolidColorBrush != null)
					{
						uint fill = (__Color)AsSolidColorBrush.Color;
						var rect = AsRectangleGeometry.Rect;

						g.beginFill(fill);
						g.drawRect(
							rect.X,
							rect.Y,
							rect.Width,
							rect.Height
							);

					}
			}
		}
	}
}
