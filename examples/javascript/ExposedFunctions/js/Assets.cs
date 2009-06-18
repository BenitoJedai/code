using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared;

[assembly: ScriptResources(ExposedFunctions.js.Assets.Path)]

namespace ExposedFunctions.js
{
	[Script]
	internal static class Assets
	{
		public const string Path = "assets/ExposedFunctions";
	}
}
