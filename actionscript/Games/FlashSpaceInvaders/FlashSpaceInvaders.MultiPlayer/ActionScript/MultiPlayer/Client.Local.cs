using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlashSpaceInvaders.Shared;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.Nonoba.api;

namespace FlashSpaceInvaders.ActionScript.MultiPlayer
{
	partial class Client<T>
	{
		IGameRoutedActions Map;

		public override void InitializeMapOnce()
		{
			Map = new Game().AttachTo(Element);

		

			// hook on map events
		}
	}
}
