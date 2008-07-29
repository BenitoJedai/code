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
using ScriptCoreLib.ActionScript.Extensions.flash.display;




namespace RayCaster6.ActionScript
{



	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(Width = DefaultControlWidth, Height = DefaultControlHeight)]
	[SWF(width = DefaultControlWidth, height = DefaultControlHeight, frameRate = 60, backgroundColor = 0)]
	public partial class RayCaster6 : Sprite
	{
		// http://www.glenrhodes.com/wolf/myRay.html
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





			EgoView = new ViewEngineBase(DefaultWidth, DefaultHeight)
			{
				FloorAndCeilingVisible = false,

				ViewPosition = new Point { x = 4, y = 22 },
				ViewDirection = 90.DegreesToRadians(),

			};

			var Portals = new List<PortalInfo>();

			EgoView.ViewDirectionChanged += () => Portals.ForEach(Portal => Portal.View.ViewDirection = EgoView.ViewDirection);

			#region create a dual portal
			var PortalA = new PortalInfo
			{
				Color = 0xFF6A00,
				ViewVector = new Vector { Direction = EgoView.ViewDirection, Position = new Point { x = 4.5, y = 14 } },
				SpriteVector = new Vector { Direction = EgoView.ViewDirection, Position = new Point { x = 3.5, y = 20 } },
			}.AddTo(Portals);


			EgoView.Sprites.Add(PortalA.Sprite);


			var PortalB = new PortalInfo
			{
				Color = 0xff00,
				ViewVector = PortalA.SpriteVector,
				SpriteVector = PortalA.ViewVector,
			}.AddTo(Portals);


			EgoView.Sprites.Add(PortalB.Sprite);
			#endregion



			var Ego = default(SpriteInfo);


			EgoView.ViewPositionChanged +=
				delegate
				{
					foreach (var Portal in Portals)
					{
						var p = EgoView.SpritesFromPointOfView.SingleOrDefault(i => i.Sprite == Portal.Sprite);

						if (p != null)
						{
							if (p.Distance < Portal.Sprite.Range)
							{
								// we are going thro the portal, show it

								new Bitmap(EgoView.Buffer.clone())
								{
									scaleX = DefaultScale,
									scaleY = DefaultScale
								}.AttachTo(this).FadeOutAndOrphanize(1000 / 24, 0.2);

								// fixme: should use Ego.MovementDirection instead
								// currently stepping backwards into the portal will behave recursivly
								EgoView.ViewPosition = Portal.View.ViewPosition.MoveToArc(EgoView.ViewDirection, Portal.Sprite.Range + p.Distance);

								break;
							}
						}
					}

				};

			var CameraView = new ViewEngineBase(64, 48)
			{
			};


			EgoView.RenderOverlay += DrawMinimap;
			EgoView.FramesPerSecondChanged += () => txtMain.text = EgoView.FramesPerSecond + " fps " + new { EgoView.ViewPositionX, EgoView.ViewPositionY };

			EgoView.Image.AttachTo(this);

			txtMain.AttachTo(this);



			EgoView.Image.scaleX = DefaultScale;
			EgoView.Image.scaleY = DefaultScale;
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


			Action UpdateEgoPosition =
				delegate
				{
					if (Ego != null)
					{
						Ego.Position = EgoView.ViewPosition;
						Ego.Direction = EgoView.ViewDirection;
					}
				};

			EgoView.ViewPositionChanged +=
				delegate
				{
					UpdateEgoPosition();
				};

			(1000 / 30).AtInterval(
					delegate
					{
						if (fKeyTurnRight.IsPressed)
							EgoView.ViewDirection += 10.DegreesToRadians();
						else if (fKeyTurnLeft.IsPressed)
							EgoView.ViewDirection -= 10.DegreesToRadians();

						if (fKeyUp.IsPressed || fKeyStrafeLeft.IsPressed || fKeyStrafeRight.IsPressed)
						{
							var d = EgoView.ViewDirection;



							if (fKeyStrafeLeft.IsPressed)
								d -= 90.DegreesToRadians();
							else if (fKeyStrafeRight.IsPressed)
								d += 90.DegreesToRadians();


							EgoView.MoveTo(
									EgoView.ViewPositionX + Math.Cos(d) * 0.2,
									EgoView.ViewPositionY + Math.Sin(d) * 0.2
							);
						}
						else if (fKeyDown.IsPressed)
							EgoView.MoveTo(
								 EgoView.ViewPositionX + Math.Cos(EgoView.ViewDirection) * -0.2,
								 EgoView.ViewPositionY + Math.Sin(EgoView.ViewDirection) * -0.2
						 );


					}
			);

			var UpdatePortals = true;

			stage.keyUp +=
				   e =>
				   {
					   if (e.keyCode == Keyboard.V)
					   {
						   UpdatePortals = !UpdatePortals;
					   }

					   if (e.keyCode == Keyboard.N)
					   {
						   EgoView.RenderLowQualityWalls = !EgoView.RenderLowQualityWalls;
					   }

					   if (e.keyCode == Keyboard.M)
					   {
						   DrawMinimapEnabled = !DrawMinimapEnabled;
					   }

					   if (e.keyCode == Keyboard.B)
					   {
						   EgoView.SpritesVisible = !EgoView.SpritesVisible;
					   }

					   if (e.keyCode == Keyboard.F)
					   {
						   EgoView.FloorAndCeilingVisible = !EgoView.FloorAndCeilingVisible;
					   }

					   if (e.keyCode == Keyboard.DELETE)
					   {
						   EgoView.Sprites.RemoveAll(p => p != Ego);
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

								  //s.Direction += 180.DegreesToRadians();

								  CameraView.ViewPosition = s.Position;
								  CameraView.ViewDirection = s.Direction;
							  }

							  if (e.keyCode == Keyboard.INSERT)
							  {
								  var s = CreateWalkingDummy(Stand, Walk);

								  s.Direction += 180.DegreesToRadians();
								  s.Position = Ego.Position.MoveToArc(Ego.Direction, 0.5);
							  }

							  if (e.keyCode == Keyboard.ENTER)
							  {
								  EgoView.ViewPosition = new Point { x = 4, y = 22 };
								  EgoView.ViewDirection = 270.DegreesToRadians();


							  }



							  if (e.keyCode == Keyboard.BACKSPACE)
							  {

								  (1000 / 30).AtInterval(
									  t =>
									  {
										  EgoView.ViewDirection += 18.DegreesToRadians();

										  if (t.currentCount == 10)
											  t.stop();
									  }
								  );
							  }
						  };
				};


			Assets.ZipFiles.MyZipFile
				.ToFiles()
				.Where(f => f.FileName.EndsWith(".png"))
				.ToBitmapArray(BitmapsLoadedAction);



			Assets.ZipFiles.MyStuff.ToFiles().ToBitmapDictionary(
					f =>
					{
						// ! important
						// ----------------------------------------------------
						// ! loading png via bytes affects pixel values
						// ! this is why map is in gif format

						EgoView.Map.WorldMap = Texture32.Of(f["Map1.gif"], false);

						Action<IEnumerator<Texture64.Entry>, Texture64, Action<SpriteInfo>> AddSpriteByTexture =
								  (SpaceForStuff, tex, handler) => SpaceForStuff.Take().Do(p => CreateDummy(tex).Do(handler).Position.To(p.XIndex + 0.5, p.YIndex + 0.5));

						var FreeSpaceForStuff = EgoView.Map.WorldMap.Entries.Where(i => i.Value == 0).Randomize().GetEnumerator();

						Action<Bitmap> AddSprite =
							e => AddSpriteByTexture(FreeSpaceForStuff, e, null);

						Assets.ZipFiles.MySprites.ToFiles().ToBitmapArray(
						   sprites =>
						   {
							   foreach (var s in sprites)
							   {
								   for (int i = 0; i < 3; i++)
								   {
									   AddSprite(s);
								   }
							   }
						   }
						);

						Assets.ZipFiles.MyGold.ToFiles().ToBitmapArray(
						   sprites =>
						   {
							   var GoldSprites = new List<SpriteInfo>();

							   foreach (var s in sprites)
							   {
								   for (int i = 0; i < 20; i++)
								   {
									   // compiler bug: get a delegate to BCL class
									   //AddSpriteByTexture(FreeSpaceForStuff, s, GoldSprites.Add);

									   AddSpriteByTexture(FreeSpaceForStuff, s,
										   k =>
										   {
											   k.Range = 0.5;
											   GoldSprites.Add(k);
										   }
									   );

								   }
							   }

							   var LastPosition = new Point();

							   EgoView.ViewPositionChanged +=
								   delegate
								   {
									   // only check for items each 0.5 distance travelled
									   if ((EgoView.ViewPosition - LastPosition).length < 0.5)
										   return;

									   Action Later = delegate { };


									   foreach (var Item in EgoView.SpritesFromPointOfView)
									   {
										   var Item_Sprite = Item.Sprite;

										   if (Item.Distance <Item_Sprite.Range)
										   {
											   if (GoldSprites.Contains(Item_Sprite))
											   {
												   // ding-ding-ding!

												   new Bitmap(new BitmapData(DefaultWidth, DefaultHeight, false, 0xffff00))
												   {
													   scaleX = DefaultScale,
													   scaleY = DefaultScale
												   }.AttachTo(this).FadeOutAndOrphanize(1000 / 24, 0.2);

												   Assets.SoundFiles.treasure.ToSoundAsset().play();

												   Later += () => EgoView.Sprites.Remove(Item_Sprite);
											   }
										   }
									   }

									   Later();

									   LastPosition = EgoView.ViewPosition;
								   };


						   }
						);


						Func<string, Texture64> t =
							texname => f[texname + ".png"];

						EgoView.FloorTexture = t("floor");
						EgoView.CeilingTexture = t("roof");



						var DynamicTextureBitmap = new Bitmap(new BitmapData(Texture64.SizeConstant, Texture64.SizeConstant, false, 0));
						Texture64 DynamicTexture = DynamicTextureBitmap;
						uint DynamicTextureKey = 0xffffff;

						EgoView.Map.WorldMap[2, 22] = DynamicTextureKey;
						EgoView.Map.WorldMap[3, 15] = DynamicTextureKey;


						EgoView.Map.Textures = new Dictionary<uint, Texture64>
                        {
                            {0xff0000, t("graywall")},
                            {0x0000ff, t("bluewall")},
                            {0x00ff00, t("greenwall")},
                            {0x7F3300, t("woodwall")},

                            {DynamicTextureKey, DynamicTexture}
                        };


						if (EgoView.CurrentTile != 0)
							throw new Exception("bad start position: " + new { EgoView.ViewPositionX, EgoView.ViewPositionY, EgoView.CurrentTile }.ToString());



						CameraView.Map.WorldMap = EgoView.Map.WorldMap;
						CameraView.Map.Textures = EgoView.Map.Textures;
						CameraView.Sprites = EgoView.Sprites;
						CameraView.ViewPosition = EgoView.ViewPosition;

						foreach (var Portal in Portals)
						{
							Portal.View.Map.WorldMap = EgoView.Map.WorldMap;
							Portal.View.Map.Textures = EgoView.Map.Textures;
							Portal.View.Sprites = EgoView.Sprites;
							Portal.AlphaMask = f["portalmask.png"];
						}


						EgoView.RenderScene();


						var MirrorFrame = f["mirror.png"];
						var counter = 0;

						stage.enterFrame += e =>
							{
								counter++;

								if (UpdatePortals)
								{
									// updateing it too often causes framerate to drop

									foreach (var Portal in Portals)
									{
										Portal.Update();
									}

									DynamicTextureBitmap.bitmapData.fillRect(DynamicTextureBitmap.bitmapData.rect, (uint)(counter * 8 % 256));
									var m = new Matrix();

									// to center
									m.translate(0, 10);
									// m.scale(0.3, 0.3);

									CameraView.RenderScene();

									DynamicTextureBitmap.bitmapData.draw(CameraView.Image.bitmapData, m);
									DynamicTextureBitmap.bitmapData.draw(MirrorFrame.bitmapData);

									DynamicTexture.Update();
								}

								EgoView.RenderScene();
							};






					}
				);

			AttachMovementInput(EgoView);


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




		// fps: 58


		ViewEngineBase EgoView;


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