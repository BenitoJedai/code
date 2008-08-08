using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlashSpaceInvaders.Shared;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.Nonoba.api;

using FlashSpaceInvaders.ActionScript.Extensions;

namespace FlashSpaceInvaders.ActionScript.MultiPlayer
{
	partial class Client
	{
		IGameRoutedActions Map;

		public override void InitializeMapOnce()
		{
			Map = new Game().AttachTo(Element);


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
			// hook on map events
		}
	}
}
