using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

//[assembly: ScriptResources(TextSuggestions2.ActionScript.Assets.Path)]

namespace TextSuggestions2.ActionScript
{

	[Script]
	public class Assets
	{
		public const string Path = "assets/TextSuggestions2";

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
