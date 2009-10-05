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
namespace InteractiveMatrixTransformF
{
	public delegate T6 Func<T1, T2, T3, T4, T5, T6>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);

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
				Source = ("assets/InteractiveMatrixTransformF/jsc.png").ToSource()
			}.MoveTo(DefaultWidth - 128, DefaultHeight - 128).AttachTo(this);

			var t = new TextBox
			{
				FontSize = 10,
				Text = "powered by jsc",
				BorderThickness = new Thickness(0),
				Foreground = 0xffffffff.ToSolidColorBrush(),
				Background = Brushes.Transparent,
				IsReadOnly = true,
				Width = DefaultWidth
			}.MoveTo(8, 8).AttachTo(this);




			AffineContent = new Canvas
			{

			}.AttachTo(this);




			InfoContent = new Canvas
			{

			}.AttachTo(this);



			var a = new AffineMesh();





			// front
			 AddCubeFace(a, "front",
				new AffinePoint(-100, -100, 100),
				new AffinePoint(100, -100, 100),
				new AffinePoint(-100, 100, 100),
				new AffinePoint(100, 100, 100)
			);

			// right
			 AddCubeFace(a, "right",
				new AffinePoint(100, -100, 100),
				new AffinePoint(100, -100, -100),
				new AffinePoint(100, 100, 100),
				new AffinePoint(100, 100, -100)
			);

			// left
			 AddCubeFace(a, "left",
				new AffinePoint(-100, -100, 100),
				new AffinePoint(-100, -100, -100),
				new AffinePoint(-100, 100, 100),
				new AffinePoint(-100, 100, -100)
			);

			// back
			 AddCubeFace(a, "back",
				new AffinePoint(-100, -100, -100),
				new AffinePoint(100, -100, -100),
				new AffinePoint(-100, 100, -100),
				new AffinePoint(100, 100, -100)
			);

			// top
			 AddCubeFace(a, "top",
				new AffinePoint(-100, 100, -100),
				new AffinePoint(100, 100, -100),
				new AffinePoint(-100, 100, 100),
				new AffinePoint(100, 100, 100)
			);

			// bottom
			AddCubeFace(a, "bottom",
				new AffinePoint(-100, -100, -100),
				new AffinePoint(100, -100, -100),
				new AffinePoint(-100, -100, 100),
				new AffinePoint(100, -100, 100)
			);

		   // AddCubeFace(a, "back",
		   //    new AffinePoint(-100, -100, -100),
		   //    new AffinePoint(100, -100, -100),
		   //    new AffinePoint(-100, 100, -100),
		   //    new AffinePoint(100, 100, -100)
		   //);


			//a = a.ToZoom(new AffineZoom { X = 2 });


			//a = a.ToZoom(0.8);
			//a = a.ToZoom(1.2);

			this.MouseMove +=
				(sender, args) =>
				{
					var pp = args.GetPosition(this);

					var Rotation = new AffineRotation
					{
						XY = 0.01 * pp.X * 0.5,
						YZ = 0.02 * pp.X * 0.5,
						XZ = 0.03 * pp.X * 0.5
					};

					t.Text = new
					{
						XY = Rotation.XZ.RadiansToDegrees(),
						YZ = Rotation.YZ.RadiansToDegrees(),
						XZ = Rotation.XZ.RadiansToDegrees()
					}.ToString();

					// rotate floor
					var _a = a.ToZoom((DefaultHeight / 2 + pp.Y) / (DefaultHeight)).ToRotation(Rotation);



					foreach (var k in _a.Vertecies)
					{
						k.Element.Orphanize();
						k.Element.AttachTo(AffineContent);
						k.Element.RenderTransform = new AffineTransform
						{
							Left = 0,
							Top = 0,
							Width = k.ElementWidth,
							Height = k.ElementHeight,

							X1 = k.B.X + DefaultWidth / 2,
							Y1 = k.B.Y + DefaultHeight / 2,

							X2 = k.C.X + DefaultWidth / 2,
							Y2 = k.C.Y + DefaultHeight / 2,

							X3 = k.A.X + DefaultWidth / 2,
							Y3 = k.A.Y + DefaultHeight / 2,


						};

						//((Action<AffineVertex>)k.Tag)(k);
					}
				}
			;
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
					   Source = "assets/InteractiveMatrixTransformF/17.png".ToSource()
				   }.AttachTo(AffineContent),
				   ElementWidth = 100,
				   ElementHeight = 100
			   };

			//var t1 = new TextBox { Text = t, Foreground = Brushes.Blue }.AttachTo(InfoContent);

			//v1.Tag = new Action<AffineVertex>(
			//    k =>
			//    {
			//        t1.Text = t + " " + Convert.ToInt32(k.Center.Z);
			//        t1.MoveTo(k.Center.X + DefaultWidth / 2, k.Center.Y + DefaultHeight / 2);

			//    }
			//);

			a.Add(v1);

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
						Source = "assets/InteractiveMatrixTransformF/19g.png".ToSource()
					}.AttachTo(AffineContent),
					ElementWidth = 100,
					ElementHeight = 100
				};

			//v2.Element.Opacity = 0.5;

			//var t2 = new TextBox { Text = t }.AttachTo(InfoContent);

			//v2.Tag = new Action<AffineVertex>(
			//    k =>
			//    {
			//        t2.Text = t + " " + Convert.ToInt32(k.Center.Z);
			//        t2.MoveTo(k.Center.X + DefaultWidth / 2, k.Center.Y + DefaultHeight / 2);

			//    }
			//);

			a.Add(v2);

		}


	}
}
