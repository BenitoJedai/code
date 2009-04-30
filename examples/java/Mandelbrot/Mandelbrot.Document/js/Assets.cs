using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared;

[assembly: ScriptResources(Mandelbrot.Document.js.Assets.Path)]

namespace Mandelbrot.Document.js
{
	[Script]
	internal static class Assets
	{
		public const string Path = "assets/Mandelbrot.Document";
	}
}
