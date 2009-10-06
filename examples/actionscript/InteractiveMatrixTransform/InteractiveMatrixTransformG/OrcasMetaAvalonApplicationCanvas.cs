using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;
using InteractiveMatrixTransformE.AffineEngine;
using InteractiveMatrixTransformF.AffineEngine;
using System.Diagnostics;

namespace InteractiveMatrixTransformG
{
	public class OrcasMetaAvalonApplicationCanvas : Canvas
	{
		// http://code.google.com/p/kml-library/source/browse/#svn/trunk/KMLib/Abstract

		public const int DefaultWidth = 800;
		public const int DefaultHeight = 500;

		Canvas AffineContent;
		Canvas InfoContent;

		public OrcasMetaAvalonApplicationCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			this.ClipToBounds = true;

			new[] {
				Colors.Black,
				Colors.Blue,
				Colors.Black
			}.ToGradient(DefaultHeight / 2).Select(
				(c, i) =>
					new Rectangle
					{
						Fill = new SolidColorBrush(c),
						Width = DefaultWidth,
						Height = 3,
					}.MoveTo(0, i * 2).AttachTo(this)
			).ToArray();



			//var help = new Image
			//{
			//    Source = (KnownAssets.Path.Assets + "/help.png").ToSource()
			//}.AttachTo(this);

			//help.Opacity = 0;

			var img = new Image
			{
				Source = ("assets/InteractiveMatrixTransformG/jsc.png").ToSource()
			}.MoveTo(DefaultWidth - 128, DefaultHeight - 128).AttachTo(this);





			AffineContent = new Canvas
			{

			}.AttachTo(this);




			InfoContent = new Canvas
			{

			}.AttachTo(this);



			var t = new TextBox
			{
				FontSize = 10,
				Text = "powered by jsc",
				BorderThickness = new Thickness(0),
				Foreground = 0xffffffff.ToSolidColorBrush(),
				Background = Brushes.Transparent,
				IsReadOnly = true,
				Width = DefaultWidth
			}.MoveTo(8, 8).AttachTo(InfoContent);

			var a = new AffineMesh();


			var _17 = "assets/InteractiveMatrixTransformG/17.png".ToSource();
			var _17g = "assets/InteractiveMatrixTransformG/17g.png".ToSource();
			var _18 = "assets/InteractiveMatrixTransformG/18.png".ToSource();
			var _18g = "assets/InteractiveMatrixTransformG/18g.png".ToSource();

			for (int cubex = -2; cubex < 2; cubex++)
			{


				AddCube(a, _18, _18g, new AffinePoint(-1 + cubex * 4, -1, 0));
				AddCube(a, _18, _18g, new AffinePoint(-1 + cubex * 4, 1, 0));

				AddCube(a, _18, _18g, new AffinePoint(1 + cubex * 4, -1, 0));
				AddCube(a, _18, _18g, new AffinePoint(1 + cubex * 4, 1, 0));

				AddCube(a, _17, _17g, new AffinePoint(-1 + cubex * 4, -1, 1));
				AddCube(a, _17, _17g, new AffinePoint(-1 + cubex * 4, 1, 1));

				AddCube(a, _17, _17g, new AffinePoint(1 + cubex * 4, -1, 1));
				AddCube(a, _17, _17g, new AffinePoint(1 + cubex * 4, 1, 1));


			}


			var top = AddCube(a, _18, _18g, new AffinePoint(0, 0, 2));
			var topdef = top;

			var sandcount = 8;

			for (int ix = -sandcount; ix <= sandcount; ix++)
				for (int iy = -sandcount; iy <= sandcount; iy++)
				{
					AddCubeFace(a,
						"assets/InteractiveMatrixTransformG/sandv.png".ToSource(),
						"assets/InteractiveMatrixTransformG/sandv.png".ToSource(),
						new AffinePoint(-100 + ix * 200, -100, -100 + iy * 200),
						new AffinePoint(100 + ix * 200, -100, -100 + iy * 200),
						new AffinePoint(-100 + ix * 200, -100, 100 + iy * 200),
						new AffinePoint(100 + ix * 200, -100, 100 + iy * 200)
					);
				}





			//z			a = a.ToZoom(0.5);


			//a = a.ToZoom(0.8);
			//a = a.ToZoom(1.2);

			var _a = a;
			var Rotation = new AffineRotation
			{
				XY = (180 + 22).DegreesToRadians(),
				YZ = -22.DegreesToRadians(),
				XZ = 45.DegreesToRadians()
			};

			var MouseOffset0 = 0.0;
			var MouseOffset1 = 0.0;
			var MouseOffset2 = 0.0;

			var MouseMode = 0;

			this.MouseLeftButtonUp +=
				delegate
				{
					MouseMode++;
				};

			this.MouseMove +=
				(sender, args) =>
				{

					var pp = args.GetPosition(this);

					if ((MouseMode % 4) == 1)
					{
						MouseOffset1 = pp.X;
						Rotation = new AffineRotation
						{
							XZ = Rotation.XZ,
							YZ = Rotation.YZ,

							XY = 0.01 * (pp.X - MouseOffset0) * 2,

						};
					}

					if ((MouseMode % 4) == 2)
					{
						MouseOffset2 = pp.X;
						Rotation = new AffineRotation
						{
							XY = Rotation.XY,
							YZ = Rotation.YZ,


							XZ = 0.01 * (pp.X - MouseOffset1) * 2,

						};
					}

					if ((MouseMode % 4) == 3)
					{
						MouseOffset0 = pp.X;
						Rotation = new AffineRotation
						{
							XY = Rotation.XY,
							XZ = Rotation.XZ,

							YZ = 0.01 * (pp.X - MouseOffset2) * 2,

						};
					}

				};


			Action<int> nextframe = null;


			nextframe =
				c =>
				{
					var sw = new Stopwatch();

					sw.Start();

					a.Meshes.Remove(top);

					top = topdef.ToTranslation(
						new AffinePoint(0, -200 * 3, 0)
					).ToRotation(
						new AffineRotation { XZ = 0.05 * c }
					).ToTranslation(
						new AffinePoint(0, 200 * 3, 0)
					);

					a.Meshes.Add(top);



					// rotate floor
					_a = a.ToZoom(0.5).ToRotation(Rotation);


					Show(_a);

					sw.Stop();

					t.Text = new
					{
						ShowCounter,
						XY = Rotation.XY.RadiansToDegrees() % 360,
						YZ = Rotation.YZ.RadiansToDegrees() % 360,
						XZ = Rotation.XZ.RadiansToDegrees() % 360,
						Renderer = sw.ElapsedMilliseconds + "ms"
					}.ToString();

					1.AtDelay(() => nextframe(c + 1));
				}
			;

			1.AtDelay(() => nextframe(0));
		}


		int ShowCounter;

		private void Show(AffineMesh _a)
		{
			ShowCounter++;
			double Zoom = 0.2;

			// js: 130
			// as: 70
			// c#: 30

			// simple z sort
			// js: 63
			// as: 28
			// c#: 27

			// js: 61
			// as: 26

			// n.Vertecies = v.OrderBy(k => k.Center.Z).ToList();
			foreach (var k in _a.GetCombinedVertices().OrderBy(k => k.Center.Z))
			//foreach (var k in _a.GetSortedCombinedVertices())
			{
				if (k != null)
				{
					if (ShowCounter % 100 == 1)
					{
						k.Element.Orphanize();
						k.Element.AttachTo(AffineContent);
					}

					k.Element.RenderTransform = new AffineTransform
					{
						Left = 0,
						Top = 0,
						Width = k.ElementWidth,
						Height = k.ElementHeight,

						X1 = k.B.X * Zoom + DefaultWidth / 2,
						Y1 = k.B.Y * Zoom + DefaultHeight / 2,

						X2 = k.C.X * Zoom + DefaultWidth / 2,
						Y2 = k.C.Y * Zoom + DefaultHeight / 2,

						X3 = k.A.X * Zoom + DefaultWidth / 2,
						Y3 = k.A.Y * Zoom + DefaultHeight / 2,


					};

				}
				//((Action<AffineVertex>)k.Tag)(k);
			}


		}

		private AffineMesh AddCube(AffineMesh context, ImageSource Source, ImageSource Source2, AffinePoint TopLocation)
		{
			var a = new AffineMesh();


			// front
			AddCubeFace(a, Source, Source2,
			   new AffinePoint(-100, -100, 100),
			   new AffinePoint(100, -100, 100),
			   new AffinePoint(-100, 100, 100),
			   new AffinePoint(100, 100, 100)
		   );

			// right
			AddCubeFace(a, Source, Source2,
			   new AffinePoint(100, -100, 100),
			   new AffinePoint(100, -100, -100),
			   new AffinePoint(100, 100, 100),
			   new AffinePoint(100, 100, -100)
		   );

			// left
			AddCubeFace(a, Source, Source2,
			   new AffinePoint(-100, -100, 100),
			   new AffinePoint(-100, -100, -100),
			   new AffinePoint(-100, 100, 100),
			   new AffinePoint(-100, 100, -100)
		   );

			// back
			AddCubeFace(a, Source, Source2,
			   new AffinePoint(-100, -100, -100),
			   new AffinePoint(100, -100, -100),
			   new AffinePoint(-100, 100, -100),
			   new AffinePoint(100, 100, -100)
		   );

			// top
			AddCubeFace(a, Source, Source2,
			   new AffinePoint(-100, 100, -100),
			   new AffinePoint(100, 100, -100),
			   new AffinePoint(-100, 100, 100),
			   new AffinePoint(100, 100, 100)
		   );

			var _a = a.ToTranslation(new AffinePoint(TopLocation.X * 200, TopLocation.Z * 200, TopLocation.Y * 200));

			context.Meshes.Add(
				_a
			);

			return _a;
		}

		private void AddCubeFace(AffineMesh a, ImageSource Source, ImageSource Source2, AffinePoint A, AffinePoint B, AffinePoint C, AffinePoint D)
		{
			var v1 =
			   new AffineVertex
			   {
				   A = A,
				   B = B,
				   C = C,

				   Element = new Image
				   {
					   Width = 100,
					   Height = 100,
					   Source = Source
				   }.AttachTo(AffineContent),
				   ElementWidth = 100,
				   ElementHeight = 100
			   };



			a.Vertecies.Add(v1);

			var v2 =
				new AffineVertex
				{
					A = D,
					B = C,
					C = B,

					Element = new Image
					{
						Width = 100,
						Height = 100,
						Source = Source2
					}.AttachTo(AffineContent),
					ElementWidth = 100,
					ElementHeight = 100
				};



			a.Vertecies.Add(v2);

		}



		private void AddCubeFace(AffineMesh a, string t, AffinePoint A, AffinePoint B, AffinePoint C, AffinePoint D)
		{
			var v1 =
			   new AffineVertex
			   {
				   A = A,
				   B = B,
				   C = C,

				   Element = new Image
				   {
					   Width = 100,
					   Height = 100,
					   Source = "assets/InteractiveMatrixTransformG/17.png".ToSource()
				   }.AttachTo(AffineContent),
				   ElementWidth = 100,
				   ElementHeight = 100
			   };

			a.Vertecies.Add(v1);

			var v2 =
				new AffineVertex
				{
					A = D,
					B = C,
					C = B,

					Element = new Image
					{
						Width = 100,
						Height = 100,
						Source = "assets/InteractiveMatrixTransformG/17g.png".ToSource()
					}.AttachTo(AffineContent),
					ElementWidth = 100,
					ElementHeight = 100
				};



			a.Vertecies.Add(v2);

		}


	}
}
