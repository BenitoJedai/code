using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

//[assembly: ScriptResources(EmbeddedResourcesBrowser.Shared.Assets.Path)]

namespace EmbeddedResourcesBrowser.Shared
{
	public class Assets
	{
		public const string Path = "assets/EmbeddedResourcesBrowser";

		public readonly static Assets Default = new Assets();
 
		public string[] FileNames
		{
			get
			{
				return ScriptCoreLib.CSharp.Extensions.EmbeddedResourcesExtensions.GetEmbeddedResources(null, this.GetType().Assembly);
			}
		}
	}
}
