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
		public int CurrentLevelScore = 0;

		public int GoldTotal;
		public int GoldTotalCollected = 0;

		public Action HalfOfTheTreasureCollected;

		private void AddTreasureCollected()
		{
			GoldTotalCollected++;

			//WriteLine("GoldTotalCollected: " + GoldTotalCollected);

			CurrentLevelScore += 10;

			if (GoldTotal / 2 < GoldTotalCollected)
				if (HalfOfTheTreasureCollected != null)
					HalfOfTheTreasureCollected();

			// add some logic here
		}

		private void WaitForCollectingHalfTheTreasureToRevealEndGoal()
		{
			HalfOfTheTreasureCollected =
				delegate
				{
					// ding ding ding - end of level revealed

					HalfOfTheTreasureCollected = null;

					Assets.Default.Sounds.revealed.play();

					EgoView.Image.FadeOut(
						delegate
						{

							CompassContainer.FadeIn(
								delegate
								{
									EgoView.Image.FadeIn();
								}
							);

						}
					);

					//this.WriteLine("game goal is now there");

					TheGoldStack.AddTo(EgoView.Sprites);

					// cuz we removed and then added, we have to manually update
					// pov info, otherwise we need to wait
					// until we collect another treasure
					// for the endlevel to show up

					EgoView.UpdatePOV(true);
				};
		}

	}
}