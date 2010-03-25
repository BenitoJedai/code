using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Ultra;
using java.lang;

namespace jsc.meta.Library.Mashups
{
	class UltraApplicationWithInlineTier
	{
		public UltraApplicationWithInlineTier()
		{
			var btn = new IHTMLButton
			{
				"Hello"
			};


			btn.AttachToDocument();

			btn.onclick +=
				delegate
				{
					var Screen_width = Native.Screen.width;

					#region javascript to flash to javascript
					Tier.Flash();

					var Flash_hasAudio = ScriptCoreLib.ActionScript.flash.system.Capabilities.hasAudio;

					Tier.JavaScript();
					#endregion

					#region javascript to java applet to javascript
					Tier.Applet();

					var Java_totalMemory = "" + java.lang.Runtime.getRuntime().totalMemory();
					Tier.JavaScript();
					#endregion

					#region javascript to server to javascript
					Tier.Server();

					var Data = Debuggable(Screen_width, Flash_hasAudio, Java_totalMemory);

					Tier.JavaScript();
					#endregion

					Native.Window.alert("Message froms server:" + Data);
				};
		}

		private static string Debuggable(int Screen_width, bool Flash_hasAudio, string Java_totalMemory)
		{
			// this method can be debugged under debugger as it does not demand a rewrite.

			var Data = "Server thinks: " + new { Screen_width, Flash_hasAudio, Java_totalMemory };

			return Data;
		}
	}
}
