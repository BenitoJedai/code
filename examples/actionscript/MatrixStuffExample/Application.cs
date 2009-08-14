﻿using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.utils;

namespace MatrixStuffExample
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(WithResources = true, Width = 600, Height = 600, Background = true, BackgroundColor = 0, AlignToCenter = true)]
	[SWF(width = 600, height = 600)]
	public class Application : Sprite
	{
		// port of http://www.bit-101.com/blog/?p=2339

		// how can we port this to wpf?
		// http://msdn.microsoft.com/en-us/library/ms753347.aspx
		// http://www.odewit.net/Perspective/dotnet3.5/PerspectiveDemo.xbap?page=pWpf3D/ButtonFaderKnob3D.xaml
		// http://www.odewit.net/ArticleContent.aspx?id=Wpf3DControls&format=html
		// seems like this could be done in silverlight, but not in WPF at this time.
		// not cool - WPF has no audio nor no decent easy 3D api.

		public Application()
		{
			var cloud = new TheCloudEffect().AttachTo(this);



			#region powered by jsc
			new TextField
			{
				width = 600,
				height = 400,
				x = 20,
				y = 20,
				defaultTextFormat = new TextFormat
				{
					size = 30,
					color = 0xff,
					font = "Verdana"
				},
				text = "powered by jsc",

				filters = new BitmapFilter[] { new DropShadowFilter() },
			}.AttachTo(this);
			#endregion

			#region jsc_diagram
			var jsc_diagram = KnownEmbeddedResources.Default["assets/MatrixStuffExample/jsc_diagram.png"].ToBitmapAsset();

			jsc_diagram.x = -jsc_diagram.width / 2;
			jsc_diagram.y = -jsc_diagram.height / 2;

			var sprite = new Sprite
			{
				x = 300,
				y = 300,
				z = 100
			}.AttachTo(this);

			jsc_diagram.filters = new BitmapFilter[] { new GlowFilter(0xffffff, 1, 12, 12) };
			jsc_diagram.AttachTo(sprite);

			#endregion

	
			this.enterFrame +=
				e =>
				{
					sprite.transform.matrix3D.pointAt(new Vector3D(mouseX, mouseY, 0),
						// fixed: an now we are not showing up in reverse
						new Vector3D(0, 0, -0.9999), new Vector3D(0,  -0.9999, 0)
					);

				
				};

			#region jsc_preview2

			var jsc_preview2 = KnownEmbeddedResources.Default["assets/MatrixStuffExample/Preview.png"].ToBitmapAsset();

			jsc_preview2.x = -jsc_preview2.width / 2;
			jsc_preview2.y = -jsc_preview2.height / 2;

			var sprite2 = new Sprite
			{
				x = 6 + 60,
				y = 600 - 45 - 6,
				z = 0.001
			}.AttachTo(this);

			jsc_preview2.filters = new BitmapFilter[] { new GlowFilter(0xffffff, 1, 12, 12) };
			jsc_preview2.AttachTo(sprite2);
			#endregion



			var t = new Timer(1000 / 60);

			t.timer +=
				delegate
				{
					var x = sprite2.x;
					var y = sprite2.y;

					sprite2.transform.matrix3D.appendTranslation(-x, -y, 0);
					sprite2.transform.matrix3D.appendRotation(1, Vector3D.Y_AXIS);
					sprite2.transform.matrix3D.appendTranslation(x, y, 0);

				};

			t.start();

			KnownEmbeddedResources.Default["assets/MatrixStuffExample/jsc.png"].ToBitmapAsset().AttachTo(this).MoveTo(600 - 96, 600 - 96);


		}


	}

}