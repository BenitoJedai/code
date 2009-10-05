using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Windows.Media;

namespace InteractiveMatrixTransformG
{
	public class AffineTransform
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

		public static implicit operator MatrixTransform(AffineTransform e)
		{


			var R = new { x = e.X1 - e.X3, y = e.Y1 - e.Y3 };
			var G = new { x = e.X2 - e.X3, y = e.Y2 - e.Y3 };
			var B = new { x = e.X3 - e.Left, y = e.Y3 - e.Top };

			var m = new
			{
				M11 = R.x / e.Width,
				M12 = R.y / e.Height,

				M21 = G.x / e.Width,
				M22 = G.y / e.Height,


				OX = B.x,
				OY = B.y
			};

			return new MatrixTransform(m.M11, m.M12, m.M21, m.M22, m.OX, m.OY);
		}
	}
}
