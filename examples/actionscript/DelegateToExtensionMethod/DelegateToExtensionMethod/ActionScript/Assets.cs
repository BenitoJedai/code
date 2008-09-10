using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

[assembly: ScriptResources(DelegateToExtensionMethod.ActionScript.Assets.Path)]

namespace DelegateToExtensionMethod.ActionScript
{

	[Script]
	public class Assets
	{
		public const string Path = "assets/DelegateToExtensionMethod";

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
