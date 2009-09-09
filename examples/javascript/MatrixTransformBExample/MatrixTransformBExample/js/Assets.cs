using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared;

[assembly: ScriptResources(MatrixTransformBExample.js.Assets.Path)]

namespace MatrixTransformBExample.js
{
	[Script]
	internal static class Assets
	{
		public const string Path = "assets/MatrixTransformBExample";
	}
}
