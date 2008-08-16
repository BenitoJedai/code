using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

[assembly: ScriptResources(BrowserAvalonExample.js.Assets.Path)]

namespace BrowserAvalonExample.js
{
	[Script]
	internal static class Assets
	{
		public const string Path = "assets/BrowserAvalonExample";
	}
}
