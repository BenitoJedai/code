using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;

namespace InteractiveAffineCube.AffineEngine
{
	public class AffineVertex
	{
		public AffinePoint A;
		public AffinePoint B;
		public AffinePoint C;

		public FrameworkElement Element;

		public double ElementWidth;
		public double ElementHeight;

		//public object Tag;

		public AffinePoint Center
		{
			get
			{
				return (B + C) * 0.5;

				//    (A.X + B.X + C.X) / 3,
				//    (A.Y + B.Y + C.Y) / 3,
				//    (A.Z + B.Z + C.Z) / 3
				//);

				//return new AffinePoint(
				//    (A.X + B.X + C.X) / 3,
				//    (A.Y + B.Y + C.Y) / 3,
				//    (A.Z + B.Z + C.Z) / 3
				//);
			}
		}
	}
}
