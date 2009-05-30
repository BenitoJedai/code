using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared;

[assembly: ScriptResources(CakeCuttingProblemDocument.js.Assets.Path)]

namespace CakeCuttingProblemDocument.js
{
	[Script]
	internal static class Assets
	{
		public const string Path = "assets/CakeCuttingProblemDocument";
	}
}
