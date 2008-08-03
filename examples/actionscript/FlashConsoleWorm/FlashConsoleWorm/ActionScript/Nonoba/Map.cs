using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib;
using FlashConsoleWorm.Shared;
using FlashConsoleWorm.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.Nonoba.api;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlashConsoleWorm.ActionScript.Nonoba
{
	partial class Client
	{
		bool InitializeMapDone;

		FlashConsoleWorm Map;

		private void InitializeMap()
		{
			if (InitializeMapDone)
				return;

			InitializeMapDone = true;

			InitializeShowMessage();

			Map = new FlashConsoleWorm().AttachTo(this);

			Map.Ego.VectorChanged +=
				delegate
				{
					// let others know we changed vector

					Messages.TeleportTo((int)Map.Ego.Location.x, (int)Map.Ego.Location.y);
					Messages.VectorChanged((int)Map.Ego.Vector.x, (int)Map.Ego.Vector.y);
				};

			var MyColor = (uint)0xffffff.Random();

			stage.mouseMove +=
				e =>
				{
					Messages.MouseMove((int)e.stageX, (int)e.stageY, (int)MyColor);
				};

			stage.mouseOut +=
				delegate
				{
					Messages.MouseOut((int)MyColor);
				};

			Map.Ego.HasEatenAnApple +=
				e =>
				{
					Map.Ego.ApplesEaten++;

					Messages.EatApple((int)e.Location.x, (int)e.Location.y);

					if (Map.Apples.Count == 0)
					{
						// end of level
						Messages.LevelHasEnded();

						OnLevelHasEnded();

						EndOfLevelDelay.AtDelayDo(
							delegate
							{
								// make new apples
								Map.AddApples();

								// send them to others
								OnServerSendMap();
							}
						);

					}
				};

			Map.Ego.EatThisWormSoon +=
				e =>
				{

					// somebody else is going to eat it
					if (e.WormWhoIsGoingToEatMe != null)
					{

						if (e.WormWhoIsGoingToEatMe != Map.Ego)
							ShowMessage("somebody else is going to eat that worm");
						else
							ShowMessage("already am going to eat that worm");

						return;
					}

					if (!RemoteEgos.ContainsValue(e))
					{
						ShowMessage("not a valid remote ego");

						return;
					}

					var p = RemoteEgos.Where(w => w.Value == e).First();

					var food = p.Key;

					Messages.EatThisWormBegin(food);

					var worm = p.Value;

					worm.WormWhoIsGoingToEatMe = Map.Ego;

					AsyncDelay.AtDelayDo(
						delegate
						{
							// if we are still going to eat it finish eating it!

							if (worm.WormWhoIsGoingToEatMe != Map.Ego)
								return;

							Map.Ego.WormsEaten++;

							Messages.EatThisWormEnd(food);

							worm.IsAlive = false;

							// do the eating 
							while (worm.Parts.Count > 1)
							{
								worm.Shrink();
								Map.Ego.Grow();
							}

							worm.WormWhoIsGoingToEatMe = null;

							// how long are we dead? worm owner decides...

						}
					);
				};

			// only dudes which are already in room get this
			Messages.TeleportTo((int)Map.Ego.Location.x, (int)Map.Ego.Location.y);

		}

		// should depend on lag
		public const int AsyncDelay = 400;

		public const int DeathDelay = 2000;


		public const int EndOfLevelDelay = 4000;

	}
}
