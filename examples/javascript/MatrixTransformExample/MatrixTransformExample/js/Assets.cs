using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared;

[assembly: ScriptResources(MatrixTransformExample.js.Assets.Path)]

namespace MatrixTransformExample.js
{
	[Script]
	internal static class Assets
	{
		public const string Path = "assets/MatrixTransformExample";
	}
}
