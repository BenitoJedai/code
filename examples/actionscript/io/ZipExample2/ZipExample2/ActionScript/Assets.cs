using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.Shared;

//[assembly: ScriptResources(ZipExample2.ActionScript.Assets.Path)]

namespace ZipExample2.ActionScript
{

	[Script]
	public class Assets
	{
		public const string Path = "assets/ZipExample2";

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
