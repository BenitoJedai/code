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
using ScriptCoreLib.ActionScript.flash.utils;

namespace FlashTreasureHunt.ActionScript
{
	partial class FlashTreasureHunt
	{
		public Action SwitchToWeapon;

		public bool WeaponIsActive;

		public event Action Sync_FireWeapon;
		public Action FireWeapon;

		public int WeaponAmmo;

		public Action SwitchToHand;

		private void InitializeWeaponOverlay(Dictionary<string, Bitmap> f)
		{
			Func<int, Bitmap> id = _id => f[_id + ".png"];

			var hand = new Sprite();

			var noweapon = f["hand.png"].AttachTo(hand);

			var gun_index = 0;

			var gun = new[]
				{
					id(326),
					id(327),
					id(328),
					id(329),
					id(330),
				};

			this.SwitchToHand =
				delegate
				{
					gun.First().Orphanize();
					noweapon.AttachTo(hand);
				};

			//var hand = f["330.png"];
			const int handsize = 4;

			var hand_x = (DefaultControlWidth - 64 * handsize) / 2;
			var hand_y_default = DefaultControlHeight - 64 * handsize;

			var hand_y = hand_y_default;

			hand.x = hand_x;
			hand.y = hand_y;
			hand.scaleX = handsize;
			hand.scaleY = handsize;
			hand.AttachTo(HudContainer);

			#region make it float
			(1000 / 24).AtInterval(
				tt =>
				{
					hand.x = hand_x + Math.Cos(tt.currentCount * 0.2) * 6;
					hand.y = hand_y + Math.Abs(Math.Sin(tt.currentCount * 0.2)) * 4;
				}
			);
			#endregion


			var WeaponChangeSpeed = handsize * 4;

			Action<Action> BringWeaponUp =
				ChangeDone =>
				{


					(1000 / 24).AtInterval(
						tt =>
						{
							hand_y -= WeaponChangeSpeed;

							if (hand_y <= hand_y_default)
							{
								tt.stop();

								hand_y = hand_y_default;

								// ready to fire

								ChangeDone();
							}
						}
					);
				};

			Action<Action, Action> BringWeaponDown =
				(MomentOfChange, ChangeDone) =>
				{
					(1000 / 24).AtInterval(
						tt =>
						{
							hand_y += WeaponChangeSpeed;

							if (hand_y >= DefaultControlHeight - (32 * handsize))
							{
								// hand is off screen
								// lets switch to a weapon

								MomentOfChange();

								tt.stop();

								BringWeaponUp(ChangeDone);
							}
						}
					);
				};

			Action SwitchToWeaponDefault =
				delegate
				{
					// disable this function
					SwitchToWeapon = delegate { };


					BringWeaponDown(
						delegate
						{
							noweapon.Orphanize();
							gun.First().AttachTo(hand);
						}
						,
						delegate
						{
							WeaponIsActive = true;
						}
					);
				};


			SwitchToWeapon = SwitchToWeaponDefault;


			var PlayFireAnimationTimer = default(Timer);

			Action<Action> PlayFireAnimation =
				done =>
				{

					PlayFireAnimationTimer = FrameRate_FireWeapon.AtInterval(
						tt =>
						{
							gun[gun_index].Orphanize();

							gun_index++;

							if (gun_index == gun.Length)
							{
								// done

								gun_index = 0;
								tt.stop();

								PlayFireAnimationTimer = null;

								done();
							}

							gun[gun_index].AttachTo(hand);

						}
					);
				};

			FireWeapon =
				delegate
				{
					if (EndLevelMode)
						return;

					if (!WeaponIsActive)
						return;

					// we can fire only if the animation has stopped
					if (PlayFireAnimationTimer != null)
						return;

			

					if (WeaponAmmo <= 0)
					{
						WeaponIsActive = false;

						SwitchToWeapon = delegate { };

						BringWeaponDown(SwitchToHand,
								delegate
								{
									SwitchToWeapon = SwitchToWeaponDefault;
								}
							);

						return;
					}

					if (Sync_FireWeapon != null)
						Sync_FireWeapon();

					Assets.Default.Sounds.gunshot.play();

					WeaponAmmo--;

					// add damage to sprites

					// we need to find out the one we are shooting at!

					// try adding damage to all

					//WriteLine("fire:");

					var PossibleTargets =
						from p in EgoView.Sprites
							let fragile = p as SpriteInfoExtended
							where fragile != null
							where fragile.Health > 0
							where fragile.TakeDamage != null
							select p;


					var query =
						from k in EgoView.GetVisibleSprites(15.DegreesToRadians(), PossibleTargets)
						let VisiblePercentage = k.LastRenderedClip.width / k.LastRenderedZoom
						where VisiblePercentage > 0.5
						orderby k.Distance
						let fragile = k.Sprite as SpriteInfoExtended
						select new { k, fragile };

					WriteLine("" + new { PossibleTargets = PossibleTargets.Count(), Targets = query.Count() });

					//foreach (var q in query)
					//{
					//    WriteLine("" + new { q.k.LastRenderedClip.width, q.k.LastRenderedZoom, vis = q.k.LastRenderedClip.width / q.k.LastRenderedZoom } );
					//}
			

					var first = query.FirstOrDefault();

					if (first != null)
					{
						var DamageToBeTaken = 1 / first.k.Distance;

						//WriteLine("hit: " + DamageToBeTaken);

						first.fragile.TakeDamage(DamageToBeTaken);
					}


					PlayFireAnimation(
						delegate
						{
							if (WeaponAmmo <= 0)
							{
								// dude, we are out of ammo!!

								WeaponIsActive = false;

								Action ReadyForMoreAmmo =
									delegate
									{
										SwitchToWeapon = SwitchToWeaponDefault;
									};

								SwitchToWeapon =
									delegate
									{
										SwitchToWeapon = delegate { };

										// the animation has not yet stopped but we found the ammo
										// when we are ready we switch back for the gun
										// this can be tested if the hand movement is really slow
										ReadyForMoreAmmo = SwitchToWeaponDefault;
									};

								BringWeaponDown(SwitchToHand,
									delegate
									{
										ReadyForMoreAmmo();
									}
								);

							}
						}
					);
				};
		}

	}
}
