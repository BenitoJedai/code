using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.Shared;

[assembly: ScriptResources(System_Linq_Enumerable_OrderBy.ActionScript.Assets.Path)]

namespace System_Linq_Enumerable_OrderBy.ActionScript
{

	[Script]
	public class Assets
	{
		public const string Path = "assets/System_Linq_Enumerable_OrderBy";

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
