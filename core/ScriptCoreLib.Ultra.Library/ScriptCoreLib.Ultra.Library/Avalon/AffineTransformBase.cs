using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.ComponentModel;

namespace ScriptCoreLib.Avalon
{
	[Description("RightTop, LeftBottom, LeftTop")]
	public class AffineTransformBase
	{
		// http://en.wikipedia.org/wiki/Affine_transformation

		public double Left;
		public double Top;

		public double Width;
		public double Height;

		public double X1;
		public double Y1;

		public double X2;
		public double Y2;

		public double X3;
		public double Y3;

		public enum Indecies
		{
			M11,
			M12,
			M21,
			M22,
			OX,
			OY
		}

		public static implicit operator double[](AffineTransformBase e)
		{


			var R = new { x = e.X1 - e.X3, y = e.Y1 - e.Y3 };
			var G = new { x = e.X2 - e.X3, y = e.Y2 - e.Y3 };
			var B = new { x = e.X3 - e.Left, y = e.Y3 - e.Top };

			var m = new[]
			{
				/*M11 =*/ R.x / e.Width,
				/*M12 =*/ R.y / e.Height,

				/*M21 =*/ G.x / e.Width,
				/*M22 =*/ G.y / e.Height,


				/*OX =*/ B.x,
				/*OY =*/ B.y
			};

			return m;
		}

		
	}
}
