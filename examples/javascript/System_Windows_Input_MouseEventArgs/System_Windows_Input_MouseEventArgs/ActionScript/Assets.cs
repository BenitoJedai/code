using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.Shared;

[assembly: ScriptResources(System_Windows_Input_MouseEventArgs.ActionScript.Assets.Path)]

namespace System_Windows_Input_MouseEventArgs.ActionScript
{

	[Script]
	public class Assets
	{
		public const string Path = "assets/System_Windows_Input_MouseEventArgs";

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
