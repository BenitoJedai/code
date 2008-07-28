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

		private void InitializeMap()
		{
			if (InitializeMapDone)
				return;

			InitializeMapDone = true;

			InitializeShowMessage();

			var Map = new FlashConsoleWorm().AttachTo(this);
 
		}
	}
}
