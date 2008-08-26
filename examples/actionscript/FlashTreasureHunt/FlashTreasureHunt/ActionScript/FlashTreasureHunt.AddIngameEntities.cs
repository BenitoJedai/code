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
		public int NonblockingTotal;
		public int AmmoTotal;


		// items for pickup
		public readonly List<SpriteInfoExtended> AmmoSprites = new List<SpriteInfoExtended>();
		public readonly List<SpriteInfoExtended> GoldSprites = new List<SpriteInfoExtended>();
		public readonly List<SpriteInfoExtended> NonblockSprites = new List<SpriteInfoExtended>();

		public event Action Sync_Suicide;
		public event Action<int> Sync_TakeGold;
		public event Action<int> Sync_TakeAmmo;

		public bool EnableItemPickup = true;

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


		Bitmap[] CachedGoldTextures;
		Bitmap[] CachedAmmoTextures;
		Bitmap[] CachedNonblockTextures;

		public void InsertGoldSprite(int i, double x, double y)
		{
			CreateDummy(CachedGoldTextures.AtModulus(i)).Do(
				k =>
				{
					k.Range = 0.5;
					k.ItemTaken +=
						delegate
						{
							Assets.Default.Sounds.treasure.play();

							AddTreasureCollected();

						
						};

					k.ConstructorIndexForSync = i;
					GoldSprites.Add(k);
				}
			).Position.To(x, y);
		}

		public void InsertAmmoSprite(int i, double x, double y)
		{
			CreateDummy(CachedAmmoTextures.AtModulus(i)).Do(
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
					k.ConstructorIndexForSync = i;

					AmmoSprites.Add(k);
				}
			).Position.To(x, y);
		}

		public void InsertNonblockSprite(int i, double x, double y)
		{
			CreateDummy(CachedNonblockTextures.AtModulus(i)).Do(
				k =>
				{
					k.Range = 0.5;
					k.ConstructorIndexForSync = i;
					NonblockSprites.Add(k);
				}
			).Position.To(x, y);
		}

		private void AddIngameEntities(Action done)
		{
			//var done_3 = new JoinAction(done, 3);


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

			//WriteLine(new { GoldTotal, AmmoTotal, NonblockingTotal }.ToString());



			#region 3. nonblock
			Action AddNonBlockingItems =
				() => Assets.Default.nonblock.ToBitmapArray(CachedNonblockTextures,
					sprites =>
					{
						CachedNonblockTextures = sprites;

						#region add nonblock
						for (int i = 0; i < NonblockingTotal; i++)
						{
							// compiler bug: get a delegate to BCL class
							//AddSpriteByTexture(FreeSpaceForStuff, s, GoldSprites.Add);

							AddSpriteByTexture(FreeSpaceForStuff, CachedNonblockTextures.AtModulus(i),
								k =>
								{
									k.Range = 0.5;
									k.ConstructorIndexForSync = i;

									NonblockSprites.Add(k);
								}
							);

						}
						#endregion

						done();
						//done_3.Signal();
					}
				);


			#endregion



			#region 2. AddAmmoPickups
			Action AddAmmoPickups =
				() => Assets.Default.ammo_sprites.ToBitmapArray(CachedAmmoTextures,
				   sprites =>
				   {
					   if (CachedAmmoTextures == null)
					   {
						   CachedAmmoTextures = sprites;

						   // this should only be done once
						   #region track ammo pickup
						   var LastPosition = new Point();

						   EgoView.ViewPositionChanged +=
							   delegate
							   {
								   if (!EnableItemPickup)
									   return;

								   if (EgoView.SpritesFromPointOfView == null)
									   return;

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

													   uint color = 0x0000ff;
													   FlashColors(color);



													   if (Item_Sprite != null)
													   {
														   if (Item_Sprite.ItemTaken != null)
															   ItemTaken += () => Item_Sprite.ItemTaken();

														   if (Sync_TakeAmmo != null)
															   Sync_TakeAmmo(Item_Sprite.ConstructorIndexForSync);
													   }

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

						   #endregion

					   }

					   #region add ammo
					   for (int i = 0; i < AmmoTotal; i++)
					   {
						   // compiler bug: get a delegate to BCL class
						   //AddSpriteByTexture(FreeSpaceForStuff, s, GoldSprites.Add);

						   AddSpriteByTexture(FreeSpaceForStuff, CachedAmmoTextures.AtModulus(i),
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
								   k.ConstructorIndexForSync = i;

								   AmmoSprites.Add(k);
							   }
						   );

					   }


					   #endregion

					   AddNonBlockingItems();

					  // done_3.Signal();

				   }
				);

			#endregion


			#region 1. gold
			Assets.Default.gold.ToBitmapArray(
				CachedGoldTextures,
			   sprites =>
			   {
				   if (CachedGoldTextures == null)
				   {
					   CachedGoldTextures = sprites;

					   // this should only be done once

					   #region Track gold pickup
					   var LastPosition = new Point();

					   EgoView.ViewPositionChanged +=
						   delegate
						   {
							   if (!EnableItemPickup)
								   return;


							   if (EgoView.SpritesFromPointOfView == null)
								   return;


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

												   FlashColors(0xffff00);

												   


												   if (Item_Sprite != null)
												   {
													   if (Item_Sprite.ItemTaken != null)
														   ItemTaken += () => Item_Sprite.ItemTaken();

													   if (Sync_TakeGold != null)
														   Sync_TakeGold(Item_Sprite.ConstructorIndexForSync);
												   }
												   


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
					   #endregion

				   }

				   #region add gold
				   for (int i = 0; i < GoldTotal; i++)
				   {

					   // compiler bug: get a delegate to BCL class
					   //AddSpriteByTexture(FreeSpaceForStuff, s, GoldSprites.Add);

					   var gold_version = CachedGoldTextures.AtModulus(i);

					   AddSpriteByTexture(FreeSpaceForStuff, gold_version,
						   k =>
						   {
							   k.Range = 0.5;
							   k.ItemTaken +=
								   delegate
								   {
									   Assets.Default.Sounds.treasure.play();

									   AddTreasureCollected();
								   };

							   k.ConstructorIndexForSync = i;
							   GoldSprites.Add(k);
						   }
					   );
				   }


				   #endregion

				   AddAmmoPickups();

				   //done_3.Signal();

			   }
			);
			#endregion
		}

	

	}
}