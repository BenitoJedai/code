using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Runtime;
using System.Diagnostics;
using System;
using System.Linq;


namespace MatrixTransformC1Example.js
{
	[Script, ScriptApplicationEntryPoint]
	public class MatrixTransformC1Example
	{



		public MatrixTransformC1Example()
		{

			AddAnimation(100, 200);
			AddAnimation(300, 300);
			AddAnimation(600, 250);
			AddAnimation(900, 350);

		}

		private static void AddAnimation(int X, int Y)
		{
			XRectangle Image1;
			XRectangle Image2;
			XRectangle Image3;
			XRectangle Image4;


			Func<string, XRectangle> f =
				src =>
				{
					var r = new XRectangle();

					r.ContentImage = new IHTMLImage(src);
					r.ContentImage.style.SetSize(185, 100);
					r.Content.appendChild(r.ContentImage);
					r.Element.AttachToDocument().MoveTo(X, Y);


					return r;
				};

			Image1 = f("assets/MatrixTransformC1Example/wood_green.png");
			Image2 = f("assets/MatrixTransformC1Example/wood_green.png");
			Image3 = f("assets/MatrixTransformC1Example/wood_green.png");
			Image4 = f("assets/MatrixTransformC1Example/wood_green.png");


			var t = new Timer();
			var sw = new Stopwatch();
			var x = 0.0;

			t.Tick +=
				delegate
				{
					sw.Stop();

					var elapsed = sw.ElapsedMilliseconds;

					x += 0.0015 * elapsed;
					//sw.Reset();

					sw = new Stopwatch();
					sw.Start();



					var DefaultRotation = 0;


					var z = Enumerable.ToArray(
							from k in new[] { 
								new {Image = Image1, r = (double)0.0 + DefaultRotation }, 
								new {Image =Image2, r = (double)0.5 + DefaultRotation }, 
								new {Image =Image3, r = (double)1.0 + DefaultRotation }, 
								new {Image =Image4, r = (double)1.5 + DefaultRotation} 
							}
							let M11 = Math.Cos(x + Math.PI * k.r)
							let M12 = Math.Sin(x + Math.PI * k.r) * 0.5
							orderby M12
							select new { k.Image, M11, M12 }
					);


					foreach (var k in z)
					{
						k.Image.Element.Dispose();


						//k.Image.style.SetSize(System.Convert.ToInt32(100 * k.M11), 100);

						k.Image.ApplyMatrix(0, 0,
							k.M11, k.M12, 0, 1, 0, 0);

						//k.Image.RenderTransform =
						//    new MatrixTransform
						//    {
						//        Matrix = new Matrix
						//        {
						//            M11 = k.M11,
						//            M12 = k.M12,
						//            M21 = 0,
						//            M22 = 1,
						//            //M22 = -1,
						//            //OffsetY = -75
						//            OffsetY = 0
						//        }
						//    };
					}

					foreach (var k in z)
					{
						k.Image.Element.AttachToDocument();
					}


				};

			t.StartInterval(1000 / 30);
		}


		static MatrixTransformC1Example()
		{
			typeof(MatrixTransformC1Example).SpawnTo(i => new MatrixTransformC1Example());
		}

	}

}
