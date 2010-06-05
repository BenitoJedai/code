using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InteractiveAffineCube.AffineEngine;

namespace InteractiveAffineCube.AffineEngine
{
	public class AffineMesh
	{
		public List<AffinePoint> Points = new List<AffinePoint>();
		public List<AffineVertex> Vertecies = new List<AffineVertex>();

		public void Add(AffinePoint p)
		{
			Points.Add(p);
		}

		public void Add(AffineVertex v)
		{
			Vertecies.Add(v);

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

			var v = new List<AffineVertex>();

			foreach (var k in this.Vertecies)
			{
				var kn = new AffineVertex
				{
					Element = k.Element,
					ElementWidth = k.ElementWidth,
					ElementHeight = k.ElementHeight,
					A = k.A.Rotate(Rotation),
					B = k.B.Rotate(Rotation),
					C = k.C.Rotate(Rotation),
					//Tag = k.Tag
				};

				v.Add(kn);
			}

			n.Vertecies = v.OrderBy(k => k.Center.Z).ToList();
			//n.Vertecies = v;

			return n;
		}

		public AffineMesh ToZoom(AffineZoom Zoom)
		{
			var n = new AffineMesh();

			foreach (var p in this.Points)
			{
				n.Add(p.Zoom(Zoom));
			}

			var v = new List<AffineVertex>();

			foreach (var k in this.Vertecies)
			{
				var kn = new AffineVertex
				{
					Element = k.Element,
					ElementWidth = k.ElementWidth,
					ElementHeight = k.ElementHeight,
					A = k.A.Zoom(Zoom),
					B = k.B.Zoom(Zoom),
					C = k.C.Zoom(Zoom),
					//Tag = k.Tag

				};

				v.Add(kn);
			}

			n.Vertecies = v;


			return n;
		}
	}
}
