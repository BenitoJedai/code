using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using System.Linq;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Archive.Extensions;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.Shared.Maze;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashTreasureHunt.ActionScript
{
	partial class FlashTreasureHunt
	{
		int NonblockingTotal;
		int AmmoTotal;
	

		public IEnumerable<TextureBase.Entry> FreeSpace
		{
			get
			{
				return EgoView.Map.WallMap.Entries.Where(
					i =>
					{
						if (i.Value != 0)
							return false;

						var c = 0;

						if (EgoView.Map.WallMap[i.XIndex - 1, i.YIndex] == 0)
							c++;

						if (EgoView.Map.WallMap[i.XIndex + 1, i.YIndex] == 0)
							c++;

						if (EgoView.Map.WallMap[i.XIndex, i.YIndex + 1] == 0)
							c++;

						if (EgoView.Map.WallMap[i.XIndex, i.YIndex - 1] == 0)
							c++;

						return c >= 2;
					}
				);
			}
		}

	



		private void AddIngameEntities()
		{

			



			Action<IEnumerator<Texture64.Entry>, Texture64, Action<SpriteInfoExtended>> AddSpriteByTexture =
									 (SpaceForStuff, tex, handler) =>
									 {
										 var p = SpaceForStuff.TakeOrDefault();

										 if (p == null)
											 return;

										 CreateDummy(tex).Do(handler).Position.To(p.XIndex + 0.5, p.YIndex + 0.5);

									 };



			var FreeSpaceForStuff = FreeSpace.Randomize().GetEnumerator();

			CreateGuards(FreeSpaceForStuff);


			var FreeSpaceCount = FreeSpace.Count();

			NonblockingTotal = (FreeSpaceCount * 0.4).Floor();
			AmmoTotal = (FreeSpaceCount * 0.2).Floor();
			GoldTotal = (FreeSpaceCount * 0.4).Floor();

			WriteLine(new { GoldTotal, AmmoTotal, NonblockingTotal }.ToString());
			#region gold


			#region nonblock
			Action AddNonBlockingItems =
				delegate
				{



					Assets.Default.nonblock.ToBitmapArray(
						sprites =>
						{
							for (int i = 0; i < NonblockingTotal; i++)
							{
								// compiler bug: get a delegate to BCL class
								//AddSpriteByTexture(FreeSpaceForStuff, s, GoldSprites.Add);

								AddSpriteByTexture(FreeSpaceForStuff, sprites.AtModulus(i),
									k =>
									{
										k.Range = 0.5;
										//GoldSprites.Add(k);
									}
								);

							}
						}
					);


				};
			#endregion



			#region AddAmmoPickups
			Action AddAmmoPickups =
				delegate
				{
					Assets.Default.ammo_sprites.ToBitmapArray(
					   sprites =>
					   {

						   for (int i = 0; i < AmmoTotal; i++)
						   {
							   // compiler bug: get a delegate to BCL class
							   //AddSpriteByTexture(FreeSpaceForStuff, s, GoldSprites.Add);

							   AddSpriteByTexture(FreeSpaceForStuff, sprites.AtModulus(i),
								   k =>
								   {
									   k.Range = 0.5;
									   k.ItemTaken +=
										   delegate
										   {
											   Assets.Default.Sounds.ammo.play();

											   // we have taken ammo

											   AddAmmoToEgoAndSwitchWeapon();
										   };

									   AmmoSprites.Add(k);
								   }
							   );

						   }

						   var LastPosition = new Point();

						   EgoView.ViewPositionChanged +=
							   delegate
							   {
								   // only check for items each 0.5 distance travelled
								   if ((EgoView.ViewPosition - LastPosition).length < 0.5)
									   return;

								   Action Later = null;
								   Action ItemTaken = null;


								   foreach (var Item in EgoView.SpritesFromPointOfView)
								   {
									   var Item_Sprite = Item.Sprite as SpriteInfoExtended;

									   if (Item_Sprite != null)
										   if (!Item_Sprite.IsTaken)
											   if (Item.Distance < Item_Sprite.Range)
											   {
												   if (AmmoSprites.Contains(Item_Sprite))
												   {
													   // ding-ding-ding!
													   Item_Sprite.IsTaken = true;

													   new Bitmap(new BitmapData(DefaultWidth, DefaultHeight, false, 0x0000ff))
													   {
														   scaleX = DefaultScale,
														   scaleY = DefaultScale
													   }.AttachTo(this).FadeOutAndOrphanize(1000 / 24, 0.2);



													   if (Item_Sprite != null)
														   if (Item_Sprite.ItemTaken != null)
															   ItemTaken += () => Item_Sprite.ItemTaken();

													   // GoldTakenCounter = (GoldTakenCounter + 1).Min(1);

													   Later +=
														   delegate
														   {
															   EgoView.Sprites.Remove(Item_Sprite);
															   AmmoSprites.Remove(Item_Sprite);
														   };
												   }
											   }
								   }

								   if (Later != null)
									   Later();

								   LastPosition = EgoView.ViewPosition;

								   if (ItemTaken != null)
									   ItemTaken();
							   };

						   AddNonBlockingItems();
					   }
					);

				};
			#endregion



			Assets.Default.gold.ToBitmapArray(
			   sprites =>
			   {

				   for (int i = 0; i < GoldTotal; i++)
				   {

					   // compiler bug: get a delegate to BCL class
					   //AddSpriteByTexture(FreeSpaceForStuff, s, GoldSprites.Add);

					   AddSpriteByTexture(FreeSpaceForStuff, sprites.AtModulus(i),
						   k =>
						   {
							   k.Range = 0.5;
							   k.ItemTaken +=
								   delegate
								   {
									   Assets.Default.Sounds.treasure.play();

									   AddTreasure();
								   };

							   GoldSprites.Add(k);
						   }
					   );

				   }

				   var LastPosition = new Point();

				   EgoView.ViewPositionChanged +=
					   delegate
					   {
						   // only check for items each 0.5 distance travelled
						   if ((EgoView.ViewPosition - LastPosition).length < 0.5)
							   return;

						   Action Later = null;
						   Action ItemTaken = null;


						   foreach (var Item in EgoView.SpritesFromPointOfView)
						   {
							   var Item_Sprite = Item.Sprite as SpriteInfoExtended;

							   if (Item_Sprite != null)
								   if (!Item_Sprite.IsTaken)
									   if (Item.Distance < Item_Sprite.Range)
									   {
										   if (GoldSprites.Contains(Item_Sprite))
										   {
											   // ding-ding-ding!
											   Item_Sprite.IsTaken = true;

											   new Bitmap(new BitmapData(DefaultWidth, DefaultHeight, false, 0xffff00))
											   {
												   scaleX = DefaultScale,
												   scaleY = DefaultScale
											   }.AttachTo(this).FadeOutAndOrphanize(1000 / 24, 0.2);



											   if (Item_Sprite != null)
												   if (Item_Sprite.ItemTaken != null)
													   ItemTaken += () => Item_Sprite.ItemTaken();

											   GoldTakenCounter = (GoldTakenCounter + 1).Min(1);

											   Later +=
												   delegate
												   {
													   EgoView.Sprites.Remove(Item_Sprite);
													   GoldSprites.Remove(Item_Sprite);
												   };
										   }
									   }
						   }

						   if (Later != null)
							   Later();

						   LastPosition = EgoView.ViewPosition;

						   if (ItemTaken != null)
							   ItemTaken();
					   };

				   AddAmmoPickups();
			   }
			);
			#endregion
		}

	}
}