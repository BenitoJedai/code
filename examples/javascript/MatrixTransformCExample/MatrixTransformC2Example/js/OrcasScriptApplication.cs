using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;
using System;


namespace MatrixTransformC2Example.js
{
	[Script, ScriptApplicationEntryPoint]
	public class MatrixTransformC2Example
	{

		public MatrixTransformC2Example()
		{
			for (int i = 0; i < 32; i++)
			{

				Spawn(0.1 * i);
			}

		}

		private static void Spawn(double Rotation)
		{

			var z = new IHTMLDiv().AttachToDocument();
			var r = new IHTMLDiv().AttachTo(z);
			z.style.position = IStyle.PositionEnum.relative;
			var i = new IHTMLImage(Assets.Path + "/Preview.png").AttachTo(r);

			var b = new IHTMLButton().AttachTo(r);

			b.innerText = "hello world";
			b.style.SetLocation(20, 20);
			z.style.SetLocation(100, 100);


			var costheta = Math.Cos(Rotation);
			var sintheta = Math.Sin(Rotation);

			var M11 = costheta;
			var M12 = -sintheta;
			var M21 = sintheta;
			var M22 = costheta;


			z.style.SetMatrixTransform(
					M11, M21,
						M12, M22,

					0, 0
			);
		}


		static MatrixTransformC2Example()
		{
			typeof(MatrixTransformC2Example).SpawnTo(i => new MatrixTransformC2Example());
		}

	}

}
