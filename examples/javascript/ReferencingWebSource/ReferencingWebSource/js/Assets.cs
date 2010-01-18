using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared;

[assembly: ScriptResources(ReferencingWebSource.js.Assets.Path)]

namespace ReferencingWebSource.js
{
	[Script]
	internal static class Assets
	{
		public const string Path = "assets/ReferencingWebSource";
	}
}
