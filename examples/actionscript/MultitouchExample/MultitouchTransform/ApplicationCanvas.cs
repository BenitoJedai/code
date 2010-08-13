// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;
using MultitouchTransform;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Input;
using MultitouchTransform.Library;
using ScriptCoreLib.Avalon;

namespace MultitouchTransform
{
    public class ApplicationCanvas : Canvas
    {
       	public const int DefaultWidth = 800;
		public const int DefaultHeight = 600;

        public ApplicationCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

            //this.ClipToBounds = true;

			Colors.Blue.ToGradient(Colors.Red, DefaultHeight / 4).Select(
				(c, i) =>
					new Rectangle
					{
						Fill = new SolidColorBrush(c),
						Width = DefaultWidth,
						Height = 4,
					}.MoveTo(0, i * 4).AttachTo(this)
			).ToArray();



			//var help = new Image
			//{
			//    Source = (KnownAssets.Path.Assets + "/help.png").ToSource()
			//}.AttachTo(this);

			//help.Opacity = 0;

			var img = new Avalon.Images.jsc().MoveTo(DefaultWidth - 128, DefaultHeight - 128).AttachTo(this);

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

			var t2 = new TextBox
			{
				FontSize = 10,
				AcceptsReturn = true,
				Text = @"
ie and opera are not supported
firefox reports after transform
chrome reports before transform
we cannot rely on mouse position currently
shall wait for improvements like touch API
",
				BorderThickness = new Thickness(0),
				Foreground = 0xffffffff.ToSolidColorBrush(),
				Background = Brushes.Transparent,
				IsReadOnly = true,
				Width = DefaultWidth,
				Height = 128,
			}.MoveTo(8, 32).AttachTo(this);

			//help.Opacity = 1;
			img.Opacity = 0.5;

			t.MouseEnter +=
				delegate
				{
					//help.Opacity = 0.5;

					img.Opacity = 1;
					t.Foreground = 0xffffff00.ToSolidColorBrush();
				};

			t.MouseLeave +=
				delegate
				{
					//help.Opacity = 1;

					img.Opacity = 0.5;
					t.Foreground = 0xffffffff.ToSolidColorBrush();
				};


			var sand = new Avalon.Images.sand()
			{
				Width = 100,
				Height = 100,
			}.AttachTo(this);

			// structs are not that good for translations...
			// we might be able to skip them it seems
			sand.RenderTransform = new MatrixTransform(1.2, 0.5, 0.2, 1, 100, 100);

			sand.Cursor = Cursors.Hand;

			// jsc/mxmlc cannot handle property name "this" :)

			sand.Opacity = 0.7;

			sand.MouseMove +=
				(sender, args) =>
				{
					var p1 = args.GetPosition(sand);
					var p2 = args.GetPosition(this);

					t.Text = new
					{
						sand = new { p1.X, p1.Y },
						that = new { p2.X, p2.Y }
					}.ToString();

				};


			


			var tri = new Avalon.Images._17()
			{
				Width = 100,
				Height = 100,
			}.AttachTo(this);


            var trig = new Avalon.Images._17g()
			{
				Width = 100,
				Height = 100,
			}.AttachTo(this);

            var tri2 = new Avalon.Images._17()
			{
				Width = 100,
				Height = 100,
			}.AttachTo(this);


            var trig2 = new Avalon.Images._17g()
			{
				Width = 100,
				Height = 100,
			}.AttachTo(this);


			// cursor position calculations are not ready
			// for transofrmed elements.
			// we will provide a floor for those events...
			var shadow = new Rectangle
			{
				Width = DefaultWidth,
				Height = DefaultHeight,

				Fill = Brushes.Black,
			}.AttachTo(this);

			var shadowa = shadow.ToAnimatedOpacity();

			shadowa.Opacity = 0;

			Func<Brush, int, int, Movable> m =
				(Color, X, Y) =>
				{
					var m0 = new Movable
					{
						Context = this,
						Color = Color
					};

					m0.MoveTo(X, Y);

					return m0;
				};

			var m1 = m(Brushes.Green, 250, 50);
			var m2 = m(Brushes.Red, 250 + 100, 50);
			var m3 = m(Brushes.Blue, 250, 50 + 100);
			var m4 = m(Brushes.Yellow, 250 + 100, 50 + 100);

			var m5 = m(Brushes.Green, 250 + 200, 50);
			var m6 = m(Brushes.Blue, 250 + 200, 50 + 100);

            Movables = new[] { m1, m2, m3, m4, m5, m6};


			Action Update =
				delegate
				{
					

					tri.RenderTransform = new AffineTransform
					{
						Left = 0,
						Top = 0,
						Width = 100,
						Height = 100,

						X1 = m2.X,
						Y1 = m2.Y,

						X2 = m3.X,
						Y2 = m3.Y,

						X3 = m1.X,
						Y3 = m1.Y,
						
					};


					trig.RenderTransform = new AffineTransform
					{
						Left = 0,
						Top = 0,
						Width = 100,
						Height = 100,

						X1 = m3.X,
						Y1 = m3.Y,

						X2 = m2.X,
						Y2 = m2.Y,

						X3 = m4.X,
						Y3 = m4.Y,

					};

					tri2.RenderTransform = new AffineTransform
					{
						Left = 0,
						Top = 0,
						Width = 100,
						Height = 100,

						X1 = m5.X,
						Y1 = m5.Y,

						X2 = m4.X,
						Y2 = m4.Y,

						X3 = m2.X,
						Y3 = m2.Y,

					};


					trig2.RenderTransform = new AffineTransform
					{
						Left = 0,
						Top = 0,
						Width = 100,
						Height = 100,

						X1 = m4.X,
						Y1 = m4.Y,

						X2 = m5.X,
						Y2 = m5.Y,

						X3 = m6.X,
						Y3 = m6.Y,

					};
				};

			foreach (var k in this.Movables)
			{
				k.Container.MouseLeftButtonDown += delegate { shadowa.Opacity = 0.2; };
				k.Container.MouseLeftButtonUp += delegate { shadowa.Opacity = 0; };
				k.Changed += Update;
			}

			Update();
		}

        public Movable[] Movables;
    }
}
