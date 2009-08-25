using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared;

[assembly: ScriptResources(WindowsFormsApplication1Document.js.Assets.Path)]

namespace WindowsFormsApplication1Document.js
{
	[Script]
	internal static class Assets
	{
		public const string Path = "assets/WindowsFormsApplication1Document";
	}
}
