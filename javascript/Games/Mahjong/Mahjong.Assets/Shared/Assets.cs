using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Shared;

[assembly: ScriptResources("assets/Mahjong")]
[assembly: ScriptResources("assets/Mahjong.Assets")]
[assembly: ScriptResources("assets/Mahjong.Layouts")]
[assembly: ScriptResources("assets/Mahjong.Sounds")]

namespace Mahjong.Shared
{
	public class Assets
	{

		public static readonly Assets Default = new Assets();

		public string[] FileNames
		{
			[EmbedGetFileNames]
			get
			{
				return ScriptCoreLib.CSharp.Extensions.EmbeddedResourcesExtensions.GetEmbeddedResources(null, this.GetType().Assembly);
			}
		}

	}
}
