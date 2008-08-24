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
		public Action SwitchToWeapon;

		public bool WeaponIsActive;

		public Action FireWeapon;

		public int WeaponAmmo;

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


			var WeaponChangeSpeed = handsize * 5;

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

							if (hand_y >= DefaultControlHeight)
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


			Action<Action> PlayFireAnimation =
				done =>
				{

					(1000 / 15).AtInterval(
						tt =>
						{
							gun[gun_index].Orphanize();

							gun_index++;

							if (gun_index == gun.Length)
							{
								// done

								gun_index = 0;
								tt.stop();

								done();
							}

							gun[gun_index].AttachTo(hand);

						}
					);
				};

			FireWeapon =
				delegate
				{
					if (!WeaponIsActive)
						return;

					// we can fire only if the animation has stopped
					if (gun_index != 0)
						return;

					Assets.Default.gunshot.play();

					WeaponAmmo--;

					// add damage to sprites

					// we need to find out the one we are shooting at!

					// try adding damage to all

					//WriteLine("fire:");


		

					var query =
						from p in EgoView.GetPointOfViewSprites(15.DegreesToRadians())
						where p.ViewInfo.IsInView
						let fragile = p.Sprite as SpriteInfoExtended
						where fragile != null
						where fragile.TakeDamage != null
						orderby p.Distance
						select fragile;


					var first = query.FirstOrDefault();

					if (first != null)
						first.TakeDamage();


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

								BringWeaponDown(
									delegate
									{
										gun.First().Orphanize();
										noweapon.AttachTo(hand);
									}
									,
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
