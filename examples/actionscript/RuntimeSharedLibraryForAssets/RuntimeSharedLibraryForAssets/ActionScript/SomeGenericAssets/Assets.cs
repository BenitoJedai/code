using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

namespace SomeGenericAssets.ActionScript
{
	[Script(IsNative = true)]
	public class Assets
	{
		public const string Path = "assets/SomeGenericAssets";


		public Class this[string e]
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
