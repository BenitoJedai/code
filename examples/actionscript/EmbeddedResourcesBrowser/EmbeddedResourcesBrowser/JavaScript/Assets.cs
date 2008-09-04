using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared;
using EmbeddedResourcesBrowser.Shared;

namespace EmbeddedResourcesBrowser.JavaScript
{
	[Script(Implements = typeof(Assets))]
	public class __Assets
	{
		public static readonly __Assets Default = new __Assets();

		public string[] FileNames
		{
			[EmbedGetFileNames]
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
