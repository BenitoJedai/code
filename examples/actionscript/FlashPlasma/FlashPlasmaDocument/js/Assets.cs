using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared;

[assembly: ScriptResources(FlashPlasmaDocument.js.Assets.Path)]

namespace FlashPlasmaDocument.js
{
	[Script]
	internal static class Assets
	{
		public const string Path = "assets/FlashPlasmaDocument";
	}
}
