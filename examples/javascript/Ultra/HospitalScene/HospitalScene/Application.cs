using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.ComponentModel;
using HospitalScene.HTML.Images.SpriteSheet.FromAssets;

namespace HospitalScene
{

	[Description("HospitalScene. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{

		public Application(IHTMLElement e)
		{
			Native.Document.title = "HospitalScene";


			// http://code.google.com/p/corsix-th/wiki/Rendering
			// http://code.google.com/p/corsix-th/wiki/CoordinateSystems#C++_tile_co-ordinates

			var c = new MyCanvas();

			c.AttachToContainer(Native.Document.body);
		
		}


	}


}
