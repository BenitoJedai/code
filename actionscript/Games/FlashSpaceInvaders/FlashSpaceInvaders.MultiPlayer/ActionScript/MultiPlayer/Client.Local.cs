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
		IGameRoutedActions Map;
		GameSharedState MapSharedState;

		public override void InitializeMapOnce()
		{
			var n = new Game().AttachTo(Element);

			this.Map = n;
			this.MapSharedState = n.SharedState;
			
			#region MouseMove
			var MyColor = (uint)0xffffff.Random();

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

			#endregion

			Map.DoPlayerMovement.Handler +=
				(ego, p) =>
				{
					// ego should be const

					// ego has moved
					Messages.VectorChanged((int)p.x, (int)p.y);
				};

			Map.FireBullet.Handler +=
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
