using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.ComponentModel;
using UltraApplicationWithAssets1.HTML.Images.FromAssets;
using ScriptCoreLib.Avalon;

namespace UltraApplicationWithAssets1
{

	[Description("UltraApplicationWithAssets1. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{

		public Application(IHTMLElement e)
		{
			Native.Document.title = "Ultra Application";

			Action<AffineTransformBase> C = a =>
			{
				var x = new Shadow1x256();

				x.style.SetLocation(0, 0);
				x.AttachToDocument();

				x.style.SetMatrixTransform(
					a
				);
			};

			C(
				new AffineTransformBase
				{
					Width = 256,
					Height = 256,

			
					X1 = 250,
					Y1 = 150,

					X2 = 100,
					Y2 = 150,

					X3 = 100,
					Y3 = 100, 

				}
			);


			//C(
			//        new AffineTransformBase
			//        {
			//            Width = 256,
			//            Height = 256,



			//            X1 = 250,
			//            Y1 = 150,

			//            X2 = 100,
			//            Y2 = 100,

			//            X3 = 100,
			//            Y3 = 500, 

			//        }
			//    );

		}


	}


}
