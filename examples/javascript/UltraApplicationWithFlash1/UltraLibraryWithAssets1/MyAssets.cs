using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript;

namespace UltraLibraryWithAssets1
{
	public class MyAssets : HTML.Pages.FromAssets.Assets
	{
		// Note: 
		// in Assets build configuration post build event
		// this assembly is being merged with the UltraSource

		public MyAssets()
		{
			this.jsc.onclick +=
				delegate
				{
					Native.Window.alert("hello");
				};
		}
	}
}
