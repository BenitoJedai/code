using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Shared;

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
