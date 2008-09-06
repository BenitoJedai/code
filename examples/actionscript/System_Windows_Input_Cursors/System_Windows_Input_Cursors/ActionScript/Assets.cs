using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

[assembly: ScriptResources(System_Windows_Input_Cursors.ActionScript.Assets.Path)]

namespace System_Windows_Input_Cursors.ActionScript
{

	[Script]
	public class Assets
	{
		public const string Path = "assets/System_Windows_Input_Cursors";

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
