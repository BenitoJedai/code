using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

namespace FlashResources.ActionScript
{
	[Script]
	public class Assets
	{
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
