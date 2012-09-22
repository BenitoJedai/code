using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InteractiveMatrixTransformE.AffineEngine
{
	public class AffineMesh
	{
		public readonly List<AffinePoint> Points = new List<AffinePoint>();

		public void Add(AffinePoint p)
		{
			Points.Add(p);
		}

		
		
		public AffineMesh ToRotation(AffineRotation Rotation)
		{
			//var yawn = Math.Cos(Yawn);
			//var yawn2 = Math.Sin(Yawn);

			var n = new AffineMesh();

			foreach (var p in this.Points)
			{
				n.Add(p.Rotate(Rotation));
			}

			return n;
		}
	}
}
