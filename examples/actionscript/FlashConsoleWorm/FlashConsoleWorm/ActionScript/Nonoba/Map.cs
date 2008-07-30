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
		}
	}
}
