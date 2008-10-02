using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

[assembly: ScriptResources(AvalonPipeMania.Assets.ActionScript.Assets.Path)]

namespace AvalonPipeMania.Assets.ActionScript
{

	[Script]
	public class Assets
	{
		public const string Path = "assets/AvalonPipeMania.Assets";

		public static readonly Assets Default = new Assets();

		public Class this[string e]
		{
			[EmbedByFileName]
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
