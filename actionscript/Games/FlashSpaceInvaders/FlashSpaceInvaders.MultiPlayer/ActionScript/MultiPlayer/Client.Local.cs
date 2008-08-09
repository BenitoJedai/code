using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlashSpaceInvaders.Shared;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.Nonoba.api;

using FlashSpaceInvaders.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashSpaceInvaders.ActionScript.MultiPlayer
{
	partial class Client
	{
		public GameRoutedActions MapRoutedActions;
		public GameSharedState MapSharedState;
		public Game Map;

		public void InitializeMapOnce()
		{
			var n = new Game().AttachTo(Element);

			this.Map = n;
			this.MapRoutedActions = n.RoutedActions;
			this.MapSharedState = n.SharedState;

			#region MouseMove
			var MyColor = (uint)0xffffff.Random();

			Element.InvokeWhenStageIsReady(
				delegate
				{
					Element.stage.mouseMove +=
						e =>
						{
							var p = this.Element.globalToLocal(e.ToStagePoint());

							Messages.MouseMove((int)p.x, (int)p.y, (int)MyColor);
						};

					Element.stage.mouseOut +=
						delegate
						{
							Messages.MouseOut((int)MyColor);
						};
				}
			);

			#endregion

			MapRoutedActions.DoPlayerMovement.Handler +=
				(ego, p) =>
				{
					// ego should be const

					// ego has moved
					Messages.VectorChanged((int)p.x, (int)p.y);
				};

			MapRoutedActions.RestoreStarship.Handler +=
				s =>
				{
					var starship = this.MapSharedState[s];

					// must be local id

					if (starship >= GameSharedState.MaxObjectsPerSection)
						throw new Exception("RestoreStarship must be local starship");

					// MapRoutedActions.SendTextMessage.Direct("send RestoreStarship " + starship);

					Messages.RestoreStarship(starship);
				};

			MapRoutedActions.AddDamage.Handler +=
				(target, damage, shooter) =>
				{
					// how much damage are we making?

					// is it a fake?
					if (damage == 0)
						return;

					// who are we damaging?
					var target_id = MapSharedState[target];
					var shooter_id = MapSharedState[shooter];

					MapRoutedActions.SendTextMessage.Direct("sent damage: " + target_id + " " + damage + " by shooter " + shooter_id);
					Messages.AddDamage(target_id, damage, shooter_id);
				};

			MapRoutedActions.FireBullet.Handler +=
				(StarShip s, int Multiplier, Point From, Point To, double Limit, Action<BulletInfo> handler) =>
				{
					// serialize arguments
					// how to sync bullet owners?

					// serialize arguments to bytestream

					var starship = this.MapSharedState[s];

					Messages.FireBullet(
						starship, // synced id
						Multiplier,
						(int)(From.x),
						(int)(From.y),
						(int)(To.x),
						(int)(To.y),
						(int)(Limit)
					);
				};

			// hook on map events
		}
	}
}
