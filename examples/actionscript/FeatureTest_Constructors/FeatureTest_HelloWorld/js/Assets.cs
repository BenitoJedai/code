using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared;

[assembly: ScriptResources(FeatureTest_HelloWorld.js.Assets.Path)]

namespace FeatureTest_HelloWorld.js
{
	[Script]
	internal static class Assets
	{
		public const string Path = "assets/FeatureTest_HelloWorld";
	}
}
