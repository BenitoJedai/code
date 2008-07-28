using System;
using System.Collections.Generic;
using System.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.RayCaster;
using System.Collections.Specialized;




namespace RayCaster6.ActionScript
{


	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(Width = DefaultControlWidth, Height = DefaultControlHeight)]
	[SWF(width = DefaultControlWidth, height = DefaultControlHeight, frameRate = 60, backgroundColor = 0)]
	public partial class RayCaster6 : Sprite
	{
		// http://nihilogic.dk/labs/wolf/sounds/
		// http://www.lostinactionscript.com/blog/index.php/2007/10/13/flash-you-tube-api/
		// http://www.digital-ist-besser.de/
		// http://www.fredheintz.com/sitefred/main.html

		const int DefaultControlWidth = DefaultWidth * DefaultScale;
		const int DefaultControlHeight = DefaultHeight * DefaultScale;

		// 120x90
		// 160x120
		const int DefaultWidth = DefaultHeight * 3 / 2;
		const int DefaultHeight = 120;

		const int DefaultScale = 4;

		public TextField txtMain;

		public RayCaster6()
		{
			txtMain = new TextField
			{
				defaultTextFormat = new TextFormat
				{
					font = "Verdana",
					align = TextFormatAlign.LEFT,
					size = 10,
					color = 0xffffff
				},
				autoSize = TextFieldAutoSize.LEFT,
				text = "0"
			};

			AddFullscreenMenu();


			var r2 = new ViewEngineBase(64, 48)
			{
			};


			var r = new ViewEngineBase(DefaultWidth, DefaultHeight)
			{
				FloorAndCeilingVisible = false,

				ViewPosition = new Point { x = 4, y = 22 },
				ViewDirection = 0.DegreesToRadians(),

			};

			EgoView = r;

			r.RenderOverlay += DrawMinimap;
			r.FramesPerSecondChanged += () => txtMain.text = r.FramesPerSecond + " fps";

			r.Image.AttachTo(this);

			txtMain.AttachTo(this);



			r.Image.scaleX = DefaultScale;
			r.Image.scaleY = DefaultScale;
			//this.filters = new[] { new BlurFilter() };


			KeyboardButton fKeyTurnLeft = new uint[] { Keyboard.LEFT, 'j', 'J', };
			KeyboardButton fKeyTurnRight = new uint[] { Keyboard.RIGHT, 'l', 'L', };

			KeyboardButton fKeyStrafeLeft = new uint[] { 'a', 'A' };
			KeyboardButton fKeyStrafeRight = new uint[] { 'd', 'D' };

			KeyboardButton fKeyUp = new uint[] { Keyboard.UP, 'i', 'I', 'w', 'W' };
			KeyboardButton fKeyDown = new uint[] { Keyboard.DOWN, 'k', 'K', 's', 'S' };


			stage.keyDown +=
				e =>
				{
					var key = e.keyCode;

					fKeyStrafeLeft.ProcessKeyDown(key);
					fKeyStrafeRight.ProcessKeyDown(key);
					fKeyTurnLeft.ProcessKeyDown(key);
					fKeyTurnRight.ProcessKeyDown(key);

					fKeyUp.ProcessKeyDown(key);
					fKeyDown.ProcessKeyDown(key);


				};

			stage.keyUp +=
				e =>
				{
					var key = e.keyCode;


					fKeyStrafeLeft.ProcessKeyUp(key);
					fKeyStrafeRight.ProcessKeyUp(key);

					fKeyTurnLeft.ProcessKeyUp(key);
					fKeyTurnRight.ProcessKeyUp(key);

					fKeyUp.ProcessKeyUp(key);
					fKeyDown.ProcessKeyUp(key);

				};

			var Ego = default(SpriteInfo);

			Action UpdateEgoPosition =
				delegate
				{
					if (Ego != null)
					{
						Ego.Position = r.ViewPosition;
						Ego.Direction = r.ViewDirection;
					}
				};

			r.ViewPositionChanged +=
				delegate
				{
					UpdateEgoPosition();
				};

			(1000 / 30).AtInterval(
					delegate
					{
						if (fKeyTurnRight.IsPressed)
							r.ViewDirection += 10.DegreesToRadians();
						else if (fKeyTurnLeft.IsPressed)
							r.ViewDirection -= 10.DegreesToRadians();

						if (fKeyUp.IsPressed || fKeyStrafeLeft.IsPressed || fKeyStrafeRight.IsPressed)
						{
							var d = r.ViewDirection;



							if (fKeyStrafeLeft.IsPressed)
								d -= 90.DegreesToRadians();
							else if (fKeyStrafeRight.IsPressed)
								d += 90.DegreesToRadians();


							r.MoveTo(
									r.ViewPositionX + Math.Cos(d) * 0.2,
									r.ViewPositionY + Math.Sin(d) * 0.2
							);
						}
						else if (fKeyDown.IsPressed)
							r.MoveTo(
								 r.ViewPositionX + Math.Cos(r.ViewDirection) * -0.2,
								 r.ViewPositionY + Math.Sin(r.ViewDirection) * -0.2
						 );


					}
			);


			stage.keyUp +=
				   e =>
				   {
					   if (e.keyCode == Keyboard.N)
					   {
						   r.RenderLowQualityWalls = !r.RenderLowQualityWalls;
					   }

					   if (e.keyCode == Keyboard.M)
					   {
						   DrawMinimapEnabled = !DrawMinimapEnabled;
					   }

					   if (e.keyCode == Keyboard.B)
					   {
						   r.SpritesVisible = !r.SpritesVisible;
					   }

					   if (e.keyCode == Keyboard.F)
					   {
						   r.FloorAndCeilingVisible = !r.FloorAndCeilingVisible;
					   }

					   if (e.keyCode == Keyboard.DELETE)
					   {
						   r.Sprites.RemoveAll(p => p != Ego);
					   }
				   };


			Action<Bitmap[]> BitmapsLoadedAction =
				Bitmaps =>
				{
					Func<Texture64[], Texture64[]> Reorder8 =
						p =>
							Enumerable.ToArray(
								from i in Enumerable.Range(0, 8)
								select p[(i + 6) % 8]
							);

					var BitmapStream = Bitmaps.Select(i => (Texture64)i).GetEnumerator();

					Func<Texture64[]> Next8 =
						delegate
						{
							// keeping compiler happy with full delegate form

							if (BitmapStream == null)
								throw new Exception("BitmapStream is null");

							return Reorder8(BitmapStream.Take(8));
						};


					var Stand = Next8();
					var Walk = new[]
                        {
                            Next8(),
                            Next8(),
                            Next8(),
                            Next8(),
                        };



					Ego = CreateWalkingDummy(Stand, Walk);
					UpdateEgoPosition();



					stage.keyUp +=
						  e =>
						  {
							  if (e.keyCode == Keyboard.SPACE)
							  {
								  var s = CreateWalkingDummy(Stand, Walk);

								  s.Direction += 180.DegreesToRadians();

								  r2.ViewPosition = s.Position;
								  r2.ViewDirection = s.Direction;
							  }

							  if (e.keyCode == Keyboard.INSERT)
							  {
								  var s = CreateWalkingDummy(Stand, Walk);

								  s.Direction += 180.DegreesToRadians();
								  s.Position = Ego.Position.MoveToArc(Ego.Direction, 0.5);
							  }

							  if (e.keyCode == Keyboard.ENTER)
							  {
								  r.ViewPosition = new Point { x = 4, y = 22 };
								  r.ViewDirection = 270.DegreesToRadians();


							  }



							  if (e.keyCode == Keyboard.BACKSPACE)
							  {

								  (1000 / 30).AtInterval(
									  t =>
									  {
										  r.ViewDirection += 18.DegreesToRadians();

										  if (t.currentCount == 10)
											  t.stop();
									  }
								  );
							  }
						  };
				};


			MyZipFile
				.ToFiles()
				.Where(f => f.FileName.EndsWith(".png"))
				.ToBitmapArray(BitmapsLoadedAction);



			MyStuff.ToFiles().ToBitmapDictionary(
					f =>
					{
						// ! important
						// ----------------------------------------------------
						// ! loading png via bytes affects pixel values
						// ! this is why map is in gif format

						r.Map.WorldMap = Texture32.Of(f["Map1.gif"], false);

						MySprites.ToFiles().ToBitmapArray(
						   sprites =>
						   {
							   Action<IEnumerator<Texture64.Entry>, Texture64> AddSpriteByTexture =
								   (SpaceForStuff, tex) => SpaceForStuff.Take().Do(p => CreateDummy(tex).Position.To(p.XIndex + 0.5, p.YIndex + 0.5));

							   var FreeSpaceForStuff = r.Map.WorldMap.Entries.Where(i => i.Value == 0).Randomize().GetEnumerator();

							   Action<Bitmap> AddSprite =
								   e => AddSpriteByTexture(FreeSpaceForStuff, e);

							   foreach (var s in sprites)
							   {
								   for (int i = 0; i < 3; i++)
								   {
									   AddSprite(s);
								   }
							   }


							   //Func<Texture64.Entry, bool> IsNearWall =
							   //   w =>
							   //   {
							   //       Func<int, int, bool> WallAtOffset =
							   //           (x, y) => r.Map.WorldMap[w.XIndex + x, w.YIndex + y] != 0;

							   //       if (WallAtOffset(1, 0))
							   //           return true;

							   //       if (WallAtOffset(-1, 0))
							   //           return true;

							   //       if (WallAtOffset(0, 1))
							   //           return true;

							   //       if (WallAtOffset(0, -1))
							   //           return true;

							   //       return false;
							   //   };

							   //var FreeSpaceNearWalls = FreeSpaceForStuff.Where(IsNearWall);
							   //var FreeSpaceForLamps = FreeSpaceForStuff.Where(w => !IsNearWall(w));

							   //var SpaceNearWalls = FreeSpaceNearWalls.Randomize().GetEnumerator();
							   //var SpaceForLamps = FreeSpaceForLamps.Randomize().GetEnumerator();




							   //Action<string> AddSpriteNearWall =
							   //    texname => AddSpriteByTexture(SpaceNearWalls, t(texname));


							   //Action<string> AddSpaceForLamps =
							   //    texname => AddSpriteByTexture(SpaceForLamps, t(texname));


							   //   AddSpaceForLamps.Multiple(
							   //       new KeyValuePairList<int, string>
							   //{
							   //    // multi dict?
							   //    {9, "lamp"},
							   //    {8, "chandelier"},

							   //}
							   //   );

							   //   AddSpriteNearWall.Multiple(
							   //      new KeyValuePairList<int, string>
							   //{
							   //    // multi dict?

							   //    {4, "armor"},
							   //    {32, "plantbrown"},
							   //    {32, "plantgreen"},
							   //}
							   //  );
						   }
						);


						Func<string, Texture64> t =
							texname => f[texname + ".png"];

						r.FloorTexture = t("floor");
						r.CeilingTexture = t("roof");



						var DynamicTextureBitmap = new Bitmap(new BitmapData(Texture64.SizeConstant, Texture64.SizeConstant, false, 0));
						Texture64 DynamicTexture = DynamicTextureBitmap;
						uint DynamicTextureKey = 0xffffff;

						r.Map.WorldMap[2, 22] = DynamicTextureKey;
						r.Map.WorldMap[3, 15] = DynamicTextureKey;


						r.Map.Textures = new Dictionary<uint, Texture64>
                        {
                            {0xff0000, t("graywall")},
                            {0x0000ff, t("bluewall")},
                            {0x00ff00, t("greenwall")},
                            {0x7F3300, t("woodwall")},

                            {DynamicTextureKey, DynamicTexture}
                        };

						r.ViewDirection = 270.DegreesToRadians();
						r.ViewPosition = r.ViewPosition;

						if (r.CurrentTile != 0)
							throw new Exception("bad start position: " + new { r.ViewPositionX, r.ViewPositionY, r.CurrentTile }.ToString());


						r.RenderScene();

						stage.enterFrame += e => r.RenderScene();

						var MirrorFrame = f["mirror.png"];

						30.AtInterval(
							timer =>
							{
								DynamicTextureBitmap.bitmapData.fillRect(DynamicTextureBitmap.bitmapData.rect, (uint)(timer.currentCount * 8 % 256));
								var m = new Matrix();

								// to center
								m.translate(0, 10);
								// m.scale(0.3, 0.3);

								r2.RenderScene();

								DynamicTextureBitmap.bitmapData.draw(r2.Image.bitmapData, m);
								DynamicTextureBitmap.bitmapData.draw(MirrorFrame.bitmapData);

								DynamicTexture.Update();
							}
						);




						r2.Map.WorldMap = r.Map.WorldMap;
						r2.Map.Textures = r.Map.Textures;
						r2.Sprites = r.Sprites;
						r2.ViewPosition = r.ViewPosition;

					}
				);

			AttachMovementInput(r);


		}

		private void AddFullscreenMenu()
		{
			var GoFullScreen = new ContextMenuItem("Fullscreen");

			GoFullScreen.menuItemSelect +=
				delegate
				{
					stage.SetFullscreen(true);
				};

			this.contextMenu = new ContextMenu
			{
				customItems = new[] { GoFullScreen }
			};
		}


		[Embed("/flashsrc/textures/dude5.zip")]
		Class MyZipFile;

		[Embed("/flashsrc/textures/stuff.zip")]
		Class MyStuff;

		[Embed("/flashsrc/textures/sprites.zip")]
		Class MySprites;

		// fps: 58

		bool DrawMinimapEnabled;

		ViewEngineBase EgoView;

		private void DrawMinimap()
		{
			if (!DrawMinimapEnabled)
				return;

			var _WallMap = EgoView.Map.WallMap;
			var posX = EgoView.ViewPositionX;
			var posY = EgoView.ViewPositionY;
			var rayDirLeft = EgoView.ViewDirectionLeftBorder;
			var rayDirRight = EgoView.ViewDirectionRightBorder;


			 int isize = stage.stageHeight / (Texture32.SizeConstant + 2);

			var minimap = new BitmapData(isize * (_WallMap.Size + 2), isize * (_WallMap.Size + 2), true, 0x0);
			var minimap_bmp = new Bitmap(minimap);


			for (int ix = 0; ix < _WallMap.Size; ix++)
				for (int iy = 0; iy < _WallMap.Size; iy++)
				{
					if (_WallMap[ix, iy] > 0)
						minimap.fillRect(new Rectangle((ix + 1) * isize, (iy + 1) * isize, isize, isize), 0x7f00ff00);

				}

			//minimap.applyFilter(minimap, minimap.rect, new Point(), new GlowFilter(0x00ff00));


			minimap.drawLine(0xffffffff,
					(posX + 1) * isize,
					(posY + 1) * isize,
					(posX + 1 + Math.Cos(rayDirLeft) * 8) * isize,
					(posY + 1 + Math.Sin(rayDirLeft) * 8) * isize
					);

			minimap.drawLine(0xffffffff,
				(posX + 1) * isize,
				(posY + 1) * isize,
				(posX + 1 + Math.Cos(rayDirRight) * 8) * isize,
				(posY + 1 + Math.Sin(rayDirRight) * 8) * isize
				);

			//Console.WriteLine("left: " + rayDirLeft);
			//Console.WriteLine("right: " + rayDirLeft);

			foreach (var ss in EgoView.SpritesFromPointOfView)
			{
				uint color = 0xff00ffff;



				if (!ss.ViewInfo.IsInView)
					color = 0xff000000;


				minimap.fillRect(new Rectangle(
						(ss.Sprite.Position.x + 0.5) * isize,
						(ss.Sprite.Position.y + 0.5) * isize,
						isize,
						isize), color);

				var _x = (ss.Sprite.Position.x + 1) * isize;
				var _y = (ss.Sprite.Position.y + 1) * isize;


				minimap.drawLine(
						0xffffffff,
						_x,
						_y,
						_x + Math.Cos(ss.Sprite.Direction) * isize * 4,
						_y + Math.Sin(ss.Sprite.Direction) * isize * 4
				);

			}

			minimap.fillRect(new Rectangle((posX + 0.5) * isize, (posY + 0.5) * isize, isize, isize), 0xffff0000);



			EgoView.Buffer.draw(minimap);
		}

		public SpriteInfo CreateDummy(Texture64 Stand)
		{
			return CreateWalkingDummy(new[] { Stand });

		}

		public SpriteInfo CreateWalkingDummy(Texture64[] Stand, params Texture64[][] Walk)
		{
			var s = new SpriteInfo
			{
				Position = new Point { x = EgoView.ViewPositionX, y = EgoView.ViewPositionY },
				Frames = Stand,
				Direction = EgoView.ViewDirection
			}.AddTo(EgoView.Sprites);

			if (Walk.Length > 0)
				(200).AtInterval(
					t =>
					{
						s.Frames = Walk[t.currentCount % Walk.Length];
					}
				);

			return s;
		}



	}
}