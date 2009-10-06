using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InteractiveMatrixTransformF.AffineEngine;

namespace InteractiveMatrixTransformE.AffineEngine
{
	public class AffineMesh
	{
		public readonly List<AffinePoint> Points = new List<AffinePoint>();
		public readonly List<AffineVertex> Vertecies = new List<AffineVertex>();
		public readonly List<AffineMesh> Meshes = new List<AffineMesh>();

	


		public AffineMesh ToRotation(AffineRotation Rotation)
		{
			//var yawn = Math.Cos(Yawn);
			//var yawn2 = Math.Sin(Yawn);

			var n = new AffineMesh();

			foreach (var p in this.Points)
			{
				n.Points.Add(p.Rotate(Rotation));
			}

		
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

				n.Vertecies.Add(kn);
			}

			

			foreach (var p in this.Meshes)
			{
				n.Meshes.Add(p.ToRotation(Rotation));
			}

			return n;
		}


		public AffineMesh ToTranslation(AffinePoint Translation)
		{
			var n = new AffineMesh();

			foreach (var p in this.Points)
			{
				n.Points.Add(p.Translation(Translation));
			}


			foreach (var k in this.Vertecies)
			{
				var kn = new AffineVertex
				{
					Element = k.Element,
					ElementWidth = k.ElementWidth,
					ElementHeight = k.ElementHeight,
					A = k.A.Translation(Translation),
					B = k.B.Translation(Translation),
					C = k.C.Translation(Translation),
					//Tag = k.Tag

				};

				n.Vertecies.Add(kn);
			}


			foreach (var p in this.Meshes)
			{
				n.Meshes.Add(p.ToTranslation(Translation));
			}


			return n;
		}


		public AffineMesh ToZoom(AffineZoom Zoom)
		{
			var n = new AffineMesh();

			foreach (var p in this.Points)
			{
				n.Points.Add(p.Zoom(Zoom));
			}


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

				n.Vertecies.Add(kn);
			}


			foreach (var p in this.Meshes)
			{
				n.Meshes.Add(p.ToZoom(Zoom));
			}


			return n;
		}

		// jsc + flash: internal keyword generates a fault
		public IEnumerable<AffineVertex> GetCombinedVertices()
		{
			return ((IEnumerable<AffineVertex>)this.Vertecies).Concat(
				this.Meshes.SelectMany(k => (IEnumerable<AffineVertex>)k.Vertecies));
		}

		const int SortBuffer = 10000;

		public AffineVertex[] GetSortedCombinedVertices()
		{
			// da buffer
			var a = new AffineVertex[SortBuffer];

			GetSortedCombinedVertices(this, a);

			return a;
		}

		static void GetSortedCombinedVertices(AffineMesh m, AffineVertex[] a)
		{
			foreach (var v in m.Vertecies)
			{
				var i = Convert.ToInt32(v.B.Z + v.C.Z) + SortBuffer / 2;

				while (a[i] != null)
					i++;

				a[i] = v;
			}

			foreach (var k in m.Meshes)
			{
				GetSortedCombinedVertices(k, a);
			}
		}

		public AffineMesh Merge()
		{
			var n = new AffineMesh();

			MergeTo(n);




			return n;
		}

		public void MergeTo(AffineMesh n)
		{
			foreach (var p in this.Points)
			{
				n.Points.Add(p);
			}


			foreach (var k in this.Vertecies)
			{
				var kn = new AffineVertex
				{
					Element = k.Element,
					ElementWidth = k.ElementWidth,
					ElementHeight = k.ElementHeight,
					A = k.A,
					B = k.B,
					C = k.C,
					//Tag = k.Tag

				};

				n.Vertecies.Add(kn);
			}


			foreach (var p in this.Meshes)
			{
				p.MergeTo(n);
			}
		}
	}
}
