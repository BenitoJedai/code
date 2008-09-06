using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

[assembly: ScriptResources(TextSuggestions.ActionScript.Assets.Path)]

namespace TextSuggestions.ActionScript
{

	[Script]
	public class Assets
	{
		public const string Path = "assets/TextSuggestions";

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
