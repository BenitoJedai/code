using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared;

[assembly: ScriptResources(StringJoin.js.Assets.Path)]

namespace StringJoin.js
{
	[Script]
	internal static class Assets
	{
		public const string Path = "assets/StringJoin";
	}
}
