using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.utils;
using AirforceExampleX.ActionScript.Images;

namespace AirforceExample
{
	public sealed class Application : Sprite
	{
        public const int DefaultWidth = 600;
        public const int DefaultHeight = 600;

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

            //#region jsc_diagram
            //var jsc_diagram = KnownEmbeddedResources.Default["assets/AirforceExample/Preview.png"].ToBitmapAsset();

            //jsc_diagram.x = -jsc_diagram.width / 2;
            //jsc_diagram.y = -jsc_diagram.height / 2;

            //var sprite = new Sprite
            //{
            //    x = 500,
            //    y = 300,
            //    z = 100
            //}.AttachTo(this);

            //jsc_diagram.filters = new BitmapFilter[] { new GlowFilter(0xffffff, 1, 12, 12) };
            //jsc_diagram.AttachTo(sprite);

            //#endregion


            //this.enterFrame +=
            //    e =>
            //    {
            //        sprite.transform.matrix3D.pointAt(new Vector3D(mouseX, mouseY, 0),
            //            // fixed: an now we are not showing up in reverse
            //            new Vector3D(0, 0, -0.9999), new Vector3D(0, -0.9999, 0)
            //        );

            //        //sprite.transform.matrix3D.appendTranslation(-300, -300, 0);
            //        //sprite.transform.matrix3D.appendRotation(45, Vector3D.Y_AXIS);
            //        //sprite.transform.matrix3D.appendTranslation(300, 300, 0);

            //    };



			#region jsc_preview2

            var jsc_preview2 = new left();
   
			jsc_preview2.x = -jsc_preview2.width / 2;
			jsc_preview2.y = -jsc_preview2.height / 2;

			var sprite2 = new Sprite
			{
				x = 200,
				y = 300,
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
					sprite2.transform.matrix3D.appendRotation(2, Vector3D.X_AXIS);
					sprite2.transform.matrix3D.appendTranslation(x, y, 0);

				};

			t.start();



			#region jsc_preview3

            var jsc_preview3 = new right();

			jsc_preview3.x = -jsc_preview3.width / 2;
			jsc_preview3.y = -jsc_preview3.height / 2;

			var sprite3 = new Sprite
			{
				x = 200,
				y = 300,
				z = 0.001
			}.AttachTo(this);

			jsc_preview3.filters = new BitmapFilter[] { new GlowFilter(0xffffff, 1, 12, 12) };
			jsc_preview3.AttachTo(sprite3);
			#endregion


			{
				var x = sprite3.x;
				var y = sprite3.y;

				sprite3.transform.matrix3D.appendTranslation(-x, -y, 0);
				sprite3.transform.matrix3D.appendRotation(45, Vector3D.Y_AXIS);
				sprite3.transform.matrix3D.appendTranslation(x, y, 0);

			};

			var t3 = new Timer(1000 / 60);


			t3.timer +=
				delegate
				{
					var x = sprite3.x;
					var y = sprite3.y;

					sprite3.transform.matrix3D.appendTranslation(-x, -y, 0);
					sprite3.transform.matrix3D.appendRotation(1, Vector3D.Y_AXIS);
					sprite3.transform.matrix3D.appendRotation(2, Vector3D.X_AXIS);
					sprite3.transform.matrix3D.appendTranslation(x, y, 0);

				};

			t3.start();

            new global::AirforceExampleX.ActionScript.Images.jsc().AttachTo(this).MoveTo(600 - 96, 600 - 96);


		}


	}

}