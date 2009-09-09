using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared;

[assembly: ScriptResources(MatrixTransformCExample.js.Assets.Path)]

namespace MatrixTransformCExample.js
{
	[Script]
	internal static class Assets
	{
		public const string Path = "assets/MatrixTransformCExample";
	}
}
