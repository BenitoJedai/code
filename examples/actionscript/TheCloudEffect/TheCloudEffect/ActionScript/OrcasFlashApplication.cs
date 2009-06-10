using System;
using System.Collections.Generic;
using System.IO;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.ui;
using TheCloudEffect.Shared;

namespace TheCloudEffect.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(AlignToCenter = true, Background = true, BackgroundColor = 0, Width = STAGE_W, Height = STAGE_H)]
	[SWF(width = STAGE_W, height = STAGE_H, backgroundColor = 0)]
	public class TheCloudEffect : Sprite
	{
		// This example is a port of http://wonderfl.kayac.com/blogparts/2a89f8ce73761709029b59ade165662fb5f0e9d3#

		public const int STAGE_W = 600;
		public const int STAGE_H = 400;

		public const int _H_RATE = 2;	

		private BitmapData _perlinNoiseBitmapData;
		private int _seed = int.MaxValue.Random();
		const double _perlinNoiseSize = 0.25;	
		private int _perlinNoiseSizeW = Convert.ToInt32(STAGE_W * _perlinNoiseSize);
		private int _perlinNoiseSizeH = Convert.ToInt32(STAGE_H * _perlinNoiseSize * _H_RATE);
		private int _cloudSize = Convert.ToInt32(_perlinNoiseSize * 240);
		const uint octaves = 5;

		private Point[] _offsetList = new Point[octaves];	
		private double[] _offsetSpeedList = new[] { 
			//0.5, 0.4, 0.3, 0.2, 0.15 
			0.3, 0.2, 0.1, 0.1, 0.05 
		};	

		private BitmapData _displacementBitmapData;
		private DisplacementMapFilter _displacementMapFilter;

		private BitmapData _palletBitmapData;
		private uint[] _pallet = new uint[256];
		private uint[] _nullPallet = new uint[256];

		private BitmapData _scaleChangeBitmapData;
		private Matrix _scaleChangeMatrix;

		private Sprite _cover;

		private Rectangle _rect = new Rectangle(0, 0, STAGE_W, STAGE_H);
		private Point _point = new Point(0, 0);
		private Rectangle _rectSmall;

		public TheCloudEffect()
		{
			_rectSmall = new Rectangle(0, 0, _perlinNoiseSizeW, _perlinNoiseSizeH);

			this.InvokeWhenStageIsReady(Initialize);

		}

		private void Initialize()
		{
			//stage.frameRate = 30;
			//stage.quality = StageQuality.HIGH;


			_perlinNoiseBitmapData = new BitmapData(_perlinNoiseSizeW, _perlinNoiseSizeH, false);
			for (var i = 0; i < octaves; i++)
				_offsetList[i] = new Point();


			_displacementBitmapData = new BitmapData(STAGE_W, STAGE_H, false);
			_displacementMapFilter = new DisplacementMapFilter(null, _point, 0, BitmapDataChannel.RED, 0,
									 100, DisplacementMapFilterMode.CLAMP);

			_palletBitmapData = new BitmapData(STAGE_W, STAGE_H, false);
			createGradation();



			_scaleChangeBitmapData = new BitmapData(STAGE_W, STAGE_H, false);
			_scaleChangeMatrix = new Matrix();
			_scaleChangeMatrix.scale(1 / _perlinNoiseSize, 1 / _perlinNoiseSize / _H_RATE);

			_cover = new Sprite();
			var matrix = new Matrix();
			matrix.createGradientBox(STAGE_W, STAGE_H, Math.PI / 2);

			var colors = new[] { 0x666666u, 0xaaaaaau };
			var alphas = new[] { 1.0, 1.0 };
			var ratios = new[] { 128, 255 };

			_cover.graphics.beginGradientFill(GradientType.LINEAR,
				colors,
				alphas,
				ratios, matrix
			);

			_cover.graphics.drawRect(0, 0, STAGE_W, STAGE_H);
			_cover.blendMode = BlendMode.OVERLAY;

			addChild(new Bitmap(_scaleChangeBitmapData));
			addChild(_cover);

			this.enterFrame += new Action<ScriptCoreLib.ActionScript.flash.events.Event>(TheCloudEffect_enterFrame);

			// some branding
			KnownEmbeddedResources.Default[KnownAssets.Path.Assets + "/jsc.png"].ToBitmapAsset().AttachTo(this).MoveTo(STAGE_W - 128, STAGE_H - 128);

			this.stage.click +=
				delegate
				{
					this.stage.SetFullscreen(true);
				};
		}

		void TheCloudEffect_enterFrame(ScriptCoreLib.ActionScript.flash.events.Event obj)
		{
			for (var i = 0; i < octaves; i++)
			{
				_offsetList[i].x -= _offsetSpeedList[i];
			}
			_perlinNoiseBitmapData.perlinNoise(_cloudSize, _cloudSize, octaves, _seed, false, true, 0, true, _offsetList);
			_displacementMapFilter.mapBitmap = _perlinNoiseBitmapData;
			_displacementBitmapData.applyFilter(_perlinNoiseBitmapData, _rectSmall, _point, _displacementMapFilter);
			_displacementBitmapData.draw(_perlinNoiseBitmapData, null, null, BlendMode.HARDLIGHT);
			_palletBitmapData.paletteMap(_displacementBitmapData, _rectSmall, _point, _pallet, _nullPallet, _nullPallet);
			_scaleChangeBitmapData.draw(_palletBitmapData, _scaleChangeMatrix, null, null, null, true);

		}

		private void createGradation()
		{
			var tmpShape = new Shape();
			var matrix = new Matrix();
			matrix.createGradientBox(255, 0);
			//var colorList = new[] { 0xa7a7c4u, 0xf3f8ffu, 0xffffffu, 0x418fdfu };
			var colorList =
				new[] { 0xa7a7c4u, 0xf3f8ffu, 0xffffffu, 0x418fdfu };
				//new[] { 0xa7a7FFu, 0xf3f8FFu, 0xffffffu, 0x418fFFu };
				//new[] { 0xFFa7a7u, 0xFFf3f8u, 0xffffffu, 0xFF418fu };
				//new[] { 0x007777u, 0x007378u, 0x0u, 0x00214fu };
				//= new[] {0x824229u, 0xfb8f1bu, 0xffc768u, 0xa0afacu };
	
			var alphaList = new[] { 1.0, 1.0, 1.0, 1.0 };
			var ratioList = new[] { 0, 80, 100, 200 };
			tmpShape.graphics.beginGradientFill(GradientType.LINEAR, colorList, alphaList, ratioList, matrix);
			tmpShape.graphics.drawRect(0, 0, 255, 1);
			var tmpBitmap = new BitmapData(255, 1, false);
			tmpBitmap.draw(tmpShape);
			for (var i = 0; i < 256; i++)
			{
				_pallet[i] = (tmpBitmap.getPixel(i, 0));
				_nullPallet[i] = (0x000000);
			}
		}





		/// <summary>
		/// Default constructor
		/// </summary>
		//public TheCloudEffect()
		//{

		//    for (var j = 0.0; j < 1; j += 0.1)
		//    {
		//        this.graphics.beginFill(0xff0000, j);
		//        this.graphics.drawCircle(40, 40, 40 * (1.0 - j));
		//        this.graphics.endFill();
		//    }

		//    var step = 100;
		//    for (int i = 0; i < 4; i++)
		//    {
		//        addChild(
		//           new TextField
		//           {
		//               text = "hello world",
		//               x = step * i,
		//               y = 20,
		//               textColor = 0x00ff00,
		//               sharpness = 400
		//           });
		//    }

		//    var t = new TextField
		//        {
		//            text = "powered by jsc",
		//            background = true,
		//            x = 20,
		//            y = 40,
		//            alwaysShowSelection = true,
		//        }.AttachTo(this);


		//}

		static TheCloudEffect()
		{
			// add resources to be found by ImageSource
			KnownEmbeddedAssets.RegisterTo(
				KnownEmbeddedResources.Default.Handlers
			);
		}

	}

	[Script]
	public class KnownEmbeddedAssets
	{
		[EmbedByFileName]
		public static Class ByFileName(string e)
		{
			throw new NotImplementedException();
		}

		public static void RegisterTo(List<Converter<string, Class>> Handlers)
		{
			// assets from current assembly
			Handlers.Add(e => ByFileName(e));

			//AvalonUgh.Assets.ActionScript.KnownEmbeddedAssets.RegisterTo(Handlers);

			//// assets from referenced assemblies
			//Handlers.Add(e => global::ScriptCoreLib.ActionScript.Avalon.Cursors.EmbeddedAssets.Default[e]);
			//Handlers.Add(e => global::ScriptCoreLib.ActionScript.Avalon.TiledImageButton.Assets.Default[e]);

		}
	}

}