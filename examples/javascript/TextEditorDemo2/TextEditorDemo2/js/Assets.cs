using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared;

[assembly: ScriptResources(TextEditorDemo2.js.Assets.Path)]

namespace TextEditorDemo2.js
{
	[Script]
	internal static class Assets
	{
		public const string Path = "assets/TextEditorDemo2";
	}
}
