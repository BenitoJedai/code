using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlashSpaceInvaders.Shared;
using ScriptCoreLib.ActionScript.Nonoba.api;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.Extensions;

namespace FlashSpaceInvaders.ActionScript.MultiPlayer
{
	partial class Client
	{
		SharedClass1.RemoteEvents.ServerPlayerHelloArguments MyIdentity;

		readonly Dictionary<int, SpriteWithMovement> Cursors = new Dictionary<int, SpriteWithMovement>();


		public override void InitializeEvents()
		{
			#region ServerPlayerHello
			Events.ServerPlayerHello +=
				e =>
				{
					MyIdentity = e;

					//ShowMessage("Howdy, " + e.name);


					// local only
					Map.SendTextMessage.Direct("Howdy, " + e.name);
				};
			#endregion

			#region MouseMove
			Events.UserMouseMove +=
					e =>
					{
						var s = default(SpriteWithMovement);

						if (Cursors.ContainsKey(e.user))
							s = Cursors[e.user];
						else
						{
							s = new SpriteWithMovement
							{
								filters = new[] { new DropShadowFilter() },
								alpha = 0.5
							};


							var g = s.graphics;

							g.beginFill((uint)e.color);
							g.moveTo(0, 0);
							g.lineTo(14, 14);
							g.lineTo(0, 20);
							g.lineTo(0, 0);
							g.endFill();

							Cursors[e.user] = s;
						};

						s.AttachTo(this.Element).TweenMoveTo(e.x, e.y);
					};


			Events.UserMouseOut +=
			   e =>
			   {
				   if (Cursors.ContainsKey(e.color))
				   {
					   Cursors[e.color].Orphanize();
				   }
			   };
			#endregion
		}
	}
}
