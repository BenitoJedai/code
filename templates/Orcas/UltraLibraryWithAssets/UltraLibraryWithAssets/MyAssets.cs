using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript;

namespace UltraLibraryWithAssets
{
	public class MyAssets : HTML.Pages.Assets
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
