using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

[assembly: ScriptResources(System_IO_StringReader.ActionScript.Assets.Path)]

namespace System_IO_StringReader.ActionScript
{

	[Script]
	public class Assets
	{
		public const string Path = "assets/System_IO_StringReader";

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
